// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using RestfulObjects.Applib;
using RestfulObjects.WSA.ViewModels;

//using AdventureWorks.UILogic.Models;

namespace RestfulObjects.WSA.ViewReprs
{
    // originally adapted from AW's CategoryViewModel, but pretty much all gone now...
    public class DomainServiceViewRepr : ObjectReprROClientAware
    {
        public IReadOnlyCollection<ObjectActionViewRepr> Actions
        {
            get { return Members.Values.Select(x => x.CastTo<ObjectActionViewRepr>()).ToList(); }
        }
    }
}
