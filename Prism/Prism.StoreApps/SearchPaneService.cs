// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using Microsoft.Practices.Prism.StoreApps.Interfaces;
using Windows.ApplicationModel.Search;

namespace Microsoft.Practices.Prism.StoreApps
{
    /// <summary>
    /// The SearchPaneService class abstracts Windows.ApplicationModel.Search.SearchPane object for use by apps that derive from the MvvmAppBase class.
    /// The SearchPane class represents and manages the Search pane that opens when a user activates the Search charm.
    /// </summary>
    
    // Documentation on using search can be found at http://go.microsoft.com/fwlink/?LinkID=288822&clcid=0x409 
    public class SearchPaneService : ISearchPaneService
    {
        /// <summary>
        /// Shows the Search pane.
        /// </summary>
        public void Show()
        {
            SearchPane.GetForCurrentView().Show();
        }

        /// <summary>
        /// Enables or disables the ability to show the Search pane when a keyboard input is detected.
        /// </summary>
        /// <param name="enable">If set to <c>true</c> it will show the Search pane when a keyboard input is detected.</param>
        public void ShowOnKeyboardInput(bool enable)
        {
            SearchPane.GetForCurrentView().ShowOnKeyboardInput = enable;
        }

        /// <summary>
        /// Determines if the Show On Keyboard Input feature is enabled or not.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the Show On Keyboard Input feature is enabled; otherwise, <c>false</c>.
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
