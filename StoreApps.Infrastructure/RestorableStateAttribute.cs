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
    /// This attribute indicates that the marked property will have its state saved on suspension.
    /// </summary>
    [AttributeUsage(System.AttributeTargets.Property,
                    AllowMultiple = false)]
    public sealed class RestorableStateAttribute : Attribute
    {

    }
}
