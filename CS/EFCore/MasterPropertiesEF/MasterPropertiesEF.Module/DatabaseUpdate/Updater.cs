using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.Extensions.DependencyInjection;
using CalculatedPropertiesSolution.Module.BusinessObjects;

namespace MasterPropertiesEF.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater :ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        var cnt = ObjectSpace.GetObjectsCount(typeof(Product), null);
        if(cnt > 0) {
            return;
        }
        Product product = ObjectSpace.CreateObject<Product>();
        product.Name = "Chai";
        for(int i = 0; i < 10; i++) {
            Order order = ObjectSpace.CreateObject<Order>();
            order.Product = product;
            order.Description = "Order " + i.ToString();
            order.Total = i;
        }

        ObjectSpace.CommitChanges();
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
    }
}
