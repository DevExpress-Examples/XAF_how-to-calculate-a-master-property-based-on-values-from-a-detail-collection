Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Namespace CalculatedPropertiesSolution.Mobile

    ' NOTE
    ' The following change to web.config is required
    ' <system.serviceModel>
    '    <serviceHostingEnvironment aspNetCompatibilityEnabled="true" /> 

    Friend NotInheritable Class CorsSupport

        Private Sub New()
        End Sub


        Public Shared Sub HandlePreflightRequest(ByVal context As HttpContext)
            Dim req = context.Request
            Dim res = context.Response
            Dim origin = req.Headers("Origin")

            If Not String.IsNullOrEmpty(origin) Then
                res.AddHeader("Access-Control-Allow-Origin", origin)
                res.AddHeader("Access-Control-Allow-Credentials", "true")
                res.AddHeader("Vary", "Origin")

                Dim methods = req.Headers("Access-Control-Request-Method")
                Dim headers = req.Headers("Access-Control-Request-Headers")

                If Not String.IsNullOrEmpty(methods) Then
                    res.AddHeader("Access-Control-Allow-Methods", methods)
                End If

                If Not String.IsNullOrEmpty(headers) Then
                    res.AddHeader("Access-Control-Allow-Headers", headers)
                End If

                If req.HttpMethod = "OPTIONS" Then
                    res.StatusCode = 204
                    res.End()
                End If
            End If
        End Sub

    End Class
End Namespace
