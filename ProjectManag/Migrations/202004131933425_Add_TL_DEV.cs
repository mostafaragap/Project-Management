namespace ProjectManag.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TL_DEV : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Developers", "developerID", "dbo.AspNetUsers");
            DropIndex("dbo.Developers", new[] { "developerID" });
            DropTable("dbo.Developers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        developerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.developerID);
            
            CreateIndex("dbo.Developers", "developerID");
            AddForeignKey("dbo.Developers", "developerID", "dbo.AspNetUsers", "Id");
        }
    }
}
