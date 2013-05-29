// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using Microsoft.Practices.StoreApps.Infrastructure.Interfaces;
using Windows.ApplicationModel.Search;

namespace Microsoft.Practices.StoreApps.Infrastructure
{
    /// <summary>
    /// The SearchPaneService class abstracts Windows.ApplicationModel.Search.SearchPane object for use by apps that derive from the MvvmAppBase class.
    /// The SearchPane class represents and manages the search pane that opens when a user activates the Search charm.
    /// </summary>
    public class SearchPaneService : ISearchPaneService
    {
        /// <summary>
        /// Shows the Search Pane.
        /// </summary>
        public void Show()
        {
            SearchPane.GetForCurrentView().Show();
        }

        /// <summary>
        /// Enables or disables the ability of showing the Search Pane when a keyboard input is detected.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> it will show the Search Pane when a keyboard input is detected.</param>
        public void ShowOnKeyboardInput(bool enable)
        {
            SearchPane.GetForCurrentView().ShowOnKeyboardInput = enable;
        }

        /// <summary>
        /// Determines if the the Show On Keyboard Input feature is enabled or not.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the Show On Keyboard Input feature is enabled; otherwise, <c>false</c>.
        /// </returns>
        public bool IsShowOnKeyboardInputEnabled()
        {
            if (!AppManifestHelper.IsSearchDeclared())
            {
                return false;
            }

            return SearchPane.GetForCurrentView().ShowOnKeyboardInput;
        }
    }
}
