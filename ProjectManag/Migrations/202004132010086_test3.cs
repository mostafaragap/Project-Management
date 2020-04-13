namespace ProjectManag.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        developerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.developerID)
                .ForeignKey("dbo.AspNetUsers", t => t.developerID)
                .Index(t => t.developerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Developers", "developerID", "dbo.AspNetUsers");
            DropIndex("dbo.Developers", new[] { "developerID" });
            DropTable("dbo.Developers");
        }
    }
}
