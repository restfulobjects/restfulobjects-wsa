// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System;
using Microsoft.Practices.StoreApps.Infrastructure.Interfaces;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Microsoft.Practices.StoreApps.Infrastructure
{
    /// <summary>
    /// The FlyoutService class is used for displaying flyouts view in your Windows 8 applications. This works for flyout views that implement the FlyoutView base class.
    /// The FlyoutService uses the FlyoutResolver Func to determine the flyout that will be shown when the ShowFlyout method is called.
    /// </summary>
    public class FlyoutService : IFlyoutService
    {
        private bool _isUnsnapping;
        private string _flyoutId;
        private object _parameter;
        private Action _successAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlyoutService"/> class.
        /// </summary>
        public FlyoutService()
        {
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        /// <summary>
        /// Gets or sets the flyout resolver that is used for retrieving the flyouts based on a string id.
        /// </summary>
        /// <value>
        /// The flyout resolver.
        /// </value>
        public Func<string, FlyoutView> FlyoutResolver { get; set; }

        /// <summary>
        /// Shows the flyout that has the passed flyout Id.
        /// </summary>
        /// <param name="flyoutId">The flyout id.</param>
        public void ShowFlyout(string flyoutId)
        {
            ShowFlyout(flyoutId, null, null);
        }

        /// <summary>
        /// Shows the flyout that has the passed flyout Id and also allows to pass a parameter and a success callback.
        /// </summary>
        /// <param name="flyoutId">The flyout id.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="successAction">The success action.</param>
        public void ShowFlyout(string flyoutId, object parameter, Action successAction)
        {
            if (FlyoutResolver == null) return;

            if (ApplicationView.Value == ApplicationViewState.Snapped)
            {
                _isUnsnapping = true;
                
                // Save ShowFlyout parameters so that they can be called in Current_SizeChanged handler
                _flyoutId = flyoutId;
                _parameter = parameter;
                _successAction = successAction;
                ApplicationView.TryUnsnap();
            }
            else
            {
                var flyout = FlyoutResolver(flyoutId);

                if (flyout != null)
                {
                    flyout.Open(parameter, successAction);
                }
            }
        }

        /// <summary>
        /// Handles the SizeChanged event of the current page, to show the flyout once the page is unsnapped.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Core.WindowSizeChangedEventArgs"/> instance containing the event data.</param>
        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (_isUnsnapping)
            {
                ShowFlyout(_flyoutId, _parameter, _successAction);
                _isUnsnapping = false;
            }
        }
    }
}
