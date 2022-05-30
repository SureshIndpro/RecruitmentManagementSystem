namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterviewModel7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InterviewModels", "isUpdateEnabled", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InterviewModels", "isUpdateEnabled");
        }
    }
}
