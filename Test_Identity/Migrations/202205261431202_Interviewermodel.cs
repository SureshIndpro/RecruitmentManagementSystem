namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Interviewermodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InterviewerModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Designation = c.String(nullable: false),
                        Timing = c.DateTime(nullable: true),
                        SelectedSkillID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InterviewerModels");
        }
    }
}
