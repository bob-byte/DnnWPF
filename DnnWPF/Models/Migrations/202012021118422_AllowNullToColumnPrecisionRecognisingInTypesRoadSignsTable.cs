namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNullToColumnPrecisionRecognisingInTypesRoadSignsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TypesRoadSigns", "PrecisionRecognising", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TypesRoadSigns", "PrecisionRecognising", c => c.Double(nullable: false));
        }
    }
}
