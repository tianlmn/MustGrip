using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entity;

namespace Business
{
    public class BgUserBusiness
    {
        public static int WriteBgUserEntity(BgUserEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Email))
            {
                return 0;
            }

            int bgUserId = 0;
            var userList = BgUserData.GetBgUserEntityList(entity);
            if (userList != null && userList.Count > 0)
            {
                entity.BgUserId = userList[0].BgUserId;
                BgUserData.Update(entity);
                bgUserId = entity.BgUserId;
            }
            else
            {
                bgUserId = BgUserData.Insert(entity);
            }
            return bgUserId;
        }

        public static List<BgUserEntity> GetBgUserEntityList(BgUserEntity entity)
        {
            return BgUserData.GetBgUserEntityList(entity);
        }

    }
}
