using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=wdq")
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Token> Token { get; set; }
    }
}