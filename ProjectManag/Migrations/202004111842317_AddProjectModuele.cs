namespace ProjectManag.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectModuele : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.projects",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        decsription = c.String(nullable: false),
                        NoOfHours = c.Double(nullable: false),
                        skill_need = c.String(nullable: false),
                        due_deta = c.DateTime(nullable: false),
                        creation_date = c.DateTime(nullable: false),
                        isopen = c.Int(nullable: false),
                        customerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.customerId)
                .Index(t => t.customerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.projects", "customerId", "dbo.AspNetUsers");
            DropIndex("dbo.projects", new[] { "customerId" });
            DropTable("dbo.projects");
        }
    }
}
