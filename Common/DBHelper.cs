using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Common
{
    /// <summary>
    /// 数据库访问通用类
    /// </summary>
    public class DBHelper
    {

        /// <summary>
        /// 执行SQL获取数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>DataTable数据</returns>
        public static DataTable ExecuteSQL(string sql, string connectionString, IList<Parameter> parameters)
        {
            DataTable dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase(connectionString);
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            dbCommand.CommandTimeout = 120000;
            foreach (Parameter item in parameters)
            {
                db.AddInParameter(dbCommand, item.Name, item.Type, item.Value);
            }

            using (IDataReader idr = db.ExecuteReader(dbCommand))
            {
                dt.Load(idr);
            }

            return dt;
        }

        /// <summary>
        /// 执行SQL获取数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>DataSet数据</returns>
        public static DataSet ExecuteSQLDataSet(string sql, string connectionString, IList<Parameter> parameters)
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase(connectionString);
            using (DbConnection connection = db.CreateConnection())
            {

                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                dbCommand.Connection = connection;
                dbCommand.CommandTimeout = 1800;
                if (parameters != null)
                {
                    foreach (Parameter item in parameters)
                    {
                        db.AddInParameter(dbCommand, item.Name, item.Type, item.Value);
                    }
                }

                ds = db.ExecuteDataSet(dbCommand);

                return ds;
            }
        }


        /// <summary>
        /// 执行SQL获取数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="connectionString">连接字符串</param>		
        /// <returns>DataTable数据</returns>
        public static DataTable ExecuteSQL(string sql, string connectionString)
        {
            return ExecuteSQL(sql, connectionString, new List<Parameter>());
        }

        /// <summary>
        /// 批量执行SPT存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="parameters">参数集合</param>
        /// <returns></returns>
        public static void ExecuteProc(string procName, string connectionString, IList<SqlParameter> allParamsList)
        {
            if (allParamsList.Count == 0) return;

            Database db = DatabaseFactory.CreateDatabase(connectionString);
            SqlConnection connection = (SqlConnection)db.CreateConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {

                foreach (SqlParameter param in allParamsList)
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = procName;
                    command.Transaction = transaction;
                    command.Connection = connection;
                    command.CommandTimeout = 300;
                    command.Parameters.Add(param);

                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;

            }
            finally
            {
                connection.Close();
            }

        }

        /// <summary>
        /// 事务执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>       
        /// <param name="parameters">参数集合</param>
        /// <returns></returns>
        public static Dictionary<string, object> ExecuteProc(string procName, IList<SqlParameter> parameters, DbTransaction transaction)
        {
            if (parameters.Count == 0) return new Dictionary<string, object>();

            DbCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procName;
            command.Transaction = transaction;
            command.Connection = transaction.Connection;
            command.CommandTimeout = 300;
            foreach (SqlParameter param in parameters)
            {

                command.Parameters.Add(param);

            }


            command.ExecuteNonQuery();

            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (SqlParameter item in parameters)
            {
                if (item.Direction == ParameterDirection.Output)
                {
                    dic.Add(item.ParameterName, command.Parameters[item.ParameterName].Value);

                }
            }

            return dic;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="paramsList">参数集合</param>
        /// <returns>DataTable数据</returns>
        public static DataTable ExecuteProc(string procName, string connectionString, IList<Parameter> paramsList)
        {
            DataTable dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase(connectionString);
            DbCommand dbCommand = db.GetStoredProcCommand(procName);
            dbCommand.CommandTimeout = 120000;
            foreach (Parameter item in paramsList)
            {
                if (item.Direction == ParameterDirection.Input)
                {
                    db.AddInParameter(dbCommand, item.Name, item.Type, item.Value);
                }
                if (item.Direction == ParameterDirection.Output)
                {
                    db.AddOutParameter(dbCommand, item.Name, item.Type, item.Size);
                }
            }

            using (IDataReader idr = db.ExecuteReader(dbCommand))
            {
                dt.Load(idr);
            }

            return dt;
        }


        /// <summary>
        /// 批量执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="paramsList">参数集合</param>
        public static void ExecuteProc(string procName, string connectionString, Dictionary<int, IList<Parameter>> allParamsList)
        {
            if (allParamsList.Count == 0) return;

            Database db = DatabaseFactory.CreateDatabase(connectionString);
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();
            try
            {

                foreach (IList<Parameter> paramsList in allParamsList.Values)
                {
                    DbCommand dbCommand = connection.CreateCommand();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = procName;
                    dbCommand.Transaction = transaction;
                    dbCommand.Connection = connection;
                    dbCommand.CommandTimeout = 120000;

                    foreach (Parameter item in paramsList)
                    {
                        if (item.Direction == ParameterDirection.Input)
                        {
                            db.AddInParameter(dbCommand, item.Name, item.Type, item.Value);
                        }
                        if (item.Direction == ParameterDirection.Output)
                        {
                            db.AddOutParameter(dbCommand, item.Name, item.Type, item.Size);
                        }
                    }
                    dbCommand.ExecuteNonQuery();


                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;

            }
            finally
            {
                connection.Close();
            }


        }

        /// <summary>
        /// 需要close 这个Reader
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>一个打开的IDataReader</returns>
        public static IDataReader ExecuteReader(string sql, string connectionString, IList<Parameter> parameters)
        {
            Database db = DatabaseFactory.CreateDatabase(connectionString);
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            foreach (Parameter item in parameters)
            {
                db.AddInParameter(dbCommand, item.Name, item.Type, item.Value);
            }
            dbCommand.CommandTimeout = 60000;
            return db.ExecuteReader(dbCommand);
        }

        public static IDataReader ExecuteReader(string sql, string connectionString)
        {
            return ExecuteReader(sql, connectionString, new List<Parameter>());
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="inParamsList">输入参数集合</param>
        /// <param name="outParamsList">输出参数集合</param>
        /// <returns>Out数据</returns>
        public static Dictionary<string, object> ExecuteProc(string procName, string connectionString, IList<Parameter> inParamsList, IList<Parameter> outParamsList)
        {
            DataTable dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase(connectionString);
            DbCommand dbCommand = db.GetStoredProcCommand(procName);
            dbCommand.CommandTimeout = 120000;
            foreach (Parameter item in inParamsList)
            {
                if (item.Direction == ParameterDirection.Input)
                {
                    db.AddInParameter(dbCommand, item.Name, item.Type, item.Value);
                }
            }

            foreach (Parameter item in outParamsList)
            {
                if (item.Direction == ParameterDirection.Output)
                {
                    db.AddOutParameter(dbCommand, item.Name, item.Type, item.Size);
                }
            }

            db.ExecuteNonQuery(dbCommand);


            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (Parameter item in outParamsList)
            {
                if (item.Direction == ParameterDirection.Output)
                {
                    dic.Add(item.Name, db.GetParameterValue(dbCommand, item.Name));

                }
            }

            return dic;

        }




        /// <summary>
        /// 根据表名，检索字段名，主键字段名，检索条件取得制定字段的值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="selectFieldName">select字段名</param>
        /// <param name="keyFieldName">检索条件字段名</param>
        /// <param name="keyFieldValue">检索条件字段值</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        public static string GetOneFieldValueByCustomSQL(string tableName, string selectFieldName, string keyFieldName, int keyFieldValue, string connectionString)
        {
            string sql = @"SELECT " + selectFieldName + " FROM " + tableName + " WITH(NOLOCK) WHERE " + keyFieldName + "=@ID";

            IList<Parameter> paramList = new List<Parameter>();
            paramList.Add(DBHelper.CreateInParamter("@ID", DbType.Int32, keyFieldValue));

            DataTable dt = DBHelper.ExecuteSQL(sql.ToString(), connectionString, paramList);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][selectFieldName].ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 创建输入参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">参数类型</param>
        /// <param name="value">参数值</param>
        /// <returns>参数对象</returns>
        public static Parameter CreateInParamter(string name, DbType type, object value)
        {
            return new Parameter(name, type, value, ParameterDirection.Input);
        }

        /// <summary>
        /// 创建输出参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">参数类型</param>
        /// <param name="value">参数值</param>
        /// <returns>参数对象</returns>
        public static Parameter CreateOutParamter(string name, DbType type, object value)
        {
            return new Parameter(name, type, value, ParameterDirection.Output);
        }
        /// <summary>
        /// 创建输出参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">参数类型</param>
        /// <param name="value">参数值</param>
        /// <param name="size">大小</param>
        /// <returns>参数对象</returns>

        public static Parameter CreateOutParamter(string name, DbType type, object value, int size)
        {
            return new Parameter(name, type, value, ParameterDirection.Output, size);
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        public static DataTable GetPagerDataManyTableSql(string GetFields, string SortField, int PageSize, int PageIndex, int doCount, int OrderType, string QueryCondition, string MyconnectionString)
        {
            string strSQL = string.Empty;
            string orderTemp = string.Empty;
            string condition = string.Empty;

            if (doCount != 0)
            {
                strSQL = "select count(*) as RecordCount from " + QueryCondition;
            }
            else
            {
                if (OrderType == 0)
                {
                    orderTemp = " ORDER BY " + SortField + "  asc";
                }
                else
                {
                    orderTemp = " ORDER BY " + SortField + "  desc";
                }

                condition = " WHERE T.SerialNumber >" + PageIndex * PageSize + "  and T.SerialNumber <= " + (PageIndex + 1) * PageSize;

                strSQL = "select * from (select " + GetFields + ",ROW_NUMBER() OVER (" + orderTemp + ") AS SerialNumber FROM " + QueryCondition + " ) AS T " + condition;

            }

            DataTable dt = DBHelper.ExecuteSQL(strSQL, MyconnectionString);

            return dt;

        }
        public static DataTable GetPagerDataByOneTableSql(string TblName, string GetFields, string SortField, int PageSize, int PageIndex, int doCount, int OrderType, string QueryCondition, string MyconnectionString)
        {
            string strSQL = string.Empty;
            string strTmp = string.Empty;
            string strOrder = string.Empty;

            if (doCount != 0)
            {
                if (!string.IsNullOrEmpty(QueryCondition))
                {
                    strSQL = "select count(*) as RecordCount from " + TblName + " with(nolock)  where " + QueryCondition;
                }
                else
                {
                    strSQL = "select count(*) as RecordCount from" + TblName + " with(nolock)";
                }
            }
            else
            {
                if (OrderType != 0)
                {
                    strTmp = "<(select min";
                    strOrder = " order by " + SortField + " desc";
                }
                else
                {
                    strTmp = ">(select max";
                    strOrder = " order by " + SortField + " asc";
                }
                if (PageIndex == 1)
                {
                    if (!string.IsNullOrEmpty(QueryCondition))
                    {
                        strSQL = "select top  " + PageSize + " " + GetFields + " from " + TblName + " with(nolock)  where " + QueryCondition + " " + strOrder;
                    }
                    else
                    {
                        strSQL = "select top  " + PageSize + " " + GetFields + " from " + TblName + " with(nolock)  " + strOrder;
                    }
                }
                else if (PageIndex == -1)
                {
                    if (!string.IsNullOrEmpty(QueryCondition))
                    {
                        strSQL = "select " + GetFields + " from " + TblName + " with(nolock)  where " + QueryCondition + " " + strOrder;
                    }
                    else
                    {
                        strSQL = "select " + GetFields + " from " + TblName + " with(nolock)  " + strOrder;
                    }
                }
                else
                {
                    strSQL = "select top " + PageSize + " " + GetFields + " from " + TblName + " with(nolock) where " + SortField + " " + strTmp + "(" + SortField + ") from (select top " + (PageIndex - 1) * PageSize + "  " + SortField + " from " + TblName + " with(nolock) " + strOrder + ") as tblTmp)" + strOrder;
                    if (!string.IsNullOrEmpty(QueryCondition))
                    {
                        strSQL = "select top  " + PageSize + " " + GetFields + " from " + TblName + " with(nolock) where " + SortField + " " + strTmp + "(" + SortField + ") from (select top  " + (PageIndex - 1) * PageSize + " " + SortField + " from " + TblName + " with(nolock) where " + QueryCondition + " " + strOrder + ") as tblTmp ) and " + QueryCondition + " " + strOrder;
                    }
                }

            }
            DataTable dt = DBHelper.ExecuteSQL(strSQL, MyconnectionString);
            return dt;
        }

        /// <summary>
        /// 执行存储过程(带事务，带返回值)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="inParamsList">输入参数集合</param>
        /// <param name="outParamsList">输出参数集合</param>
        /// <returns>Out数据</returns>
        public static Dictionary<string, object> ExecuteProc(string procName, DataBaseTransaction transaction, IList<Parameter> inParamsList, IList<Parameter> outParamsList)
        {
            DataTable dt = new DataTable();
            Database db = transaction.Database;
            DbCommand dbCommand = db.GetStoredProcCommand(procName);
            dbCommand.CommandTimeout = 180;
            foreach (Parameter item in inParamsList)
            {
                if (item.Direction == ParameterDirection.Input)
                {
                    db.AddInParameter(dbCommand, item.Name, item.Type, item.Value);
                }
            }

            foreach (Parameter item in outParamsList)
            {
                if (item.Direction == ParameterDirection.Output)
                {
                    db.AddOutParameter(dbCommand, item.Name, item.Type, item.Size);
                }
            }

            db.ExecuteNonQuery(dbCommand, transaction.Transaction);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (Parameter item in outParamsList)
            {
                if (item.Direction == ParameterDirection.Output)
                {
                    dic.Add(item.Name, db.GetParameterValue(dbCommand, item.Name));
                }
            }

            return dic;
        }

        /// <summary>
        /// 执行存储过程(带事务锁表)
        /// </summary>
        /// <param name="procName">SQL存储过程名称</param>
        /// <param name="transaction">数据库事务对象</param>
        /// <param name="paramsList">参数集合</param>
        /// <returns>DataTable数据</returns>
        public static DataTable ExecuteProc(string procName, DataBaseTransaction transaction, IList<Parameter> paramsList)
        {
            DataTable dt = new DataTable();
            Database db = transaction.Database;
            DbCommand dbCommand = db.GetStoredProcCommand(procName);
            dbCommand.CommandTimeout = 180;
            foreach (Parameter item in paramsList)
            {
                db.AddInParameter(dbCommand, item.Name, item.Type, item.Value);
            }

            using (IDataReader idr = db.ExecuteReader(dbCommand, transaction.Transaction))
            {
                dt.Load(idr);
            }

            return dt;
        }
    }
}
