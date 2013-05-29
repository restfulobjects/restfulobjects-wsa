// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using Windows.ApplicationModel.Search;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;

namespace Microsoft.Practices.Prism.StoreApps
{
    /// <summary>
    /// The FlyoutView base class can be used for views that are going to be displayed as Flyouts.
    /// It contains methods to display the view inside a <see cref="Popup"/> control, and also to close the popup, and handle the go back navigation.
    /// Additionally, this class disables the ShowOnKeyboardInput behavior of the SearchPane if it is enabled, and restores it after the Flyout is closed.
    /// </summary>
    public class FlyoutView : Page
    {
        #region Fields
        private Popup _popup;
        private bool _wasSearchOnKeyboardInputEnabled;
        #endregion

        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="FlyoutView"/> class.
        /// </summary>
        /// <param name="flyoutSize">The size of the Flyout.</param>
        public FlyoutView(int flyoutSize)
        {
            FlyoutSize = flyoutSize;
        }

        #endregion

        #region Properties
        /// <summary>
        /// The width of the Flyout.
        /// </summary>
        public int FlyoutSize
        {
            get { return (int)GetValue(FlyoutSizeProperty); }
            set { SetValue(FlyoutSizeProperty, value); }
        }
        #endregion

        #region Dependency Properties
        /// <summary>
        /// DependencyProperty for FlyoutSize.
        /// </summary>
        public static DependencyProperty FlyoutSizeProperty =
            DependencyProperty.Register("FlyoutSize", typeof(int), typeof(FlyoutView), new PropertyMetadata(0));
        #endregion

        #region Public Methods
        /// <summary>
        /// Called to present the Flyout view.
        /// </summary>
        /// <param name="parameter">Optional parameter for the caller to pass into the view.</param>
        /// <param name="successAction">Method to be invoked on successful completion of the user task in the Flyout.</param>
        public void Open(object parameter, Action successAction)
        {
            // Create a new Popup to display the Flyout
            _popup = new Popup();
            _popup.IsLightDismissEnabled = true;
            var frame = Window.Current.Content as Frame;
            if (frame != null)
            {
                var page = frame.Content as Page;
                if (page != null)
                {
                    var flowDirection = page.FlowDirection;
                    if (flowDirection == FlowDirection.LeftToRight)
                    {
                        _popup.SetValue(Canvas.LeftProperty, Window.Current.Bounds.Width - FlyoutSize);
                    }
                    else
                    {
                        _popup.SetValue(Canvas.LeftProperty, FlyoutSize);
                    }
                }
            }

            _popup.SetValue(Canvas.TopProperty, 0);

            // Handle the Closed & Activated events of the Popup
            _popup.Closed += OnPopupClosed;
            Window.Current.Activated += OnWindowActivated;

            // Update the Flyout dimensions
            Width = FlyoutSize;
            Height = Window.Current.Bounds.Height;

            // Add animations for the panel.
            _popup.ChildTransitions = new TransitionCollection();
            _popup.ChildTransitions.Add(new PaneThemeTransition()
            {
                Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right) ? EdgeTransitionLocation.Right : EdgeTransitionLocation.Left
            });

            // Place the Flyout inside the Popup and open it
            _popup.Child = this;
            _popup.IsOpen = true;

            // If the Flyout has a DataContext, call the viewModel.Open method and set the set the Close and GoBack actions for future use
            var viewModel = DataContext as IFlyoutViewModel;
            if (viewModel != null)
            {
                viewModel.CloseFlyout = Close;
                viewModel.GoBack = GoBack;
                viewModel.Open(parameter, successAction);
            }

            // First verify if Search is enabled in the manifest, to be able to call the SearchPane class.
            // If SearchOnKeyboardInput is enabled, disable it. Also, save the current state.
            if (!AppManifestHelper.IsSearchDeclared()) return;
            var searchPane = SearchPane.GetForCurrentView();
            _wasSearchOnKeyboardInputEnabled = searchPane.ShowOnKeyboardInput;
            if (_wasSearchOnKeyboardInputEnabled)
            {
                searchPane.ShowOnKeyboardInput = false;
            }
        }

        /// <summary>
        /// Closes the Flyout.
        /// </summary>
        public void Close()
        {
            _popup.IsOpen = false;
        }

        /// <summary>
        /// Handler for the GoBack button in the Flyout to go back to the settings Flyout if that is how the user arrived at this Flyout.
        /// </summary>
        public void GoBack()
        {
            SettingsPane.Show();
            Close();
        }
        #endregion

        #region Private Methods
        private void OnPopupClosed(object sender, object e)
        {
            _popup.Child = null;
            Window.Current.Activated -= OnWindowActivated;
            if (_wasSearchOnKeyboardInputEnabled)
            {
                SearchPane.GetForCurrentView().ShowOnKeyboardInput = true;
            }
        }

        private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                Close();
            }
        }
        #endregion
    }
}
