using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Common;
using Entity;

namespace Data
{
    public class BgPictureData
    {
        /// <summary>
        /// 根据搜索条件获取符合要求的图片表实体类集合
        /// </summary>
        /// <param name="conditionEntity">图片表搜索条件实体类</param>
        /// <returns>符合条件的图片表实体类集合</returns>
        public static List<BgPictureEntity> GetDataByEntity(BgPictureEntity conditionEntity)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT ,BgPictureId,Url,Rank,Name,Size,DataChange_LastTime,DataCreate_LastTime,Description,Group,AgreeFROM BgPicture(nolock) ");

            StringBuilder sqlWhere = new StringBuilder();
            if (conditionEntity.BgPictureId > 0)
            {
                sqlWhere.Append("and BgPictureId = @BgPictureId ");
                paramList.Add(new SqlParameter("BgPictureId", conditionEntity.BgPictureId));
            }

            if (conditionEntity.Rank > 0)
            {
                sqlWhere.Append("and Rank = @Rank ");
                paramList.Add(new SqlParameter("Rank", conditionEntity.Rank));
            }

            if (conditionEntity.Group > 0)
            {
                sqlWhere.Append("and Group = @Group ");
                paramList.Add(new SqlParameter("Group", conditionEntity.Group));
            }


            if (sqlWhere.Length > 0)
            {
                sql.Append(" where ");
                sql.Append(sqlWhere.ToString().Trim().TrimStart("and".ToCharArray()));
            }

            List<BgPictureEntity> entityList = new List<BgPictureEntity>();
            var dbhelper = new MDBHelper(DBConnectionString.DB1);
            DataTable dt = dbhelper.ExecuteSql(sql.ToString(), paramList);
            foreach (DataRow idr in dt.Rows)
            {

                BgPictureEntity classEntity = new BgPictureEntity();
                classEntity.BgPictureId = CommonFunc.ConvertObjectToInt32(idr["BgPictureId"]);
                classEntity.Url = CommonFunc.ConvertObjectToString(idr["Url"]);
                classEntity.Rank = CommonFunc.ConvertObjectToInt32(idr["Rank"]);
                classEntity.Name = CommonFunc.ConvertObjectToString(idr["Name"]);
                classEntity.Size = CommonFunc.ConvertObjectToString(idr["Size"]);
                classEntity.DataChange_LastTime = CommonFunc.ConvertDateTime(idr["DataChange_LastTime"]);
                classEntity.DataCreate_LastTime = CommonFunc.ConvertDateTime(idr["DataCreate_LastTime"]);
                classEntity.Description = CommonFunc.ConvertObjectToString(idr["Description"]);
                classEntity.Group = CommonFunc.ConvertObjectToInt32(idr["Group"]);
                classEntity.Agree = CommonFunc.ConvertObjectToInt32(idr["Agree"]);
                entityList.Add(classEntity);
            }

            return entityList;
        }

    }
}
