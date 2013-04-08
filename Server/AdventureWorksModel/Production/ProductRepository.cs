// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NakedObjects;
using NakedObjects.Services;

namespace AdventureWorksModel {
    [DisplayName("Products")]
    public class ProductRepository : AbstractFactoryAndRepository {
        #region FindProductByName

        public IQueryable<Product> FindProductByName(string searchString) {
            return from obj in Instances<Product>()
                    where obj.Name.ToUpper().Contains(searchString.ToUpper())
                    orderby obj.Name
                    select obj;
        }

        #endregion

        #region ListProductsBySubCategory

        public IQueryable<Product> ListProductsBySubCategory(ProductSubcategory subCategory)
        {
            return from obj in Instances<Product>()
                                        where obj.ProductSubcategory.ProductSubcategoryID == subCategory.ProductSubcategoryID
                                        orderby obj.Name
                                        select obj;

        }

        #endregion

        #region FindProductByNumber

        public Product FindProductByNumber(string number) {
            IQueryable<Product> query = from obj in Instances<Product>()
                                        where obj.ProductNumber == number
                                        select obj;

            return SingleObjectWarnIfNoMatch(query);
        }

        #endregion

        public Product RandomProduct() {
            return Random<Product>();
        }

        public IQueryable<Product> QueryProducts([Optionally, TypicalLength(40)] string whereClause,
                                            [Optionally, TypicalLength(40)] string orderByClause,
                                            bool descending) {
            IQueryable<Product> q = DynamicQuery<Product>(whereClause, orderByClause, descending);
            return q;
        }

        public virtual string ValidateQueryProducts(string whereClause, string orderByClause, bool descending) {
            return ValidateDynamicQuery<Product>(whereClause, orderByClause, descending);
        }

        public virtual Product NewProduct() {
            return Container.NewTransientInstance<Product>();
        }

    }
}