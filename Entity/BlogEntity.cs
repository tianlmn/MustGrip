using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PassageEntity
    {
        public int PassageId { get; set; }

        public string Path { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int Type { get; set; }

        public string Content { get; set; }

        public DateTime DataChange_LastTime { get; set; }

        public DateTime DataChange_CreateTime { get; set; }

        public string Summary { get; set; }
    }

}
