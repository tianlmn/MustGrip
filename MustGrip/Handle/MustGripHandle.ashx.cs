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
                    response = json.Serialize(new { success = 1, msg = "success" });
                    break;
                case "GetPassageList":
                    List<PassageEntity> pList = BlogBusiness.GetPassageList(json.Deserialize<PassageEntity>(data));
                    response = json.Serialize(new { success = 1, result = new{PassageList=pList,Total=1} });
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