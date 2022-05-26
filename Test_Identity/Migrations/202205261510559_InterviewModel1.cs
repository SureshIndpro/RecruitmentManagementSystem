namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterviewModel1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "JobName", c => c.String(nullable: false));
            AlterColumn("dbo.Jobs", "JobDescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "JobDescription", c => c.String());
            AlterColumn("dbo.Jobs", "JobName", c => c.String());
        }
    }
}
