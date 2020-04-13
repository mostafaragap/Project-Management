namespace ProjectManag.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "UserType2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserType2", c => c.String());
        }
    }
}
