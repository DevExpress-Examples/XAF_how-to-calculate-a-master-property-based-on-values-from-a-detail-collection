using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatedPropertiesSolution.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Order : BaseObject {
        public Order() {
            ((INotifyPropertyChanged)this).PropertyChanged += Order_PropertyChanged;
        }

        private void Order_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            Product.UpdateOrdersCount(true);
            Product.UpdateOrdersTotal(true);
            Product.UpdateMaximumOrder(true);
        }

        public virtual string Description { get; set; }
        public virtual decimal Total { get; set; }
        //public decimal Total {
        //    get { return fTotal; }
        //    set {
        //        bool modified = SetPropertyValue("Total", ref fTotal, value);
        //        if (!IsLoading && !IsSaving && Product != null && modified) {
        //            Product.UpdateOrdersTotal(true);
        //            Product.UpdateMaximumOrder(true);
        //        }
        //    }
        //}
        public virtual Product Product { get; set; }
        //private Product fProduct;
        //[Association("Product-Orders")]
        //public Product Product {
        //    get { return fProduct; }
        //    set {
        //        Product oldProduct = fProduct;
        //        bool modified = SetPropertyValue("Product", ref fProduct, value);
        //        if (!IsLoading && !IsSaving && oldProduct != fProduct && modified) {
        //            oldProduct = oldProduct ?? fProduct;
        //            oldProduct.UpdateOrdersCount(true);
        //            oldProduct.UpdateOrdersTotal(true);
        //            oldProduct.UpdateMaximumOrder(true);
        //        }
        //    }
        //}
    }
}
