// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NakedObjects.Services;

namespace AdventureWorksModel {
    [DisplayName("Purchase Orders")]
    public class PurchaseOrderRepository : AbstractFactoryAndRepository {
        #region Injected Services

        // This region should contain properties to hold references to any services required by the
        // object.  Use the 'injs' shortcut to add a new service.

        #endregion

        #region OpenPurchaseOrdersForVendor

        public IQueryable<PurchaseOrderHeader> OpenPurchaseOrdersForVendor(Vendor vendor)
        {
            return from obj in Instances<PurchaseOrderHeader>()
                                                    where obj.Vendor.VendorID == vendor.VendorID && obj.Status <= 2
                                                    select obj;
        }

        #endregion

        #region OpenPurchaseOrdersForVendor

        public IQueryable<PurchaseOrderHeader> ListPurchaseOrders(Vendor vendor, DateTime? fromDate, DateTime? toDate)
        {
            IQueryable<PurchaseOrderHeader> query = from obj in Instances<PurchaseOrderHeader>()
                                                    where obj.Vendor.VendorID == vendor.VendorID &&
                                                          (fromDate == null || obj.OrderDate >= fromDate) &&
                                                          (toDate == null || obj.OrderDate <= toDate)
                                                    orderby obj.OrderDate
                                                    select obj;

            return query;
        }

        #endregion

        #region OpenPurchaseOrdersForProduct

        public IQueryable<PurchaseOrderHeader> OpenPurchaseOrdersForProduct(Product product)
        {
            return from obj in Instances<PurchaseOrderDetail>()
                                                    where obj.Product.ProductID == product.ProductID &&
                                                          obj.PurchaseOrderHeader.Status <= 2
                                                    select obj.PurchaseOrderHeader;
        }

        #endregion

        #region RandomPurchaseOrder

        public PurchaseOrderHeader RandomPurchaseOrder() {
            return Random<PurchaseOrderHeader>();
        }

        #endregion

        public PurchaseOrderHeader CreateNewPurchaseOrder(Vendor vendor) {
            var _PurchaseOrderHeader = NewTransientInstance<PurchaseOrderHeader>();
            _PurchaseOrderHeader.Vendor = vendor;
            //MakePersistent(_PurchaseOrderHeader);
            return _PurchaseOrderHeader;
        }
    }
}