using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime ExpTime { get; set; }
        public bool IsEnable { get; set; }
    }
}