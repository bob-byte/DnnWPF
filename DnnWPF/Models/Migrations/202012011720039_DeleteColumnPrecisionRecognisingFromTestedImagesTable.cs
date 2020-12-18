namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteColumnPrecisionRecognisingFromTestedImagesTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TestedImages", "PrecisionRecognising");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestedImages", "PrecisionRecognising", c => c.Double(nullable: false));
        }
    }
}
