﻿namespace Test_Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateInterviewTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InterviewModels", "SelectedSkillID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InterviewModels", "SelectedSkillID");
        }
    }
}
