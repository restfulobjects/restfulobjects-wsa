// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NakedObjects;
using NakedObjects.Services;

namespace AdventureWorksModel {
    [DisplayName("Special Offers")]
    public class SpecialOfferRepository : AbstractFactoryAndRepository {
        #region CurrentSpecialOffers

        [MemberOrder(1)]
        public IQueryable<SpecialOffer> CurrentSpecialOffers()
        {
            return from obj in Instances<SpecialOffer>()
                                             where obj.StartDate <= DateTime.Now &&
                                                   obj.EndDate >= new DateTime(2004, 6, 1)
                                             select obj;
        }

        #endregion

        #region NoDiscount

        private SpecialOffer _noDiscount;

        [Hidden]
        public SpecialOffer NoDiscount() {
            if (_noDiscount == null) {
                IQueryable<SpecialOffer> query = from obj in Instances<SpecialOffer>()
                                                 where obj.SpecialOfferID == 1
                                                 select obj;

                _noDiscount = query.FirstOrDefault();
            }
            return _noDiscount;
        }

        #endregion

        #region AssociateSpecialOfferWithProduct

        [MemberOrder(2)]
        public SpecialOfferProduct AssociateSpecialOfferWithProduct(SpecialOffer offer, Product product) {
            //First check if association already exists
            IQueryable<SpecialOfferProduct> query = from sop in Instances<SpecialOfferProduct>()
                                                    where sop.SpecialOfferID == offer.SpecialOfferID &&
                                                          sop.ProductID == product.ProductID
                                                    select sop;

            if (query.Count() != 0) {
                var t = new TitleBuilder();
                t.Append(offer).Append(" is already associated with").Append(product);
                WarnUser(t.ToString());
                return null;
            }
            var newSop = NewTransientInstance<SpecialOfferProduct>();
            newSop.SpecialOffer = offer;
            newSop.Product = product;
            //product.SpecialOfferProduct.Add(newSop);
            Persist(ref newSop);
            return newSop;
        }

        #endregion

        [MemberOrder(3)]
        public SpecialOffer CreateNewSpecialOffer() {
            var obj = NewTransientInstance<SpecialOffer>();
            //set up any parameters
            //MakePersistent();
            return obj;
        }
    }
}