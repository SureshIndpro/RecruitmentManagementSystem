namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterviewModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InterviewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Round = c.String(),
                        CandidateId = c.String(),
                        InterviewerId = c.String(),
                        ModeOfInterview = c.String(),
                        DateTime = c.DateTime(nullable: true),
                        Comments = c.String(),
                        Result = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InterviewModels");
        }
    }
}
