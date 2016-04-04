using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Common
{
    /// <summary>
    /// ���ݿ�����
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
        /// ���ݿ�����
        /// </summary>
        /// <param name="dbConnectionString">�����ַ���</param>
        public DataBaseTransaction(string dbConnectionString)
        {
            this.connectionString = dbConnectionString;
            this.isConnectionOpen = false;
            this.isUsingTransaction = false;
            this.connection = null;
            this.transaction = null;
        }

        #region ����

        /// <summary>
        /// Gets ���ݿ������ַ���
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        /// <summary>
        /// Gets ���ݿ�����
        /// </summary>
        public DbConnection Connection
        {
            get
            {
                return this.connection;
            }
        }

        /// <summary>
        /// Gets ���ݿ�����
        /// </summary>
        public DbTransaction Transaction
        {
            get
            {
                return this.transaction;
            }
        }

        /// <summary>
        /// Gets a value indicating whether �Ƿ�����������
        /// </summary>
        public bool IsUsingTransaction
        {
            get
            {
                return this.isUsingTransaction;
            }
        }

        /// <summary>
        /// Gets ���ݿ����
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
        /// ʹ��Ĭ��������뼶��������
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
        /// �ύ����
        /// </summary>
        public void Commit()
        {
            if (this.transaction != null)
            {
                this.transaction.Commit();
            }
        }

        /// <summary>
        /// �ع�����
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
        /// �ͷ���Դ
        /// </summary>
        public void Dispose()
        {
            // ����������������ͷ��������
            if (this.isUsingTransaction && this.transaction != null)
            {
                this.transaction.Dispose();
            }

            // ��������Ѿ��򿪣���ر����Ӳ��ͷ���Դ
            if (this.isConnectionOpen && this.connection != null && this.connection.State != ConnectionState.Closed)
            {
                this.connection.Close();
                this.connection.Dispose();
            }
        }

        #endregion
    }
}
