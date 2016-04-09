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
            List<PassageEntity> pList;
            string response = string.Empty;
            switch (f)
            {
                case "SavePassage":
                    BlogBusiness.SavePassage(json.Deserialize<PassageEntity>(data), PassageRootPath);
                    response = json.Serialize(new { success = 1, msg = "success" });
                    break;
                case "GetPassageList":
                    pList = BlogBusiness.GetPassageList(json.Deserialize<PassageEntity>(data));
                    response = json.Serialize(new { success = 1, result = new{PassageList=pList} });
                    break;
                case "GetPassage":
                    pList = BlogBusiness.GetPassageList(json.Deserialize<PassageEntity>(data));
                    if (pList != null && pList.Count > 0)
                    {
                        var htmlcontent = BlogBusiness.ReadFile(pList[0].Path);
                        response = json.Serialize(new { success = 1, result = new { content = htmlcontent, passage = pList[0] } });
                    }
                    else
                    {
                        response = json.Serialize(new { success = 0, msg = "文章内容被删除" });
                    }
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