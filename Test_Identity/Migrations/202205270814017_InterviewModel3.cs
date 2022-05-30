namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterviewModel3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.InterviewModels", "ps");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InterviewModels", "ps", c => c.Int(nullable: false));
        }
    }
}
