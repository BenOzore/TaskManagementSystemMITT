namespace TaskManagementSystemMITT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editseedmethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Priority");
        }
    }
}
