using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class User
    {

        public int Id { get; set; }
        [DisplayName("用户名"), Required]
        public string UserName { get; set; }

        [DisplayName("密码"), Required(ErrorMessage = "密码不能为空"), MinLength(6)]
        public string Pwd { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}