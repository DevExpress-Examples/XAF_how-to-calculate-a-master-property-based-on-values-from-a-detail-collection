using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatedPropertiesSolution.Mobile {

    // NOTE
    // The following change to web.config is required
    // <system.serviceModel>
    //    <serviceHostingEnvironment aspNetCompatibilityEnabled="true" /> 

    static class CorsSupport {

        public static void HandlePreflightRequest(HttpContext context) {
            var req = context.Request;
            var res = context.Response;
            var origin = req.Headers["Origin"];

            if (!String.IsNullOrEmpty(origin)) {
                res.AddHeader("Access-Control-Allow-Origin", origin);
                res.AddHeader("Access-Control-Allow-Credentials", "true");
                res.AddHeader("Vary", "Origin");

                var methods = req.Headers["Access-Control-Request-Method"];
                var headers = req.Headers["Access-Control-Request-Headers"];

                if (!String.IsNullOrEmpty(methods))
                    res.AddHeader("Access-Control-Allow-Methods", methods);

                if (!String.IsNullOrEmpty(headers))
                    res.AddHeader("Access-Control-Allow-Headers", headers);

                if (req.HttpMethod == "OPTIONS") {
                    res.StatusCode = 204;
                    res.End();
                }
            }
        }

    }
}
