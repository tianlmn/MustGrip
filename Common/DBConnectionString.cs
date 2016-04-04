using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DBConnectionString
    {
        public static string WebUserDB_Select 
        {
            get { return ConfigurationManager.ConnectionStrings["WebUserDB"].ToString(); }
        }

        public static string WebUserDB_Insert
        {
            get { return ConfigurationManager.ConnectionStrings["WebUserDB"].ToString(); }
        }
    }
}
