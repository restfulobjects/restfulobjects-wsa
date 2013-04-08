// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using NakedObjects.Services;

namespace AdventureWorksModel {
    [DisplayName("Customers")]
    public class CustomerContributedActions : AbstractFactoryAndRepository {
        public IList<Customer> ShowCustomersWithAddressInRegion(CountryRegion region, IQueryable<Customer> customers) {
            return customers.Where(c => c.Addresses.Any(a => a.Address.CountryRegion == region)).ToList();
        }
    }
}