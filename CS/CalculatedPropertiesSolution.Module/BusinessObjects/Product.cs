using DevExpress.Data.Filtering;
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
    public class Product : BaseObject {
        public Product(Session session) : base(session) { }
        private string fName;
        public string Name {
            get { return fName; }
            set { SetPropertyValue("Name", ref fName, value); }
        }
        [Association("Product-Orders"), Aggregated]
        public XPCollection<Order> Orders {
            get { return GetCollection<Order>("Orders"); }
        }
        private int? fOrdersCount = null;
        public int? OrdersCount {
            get {
                if (!IsLoading && !IsSaving && fOrdersCount == null)
                    UpdateOrdersCount(false);
                return fOrdersCount;
            }
        }
        private decimal? fOrdersTotal = null;
        public decimal? OrdersTotal {
            get {
                if (!IsLoading && !IsSaving && fOrdersTotal == null)
                    UpdateOrdersTotal(false);
                return fOrdersTotal;
            }
        }
        private decimal? fMaximumOrder = null;
        public decimal? MaximumOrder {
            get {
                if (!IsLoading && !IsSaving && fMaximumOrder == null)
                    UpdateMaximumOrder(false);
                return fMaximumOrder;
            }
        }
        public void UpdateOrdersCount(bool forceChangeEvents) {
            int? oldOrdersCount = fOrdersCount;
            fOrdersCount = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("Orders.Count")));
            if (forceChangeEvents)
                OnChanged("OrdersCount", oldOrdersCount, fOrdersCount);
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
        protected override void OnLoaded() {
            Reset();
            base.OnLoaded();
        }
        private void Reset() {
            fOrdersCount = null;
            fOrdersTotal = null;
            fMaximumOrder = null;
        }
    }
}
