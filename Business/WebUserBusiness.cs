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
        public static int WriteWebUserEntity(WebUserEntity entity)
        {
            int userId = 0;
            var userList = GetWebUserEntityList(entity);
            if (userList != null && userList.Count > 0)
            {
                entity.UserId = userList[0].UserId;
                WebUserData.Update(entity);
                userId = entity.UserId;
            }
            else
            {
                WebUserData.Insert(entity);
            }
            return userId;
        }

        public static List<WebUserEntity> GetWebUserEntityList(WebUserEntity entity)
        {
            return WebUserData.GetPassageEntityList(entity);
        }

    }
}
