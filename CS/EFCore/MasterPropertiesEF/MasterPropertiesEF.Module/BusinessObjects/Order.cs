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
      
        public virtual Product Product { get; set; }
        
    }
}
