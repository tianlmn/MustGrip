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
        public int Author { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DataChange_LastTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DataChange_CreateTime { get; set; }

    }

    public class BgMessageRankEntity
    {
        public int BgMessageId { get; set; }

        public int MaxRankId { get; set; }
    }

    public class BgMessageTreeEntity
    {
        public string Name { get; set; }

        public string CreateTime { get; set; }

        public string Message { get; set; }

        public string WebAddress { get; set; }

        public int MasterMessageId { get; set; }

        public int PRankId { get; set; }

        public int BgMessageId { get; set; }

        public int MaxRankId { get; set; }

        public List<BgMessageTreeEntity> ChildList { get; set; }

    }

}
