using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entity;

namespace Business
{
    public class BlogBusiness
    {
        public static int SavePassage(PassageEntity entity, out string msg)
        {
            msg = string.Empty;
            try
            {
                if (CheckInput(entity, ref msg))
                {
                    entity.DataChange_CreateTime = DateTime.Now;
                    entity.DataChange_LastTime = DateTime.Now;
                    WriteFile(entity.Content, entity.Path);
                    if (entity.PassageId > 0)
                    {
                        BlogData.UpdateEntity(entity);
                        msg = "更新成功";
                    }
                    else if (entity.PassageId == 0)
                    {
                        BlogData.InsertEntity(entity);
                        msg = "插入成功";
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
                throw;
            }
            
            return 0;
        }



        private static bool CheckInput(PassageEntity entity, ref string msg)
        {
            if (string.IsNullOrEmpty(entity.Title))
            {
                msg = "标题不能为空";
                return false;
            }
            return true;
        }


        /// <summary>
        /// 写入XML方法
        /// </summary>
        /// <param name="input">要写入XML文件的内容</param>
        /// <param name="path">相对路径就OK了，不用绝对路径</param>
        public static void WriteFile(string input, string filename)
        {
            FileStream fs = new FileStream(filename,FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            sw.Write(input);
            sw.Flush();
            sw.Close();
        }

        public static string ReadFile(string filename)
        {
            FileStream fs = new FileStream(filename,FileMode.Open,FileAccess.Read);
            StreamReader sr = new StreamReader(fs,Encoding.UTF8);
            string r1 = null;
            StringBuilder sb = new StringBuilder();
            while ((r1=sr.ReadLine()) != null)
            {
                sb.Append(r1+"<br />");
            }
            sr.Close();
            fs.Close();
            return sb.ToString();
        }

        public static List<PassageEntity> GetPassageList(PassageEntity condition, string serverRootPath)
        {
            var plist = BlogData.GetPassageEntityList(condition);
            plist.ForEach(p => p.Path = serverRootPath + p.Path);
            return plist;
        }
    }
}
