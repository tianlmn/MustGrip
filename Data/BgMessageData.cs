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
        public static void Insert(BgMessageEntity classEntity)
        {
            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            SqlParameter[] paramList = new SqlParameter[]
            {	            new SqlParameter("@BgMessageId", classEntity.BgMessageId),	            new SqlParameter("@PassageId", classEntity.PassageId),	            new SqlParameter("@MasterMessageId", classEntity.MasterMessageId),	            new SqlParameter("@PRankId", classEntity.PRankId),	            new SqlParameter("@Author", classEntity.Author),	            new SqlParameter("@Message", classEntity.Message),	            new SqlParameter("@DataChange_LastTime", classEntity.DataChange_LastTime),	            new SqlParameter("@DataChange_CreateTime", classEntity.DataChange_CreateTime),		        };
            dbhelper.ExecuteNonQuery("spA_BgMessage_i", CommandType.StoredProcedure, paramList);
        }



    }
}