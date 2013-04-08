// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace RestfulObjects.WSA.Converters
{
    /// <summary>
    /// Value converter that translates a boolean value to an invalid sign-in message.
    /// </summary>
    public sealed class IsSignInInvalidConverter : IValueConverter
    {
        //private readonly ResourceLoader _resourceLoader = new ResourceLoader();

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is bool && (bool)value) ? "Invalid user/password" /*resourceLoader.GetString("ErrorInvalidSignInErrorMessage")*/ : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
