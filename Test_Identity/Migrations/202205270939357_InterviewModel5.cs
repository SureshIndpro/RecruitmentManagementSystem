namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterviewModel5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InterviewModels", "Date_Time", c => c.DateTime());
            DropColumn("dbo.InterviewModels", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InterviewModels", "DateTime", c => c.DateTime());
            DropColumn("dbo.InterviewModels", "Date_Time");
        }
    }
}
