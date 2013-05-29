// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


namespace Microsoft.Practices.StoreApps.Infrastructure.Interfaces
{
    /// <summary>
    /// The ISearchPaneService interface abstracts Windows.ApplicationModel.Search.SearchPane object for use by apps that derive from the MvvmAppBase class.
    /// The SearchPane class represents and manages the search pane that opens when a user activates the Search charm.
    /// The default implementation of ISearchPaneService is the SearchPaneService class, which simply passes method  invocations to an underlying
    /// Windows.ApplicationModel.Search.SearchPane object.
    /// </summary>
    public interface ISearchPaneService
    {
        /// <summary>
        /// Shows the Search Pane.
        /// </summary>
        void Show();

        /// <summary>
        /// Enables or disables the ability of showing the Search Pane when a keyboard input is detected.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> it will show the Search Pane when a keyboard input is detected.</param>
        void ShowOnKeyboardInput(bool enable);

        /// <summary>
        /// Determines if the the Show On Keyboard Input feature is enabled or not.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the Show On Keyboard Input feature is enabled; otherwise, <c>false</c>.
        /// </returns>
        bool IsShowOnKeyboardInputEnabled();
    }
}