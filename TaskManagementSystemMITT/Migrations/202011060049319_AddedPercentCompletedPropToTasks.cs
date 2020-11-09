namespace TaskManagementSystemMITT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPercentCompletedPropToTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectTasks", "PercentCompleted", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectTasks", "PercentCompleted");
        }
    }
}
