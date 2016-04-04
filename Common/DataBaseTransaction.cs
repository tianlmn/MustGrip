using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Common
{
    /// <summary>
    /// 数据库事务
    /// </summary>
    public class DataBaseTransaction : IDisposable
    {
        private string connectionString;
        private DbConnection connection;
        private DbTransaction transaction;
        private bool isConnectionOpen;
        private bool isUsingTransaction;
        private Database database;

        /// <summary>
        /// 数据库事务
        /// </summary>
        /// <param name="dbConnectionString">连接字符串</param>
        public DataBaseTransaction(string dbConnectionString)
        {
            this.connectionString = dbConnectionString;
            this.isConnectionOpen = false;
            this.isUsingTransaction = false;
            this.connection = null;
            this.transaction = null;
        }

        #region 属性

        /// <summary>
        /// Gets 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        /// <summary>
        /// Gets 数据库链接
        /// </summary>
        public DbConnection Connection
        {
            get
            {
                return this.connection;
            }
        }

        /// <summary>
        /// Gets 数据库事务
        /// </summary>
        public DbTransaction Transaction
        {
            get
            {
                return this.transaction;
            }
        }

        /// <summary>
        /// Gets a value indicating whether 是否正在事务中
        /// </summary>
        public bool IsUsingTransaction
        {
            get
            {
                return this.isUsingTransaction;
            }
        }

        /// <summary>
        /// Gets 数据库对象
        /// </summary>
        public Database Database
        {
            get
            {
                return this.database;
            }
        }

        #endregion

        /// <summary>
        /// 使用默认事务隔离级别开启事务
        /// </summary>
        public void BeginTransaction()
        {
            if (this.connection == null)
            {
                this.database = DatabaseFactory.CreateDatabase(this.connectionString);
                this.connection = this.database.CreateConnection();
            }

            if (this.connection.State != ConnectionState.Open)
            {
                this.connection.Open();
            }

            this.isConnectionOpen = true;

            if (this.transaction == null)
            {
                this.transaction = this.connection.BeginTransaction();
            }

            this.isUsingTransaction = true;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if (this.transaction != null)
            {
                this.transaction.Commit();
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
            }
        }

        #region IDisposable Members

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // 如果事务开启过，则释放事务对象
            if (this.isUsingTransaction && this.transaction != null)
            {
                this.transaction.Dispose();
            }

            // 如果连接已经打开，则关闭连接并释放资源
            if (this.isConnectionOpen && this.connection != null && this.connection.State != ConnectionState.Closed)
            {
                this.connection.Close();
                this.connection.Dispose();
            }
        }

        #endregion
    }
}
