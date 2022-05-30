namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterviewModel4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InterviewModels", "RecruitmentStatus", c => c.Int(nullable: false));
            DropColumn("dbo.InterviewModels", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InterviewModels", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.InterviewModels", "RecruitmentStatus");
        }
    }
}
