namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnPrecisionRecognisingToTestedImagesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestedImages", "PrecisionRecognising", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestedImages", "PrecisionRecognising");
        }
    }
}
