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
    public class BgMessageEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int BgMessageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PassageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MasterMessageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PRankId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Author { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DataChange_LastTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DataChange_CreateTime { get; set; }
    }

}
