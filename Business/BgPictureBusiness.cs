using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entity;

namespace Business
{
    public class BgPictureBusiness
    {
        public static List<BgPictureEntity> GetPictureListByGroup(int group)
        {
            var condition = new BgPictureEntity();
            condition.Group = group;

            var pictureList = BgPictureData.GetDataByEntity(condition);

            return pictureList;

        }
    }
}
