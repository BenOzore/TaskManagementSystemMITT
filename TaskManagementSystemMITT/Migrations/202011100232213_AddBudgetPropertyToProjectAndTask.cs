namespace TaskManagementSystemMITT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBudgetPropertyToProjectAndTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Budget", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectTasks", "Budget", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectTasks", "Budget");
            DropColumn("dbo.Projects", "Budget");
        }
    }
}
