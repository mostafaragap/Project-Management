namespace ProjectManag.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamLeaders",
                c => new
                    {
                        TeamLeaderId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.TeamLeaderId)
                .ForeignKey("dbo.AspNetUsers", t => t.TeamLeaderId)
                .Index(t => t.TeamLeaderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamLeaders", "TeamLeaderId", "dbo.AspNetUsers");
            DropIndex("dbo.TeamLeaders", new[] { "TeamLeaderId" });
            DropTable("dbo.TeamLeaders");
        }
    }
}
