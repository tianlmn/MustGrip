using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entity;

namespace Business
{
    public class BlogBusiness
    {
        public static int SavePassage(PassageEntity entity)
        {
            var pl = BlogData.GetEntity(entity.PassageId);
            if (pl != null && pl.Count > 0)
            {
                entity.PassageId = pl[0].PassageId;
                BlogData.UpdateEntity(entity);
            }
            else
            {
                BlogData.InsertEntity(entity);
            }
            return 0;
        }
    }
}
