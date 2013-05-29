// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Search;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Microsoft.Practices.Prism.StoreApps
{
    /// <summary>
    /// Provides MvvmAppBase-specific behavior to supplement the default Application class.
    /// </summary>
    // Documentation on using the MVVM pattern is at http://go.microsoft.com/fwlink/?LinkID=288814&clcid=0x409
    public abstract class MvvmAppBase : Application
    {
        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        protected MvvmAppBase()
        {
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Gets or sets the session state service.
        /// </summary>
        /// <value>
        /// The session state service.
        /// </value>
        protected ISessionStateService SessionStateService { get; set; }

        /// <summary>
        /// Gets or sets the navigation service.
        /// </summary>
        /// <value>
        /// The navigation service.
        /// </value>
        protected INavigationService NavigationService { get; set; }

        /// <summary>
        /// Gets or sets the Flyout service.
        /// </summary>
        /// <value>
        /// The Flyout service.
        /// </value>
        protected IFlyoutService FlyoutService { get; set; }

        /// <summary>
        /// Gets a value indicating whether the application is suspending.
        /// </summary>
        /// <value>
        /// <c>true</c> if the application is suspending; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuspending { get; private set; }

        /// <summary>
        /// Override this method with logic that will be performed after the application is initialized. For example, navigating to the application's home page.
        /// </summary>
        /// <param name="args">The <see cref="LaunchActivatedEventArgs"/> instance containing the event data.</param>
        protected abstract void OnLaunchApplication(LaunchActivatedEventArgs args);
        
        /// <summary>
        /// Called when any of the search entry points are triggered. You do not need to override this method if your application does not implement the Search contract.
        /// </summary>
        /// <param name="args">The search query arguments.</param>
        protected virtual void OnSearchApplication(SearchQueryArguments args) { }

        /// <summary>
        /// Gets the type of the page based on a page token.
        /// </summary>
        /// <param name="pageToken">The page token.</param>
        /// <returns>The type of the page which corresponds to the specified token.</returns>
        protected virtual Type GetPageType(string pageToken)
        {
            var assemblyQualifiedAppType = this.GetType().GetTypeInfo().AssemblyQualifiedName;

            var pageNameWithParameter = assemblyQualifiedAppType.Replace(this.GetType().FullName, this.GetType().Namespace + ".Views.{0}Page");

            var viewFullName = string.Format(CultureInfo.InvariantCulture, pageNameWithParameter, pageToken);
            var viewType = Type.GetType(viewFullName);

            if (viewType == null)
            {
                var resourceLoader = new ResourceLoader(Constants.StoreAppsInfrastructureResourceMapId);
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, resourceLoader.GetString("DefaultPageTypeLookupErrorMessage"), pageToken, this.GetType().Namespace + ".Views"),
                    "pageToken");
            }

            return viewType;
        }

        /// <summary>
        /// Used for setting up the list of known types for the SessionStateService, using the RegisterKnownType method.
        /// </summary>
        protected virtual void OnRegisterKnownTypesForSerialization() { }

        /// <summary>
        /// Override this method with the initialization logic of your application. Here you can initialize services, repositories, and so on.
        /// </summary>
        /// <param name="args">The <see cref="IActivatedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnInitialize(IActivatedEventArgs args) { }

        /// <summary>
        /// Creates the Flyout view.
        /// </summary>
        /// <param name="flyoutName">Name of the Flyout.</param>
        /// <returns>The specified Flyout view</returns>
        /// <exception cref="System.InvalidOperationException">Could not find associated Flyout in the Views folder.</exception>
        protected virtual FlyoutView CreateFlyoutView(string flyoutName)
        {
            var assemblyQualifiedAppType = this.GetType().GetTypeInfo().AssemblyQualifiedName;

            var flyoutNameWithParameter = assemblyQualifiedAppType.Replace(this.GetType().FullName, this.GetType().Namespace + ".Views.{0}Flyout");

            var flyoutFullName = string.Format(CultureInfo.InvariantCulture, flyoutNameWithParameter, flyoutName);
            var flyoutType = Type.GetType(flyoutFullName);
            if (flyoutType == null)
            {
                var resourceLoader = new ResourceLoader(Constants.StoreAppsInfrastructureResourceMapId);
                throw new InvalidOperationException(resourceLoader.GetString("CouldNotFindAssociatedFlyoutInTheViewsFolder"));
            }

            var flyoutInstance = Resolve(flyoutType);
            return flyoutInstance as FlyoutView;
        }

        /// <summary>
        /// Gets the Settings charm action items.
        /// </summary>
        /// <returns>The list of Setting charm action items that will populate the Settings pane.</returns>
        protected virtual IList<SettingsCharmActionItem> GetSettingsCharmActionItems()
        {
            return null;
        }

        /// <summary>
        /// Resolves the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A concrete instance of the specified type.</returns>
        protected virtual object Resolve(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user. Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            var rootFrame = await InitializeFrameAsync(args);

            // If the app is launched via the app's primary tile, the args.TileId property
            // will have the same value as the AppUserModelId, which is set in the Package.appxmanifest.
            // See http://go.microsoft.com/fwlink/?LinkID=288842
            string tileId = AppManifestHelper.GetApplicationId();

            if (rootFrame != null && (rootFrame.Content == null || (args != null && args.TileId != tileId)))
            {
                OnLaunchApplication(args);
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Raises the WindowCreated event.
        /// </summary>
        /// <param name="args">The <see cref="WindowCreatedEventArgs"/> instance containing the event data.</param>
        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            if (AppManifestHelper.IsSearchDeclared())
            {
                // Register the Windows.ApplicationModel.Search.SearchPane.GetForCurrentView().QuerySubmitted
                // event in OnWindowCreated to speed up searches once the application is already running
                SearchPane.GetForCurrentView().QuerySubmitted += OnQuerySubmitted;
            }
        }

        /// <summary>
        /// Invoked when the application is activated through a search association.
        /// </summary>
        /// <param name="args">Event data for the event.</param>
        // Documentation on using search is at http://go.microsoft.com/fwlink/?LinkID=288822&clcid=0x409
        protected async override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            // If the Window isn't already using Frame navigation, insert our own Frame
            var rootFrame = await InitializeFrameAsync(args);

            if (rootFrame != null)
            {
                var searchQueryArguments = new SearchQueryArguments(args);
                OnSearchApplication(searchQueryArguments);
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Initializes the Frame and its content.
        /// </summary>
        /// <param name="args">The <see cref="IActivatedEventArgs"/> instance containing the event data.</param>
        /// <returns>A task of a Frame that holds the app content.</returns>
        private async Task<Frame> InitializeFrameAsync(IActivatedEventArgs args)
        {
            var rootFrame = Window.Current.Content as Frame;
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                var frameFacade = new FrameFacadeAdapter(rootFrame);

                //Initialize MvvmAppBase common services
                SessionStateService = new SessionStateService();
                
                //Configure VisualStateAwarePage with the ability to get the session state for its frame
                VisualStateAwarePage.GetSessionStateForFrame =
                    frame => SessionStateService.GetSessionStateForFrame(frameFacade);

                //Associate the frame with a key
                SessionStateService.RegisterFrame(frameFacade, "AppFrame");

                NavigationService = CreateNavigationService(frameFacade, SessionStateService);
                FlyoutService = new FlyoutService();
                FlyoutService.FlyoutResolver = CreateFlyoutView;
                SettingsPane.GetForCurrentView().CommandsRequested += OnCommandsRequested;

                // Set a factory for the ViewModelLocator to use the default resolution mechanism to construct view models
                ViewModelLocator.SetDefaultViewModelFactory(Resolve);

                OnRegisterKnownTypesForSerialization();
                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    await SessionStateService.RestoreSessionStateAsync();
                }

                OnInitialize(args);
                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state and navigate to the last page visited
                    try
                    {
                        SessionStateService.RestoreFrameState();
                        NavigationService.RestoreSavedNavigation();
                    }
                    catch (SessionStateServiceException)
                    {
                        // Something went wrong restoring state.
                        // Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            return rootFrame;
        }

        /// <summary>
        /// Creates the navigation service.
        /// </summary>
        /// <param name="rootFrame">The root frame.</param>
        /// <param name="sessionStateService">The session state service.</param>
        /// <returns>The initialized navigation service.</returns>
        private INavigationService CreateNavigationService(IFrameFacade rootFrame, ISessionStateService sessionStateService)
        {
            var navigationService = new FrameNavigationService(rootFrame, GetPageType, sessionStateService);
            return navigationService;
        }

        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            IsSuspending = true;
            try
            {
                var deferral = e.SuspendingOperation.GetDeferral();

                //Bootstrap inform navigation service that app is suspending.
                NavigationService.Suspending();

                // Save application state
                await SessionStateService.SaveAsync();

                deferral.Complete();
            }
            finally
            {
                IsSuspending = false;
            }
        }

        /// <summary>
        /// Called when a search is performed when the application is running.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SearchPaneQuerySubmittedEventArgs"/> instance containing the event data.</param>
        private void OnQuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        {
            var searchQueryArguments = new SearchQueryArguments(args);
            OnSearchApplication(searchQueryArguments);
        }

        /// <summary>
        /// Called when the Settings charm is invoked, this handler populates the Settings charm with the charm items returned by the GetSettingsCharmActionItems function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SettingsPaneCommandsRequestedEventArgs"/> instance containing the event data.</param>
        private void OnCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            if (args == null || args.Request == null || args.Request.ApplicationCommands == null)
            {
                return;
            }

            var applicationCommands = args.Request.ApplicationCommands;
            var settingsCharmActionItems = GetSettingsCharmActionItems();

            foreach (var item in settingsCharmActionItems)
            {
                var settingsCommand = new SettingsCommand(item.Id, item.Title, (o) => item.Action.Invoke());
                applicationCommands.Add(settingsCommand);
            }
        }
    }
}
