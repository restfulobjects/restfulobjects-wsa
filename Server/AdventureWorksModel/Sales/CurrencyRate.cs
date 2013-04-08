// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 
using System;
using System.Collections.Generic;
using NakedObjects;

namespace AdventureWorksModel {
    [IconName("currency.png")]
    public class CurrencyRate : AWDomainObject {

        private ICollection<SalesOrderHeader> _SalesOrderHeader = new List<SalesOrderHeader>();

        [Hidden]
        public virtual int CurrencyRateID { get; set; }

        public virtual DateTime CurrencyRateDate { get; set; }

        [Title]
        public virtual decimal AverageRate { get; set; }

        public virtual decimal EndOfDayRate { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Currency Currency1 { get; set; }

        public virtual ICollection<SalesOrderHeader> SalesOrderHeader {
            get { return _SalesOrderHeader; }
            set { _SalesOrderHeader = value; }
        }

        #region ModifiedDate

        [MemberOrder(99)]
        [Disabled]
        public override DateTime ModifiedDate { get; set; }

        #endregion
    }
}