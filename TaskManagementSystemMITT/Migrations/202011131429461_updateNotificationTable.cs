namespace TaskManagementSystemMITT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNotificationTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Notifications", "isForManager");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "isForManager", c => c.Boolean(nullable: false));
        }
    }
}
