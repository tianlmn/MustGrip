using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Business;
using Common;
using Entity;

namespace MustGrip.Handle
{
    /// <summary>
    /// MustGripHandle 的摘要说明
    /// </summary>
    public class MustGripHandle : IHttpHandler, IRequiresSessionState
    {
        public string PassageRootPath;
        public void ProcessRequest(HttpContext context)
        {
            if (string.IsNullOrEmpty(PassageRootPath))
            {
                PassageRootPath = context.Server.MapPath("/PassageRootPath");
            }

            string data = context.Request.Params["data"];
            string f = context.Request.Params["f"];
            JavaScriptSerializer json = new JavaScriptSerializer();
            string response = string.Empty;
            switch (f)
            {
                case "SavePassage":
                    BlogBusiness.SavePassage(json.Deserialize<PassageEntity>(data), PassageRootPath);
                    response = json.Serialize(new { result = 1, msg = "success" });
                    break;
                case "ReadPassage":
                    var content = BlogBusiness.ReadFile(
                        @"D:\用户目录\我的文档\Visual Studio 2013\Projects\mustgrip\MustGrip\PassageRootPath\04a9b1be-031b-44b2-8872-6229733c94cc.html");
                    response = json.Serialize(new { result = 1, msg = content });
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