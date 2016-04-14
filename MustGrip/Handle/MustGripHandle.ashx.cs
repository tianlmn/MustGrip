using System;
using System.Collections.Generic;
using System.Configuration;
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
        

        public void ProcessRequest(HttpContext context)
        {
            string blogFileTempPath = ConfigurationManager.AppSettings["BlogFileTempPath"];
            string serverRootPath = context.Server.MapPath("/");
            

            string data = context.Request.Params["data"];
            string f = context.Request.Params["f"];
            JavaScriptSerializer json = new JavaScriptSerializer();
            PassageEntity pEntity;
            List<PassageEntity> pList;
            List<BgMessageEntity> mList;
            string response = string.Empty;
            switch (f)
            {
                case "SavePassage":
                    BlogBusiness.SavePassage(json.Deserialize<PassageEntity>(data), serverRootPath, blogFileTempPath);
                    response = json.Serialize(new { success = 1, msg = "保存文章成功" });
                    break;
                case "GetPassageList":
                    pList = BlogBusiness.GetPassageList(json.Deserialize<PassageEntity>(data), serverRootPath);
                    response = json.Serialize(new { success = 1, result = new{PassageList=pList} });
                    break;
                case "GetPassage":
                    pEntity = json.Deserialize<PassageEntity>(data);
                    pList = BlogBusiness.GetPassageList(pEntity, serverRootPath);
                    if (pList != null && pList.Count > 0)
                    {
                        var htmlcontent = BlogBusiness.ReadFile(pList[0].Path);
                        mList = BgMessageBusiness.GetMessageListByPassageId(pEntity.PassageId);
                        response = json.Serialize(new { success = 1, result = new { content = htmlcontent, passage = pList[0], messageList = mList } });
                    }
                    else
                    {
                        response = json.Serialize(new { success = 0, msg = "文章内容被删除" });
                    }
                    break;
                case "PostMessage":
                    var sUserData = context.Request.Params["userdata"];
                    var userid = BgUserBusiness.WriteBgUserEntity(json.Deserialize<BgUserEntity>(sUserData));
                    if (userid > 0)
                    {
                        var messageEntity = json.Deserialize<BgMessageEntity>(data);
                        messageEntity.Author = userid;
                        BgMessageBusiness.PostMessage(messageEntity);
                        response = json.Serialize(new { success = 1, msg = "留言成功" });
                    }
                    else
                    {
                        response = json.Serialize(new { success = 0, msg = "用户名异常" });
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