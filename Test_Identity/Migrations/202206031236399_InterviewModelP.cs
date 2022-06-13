namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterviewModelP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InterviewViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Round = c.Int(nullable: false),
                        CandidateId = c.Int(nullable: false),
                        InterviewerId = c.Int(nullable: false),
                        ModeOfInterview = c.String(),
                        Date_Time = c.DateTime(),
                        Comments = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CandModels", t => t.CandidateId, cascadeDelete: true)
                .ForeignKey("dbo.InterviewerModels", t => t.InterviewerId, cascadeDelete: true)
                .Index(t => t.CandidateId)
                .Index(t => t.InterviewerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InterviewViewModels", "InterviewerId", "dbo.InterviewerModels");
            DropForeignKey("dbo.InterviewViewModels", "CandidateId", "dbo.CandModels");
            DropIndex("dbo.InterviewViewModels", new[] { "InterviewerId" });
            DropIndex("dbo.InterviewViewModels", new[] { "CandidateId" });
            DropTable("dbo.InterviewViewModels");
        }
    }
}
