namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courseSecretCodeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tCourseContent", "SecretCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tCourseContent", "SecretCode");
        }
    }
}
