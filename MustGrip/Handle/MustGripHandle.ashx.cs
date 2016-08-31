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
            List<BgUserEntity> uList;
            List<BgMessageRankEntity> rList;
            string response = string.Empty;
            string msg = string.Empty;
            try
            {
                switch (f)
                {
                    case "SavePassage":

                        SavePassage(json.Deserialize<PassageEntity>(data), serverRootPath, blogFileTempPath, out msg);
                        response = json.Serialize(
                            new
                            {
                                success = 1,
                                msg
                            }
                            );
                        break;
                    case "GetPassageList":
                        pList = BlogBusiness.GetPassageList(json.Deserialize<PassageEntity>(data), serverRootPath);
                        response = json.Serialize(new {success = 1, result = new {PassageList = pList}});
                        break;
                    case "GetPassage":
                        pEntity = json.Deserialize<PassageEntity>(data);
                        pList = BlogBusiness.GetPassageList(pEntity, serverRootPath);
                        if (pList != null && pList.Count > 0)
                        {
                            var htmlcontent = BlogBusiness.ReadFile(pList[0].Path);
                            response = json.Serialize(new
                            {
                                success = 1,
                                result = new
                                {
                                    content = htmlcontent,
                                    passage = pList[0]
                                }
                            });
                        }
                        else
                        {
                            response = json.Serialize(new {success = 0, msg = "文章内容被删除"});
                        }
                        break;
                    case "GetMessage":
                        pEntity = json.Deserialize<PassageEntity>(data);
                        mList = BgMessageBusiness.GetMessageListByPassageId(pEntity.PassageId);
                        uList = BgUserBusiness.GetBgUserEntityList(new BgUserEntity());
                        rList = BgMessageBusiness.GetRankByPassageId(pEntity.PassageId);
                        response = json.Serialize(new
                        {
                            success = 1,
                            result = new
                            {
                                messageList = Utility.ConvertToTrees((
                                    from m in mList
                                    join u in uList on m.Author equals u.BgUserId
                                    join r in rList on m.BgMessageId equals r.BgMessageId
                                    select new BgMessageTreeEntity()
                                    {
                                        Name = u.Name,
                                        CreateTime = m.DataChange_CreateTime.ToString("yyyy年MM月dd日 HH:mm:ss"),
                                        Message = m.Message,
                                        WebAddress = u.WebAddress,
                                        MasterMessageId = m.MasterMessageId,
                                        PRankId = m.PRankId,
                                        BgMessageId = m.BgMessageId,
                                        MaxRankId = r.MaxRankId,
                                        ChildList = new List<BgMessageTreeEntity>()
                                    }).ToList(), "MasterMessageId", "BgMessageId", "ChildList")
                            }
                        });
                        break;
                    case "PostMessage":
                        var sUserData = context.Request.Params["userdata"];
                        var userid = BgUserBusiness.WriteBgUserEntity(json.Deserialize<BgUserEntity>(sUserData));
                        if (userid > 0)
                        {
                            var messageEntity = json.Deserialize<BgMessageEntity>(data);
                            messageEntity.Author = userid;
                            BgMessageBusiness.PostMessage(messageEntity);
                            response = json.Serialize(new {success = 1, msg = "留言成功"});
                        }
                        else
                        {
                            response = json.Serialize(new {success = 0, msg = "用户名异常"});
                        }
                        break;


                }

                context.Response.ContentType = "application/json";
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private int SavePassage(PassageEntity entity, string serverRootPath, string blogFileTempPath, out string msg)
        {
            string filename = Guid.NewGuid().ToString();
            entity.Path = blogFileTempPath + "\\" + filename + ".html";
            return BlogBusiness.SavePassage(entity, serverRootPath, out msg);
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