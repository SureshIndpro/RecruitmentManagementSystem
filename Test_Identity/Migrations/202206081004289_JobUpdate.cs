namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "SelectedSkillID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "SelectedSkillID", c => c.Int(nullable: false));
        }
    }
}
