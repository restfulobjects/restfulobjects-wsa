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
    /// The IFlyoutViewModel interface should be implemented by the Flyout viewmodel classes, to provide actions used for opening the flyout, closing it,
    /// and handling the back button cliks. 
    /// </summary>
    public interface IFlyoutViewModel
    {
        /// <summary>
        /// Gets or sets the action used to go back to the previous view.
        /// </summary>
        /// <value>
        /// The go back action.
        /// </value>
        Action GoBack { get; set; }

        /// <summary>
        /// Gets or sets the action used to close the flyout.
        /// </summary>
        /// <value>
        /// The action that will be executed to close the flyout.
        /// </value>
        Action CloseFlyout { get; set; }

        /// <summary>
        /// Opens the flyout using the specified parameter, and executing the success Action after the flyout is opened.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="successAction">The success action.</param>
        void Open(object parameter, Action successAction);
    }
}
