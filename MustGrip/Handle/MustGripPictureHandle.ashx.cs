using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;


namespace Ctrip.Hotel.Business.UI.HotelInfo.HostelHotelOffline.Handler
{
    /// <summary>
    /// HostelHotelConfigHande 的摘要说明
    /// </summary>
    public class HostelHotelConfigHande : IHttpHandler
    {
        public static readonly Dictionary<string,string> Dic = new Dictionary<string, string>(){{"HostelHotelConfig","HostelHotelConfig"}}; 
        public void ProcessRequest(HttpContext context)
        {
            var f = context.Request.Params["f"];
            var type = context.Request.Params["type"];
            var sData = context.Request.Params["sData"];

            string response = string.Empty;
            JavaScriptSerializer json = new JavaScriptSerializer();

            try
            {
                switch (f)
                {
                    case "getImageListByType":
                        //response = json.Serialize(new {success = 1, result = GetImageListByType(type)});
                        break;
                    case "setImageListByType":
                        //SetImageListByType(type, json.Deserialize<List<HostelHotelConfigEntity>>(sData));
                        response = json.Serialize(new { success = 1, msg = "保存成功" });
                        break;

                }
            }
            catch (Exception ex)
            {
                response = json.Serialize(new { success = 0, msg = "服务器错误" });
            }
            

            context.Response.ContentType = "application/json";
            context.Response.Write(response);
        }

        //protected List<HostelHotelConfigEntity> GetImageListByType(string sData)
        //{
        //    int type = int.Parse(sData);
        //    return HostelHotelConfigBusiness.GetImageListByType(type);
        //    return null;
        //}

        //protected void SetImageListByType(string type, List<HostelHotelConfigEntity> sData)
        //{
        //    int itype = int.Parse(type);
        //    HostelHotelConfigBusiness.SetImageListByType(itype, sData);
        //}


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}