// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System;

namespace Microsoft.Practices.Prism.StoreApps.Interfaces
{
    /// <summary>
    /// The IFlyoutService interface should be used for implmenting the service used for displaying Flyout views in your Windows Store app.
    /// This works for Flyout views that implement the FlyoutView base class.
    /// The default implementation of IFlyoutService is the FlyoutService class, which uses the FlyoutResolver to determine the Flyout that will be shown
    /// when the ShowFlyout method is called.
    /// </summary>
    public interface IFlyoutService
    {
        /// <summary>
        /// Gets or sets the Flyout resolver that is used for retrieving the Flyouts based on a string id.
        /// </summary>
        /// <value>
        /// The Flyout resolver.
        /// </value>
        Func<string, FlyoutView> FlyoutResolver { get; set; }
        
        /// <summary>
        /// Shows the Flyout that has the passed Flyout Id.
        /// </summary>
        /// <param name="flyoutId">The Flyout id.</param>
        void ShowFlyout(string flyoutId);

        /// <summary>
        /// Shows the Flyout that has the passed Flyout Id and also allows it to pass a parameter and a success callback.
        /// </summary>
        /// <param name="flyoutId">The Flyout id.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="successAction">The success action.</param>
        void ShowFlyout(string flyoutId, object parameter, Action successAction);
    }
}