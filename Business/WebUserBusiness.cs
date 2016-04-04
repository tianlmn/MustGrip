using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entity;

namespace Business
{
    public class WebUserBusiness
    {
        public static List<WebUserEntity> GetWebUserEntityList()
        {
            return WebUserData.GetWebUserDataList();
        }

    }
}
