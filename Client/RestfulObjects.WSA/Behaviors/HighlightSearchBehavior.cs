// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace RestfulObjects.WSA.Behaviors
{
    public static class HighlightSearchBehavior
    {
        public static DependencyProperty SearchTermProperty =
            DependencyProperty.RegisterAttached("SearchTerm", typeof(string), typeof(HighlightSearchBehavior), new PropertyMetadata(null, OnSearchTermChanged));

        public static DependencyProperty SearchTextProperty =
            DependencyProperty.RegisterAttached("SearchText", typeof(string), typeof(HighlightSearchBehavior), new PropertyMetadata(null, OnSearchTermChanged));

        public static void SetSearchTerm(DependencyObject sender, string searchTerm)
        {
            if (sender == null)
            {
                return;
            }

            sender.SetValue(SearchTermProperty, searchTerm);
        }

        public static string GetSearchTerm(DependencyObject sender)
        {
            if (sender == null)
            {
                return null;
            }

            return (string)sender.GetValue(SearchTermProperty);
        }

        public static void SetSearchText(DependencyObject sender, string searchText)
        {
            if (sender == null)
            {
                return;
            }

            sender.SetValue(SearchTextProperty, searchText);
        }

        public static string GetSearchText(DependencyObject sender)
        {
            if (sender == null)
            {
                return null;
            }

            return (string)sender.GetValue(SearchTextProperty);
        }

        public static void SetHighlightBrush(DependencyObject sender, Brush highlightBrush)
        {
            if (sender == null)
            {
                return;
            }

            sender.SetValue(HighlightBrushProperty, highlightBrush);
        }

        public static Brush GetHighlightBrush(DependencyObject sender)
        {
            if (sender == null)
            {
                return null;
            }

            return (Brush)sender.GetValue(HighlightBrushProperty);
        }

        private static void OnSearchTermChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var textBlock = (TextBlock)d;
            if (textBlock == null) return;

            var searchTerm = GetSearchTerm(textBlock);
            var originalText = GetSearchText(textBlock);
            if (string.IsNullOrEmpty(originalText) || (searchTerm == null)) return;
            if (searchTerm.Length == 0)
            {
                textBlock.Text = originalText;
                return;
            }

            textBlock.Inlines.Clear();
            var currentIndex = 0;
            var searchTermLength = searchTerm.Length;
            int index = originalText.IndexOf(searchTerm, 0, StringComparison.CurrentCultureIgnoreCase);
            while (index > -1)
            {
                textBlock.Inlines.Add(new Run() { Text = originalText.Substring(currentIndex, index - currentIndex) });
                currentIndex = index + searchTermLength;
                textBlock.Inlines.Add(new Run() { Text = originalText.Substring(index, searchTermLength), Foreground = GetHighlightBrush(textBlock) });
                index = originalText.IndexOf(searchTerm, currentIndex, 0, StringComparison.CurrentCultureIgnoreCase);
            }

            textBlock.Inlines.Add(new Run() { Text = originalText.Substring(currentIndex) });
        }

        public static DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register(
                "HighlightBrush",
                typeof(Brush),
                typeof(HighlightSearchBehavior),
                new PropertyMetadata(new SolidColorBrush(Colors.Orange), OnHighlightBrushChanged));

        private static void OnHighlightBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var textblock = (TextBlock)d;
            foreach (var run in textblock.Inlines)
            {
                var runObject = run as Run;
                if (runObject != null)
                {
                    if (runObject.Foreground != null)
                    {
                        runObject.Foreground = (Brush)args.NewValue;
                    }
                }
            }
        }
    }
}