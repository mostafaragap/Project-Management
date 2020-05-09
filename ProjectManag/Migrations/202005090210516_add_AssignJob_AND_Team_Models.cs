namespace ProjectManag.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_AssignJob_AND_Team_Models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DeveloperId = c.String(),
                        Feedback = c.String(),
                        TeamLeaderID = c.String(),
                        AssignId = c.Int(nullable: false),
                        JDSkills = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AssignJobs", t => t.AssignId, cascadeDelete: true)
                .Index(t => t.AssignId);
            
            CreateTable(
                "dbo.AssignJobs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        State = c.String(),
                        Customer_Notes = c.String(),
                        projectId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        TeamleaderId = c.String(maxLength: 128),
                        Price = c.Double(nullable: false),
                        TLState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.projects", t => t.projectId, cascadeDelete: true)
                .ForeignKey("dbo.TeamLeaders", t => t.TeamleaderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.projectId)
                .Index(t => t.UserId)
                .Index(t => t.TeamleaderId);
            
            AddColumn("dbo.Developers", "team_id", c => c.Int());
            CreateIndex("dbo.Developers", "team_id");
            AddForeignKey("dbo.Developers", "team_id", "dbo.Teams", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Developers", "team_id", "dbo.Teams");
            DropForeignKey("dbo.Teams", "AssignId", "dbo.AssignJobs");
            DropForeignKey("dbo.AssignJobs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignJobs", "TeamleaderId", "dbo.TeamLeaders");
            DropForeignKey("dbo.AssignJobs", "projectId", "dbo.projects");
            DropIndex("dbo.AssignJobs", new[] { "TeamleaderId" });
            DropIndex("dbo.AssignJobs", new[] { "UserId" });
            DropIndex("dbo.AssignJobs", new[] { "projectId" });
            DropIndex("dbo.Teams", new[] { "AssignId" });
            DropIndex("dbo.Developers", new[] { "team_id" });
            DropColumn("dbo.Developers", "team_id");
            DropTable("dbo.AssignJobs");
            DropTable("dbo.Teams");
        }
    }
}
