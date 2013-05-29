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
    /// The IFlyoutViewModel interface should be implemented by the Flyout view model classes, to provide actions used for opening the Flyout, closing it,
    /// and handling the back button clicks. 
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
        /// Gets or sets the action used to close the Flyout.
        /// </summary>
        /// <value>
        /// The action that will be executed to close the Flyout.
        /// </value>
        Action CloseFlyout { get; set; }

        /// <summary>
        /// Opens the Flyout using the specified parameter, and executes the success Action after the Flyout is opened.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="successAction">The success action.</param>
        void Open(object parameter, Action successAction);
    }
}
