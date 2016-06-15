using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Hotel.Product.Business.BasicInfoBusiness.HotelPicture;
using Hotel.Product.Common.Exception;
using Hotel.Product.Common.Utility.Logging;

namespace Ctrip.Hotel.Business.UI.HotelInfo.HostelHotelOffline.Handler
{
    /// <summary>
    /// UploadHandle 的摘要说明
    /// </summary>
    public class UploadHandle : IHttpAsyncHandler
    {
        private static readonly string FILE_QUERY_VAR = "file";
        private static readonly string FILE_GET_CONTENT_TYPE = "application/octet-stream";
        private static readonly int ATTEMPTS_TO_WRITE = 3;
        private static readonly int ATTEMPT_WAIT = 100; //msec
        protected static readonly string ImgDirectoryPath = ConfigurationManager.AppSettings["ImgSourceServerDimg04"];
        private static readonly Dictionary<string, string> Dic = new Dictionary<string, string>() { { "HostelHotelConfig", "HostelHotelConfig" } };


        private static class HttpMethods
        {
            public static readonly string GET = "GET";
            public static readonly string POST = "POST";
            public static readonly string DELETE = "DELETE";
        }

        [DataContract]
        private class FileResponse
        {
            [DataMember]
            public string Name;
            [DataMember]
            public string Size;
            [DataMember]
            public string ImageUrl;
        }

        [DataContract]
        private class UploaderResponse
        {
            [DataMember]
            public int success;

            [DataMember]
            public string msg;

            [DataMember]
            public FileResponse[] files;

            public UploaderResponse(int success,string msg,FileResponse[] fileResponses)
            {
                this.files = fileResponses;
                this.success = success;
                this.msg = msg;
            }
        }


        private static FileResponse CreateFileResponse(string fileName, string size, string url)
        {
            return new FileResponse()
            {
                Name = Path.GetFileName(fileName),
                Size = size,
                ImageUrl = url
            };
        }

        private static void SerializeUploaderResponse(HttpResponse response, List<FileResponse> fileResponses, int suc, string msg)
        {

            var Serializer = new global::System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(UploaderResponse));
            if (fileResponses == null) fileResponses=new List<FileResponse>();
            Serializer.WriteObject(response.OutputStream, new UploaderResponse(suc, msg, fileResponses.ToArray()));
        }

        #region IHttpAsyncHandler

        private ProcessRequestDelegate RequestDelegate;
        private delegate void ProcessRequestDelegate(HttpContext context);

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            RequestDelegate = new ProcessRequestDelegate(ProcessRequest);

            return RequestDelegate.BeginInvoke(context, cb, extraData);
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            RequestDelegate.EndInvoke(result);
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            List<FileResponse> FileResponseList = new List<FileResponse>();
            try
            {
                if (context.Request.HttpMethod.ToUpper() == HttpMethods.POST)
                {
                    for (int FileIndex = 0; FileIndex < context.Request.Files.Count; FileIndex++)
                    {
                        HttpPostedFile File = context.Request.Files[FileIndex];

                        int FileLength = File.ContentLength; //记录文件长度 
                        int width = 0;
                        int height = 0;
                        string responseXml = string.Empty;
                        if (FileLength > 0)
                        {
                            Byte[] filesArray = new byte[FileLength];
                            Stream StreamObject = File.InputStream; //建立数据流对像
                            //读取图象文件数据
                            StreamObject.Read(filesArray, 0, FileLength);

                            System.Drawing.Image image = System.Drawing.Image.FromStream(File.InputStream);
                            width = image.Width;
                            height = image.Height;
                            if (width != 520 || height != 320)
                            {
                                throw new CustomException("图片的大小必须为520*320");
                            }
                            PictureServiceClient.UploadImage(filesArray, 0, out responseXml);
                        }

                        #region 上传图片到服务器

                        System.IO.StringReader strReader = new System.IO.StringReader(responseXml);
                        System.Xml.XmlTextReader tr = new System.Xml.XmlTextReader(strReader);
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        doc.Load(tr);
                        string uploadedPicturePath =
                            doc.DocumentElement.SelectSingleNode("SaveResponse")
                                .SelectSingleNode("OriginalPath")
                                .InnerText;
                        uploadedPicturePath = uploadedPicturePath.Replace("\\", "/");
                        uploadedPicturePath = ImgDirectoryPath + uploadedPicturePath;

                        #endregion

                        FileResponseList.Add(CreateFileResponse(File.FileName, width + "/" + height, uploadedPicturePath));
                    }

                    SerializeUploaderResponse(context.Response, FileResponseList, 1, "成功");
                    context.Response.End();
                }
                else
                {
                    context.Response.StatusCode = 405;
                    context.Response.StatusDescription = "Method not allowed";
                    context.Response.End();
                }
            }
            catch (CustomException ex)
            {
                SerializeUploaderResponse(context.Response, FileResponseList, 0, ex.ToString());
                context.Response.End();
            }
            catch (Exception ex)
            {
                Logger.Info("客栈民宿后台配置",ex.ToString(),Dic);
                SerializeUploaderResponse(context.Response, FileResponseList, 0, "部分文件上传失败");
                context.Response.End();
            }

        }

        #endregion
    }
}