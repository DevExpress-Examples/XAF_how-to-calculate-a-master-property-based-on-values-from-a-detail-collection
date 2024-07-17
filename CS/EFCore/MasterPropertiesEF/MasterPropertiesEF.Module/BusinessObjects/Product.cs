using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CalculatedPropertiesSolution.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Product :BaseObject {

        public virtual string Name { get; set; }
        public virtual IList<Order> Orders { get; set; } = new ObservableCollection<Order>();
        private int? fOrdersCount = null;
        public int? OrdersCount {
            get {
                return fOrdersCount;
            }
        }
        private decimal? fOrdersTotal = null;
        public decimal? OrdersTotal {
            get {
                return fOrdersTotal;
            }
        }
        private decimal? fMaximumOrder = null;
        public decimal? MaximumOrder {
            get {
                return fMaximumOrder;
            }
        }

        public void UpdateCalculatedProperties() {

            decimal tempMaximum = 0m;
            decimal tempTotal = 0m;
            foreach(Order detail in Orders) {
                if(detail.Total > tempMaximum) {
                    tempMaximum = detail.Total;
                }
                tempTotal += detail.Total;
            }
            fMaximumOrder = tempMaximum;
            fOrdersTotal = tempTotal;
            fOrdersCount = Orders.Count;
            base.OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(OrdersCount)));
            base.OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(OrdersTotal)));
            base.OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(MaximumOrder)));
        }

        public override void OnCreated() {
            base.OnCreated();
            UpdateCalculatedProperties();
            ((ObservableCollection<Order>)this.Orders).CollectionChanged += Product_CollectionChanged;
        }
        public override void OnLoaded() {
            base.OnLoaded();
            UpdateCalculatedProperties();
            ((ObservableCollection<Order>)this.Orders).CollectionChanged += Product_CollectionChanged;
        }

        private void Product_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            UpdateCalculatedProperties();
        }
    }
}
