namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_add_createtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CreateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CreateTime");
        }
    }
}
