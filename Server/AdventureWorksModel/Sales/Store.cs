// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NakedObjects;

namespace AdventureWorksModel {
    [IconName("skyscraper.png")]
    public class Store : Customer {
        #region Title

        public override string ToString() {
            var t = new TitleBuilder();
            t.Append(Name).Append(",", this.AccountNumber);
            return t.ToString();
        }

        #endregion

        #region Properties


        [DisplayName("Store Name"),MemberOrder(20)]
        public virtual string Name { get; set; }

        #region Demographics

        [Hidden]
        public virtual string Demographics { get; set; }


        [DisplayName("Demographics"), MemberOrder(30), MultiLine(NumberOfLines = 10), TypicalLength(500)]
        public virtual string FormattedDemographics
        {
            get { return Utilities.FormatXML(Demographics); }
        }
        #endregion

        #region SalesPerson

        [Optionally]
        [MemberOrder(40)]
        public virtual SalesPerson SalesPerson { get; set; }

        #endregion

        #region Contacts

        private ICollection<StoreContact> _contact = new List<StoreContact>();

        [Disabled]
        public virtual ICollection<StoreContact> Contacts {
            get { return _contact; }
            set { _contact = value; }
        }

        #endregion

        #region ModifiedDate and rowguid

        #region ModifiedDate

        [MemberOrder(99)]
        [Disabled]
        [ConcurrencyCheck]
        public override DateTime ModifiedDate { get; set; }

        #endregion

        #region rowguid

        [Hidden]
        public override Guid rowguid { get; set; }

        #endregion

        #endregion

        #endregion

        public Contact CreateNewContact()
        {
            var _Contact = Container.NewTransientInstance<Contact>();

            _Contact.Contactee = this;
       
            return _Contact;
        }
    }
}