// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using RestfulObjects.Applib;
using RestfulObjects.WSA.Services;
using RestfulObjects.WSA.ViewReprs;
using Windows.UI.Xaml.Navigation;

namespace RestfulObjects.WSA.ViewModels
{
    public class HubPageViewModel : ViewModel
    {
        private readonly IAlertMessageService _alertMessageService;
        private readonly INavigationService _navService;
        private readonly ROClient _roClient;
        private bool _loadingData;

        public HubPageViewModel(IAlertMessageService alertMessageService, INavigationService navService, ROClient roClient)
        {
            _alertMessageService = alertMessageService;
            _navService = navService;
            _roClient = roClient;

            //NavigateToUserInputCommand = new DelegateCommand(() => navService.Navigate("UserInput", null));
            GoBackCommand = new DelegateCommand(navService.GoBack, navService.CanGoBack);

            ProductNavigationAction = NavigateToServiceAction;

            // Not sure why this isn't required in AW equivalent...
            OnNavigatedTo(null, NavigationMode.New, null);
        }


        public bool LoadingData
        {
            get { return _loadingData; }
            private set { SetProperty(ref _loadingData, value); }
        }


        public ICommand GoBackCommand { get; private set; }

        //public DelegateCommand NavigateToUserInputCommand { get; set; }

        // from AW copy-n-paste (gridview)
        public Action<object> ProductNavigationAction { get; private set; }

        public IReadOnlyCollection<DomainServiceViewRepr> RootCategories { get; set; }


        private void NavigateToServiceAction(object parameter)
        {
            var repr = parameter as ObjectActionViewRepr;
            if (repr != null)
            {
                _navService.Navigate("ObjectAction", repr);
            }
        }


        public async override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            // added by DanH... was missing in AW (why?)
            if (navigationMode == NavigationMode.New)
            {
                base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
            }

            LoadingData = true;
            try
            {
                var linkReprs = _roClient.Services().Value;
                //RootCategories = linkReprs.Select(x => new DomainServiceViewRepr(x.Title, x.Href, _roClient)).ToList();
                RootCategories = linkReprs.Select(x => _roClient.Get<DomainServiceViewRepr>(x.Href) ).ToList();
            }
            finally
            {
                LoadingData = false;
            }

            // demo
            //await _alertMessageService.ShowAsync("Error message detail", "Error");

//            string errorMessage = string.Empty;
//            ReadOnlyCollection<Category> rootCategories = null;
//            try
//            {
//                LoadingData = true;
//                // <snippet511>
//                // <snippet1100>
//                rootCategories = await _productCatalogRepository.GetRootCategoriesAsync(5);
//                // </snippet1100>
//                // </snippet511>
//            }
//            catch (HttpRequestException ex)
//            {
//                errorMessage = string.Format(CultureInfo.CurrentCulture,
//                                             _resourceLoader.GetString("GeneralServiceErrorMessage"),
//                                             Environment.NewLine, ex.Message);
//            }
//            finally
//            {
//                LoadingData = false;
//            }
//
//            if (!string.IsNullOrWhiteSpace(errorMessage))
//            {
//                await _alertMessageService.ShowAsync(errorMessage, _resourceLoader.GetString("ErrorServiceUnreachable"));
//                return;
//            }
//
//            var rootCategoryViewModels = new List<CategoryViewModel>();
//            foreach (var rootCategory in rootCategories)
//            {
//                rootCategoryViewModels.Add(new CategoryViewModel(rootCategory, _navigationService));
//            }
//            RootCategories = new ReadOnlyCollection<CategoryViewModel>(rootCategoryViewModels);
//            // <snippet1005>
//            _searchPaneService.ShowOnKeyboardInput(true);
//            // </snippet1005>
        }

        // <snippet1006>
        // <snippet709>
        public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);
//            if (!suspending)
//            {
//                _searchPaneService.ShowOnKeyboardInput(false);
//            }
        }
        // </snippet709>
        // </snippet1006>



    }
}
