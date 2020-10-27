namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelClassesUpdatedV2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tCourseContent", "CourseTitle", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tCourseContent", "CourseTitle", c => c.Int(nullable: false));
        }
    }
}
