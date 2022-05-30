namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterviewModel6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InterviewModels", "Comments", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InterviewModels", "Comments", c => c.String());
        }
    }
}
