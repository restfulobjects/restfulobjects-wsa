// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using Microsoft.Practices.Prism.StoreApps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace RestfulObjects.WSA.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class HubPage : VisualStateAwarePage
    {
        public HubPage()
        {
            InitializeComponent();
        }

        private void virtualizingStackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            // from AW copy-n-paste (gridview)

        }

        private void itemsGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // from AW copy-n-paste (gridview)
            // though not sure where corresponding code actually is in AW...
        }
    }
}
