using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Common
{
    /// <summary>
    /// ���ݿ����������
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// ������
        /// </summary>
        private string name;

        /// <summary>
        /// ������
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

		/// <summary>
		/// ����
		/// </summary>
		private int size;

		/// <summary>
		/// ����
		/// </summary>
		public int Size
		{
			get { return this.size; }
			set { this.size = value; }
		}


        /// <summary>
        /// ��������
        /// </summary>
        private DbType type;

        /// <summary>
        /// ��������
        /// </summary>
        public DbType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// ����ֵ
        /// </summary>
        private object value;

        /// <summary>
        /// ����ֵ
        /// </summary>
        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// �����Ĵ��ݷ���
        /// </summary>
        private ParameterDirection direction;

        /// <summary>
        /// �����Ĵ��ݷ���
        /// </summary>
        public ParameterDirection Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }

		/// <summary>
		/// ����洢���̲���
		/// </summary>
		/// <param name="name">������</param>
		/// <param name="type">��������</param>
		/// <param name="value">����ֵ</param>
		/// <param name="direction">���ݷ���</param>
		public Parameter(string name, DbType type, object value)
		{
			this.Name = name;
			this.Type = type;
			this.Value = value;			
		}


        /// <summary>
        /// ����洢���̲���
        /// </summary>
        /// <param name="name">������</param>
        /// <param name="type">��������</param>
        /// <param name="value">����ֵ</param>
        /// <param name="direction">���ݷ���</param>
        internal Parameter(string name, DbType type, object value, ParameterDirection direction)
        {
            this.Name = name;
            this.Type = type;
            this.Value = value;
            this.Direction = direction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public static string GetInfo(string Info)
        {
            Database db = DatabaseFactory.CreateDatabase(Info);
            var dbType = db.GetType();
            //var property = dbType.GetProperty("ConnectionString", BindingFlags.NonPublic | BindingFlags.Instance);
            var property = dbType.GetProperty("ConnectionString");
            return property.GetValue(db, null).ToString();

            //return db.ConnectionString;

        }

		/// <summary>
		/// ����洢���̲���
		/// </summary>
		/// <param name="name">������</param>
		/// <param name="type">��������</param>
		/// <param name="value">����ֵ</param>
		/// <param name="direction">���ݷ���</param>
		internal Parameter(string name, DbType type, object value, ParameterDirection direction,int size)
		{
			this.Name = name;
			this.Type = type;
			this.Value = value;
			this.Direction = direction;
			this.Size = size;
		}


       
    }
}
