// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Search;

namespace Microsoft.Practices.Prism.StoreApps
{
    /// <summary>
    /// The SearchQueryArguments class unifies the SearchActivatedEventArgs and the SearchPaneQuerySubmittedEventArgs event args classes, as they do not have a common interface.
    /// This class is used to provide a single search method that will be wired to both search entry points (When the app is running, and when it is not).
    /// </summary>
    public class SearchQueryArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchQueryArguments"/> class using an instance that implements the ISearchActivatedEventArgs interface.
        /// </summary>
        /// <param name="args">The <see cref="ISearchActivatedEventArgs"/> instance containing the event data, that will be used for initializing the SearchQueryArgument instance.</param>
        public SearchQueryArguments(ISearchActivatedEventArgs args)
        {
            if (args != null)
            {
                Language = args.Language;
                QueryText = args.QueryText;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchQueryArguments"/> class.
        /// </summary>
        /// <param name="args">The <see cref="SearchPaneQuerySubmittedEventArgs"/> instance containing the event data, that will be used for initializing the SearchQueryArgument instance.</param>
        public SearchQueryArguments(SearchPaneQuerySubmittedEventArgs args)
        {
            if (args != null)
            {
                Language = args.Language;
                QueryText = args.QueryText;
            }
        }

        /// <summary>
        /// Gets or sets the language of the query text.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the query text.
        /// </summary>
        /// <value>
        /// The query text.
        /// </value>
        public string QueryText { get; set; }
    }
}
