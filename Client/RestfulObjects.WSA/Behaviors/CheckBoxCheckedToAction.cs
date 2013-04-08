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
    public static class CheckBoxCheckedToAction
    {
        public static DependencyProperty ActionProperty =
          DependencyProperty.RegisterAttached("Action", typeof(Action<object>), typeof(CheckBoxCheckedToAction), new PropertyMetadata(null, OnActionChanged));

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
            CheckBox checkBox = (CheckBox)d;

            if (checkBox != null)
            {
                checkBox.Checked += checkBox_Checked;
                checkBox.Unloaded += checkBox_Unloaded;
            }
        }

        static void checkBox_Unloaded(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            checkBox.Checked -= checkBox_Checked;
            checkBox.Unloaded -= checkBox_Unloaded;
        }

        static void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            Action<object> action = (Action<object>)checkBox.GetValue(ActionProperty);
            action(e.OriginalSource);
        }
    }
}