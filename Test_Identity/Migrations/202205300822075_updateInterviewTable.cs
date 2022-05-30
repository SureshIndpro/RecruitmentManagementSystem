namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateInterviewTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InterviewModels", "Results", c => c.Int(nullable: false));
            DropColumn("dbo.InterviewModels", "Result");
            DropColumn("dbo.InterviewModels", "RecruitmentStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InterviewModels", "RecruitmentStatus", c => c.Int(nullable: false));
            AddColumn("dbo.InterviewModels", "Result", c => c.String());
            DropColumn("dbo.InterviewModels", "Results");
        }
    }
}
