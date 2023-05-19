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
    public class Product : BaseObject, INotifyPropertyChanged {

        public virtual string Name { get; set; }
        //[Association("Product-Orders"), Aggregated]
        //public XPCollection<Order> Orders {
        //    get { return GetCollection<Order>("Orders"); }
        //}
        public virtual IList<Order> Orders { get; set; } = new ObservableCollection<Order>();

        public void MyUpdateTotal() {
              TestInt += 5;
            OnPropertyChanged("TestInt");
            OnPropertyChanged("OrdersTotal");
            //   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OrdersTotal"));
        }
        public virtual int TestInt { get => testInt; set {
                if (SetPropertyValue(ref testInt, value))
                    OnPropertyChanged("TestInt");
            }
        }

        private int? fOrdersCount = null;
        public int? OrdersCount {
            get {
                if (fOrdersCount == null)
                    UpdateOrdersCount(false);
                return fOrdersCount;
            }
        }
        private decimal? fOrdersTotal = null;
        public decimal? OrdersTotal {
            get {
                if (fOrdersTotal == null)
                    UpdateOrdersTotal(false);
                return fOrdersTotal;
            }
        }
        private decimal? fMaximumOrder = null;

        private PropertyChangedEventHandler propertyChanged;
        private int testInt;

        protected bool SetPropertyValue<T>(ref T propertyValue, T newValue, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(propertyValue, newValue)) {
                return false;
            }
            propertyValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            if (propertyChanged != null) {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
            add { propertyChanged += value; }
            remove { propertyChanged -= value; }
        }

        public decimal? MaximumOrder {
            get {
                if (fMaximumOrder == null)
                    UpdateMaximumOrder(false);
                return fMaximumOrder;
            }
        }
        public void UpdateOrdersCount(bool forceChangeEvents) {
            int? oldOrdersCount = fOrdersCount;
            fOrdersCount = Orders.Count;
            if (forceChangeEvents) {
                OnPropertyChanged("OrdersCount");

            }
        }

  
    
        public void UpdateOrdersTotal(bool forceChangeEvents) {
            decimal? oldOrdersTotal = fOrdersTotal;
            decimal tempTotal = 0m;
            foreach (Order detail in Orders)
                tempTotal += detail.Total;
            fOrdersTotal = tempTotal;
            if (forceChangeEvents) {
                OnPropertyChanged("OrdersTotal");
            }
        }
        public void UpdateMaximumOrder(bool forceChangeEvents) {
            decimal? oldMaximumOrder = fMaximumOrder;
            decimal tempMaximum = 0m;
            foreach (Order detail in Orders)
                if (detail.Total > tempMaximum)
                    tempMaximum = detail.Total;
            fMaximumOrder = tempMaximum;
            if (forceChangeEvents)
                OnPropertyChanged("MaximumOrder");
        }



        private void Reset() {
            fOrdersCount = null;
            fOrdersTotal = null;
            fMaximumOrder = null;
        }
    }
}
