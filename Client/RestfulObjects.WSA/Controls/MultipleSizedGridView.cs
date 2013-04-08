// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using RestfulObjects.WSA.ViewModels;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RestfulObjects.WSA.Controls
{
    public class MultipleSizedGridView : GridView
    {
        private int _rowVal;
        private int _colVal;

        // <snippet608>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            var dataItem = item as ObjectActionViewRepr;

//            if (dataItem != null && dataItem.ItemPosition == 0)
//            {

                _colVal = (int)LayoutSizes.PrimaryItem.Width;
                _rowVal = (int)LayoutSizes.PrimaryItem.Height;

//            }
//            else
//            {
//                _colVal = (int)LayoutSizes.SecondaryItem.Width;
//                _rowVal = (int)LayoutSizes.SecondaryItem.Height;
//
//            }

            var uiElement = element as UIElement;
            VariableSizedWrapGrid.SetRowSpan(uiElement, _rowVal);
            VariableSizedWrapGrid.SetColumnSpan(uiElement, _colVal);
        }
        // </snippet608>
    }

    // <snippet609>
    public static class LayoutSizes
    {
        public static Size PrimaryItem
        {
            //get { return new Size(2, 2); }
            get { return new Size(1, 1); }
        } 
        public static Size SecondaryItem
        {
            get{return new Size(1, 1); }
        }
    }
    // </snippet609>
}
