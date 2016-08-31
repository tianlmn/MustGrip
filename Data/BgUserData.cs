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
    public class BgUserData
    {
        public static List<BgUserEntity> GetBgUserEntityList(BgUserEntity condition)
        {
            var result = new List<BgUserEntity>();
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT [BgUserId]
      ,[Name]
      ,[Email]
      ,[DataChange_LastTime]
      ,[DataChange_CreateTime]
      ,[WebAddress]
  FROM [qds167160447_db].[dbo].[BgUser](NOLOCK)
  WHERE 1=1 ");

            var paramList = new List<SqlParameter>();
            if (condition != null && !string.IsNullOrEmpty(condition.Name))
            {
                sql.Append(@" AND Name =@Name ");
                paramList.Add(new SqlParameter("@Name", condition.Name));
            }
            if (condition != null && !string.IsNullOrEmpty(condition.Email))
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
                    var entity = new BgUserEntity();
                    entity.BgUserId = (int)dr["BgUserId"];
                    entity.Name = (string)dr["Name"];
                    entity.Email = (string)dr["Email"];
                    entity.WebAddress = (string)(dr["WebAddress"]);

                    result.Add(entity);
                }
            }

            return result;
        }


        /// <summary>
        /// 修改一条用户表数据
        /// </summary>
        /// <param name="classEntity">被修改的用户表实体类</param>
        public static void Update(BgUserEntity classEntity)
        {
            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            SqlParameter[] paramList = new SqlParameter[]
            {
                new SqlParameter("@BgUserId", classEntity.BgUserId),
                new SqlParameter("@Name", classEntity.Name),
                new SqlParameter("@Email", classEntity.Email),
                new SqlParameter("@WebAddress", classEntity.WebAddress),
                new SqlParameter("@DataChange_LastTime", DateTime.Now),
                //new SqlParameter("@DataChange_CreateTime", classEntity.DataChange_CreateTime),

            };
            dbhelper.ExecuteProc("BgUser_Update", paramList);
        }




        /// <summary>
        /// 新增一条用户表数据
        /// </summary>
        /// <param name="classEntity">被修改的用户表实体类</param>
        public static int Insert(BgUserEntity classEntity)
        {
            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            SqlParameter[] paramList = new SqlParameter[]
            {
                
                new SqlParameter("@Name", classEntity.Name),
                new SqlParameter("@Email", classEntity.Email),
                new SqlParameter("@DataChange_LastTime", DateTime.Now),
                new SqlParameter("@DataChange_CreateTime", DateTime.Now),
                new SqlParameter("@WebAddress", classEntity.WebAddress),

            };
            SqlParameter outParam = new SqlParameter("@ReferenceID", 0);
            outParam.Direction = ParameterDirection.ReturnValue;
            int output = dbhelper.ExecuteProcWithOutput("BgUser_Insert", paramList, outParam);
            return output;
        }

    }
}
