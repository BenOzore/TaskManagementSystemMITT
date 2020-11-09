namespace TaskManagementSystemMITT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPriority : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Priority", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Name", c => c.String());
            DropColumn("dbo.Projects", "Priority");
        }
    }
}
