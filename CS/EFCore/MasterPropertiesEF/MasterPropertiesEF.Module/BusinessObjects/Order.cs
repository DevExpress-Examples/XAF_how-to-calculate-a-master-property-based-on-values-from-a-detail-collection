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
        public virtual string Description { get; set; }
        public virtual decimal Total { get; set; }
        public virtual Product Product { get; set; }
        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnPropertyChanged(sender, e);
            Product?.UpdateCalculatedProperties();
        }
    }
}
