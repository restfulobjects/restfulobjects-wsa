// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 
using System;
using System.Collections.Generic;
using NakedObjects;

namespace AdventureWorksModel {
    [Bounded]
    [Immutable]
    public class ProductCategory : AWDomainObject {
        private ICollection<ProductSubcategory> _ProductSubcategory = new List<ProductSubcategory>();

        [Hidden]
        public virtual int ProductCategoryID { get; set; }

        [Title]
        public virtual string Name { get; set; }

        public virtual ICollection<ProductSubcategory> ProductSubcategory {
            get { return _ProductSubcategory; }
            set { _ProductSubcategory = value; }
        }

        #region Row Guid and Modified Date

        #region rowguid

        [Hidden]
        public override Guid rowguid { get; set; }

        #endregion

        #region ModifiedDate

        [MemberOrder(99)]
        [Disabled]
        public override DateTime ModifiedDate { get; set; }

        #endregion

        #endregion
    }
}