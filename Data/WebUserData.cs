using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Common;

namespace Data
{
    public class WebUserData
    {
        public static List<WebUserEntity> GetWebUserDataList()
        {
            var result = new List<WebUserEntity>();
            string sql = @"SELECT wu.UserId,wu.UserName,wu.Email,wu.Password,wu.Datachange_LastTime,wu.Datachange_CreateTime
FROM dbo.WebUser wu(NOLOCK)";
            var dbhelper = new DBHelper(DBConnectionString.WebUserDB_Select);
            var dt = dbhelper.ExecuteDataTable(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var entity = new WebUserEntity();
                    entity.UserId = (int)dr["UserId"];
                    entity.UserName = (string)dr["UserName"];
                    entity.Email = (string)dr["Email"];
                    entity.Password = (string)dr["Password"];
                    entity.DataChange_LastTime = (DateTime)dr["Datachange_LastTime"];
                    entity.DataChange_CreateTime = (DateTime)dr["Datachange_CreateTime"];
                    result.Add(entity);
                }
            }

            return result;
        }
    }
}
