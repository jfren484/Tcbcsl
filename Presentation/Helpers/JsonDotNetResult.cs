using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Tcbcsl.Presentation.Helpers
{
    public class JsonDotNetResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("GET request not allowed");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data == null)
            {
                return;
            }

            response.Write(JsonConvert.SerializeObject(Data));
        }
    }
}