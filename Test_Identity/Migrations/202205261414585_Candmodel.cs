namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Candmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CandModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Phone_no = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Experience = c.String(nullable: false),
                        Skill = c.String(nullable: false),
                        Current_CTC = c.Single(nullable: false),
                        Expected_CTC = c.Single(nullable: false),
                        Notice_period = c.String(nullable: false),
                        Date_Time = c.DateTime(),
                        Current_address = c.String(nullable: false),
                        Permanent_address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CandModels");
        }
    }
}
