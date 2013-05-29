// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System;

namespace Microsoft.Practices.StoreApps.Infrastructure
{
    /// <summary>
    /// The SettingsCharmActionItem is used by the SettingsCharmService class to populate the SettingsPane.
    /// This item type has an associated Action that will be executed when selecting it in the Settings Pane.
    /// </summary>
    public class SettingsCharmActionItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsCharmActionItem"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="action">The action to be performed when the settings charm is selected.</param>
        public SettingsCharmActionItem(string title, Action action) 
        {
            Title = title;
            Id = Guid.NewGuid().ToString();
            Action = action;
        }

        /// <summary>
        /// Gets the id of the settings charm flyout item.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the title of the settings charm flyout item.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the Action that will be executed when this item is clicked in the Settings Charm.
        /// </summary>
        /// <value>
        /// The Action delegate to be executed.
        /// </value>
        public Action Action { get; private set; }
    }
}
