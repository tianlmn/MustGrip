using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class WebUserEntity
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string WebAddress { get; set; }

        public DateTime DataChange_LastTime { get; set; }

        public DateTime DataChange_CreateTime { get; set; }

    }
}
