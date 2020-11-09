namespace TaskManagementSystemMITT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCommentPropToTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectTasks", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectTasks", "Comment");
        }
    }
}
