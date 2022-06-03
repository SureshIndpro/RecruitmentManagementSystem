namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resultInter1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InterviewModels", "Results", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InterviewModels", "Results", c => c.String());
        }
    }
}
