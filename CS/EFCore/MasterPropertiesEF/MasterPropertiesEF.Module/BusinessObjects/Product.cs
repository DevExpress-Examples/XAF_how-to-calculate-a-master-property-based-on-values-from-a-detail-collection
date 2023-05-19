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
using System.Text;
using System.Threading.Tasks;

namespace CalculatedPropertiesSolution.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Product : BaseObject, INotifyPropertyChanged {
        
        public virtual string Name {get;set;        }
        //[Association("Product-Orders"), Aggregated]
        //public XPCollection<Order> Orders {
        //    get { return GetCollection<Order>("Orders"); }
        //}
        public virtual IList<Order> Orders { get; set; } = new ObservableCollection<Order>();




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

        public event PropertyChangedEventHandler PropertyChanged;

        public decimal? MaximumOrder {
            get {
                if ( fMaximumOrder == null)
                    UpdateMaximumOrder(false);
                return fMaximumOrder;
            }
        }
        public void UpdateOrdersCount(bool forceChangeEvents) {
            int? oldOrdersCount = fOrdersCount;
            fOrdersCount = Orders.Count;
            if (forceChangeEvents)
                OnChanged("OrdersCount", oldOrdersCount, fOrdersCount);
        }

        private void OnChanged(string v, decimal? oldOrdersCount, decimal? fOrdersCount) {
            //   PropertyChanged(this,new PropertyChangedEventArgs(v));  
            RaisePropertyChanged(v);
            var st = this.GetType();
            var st2 = st.BaseType;
        }
        protected void RaisePropertyChanged(string propertyName) {

            if (PropertyChanged != null)

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
        public void UpdateOrdersTotal(bool forceChangeEvents) {
            decimal? oldOrdersTotal = fOrdersTotal;
            decimal tempTotal = 0m;
            foreach (Order detail in Orders)
                tempTotal += detail.Total;
            fOrdersTotal = tempTotal;
            if (forceChangeEvents)
                OnChanged("OrdersTotal", oldOrdersTotal, fOrdersTotal);
        }
        public void UpdateMaximumOrder(bool forceChangeEvents) {
            decimal? oldMaximumOrder = fMaximumOrder;
            decimal tempMaximum = 0m;
            foreach (Order detail in Orders)
                if (detail.Total > tempMaximum)
                    tempMaximum = detail.Total;
            fMaximumOrder = tempMaximum;
            if (forceChangeEvents)
                OnChanged("MaximumOrder", oldMaximumOrder, fMaximumOrder);
        }


      
        private void Reset() {
            fOrdersCount = null;
            fOrdersTotal = null;
            fMaximumOrder = null;
        }
    }
}
