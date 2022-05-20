namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skills : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Skills", "LastUpdatedOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "LastUpdatedOn", c => c.DateTime(nullable: false));
        }
    }
}
