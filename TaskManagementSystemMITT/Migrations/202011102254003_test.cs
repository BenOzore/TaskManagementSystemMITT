namespace TaskManagementSystemMITT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notifications", "Body", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notifications", "Body", c => c.Int(nullable: false));
        }
    }
}
