using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Entity;

namespace Data
{
    public class BlogData
    {
        #region
//        public static List<PassageEntity> GetEntity(int passageId)
//        {
//            var result = new List<PassageEntity>();
//            string sql = @"SELECT [PassageId]
//      ,[Path]
//      ,[Title]
//      ,[Author]
//      ,[Type]
//      ,[DataChange_LastTime]
//      ,[DataChange_CreateTime]
//  FROM [qds113752475_db].[dbo].[BgPassage] bp(NOLOCK)
//  WHERE bp.PassageId=@PassageId";

//            List<Parameter> paramList = new List<Parameter>();
//            paramList.Add(DBHelper.CreateInParamter("@PassageId",DbType.Int32,passageId));
//            var dt = DBHelper.ExecuteSQL(sql,DBConnectionString.DB1,paramList);
//            if (dt != null)
//            {
//                foreach (DataRow dr in dt.Rows)
//                {
//                    var entity = new PassageEntity();
//                    entity.PassageId = (int)dr["PassageId"];
//                    entity.Path = (string)dr["Path"];
//                    entity.Title = (string)dr["Title"];
//                    entity.Type = (int) dr["Type"];
//                    entity.Author = (string)dr["Author"];
//                    entity.DataChange_LastTime = (DateTime)dr["Datachange_LastTime"];
//                    entity.DataChange_CreateTime = (DateTime)dr["Datachange_CreateTime"];
//                    result.Add(entity);
//                }
//            }

//            return result;
//        }

//        public static void UpdateEntity(PassageEntity entity)
//        {
//            var paramList = new List<Parameter>();
//            paramList.Add(DBHelper.CreateInParamter("@PassageId",DbType.Int32,entity.PassageId));
//            paramList.Add(DBHelper.CreateInParamter("@Path", DbType.String, entity.Path));
//            paramList.Add(DBHelper.CreateInParamter("@Title", DbType.String, entity.Title));
//            //paramList.Add(DBHelper.CreateInParamter("@Author", DbType.String, entity.Author));
//            paramList.Add(DBHelper.CreateInParamter("@Type", DbType.Int32, entity.Type));

//            DBHelper.ExecuteProc("spA_BgPassage_u", DBConnectionString.DB1, paramList);
//        }

//        public static void InsertEntity(PassageEntity entity)
//        {
//            var paramList = new List<Parameter>();
//            paramList.Add(DBHelper.CreateInParamter("@PassageId", DbType.Int32, 0));
//            paramList.Add(DBHelper.CreateInParamter("@Path", DbType.String, entity.Path));
//            paramList.Add(DBHelper.CreateInParamter("@Title", DbType.String, entity.Title));
//            paramList.Add(DBHelper.CreateInParamter("@Author", DbType.String, entity.Author));
//            paramList.Add(DBHelper.CreateInParamter("@Type", DbType.Int32, entity.Type));

//            DBHelper.ExecuteProc("spA_BgPassage_i", DBConnectionString.DB1, paramList);
//        }

        #endregion


        public static List<PassageEntity> GetEntity(int passageId)
        {
            var result = new List<PassageEntity>();
            string sql = @"SELECT [PassageId]
      ,[Path]
      ,[Title]
      ,[Author]
      ,[Type]
      ,[DataChange_LastTime]
      ,[DataChange_CreateTime]
  FROM [qds113752475_db].[dbo].[BgPassage] bp(NOLOCK)
  WHERE bp.PassageId=@PassageId";

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@PassageId", passageId) };
            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            var dt = dbhelper.ExecuteDataTable(sql,CommandType.Text, paramList);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var entity = new PassageEntity();
                    entity.PassageId = (int)dr["PassageId"];
                    entity.Path = (string)dr["Path"];
                    entity.Title = (string)dr["Title"];
                    entity.Type = (int)dr["Type"];
                    entity.Author = (string)dr["Author"];
                    entity.DataChange_LastTime = (DateTime)dr["Datachange_LastTime"];
                    entity.DataChange_CreateTime = (DateTime)dr["Datachange_CreateTime"];
                    result.Add(entity);
                }
            }

            return result;
        }

        public static void UpdateEntity(PassageEntity entity)
        {
            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            SqlParameter[] paramList = new SqlParameter[]
            {
                new SqlParameter("@PassageId", entity.PassageId),
                new SqlParameter("@Path", entity.Path??string.Empty),
                new SqlParameter("@Title", entity.Title??string.Empty),
                new SqlParameter("@Type", entity.Type),
                //new SqlParameter("@Author", entity.Author??string.Empty)
            };
            dbhelper.ExecuteNonQuery("spA_BgPassage_u", CommandType.StoredProcedure, paramList);
        }

        public static void InsertEntity(PassageEntity entity)
        {
            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            SqlParameter[] paramList = new SqlParameter[]
            {
                new SqlParameter("@PassageId", entity.PassageId),
                new SqlParameter("@Path", entity.Path??string.Empty),
                new SqlParameter("@Title", entity.Title??string.Empty),
                new SqlParameter("@Type", entity.Type),
                new SqlParameter("@Author", entity.Author??string.Empty)
            };
            dbhelper.ExecuteNonQuery("spA_BgPassage_i", CommandType.StoredProcedure, paramList);
        }
    }
}
