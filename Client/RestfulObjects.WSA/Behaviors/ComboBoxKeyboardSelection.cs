// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace RestfulObjects.WSA.Behaviors
{
    public static class ComboBoxKeyboardSelection
    {
        public static DependencyProperty EnabledProperty =
          DependencyProperty.RegisterAttached("Enabled", typeof(bool), typeof(ComboBoxKeyboardSelection), new PropertyMetadata(false, OnEnabledChanged));

        public static void SetEnabled(DependencyObject sender, bool enabled)
        {
            if (sender == null)
            {
                return;
            }

            sender.SetValue(EnabledProperty, enabled);
        }

        public static bool GetEnabled(DependencyObject sender)
        {
            if (sender == null)
            {
                return false;
            }

            return (bool)sender.GetValue(EnabledProperty);
        }

        private static void OnEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ComboBox comboBox = (ComboBox)d;

            if (comboBox != null)
            {
                comboBox.KeyUp += comboBox_KeyUp;
                comboBox.Unloaded += comboBox_Unloaded;
            }
        }

        static void comboBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            var comboBox = (ComboBox) sender;
            foreach (var item in comboBox.Items)
            {
                var comboBoxItemValue = item as ComboBoxItemValue;
                if (comboBoxItemValue != null && comboBoxItemValue.Value.StartsWith(e.Key.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    comboBox.SelectedItem = comboBoxItemValue;
                    return;
                }
            }
        }

        static void comboBox_Unloaded(object sender, RoutedEventArgs e)
        {
            var comboBox = (ComboBox) sender;
            comboBox.KeyUp -= comboBox_KeyUp;
            comboBox.Unloaded -= comboBox_Unloaded;
        }
    }
}