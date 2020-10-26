Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace CalculatedPropertiesSolution.Module.BusinessObjects
    <DefaultClassOptions> _
    Public Class Order
        Inherits BaseObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Private fDescription As String
        Public Property Description() As String
            Get
                Return fDescription
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Description", fDescription, value)
            End Set
        End Property
        Private fTotal As Decimal
        Public Property Total() As Decimal
            Get
                Return fTotal
            End Get
            Set(ByVal value As Decimal)
                Dim modified As Boolean = SetPropertyValue("Total", fTotal, value)
                If Not IsLoading AndAlso Not IsSaving AndAlso Product IsNot Nothing AndAlso modified Then
                    Product.UpdateOrdersTotal(True)
                    Product.UpdateMaximumOrder(True)
                End If
            End Set
        End Property
        Private fProduct As Product
        <Association("Product-Orders")> _
        Public Property Product() As Product
            Get
                Return fProduct
            End Get
            Set(ByVal value As Product)
                Dim oldProduct As Product = fProduct
                Dim modified As Boolean = SetPropertyValue("Product", fProduct, value)
                If Not IsLoading AndAlso Not IsSaving AndAlso oldProduct IsNot fProduct AndAlso modified Then
                    oldProduct = If(oldProduct, fProduct)
                    oldProduct.UpdateOrdersCount(True)
                    oldProduct.UpdateOrdersTotal(True)
                    oldProduct.UpdateMaximumOrder(True)
                End If
            End Set
        End Property
    End Class
End Namespace
