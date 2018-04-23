using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatedPropertiesSolution.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Order : BaseObject {
        public Order(Session session) : base(session) { }
        private string fDescription;
        public string Description {
            get { return fDescription; }
            set { SetPropertyValue("Description", ref fDescription, value); }
        }
        private decimal fTotal;
        public decimal Total {
            get { return fTotal; }
            set {
                bool modified = SetPropertyValue("Total", ref fTotal, value);
                if (!IsLoading && !IsSaving && Product != null && modified) {
                    Product.UpdateOrdersTotal(true);
                    Product.UpdateMaximumOrder(true);
                }
            }
        }
        private Product fProduct;
        [Association("Product-Orders")]
        public Product Product {
            get { return fProduct; }
            set {
                Product oldProduct = fProduct;
                bool modified = SetPropertyValue("Product", ref fProduct, value);
                if (!IsLoading && !IsSaving && oldProduct != fProduct && modified) {
                    oldProduct = oldProduct ?? fProduct;
                    oldProduct.UpdateOrdersCount(true);
                    oldProduct.UpdateOrdersTotal(true);
                    oldProduct.UpdateMaximumOrder(true);
                }
            }
        }
    }
}
