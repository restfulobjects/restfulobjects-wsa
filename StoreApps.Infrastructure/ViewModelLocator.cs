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
using Windows.UI.Xaml;

namespace Microsoft.Practices.StoreApps.Infrastructure
{
    /// <summary>
    /// The ViewModelLocator class locates the viemodel for the view that has the AutoWireViewModelChanged attached property set to true.
    /// The viewmodel will be located and injected into the view's datacontext. To locate the view, two strategies are used: First the viewmodel locator
    /// will look if there is any viewmodel factory registered for that view, if not it will try to infer the viewmodel using a convention based approach.
    /// This class also provide methods for registering the viewmodel factories,
    /// and also to override the default viewmodel factory and the default view type to VM type resolver.
    /// </summary>
    public static class ViewModelLocator
    {
        /// <summary>
        /// A dictionary that contains all the registered factories for the views.
        /// </summary>
        static Dictionary<string, Func<object>> factories = new Dictionary<string, Func<object>>();
        /// <summary>
        /// The default view model factory.
        /// </summary>
        private static Func<Type, object> defaultViewModelFactory = type => Activator.CreateInstance(type);
        
        /// <summary>
        /// Default View Type to VM Type resolver, assumes VM is in same assembly and namespace as View Type.
        /// </summary>
        private static Func<Type, Type> defaultViewTypeToViewModelTypeResolver = 
            viewType =>
            {
                var viewName = viewType.FullName;
                viewName = viewName.Replace(".Views.", ".ViewModels.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);
                return Type.GetType(viewModelName);
            };

        /// <summary>
        /// Sets the default view model factory.
        /// </summary>
        /// <param name="viewModelFactory">The view model factory.</param>
        public static void SetDefaultViewModelFactory(Func<Type, object> viewModelFactory)
        {
            defaultViewModelFactory = viewModelFactory;
        }

        /// <summary>
        /// Sets the default view type to view model type resolver.
        /// </summary>
        /// <param name="viewTypeToViewModelTypeResolver">The view type to view model type resolver.</param>
        public static void SetDefaultViewTypeToViewModelTypeResolver(Func<Type, Type> viewTypeToViewModelTypeResolver)
        {
            defaultViewTypeToViewModelTypeResolver = viewTypeToViewModelTypeResolver;
        }

        #region Attached property with convention-or-mapping based approach

        /// <summary>
        /// The auto wire view model attached property
        /// </summary>
        public static DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), 
            new PropertyMetadata(false, AutoWireViewModelChanged));

        /// <summary>
        /// Gets the value of the AutoWireViewModel attached property.
        /// </summary>
        /// <param name="obj">The dependency object which has this attached property.</param>
        /// <returns><c>True</c> if ViewModel Autowiring is enabled; otherwise, false.</returns>
        public static bool GetAutoWireViewModel(DependencyObject obj)
        {
            if (obj != null)
            {
                return (bool) obj.GetValue(AutoWireViewModelProperty);
            }
            return false;
        }

        /// <summary>
        /// Sets the value of the AutoWireViewModel attached property.
        /// </summary>
        /// <param name="obj">The dependency object which has this attached property.</param>
        /// <param name="value">if set to <c>true</c> the view model wiring will be performed.</param>
        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        {
            if (obj != null)
            {
                obj.SetValue(AutoWireViewModelProperty, value);
            }
        }

        #endregion

        /// <summary>
        /// Automatically looks up the viewmodel that correspond to the current view, using two strategies:
        /// the first one looks if there is a any mapping registered for that view, if not it will fallback to the convention based approach.
        /// </summary>
        /// <param name="d">The dependency object, typically a view.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        // <snippet300>
        // <snippet3303>
        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement view = d as FrameworkElement;
            if (view == null) return; // Incorrect hookup, do no harm

            // Try mappings first
            object viewModel = GetViewModelForView(view);
            // Fallback to convention based
            if (viewModel == null)
            {
                var viewModelType = defaultViewTypeToViewModelTypeResolver(view.GetType());
                if (viewModelType == null) return;

                // Really need Container or Factories here to deal with injecting dependencies on construction
                viewModel = defaultViewModelFactory(viewModelType);
            }
            view.DataContext = viewModel;
        }
        // </snippet3303>
        // </snippet300>

        /// <summary>
        /// Gets the view model for the specified view.
        /// </summary>
        /// <param name="view">The view which viewmodel want.</param>
        /// <returns>The viewmodel that correspond to the view passed as a paramenter.</returns>
        private static object GetViewModelForView(FrameworkElement view)
        {
            // Mapping of view models base on view type (or instance) goes here
            if (factories.ContainsKey(view.GetType().ToString()))
                return factories[view.GetType().ToString()]();
            return null;
        }

        /// <summary>
        /// Registers the view model factory for the specified view type name.
        /// </summary>
        /// <param name="viewTypeName">The name of the view type.</param>
        /// <param name="factory">The viewmodel factory.</param>
        public static void Register(string viewTypeName, Func<object> factory)
        {
            factories[viewTypeName] = factory;
        }
    }
}
