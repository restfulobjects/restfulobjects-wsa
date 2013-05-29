// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System;

namespace Microsoft.Practices.StoreApps.Infrastructure.Interfaces
{
    /// <summary>
    /// The IFlyoutService interface should be used for implmenting the service used for displaying flyouts view in your Windows 8 applications.
    /// This works for flyout views that implement the FlyoutView base class.
    /// The default implementation of IFlyoutService is the FlyoutService class, which uses the FlyoutResolver to determine the flyout that will be shown
    /// when the ShowFlyout method is called.
    /// </summary>
    public interface IFlyoutService
    {
        /// <summary>
        /// Gets or sets the flyout resolver that is used for retrieving the flyouts based on a string id.
        /// </summary>
        /// <value>
        /// The flyout resolver.
        /// </value>
        Func<string, FlyoutView> FlyoutResolver { get; set; }
        
        /// <summary>
        /// Shows the flyout that has the passed flyout Id.
        /// </summary>
        /// <param name="flyoutId">The flyout id.</param>
        void ShowFlyout(string flyoutId);

        /// <summary>
        /// Shows the flyout that has the passed flyout Id and also allows to pass a parameter and a success callback.
        /// </summary>
        /// <param name="flyoutId">The flyout id.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="successAction">The success action.</param>
        void ShowFlyout(string flyoutId, object parameter, Action successAction);
    }
}