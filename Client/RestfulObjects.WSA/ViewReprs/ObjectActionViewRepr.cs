// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


//using AdventureWorks.UILogic.Models;
//using AdventureWorks.UILogic.Repositories;

//using AdventureWorks.UILogic.Services;

using System.Linq;
using RestfulObjects.Applib;

namespace RestfulObjects.WSA.ViewModels
{

    // originally adapted from AW's ProductViewModel, though pretty much all evidence of this is now gone...
    public class ObjectActionViewRepr : ObjectMemberRepr, ROClientAware
    {

        public string Title
        {
            get
            {
                var friendlyName = Extensions["friendlyName"].CastTo<ScalarRepr>().AsString();
                return friendlyName + (HasParams? "..." : string.Empty);
            }
        }

        public bool HasParams
        {
            get { return Extensions["hasParams"].CastTo<ScalarRepr>().AsBoolean().GetValueOrDefault(); }
        }

        public string DetailsHref
        {
            get { return Links.First(l => l.Rel.StartsWith("urn:org.restfulobjects:rels/details")).Href; }
        }
        public string DescribedByHref
        {
            get { return Links.First(l => l.Rel.StartsWith("describedby")).Href; }
        }

        public ROClient ROClient { get; set; }


    }

    class ObjectActionViewReprImpl : ObjectActionViewRepr
    {
    }
}
