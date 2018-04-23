Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Security

Namespace CalculatedPropertiesSolution.Mobile
    Public Class [Global]
        Inherits System.Web.HttpApplication

        Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
            CorsSupport.HandlePreflightRequest(HttpContext.Current)
        End Sub
    End Class
End Namespace
