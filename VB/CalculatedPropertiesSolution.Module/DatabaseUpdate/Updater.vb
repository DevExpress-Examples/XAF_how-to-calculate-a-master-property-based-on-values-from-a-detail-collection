Imports System
Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports CalculatedPropertiesSolution.Module.BusinessObjects

Namespace CalculatedPropertiesSolution.Module.DatabaseUpdate
	Public Class Updater
		Inherits ModuleUpdater

		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
			Dim product As Product = ObjectSpace.CreateObject(Of Product)()
			product.Name = "Chai"
			For i As Integer = 0 To 9
				Dim order As Order = ObjectSpace.CreateObject(Of Order)()
				order.Product = product
				order.Description = "Order " & i.ToString()
				order.Total = i
			Next i

			ObjectSpace.CommitChanges()
		End Sub
		Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
			MyBase.UpdateDatabaseBeforeUpdateSchema()
		End Sub
	End Class
End Namespace
