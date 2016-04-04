using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Common
{
    /// <summary>
    /// 数据库操作参数类
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// 参数名
        /// </summary>
        private string name;

        /// <summary>
        /// 参数名
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

		/// <summary>
		/// 长度
		/// </summary>
		private int size;

		/// <summary>
		/// 长度
		/// </summary>
		public int Size
		{
			get { return this.size; }
			set { this.size = value; }
		}


        /// <summary>
        /// 数据类型
        /// </summary>
        private DbType type;

        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// 参数值
        /// </summary>
        private object value;

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// 参数的传递方向
        /// </summary>
        private ParameterDirection direction;

        /// <summary>
        /// 参数的传递方向
        /// </summary>
        public ParameterDirection Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }

		/// <summary>
		/// 构造存储过程参数
		/// </summary>
		/// <param name="name">参数名</param>
		/// <param name="type">数据类型</param>
		/// <param name="value">参数值</param>
		/// <param name="direction">传递方向</param>
		public Parameter(string name, DbType type, object value)
		{
			this.Name = name;
			this.Type = type;
			this.Value = value;			
		}


        /// <summary>
        /// 构造存储过程参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="type">数据类型</param>
        /// <param name="value">参数值</param>
        /// <param name="direction">传递方向</param>
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
		/// 构造存储过程参数
		/// </summary>
		/// <param name="name">参数名</param>
		/// <param name="type">数据类型</param>
		/// <param name="value">参数值</param>
		/// <param name="direction">传递方向</param>
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
