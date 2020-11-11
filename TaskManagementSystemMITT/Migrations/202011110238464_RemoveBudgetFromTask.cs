namespace TaskManagementSystemMITT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBudgetFromTask : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProjectTasks", "Budget");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProjectTasks", "Budget", c => c.Int(nullable: false));
        }
    }
}
