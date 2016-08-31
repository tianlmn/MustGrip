using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class BgPictureEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int BgPictureId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DataChange_LastTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DataCreate_LastTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Group { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Agree { get; set; }
    }

}
