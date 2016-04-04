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
        public static string DB1 
        {
            get { return ConfigurationManager.ConnectionStrings["db1"].ToString(); }
        }

    }
}
