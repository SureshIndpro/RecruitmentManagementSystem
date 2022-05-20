namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobupdate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobName = c.String(),
                        Experience = c.Int(nullable: false),
                        JobDescription = c.String(),
                        SelectedSkillID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Skills", "LastUpdatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Skills", "LastUpdatedOn");
            DropTable("dbo.Jobs");
        }
    }
}
