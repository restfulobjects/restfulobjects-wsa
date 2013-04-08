// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System.Net;
using Microsoft.Practices.StoreApps.Infrastructure;
using Microsoft.Practices.StoreApps.Infrastructure.Interfaces;
using Microsoft.Practices.Unity;
using RestfulObjects.Applib;
using RestfulObjects.Applib.WinRT;
using RestfulObjects.WSA.Services;
using Windows.ApplicationModel.Activation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace RestfulObjects.WSA
{
    // <snippet3300>
    sealed partial class App : MvvmAppBase
    // </snippet3300>
    {
        // New up the singleton container that will be used for type resolution in the app
        // <snippet3301>
        IUnityContainer _container = new UnityContainer();
        // </snippet3301>

        public ROClient RoClient { get; private set; }

        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Required override. Generally you do your initial navigation to launch page, or 
        /// to the page approriate based on a search, sharing, or secondary tile launch of the app
        /// </summary>
        /// <param name="args">The launch arguments passed to the application</param>
        protected override void OnLaunchApplication(LaunchActivatedEventArgs args)
        {

            // Use the logical name for the view to navigate to. The default convention
            // in the NavigationService will be to append "Page" to the name and look 
            // for that page in a .Views child namespace in the project. IF you want another convention
            // for mapping view names to view types, you can override 
            // the MvvmAppBase.GetPageNameToTypeResolver method
            NavigationService.Navigate("Hub", null);
        }

        /// <summary>
        /// This is the place you initialize your services and set default factory or default resolver for the view model locator
        /// </summary>
        /// <param name="args">The same launch arguments passed when the app starts.</param>
        // <snippet3302>
        protected override void OnInitialize(IActivatedEventArgs args)
        {
            RoClient = new ROClientOnWinRT("http://localhost:9292")
            {
                Credentials = new NetworkCredential("sven", "pass")
            };

            // Register MvvmAppBase services with the container so that view models can take dependencies on them
            _container.RegisterInstance<ISessionStateService>(SessionStateService);
            _container.RegisterInstance<INavigationService>(NavigationService);


            _container.RegisterType<IAlertMessageService, AlertMessageService>(new ContainerControlledLifetimeManager());


            // Register any app specific types with the container

            _container.RegisterInstance<ROClient>(this.RoClient);

            // Set a factory for the ViewModelLocator to use the container to construct view models so their 
            // dependencies get injected by the container
            ViewModelLocator.SetDefaultViewModelFactory((viewModelType) => _container.Resolve(viewModelType));
        }
        // </snippet3302>
    }
}
