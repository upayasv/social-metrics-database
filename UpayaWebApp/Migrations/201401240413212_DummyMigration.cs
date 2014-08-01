namespace UpayaWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DummyMigration : DbMigration
    {
        public override void Up()
        {
// Simeon            DropColumn("dbo.AspNetUsers", "LastLogin");
        }
        
        public override void Down()
        {
// Simeon            AddColumn("dbo.AspNetUsers", "LastLogin", c => c.DateTime());
        }
    }
}
