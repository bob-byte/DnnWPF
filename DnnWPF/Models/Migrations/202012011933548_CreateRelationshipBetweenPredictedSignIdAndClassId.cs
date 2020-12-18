namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRelationshipBetweenPredictedSignIdAndClassId : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TestedImages", "PredictedSignId");
            AddForeignKey("dbo.TestedImages", "PredictedSignId", "dbo.TypesRoadSigns", "ClassId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestedImages", "PredictedSignId", "dbo.TypesRoadSigns");
            DropIndex("dbo.TestedImages", new[] { "PredictedSignId" });
        }
    }
}
