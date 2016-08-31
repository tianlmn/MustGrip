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
    public class BgMessageData
    {
        /// <summary>
        /// 添加一条回复信息表数据
        /// </summary>
        /// <param name="classEntity">回复信息表实体类</param>
        public static int Insert(BgMessageEntity classEntity)
        {
            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            SqlParameter[] paramList = new SqlParameter[]
            {
	            
	            new SqlParameter("@PassageId", classEntity.PassageId),
	            new SqlParameter("@MasterMessageId", classEntity.MasterMessageId),
	            new SqlParameter("@PRankId", classEntity.PRankId),
	            new SqlParameter("@Author", classEntity.Author),
	            new SqlParameter("@Message", classEntity.Message),
	            new SqlParameter("@DataChange_LastTime", DateTime.Now),
	            new SqlParameter("@DataChange_CreateTime", DateTime.Now),
	        };
            var paramOut = new SqlParameter("@@ReferenceID", 0);
            paramOut.Direction = ParameterDirection.ReturnValue;

            return dbhelper.ExecuteProcWithOutput("BgMessage_Insert", paramList, paramOut);
        }

        public static List<BgMessageEntity> GetBgMessageEntityListByPassageId(int passageId)
        {
            var result = new List<BgMessageEntity>();
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT [BgMessageId]
      ,[PassageId]
      ,[MasterMessageId]
      ,[PRankId]
      ,[Message]
      ,[DataChange_LastTime]
      ,[DataChange_CreateTime]
      ,[Author]
  FROM [qds167160447_db].[dbo].[BgMessage](NOLOCK)
  WHERE PassageId=@PassageId ");

            var paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@PassageId", passageId));

            

            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            var dt = dbhelper.ExecuteSql(sql.ToString(), paramList);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var entity = new BgMessageEntity();
                    entity.BgMessageId = CommonFunc.ConvertObjectToInt32(dr["BgMessageId"]);
                    entity.PassageId = CommonFunc.ConvertObjectToInt32(dr["PassageId"]);
                    entity.MasterMessageId = CommonFunc.ConvertObjectToInt32(dr["MasterMessageId"]);
                    entity.PRankId = CommonFunc.ConvertObjectToInt32(dr["PRankId"]);
                    entity.Message = CommonFunc.ConvertObjectToString(dr["Message"]);
                    entity.DataChange_LastTime = CommonFunc.ConvertDateTime(dr["DataChange_LastTime"]);
                    entity.DataChange_CreateTime = CommonFunc.ConvertDateTime(dr["DataChange_CreateTime"]);
                    entity.Author = CommonFunc.ConvertObjectToInt32(dr["Author"]);
                    result.Add(entity);
                }
            }

            return result;
        }

        public static List<BgMessageRankEntity> GetRankByPassageId(int passageId)
        {
            List<BgMessageRankEntity> result = new List<BgMessageRankEntity>();
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT bm1.BgMessageId,ISNULL(MAX(bm2.PRankId),0) AS maxRankId 
  FROM dbo.BgMessage bm1(NOLOCK)
  LEFT JOIN dbo.BgMessage bm2(NOLOCK) ON bm1.BgMessageId=bm2.MasterMessageId
  WHERE bm1.PassageId=@PassageId
  GROUP BY bm1.BgMessageId ");
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("PassageId", passageId));
            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            DataTable dt = dbhelper.ExecuteSql(sql.ToString(), paramList);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BgMessageRankEntity entity = new BgMessageRankEntity();
                    entity.BgMessageId = CommonFunc.ConvertObjectToInt32(dr["BgMessageId"]);
                    entity.MaxRankId = CommonFunc.ConvertObjectToInt32(dr["maxRankId"]);
                    result.Add(entity);
                }
            }
            return result;
        }

    }
}