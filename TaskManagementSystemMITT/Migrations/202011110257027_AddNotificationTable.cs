namespace TaskManagementSystemMITT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "ProjectTaskId", c => c.Int());
            AddColumn("dbo.Notifications", "ProjectId", c => c.Int());
            AlterColumn("dbo.Notifications", "Body", c => c.String());
            CreateIndex("dbo.Notifications", "ProjectTaskId");
            CreateIndex("dbo.Notifications", "ProjectId");
            AddForeignKey("dbo.Notifications", "ProjectId", "dbo.Projects", "Id");
            AddForeignKey("dbo.Notifications", "ProjectTaskId", "dbo.ProjectTasks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "ProjectTaskId", "dbo.ProjectTasks");
            DropForeignKey("dbo.Notifications", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Notifications", new[] { "ProjectId" });
            DropIndex("dbo.Notifications", new[] { "ProjectTaskId" });
            AlterColumn("dbo.Notifications", "Body", c => c.Int(nullable: false));
            DropColumn("dbo.Notifications", "ProjectId");
            DropColumn("dbo.Notifications", "ProjectTaskId");
        }
    }
}
