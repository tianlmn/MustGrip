using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entity;

namespace Business
{
    public class BgMessageBusiness
    {
        public static void PostMessage(BgMessageEntity entity)
        {
            BgMessageData.Insert(entity);
        }


        public static List<BgMessageEntity> GetMessageList(BgMessageEntity entity)
        {
            return BgMessageData.GetBgMessageEntityListByPassageId(entity.PassageId);
        }
    }
}
