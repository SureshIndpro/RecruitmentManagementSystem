namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterviewModel2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InterviewModels", "DateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InterviewModels", "DateTime", c => c.DateTime(nullable: true));
        }
    }
}
