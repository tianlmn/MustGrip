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
    public class BgUserEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int BgUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WebAddress { get; set; }

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
