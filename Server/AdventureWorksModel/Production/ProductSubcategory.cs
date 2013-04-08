// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 
using System;
using System.Collections.Generic;
using NakedObjects;

namespace AdventureWorksModel {
    [IconName("lookup.png")]
    [Bounded]
    [Immutable]
    public class ProductSubcategory : AWDomainObject {

        private ICollection<Product> _Product = new List<Product>();

        [Hidden]
        public virtual int ProductSubcategoryID { get; set; }

        [Title]
        public virtual string Name { get; set; }

        public virtual ICollection<Product> Product {
            get { return _Product; }
            set { _Product = value; }
        }

        public virtual ProductCategory ProductCategory { get; set; }

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