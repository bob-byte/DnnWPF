namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRelationshipsBetweenColumnPredictedSignIdOfTestedImagesAndColumnClassIdOfTypesRoadSignsToOnDeleteNoAction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestedImages", "PredictedSignId", "dbo.TypesRoadSigns");
            AddForeignKey("dbo.TestedImages", "PredictedSignId", "dbo.TypesRoadSigns", "ClassId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestedImages", "PredictedSignId", "dbo.TypesRoadSigns");
            AddForeignKey("dbo.TestedImages", "PredictedSignId", "dbo.TypesRoadSigns", "ClassId", cascadeDelete: true);
        }
    }
}
