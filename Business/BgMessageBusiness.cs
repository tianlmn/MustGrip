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
            entity.PRankId++;
            BgMessageData.Insert(entity);
        }


        public static List<BgMessageEntity> GetMessageListByPassageId(int passageId)
        {
            return BgMessageData.GetBgMessageEntityListByPassageId(passageId);
        }

        public static List<BgMessageRankEntity> GetRankByPassageId(int passageId)
        {
            return BgMessageData.GetRankByPassageId(passageId);
        }
    }
}
