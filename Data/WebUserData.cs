using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Common;

namespace Data
{
    public class WebUserData
    {
        public static List<WebUserEntity> GetPassageEntityList(WebUserEntity condition)
        {
            var result = new List<WebUserEntity>();
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT [UserId]
      ,[UserName]
      ,[Email]
      ,[Password]
      ,[DataChange_LastTime]
      ,[DataChange_CreateTime]
      ,[WebAddress]
  FROM [qds113752475_db].[dbo].[WebUser](NOLOCK)
  WHERE 1=1 ");

            var paramList = new List<SqlParameter>();
            if (condition != null && !string.IsNullOrEmpty(condition.UserName))
            {
                sql.Append(@" AND UserName =@UserName ");
                paramList.Add(new SqlParameter("@UserName", condition.UserName));
            }
            if (condition != null && !string.IsNullOrEmpty(condition.UserName))
            {
                sql.Append(@" AND Email =@Email ");
                paramList.Add(new SqlParameter("@Email", condition.Email));
            }

            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            var dt = dbhelper.ExecuteSql(sql.ToString(), paramList);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var entity = new WebUserEntity();
                    entity.UserId = (int)dr["UserId"];
                    entity.UserName = (string)dr["UserName"];
                    entity.Email = (string)dr["Email"];
                    entity.WebAddress = (string)(dr["WebAddress"]);

                    result.Add(entity);
                }
            }

            return result;
        }

        /// <summary>
        /// 添加一条用户表数据
        /// </summary>
        /// <param name="classEntity">用户表实体类</param>
        public static void Insert(WebUserEntity classEntity)
        {
            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            SqlParameter[] paramList = new SqlParameter[]
            {	            new SqlParameter("@UserId", classEntity.UserId),	            new SqlParameter("@UserName", classEntity.UserName),	            new SqlParameter("@Email", classEntity.Email),	            new SqlParameter("@Password", classEntity.Password),	            new SqlParameter("@DataChange_LastTime", classEntity.DataChange_LastTime),	            new SqlParameter("@DataChange_CreateTime", classEntity.DataChange_CreateTime),	            new SqlParameter("@WebAddress", classEntity.WebAddress),		        };
            dbhelper.ExecuteNonQuery("spA_WebUser_i", CommandType.StoredProcedure, paramList);
        }



        /// <summary>
        /// 修改一条用户表数据
        /// </summary>
        /// <param name="classEntity">被修改的用户表实体类</param>
        public static void Update(WebUserEntity classEntity)
        {
            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            SqlParameter[] paramList = new SqlParameter[]
            {
                new SqlParameter("@UserId", classEntity.UserId),
                new SqlParameter("@UserName", classEntity.UserName),
                new SqlParameter("@Email", classEntity.Email),
                new SqlParameter("@Password", classEntity.Password),
                new SqlParameter("@DataChange_LastTime", classEntity.DataChange_LastTime),
                new SqlParameter("@DataChange_CreateTime", classEntity.DataChange_CreateTime),
                new SqlParameter("@WebAddress", classEntity.WebAddress),

            };
            dbhelper.ExecuteNonQuery("spA_WebUser_u", CommandType.StoredProcedure, paramList);
        }

    }
}
