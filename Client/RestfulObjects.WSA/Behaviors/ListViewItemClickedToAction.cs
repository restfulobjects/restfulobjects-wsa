// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RestfulObjects.WSA.Behaviors
{
    public static class ListViewItemClickedToAction
    {
        public static DependencyProperty ActionProperty =
            DependencyProperty.RegisterAttached("Action", typeof(Action<object>), typeof(ListViewItemClickedToAction), new PropertyMetadata(null, OnActionChanged));

        public static void SetAction(DependencyObject sender, Action<object> action)
        {
            if (sender == null)
            {
                return;
            }

            sender.SetValue(ActionProperty, action);
        }

        public static Action<object> GetAction(DependencyObject sender)
        {
            if (sender == null)
            {
                return null;
            }

            return (Action<object>)sender.GetValue(ActionProperty);
        }

        private static void OnActionChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ListViewBase listView = (ListViewBase)d;

            if (listView != null)
            {
                listView.ItemClick += listView_ItemClick;
                listView.Unloaded += listView_Unloaded;
            }
        }

        static void listView_Unloaded(object sender, RoutedEventArgs e)
        {
            ListViewBase listView = (ListViewBase)sender;
            listView.ItemClick -= listView_ItemClick;
            listView.Unloaded -= listView_Unloaded;
        }

        static void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var listView = (ListViewBase)sender;
            Action<object> action = (Action<object>)listView.GetValue(ActionProperty);
            action(e.ClickedItem);
        }
    }
}
