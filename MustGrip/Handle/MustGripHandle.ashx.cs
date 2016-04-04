using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Business;
using Common;
using Entity;

namespace MustGrip.Handle
{
    /// <summary>
    /// MustGripHandle 的摘要说明
    /// </summary>
    public class MustGripHandle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string data = context.Request.Params["data"];
            string f = context.Request.Params["f"];
            string response = string.Empty;
            switch (f)
            {
                case "SavePassage":
                    JavaScriptSerializer json = new JavaScriptSerializer();
                    BlogBusiness.SavePassage(json.Deserialize<PassageEntity>(data));
                    response = json.Serialize(new { result = 1, msg = "success" });
                    break;

            }
            
            context.Response.ContentType = "application/json";
            context.Response.Write(response);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}