using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;


using System.IO;
using System.ComponentModel;
using CalculatedPropertiesSolution.Module.BusinessObjects;

namespace dxTestSolution.Module.Controllers {
	//public class CustomWinController : ObjectViewController<ListView,Contact> {
    public class CustomWinController : ViewController {
        public CustomWinController() {
            var myAction1 = new SimpleAction(this, "MyWinAction1", PredefinedCategory.Edit);
            myAction1.Execute += MyAction1_Execute;
            
        }

        private void MyAction1_Execute(object sender, SimpleActionExecuteEventArgs e) {
            var p = View.CurrentObject as Product;
           // p.TestInt = 123;
           // p.Orders[0].Total += 99;
            p.MyUpdateTotal();
        }

        protected override void OnActivated() {
            base.OnActivated();
            var cnt = Frame.GetController<NewObjectViewController>();
            if(cnt != null) {

            }
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated() {
            base.OnDeactivated();
        }
    }
}
