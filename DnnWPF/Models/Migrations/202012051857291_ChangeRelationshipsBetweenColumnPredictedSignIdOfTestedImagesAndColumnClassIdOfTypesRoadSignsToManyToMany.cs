namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRelationshipsBetweenColumnPredictedSignIdOfTestedImagesAndColumnClassIdOfTypesRoadSignsToManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestedImages", "PredictedSignId", "dbo.TypesRoadSigns");
            DropIndex("dbo.TestedImages", new[] { "PredictedSignId" });
            CreateTable(
                "dbo.TypesAndTestedRoadSigns",
                c => new
                    {
                        ClassId = c.Byte(nullable: false),
                        PredictedSignId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClassId, t.PredictedSignId })
                .ForeignKey("dbo.TypesRoadSigns", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.TestedImages", t => t.PredictedSignId, cascadeDelete: true)
                .Index(t => t.ClassId)
                .Index(t => t.PredictedSignId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypesAndTestedRoadSigns", "PredictedSignId", "dbo.TestedImages");
            DropForeignKey("dbo.TypesAndTestedRoadSigns", "ClassId", "dbo.TypesRoadSigns");
            DropIndex("dbo.TypesAndTestedRoadSigns", new[] { "PredictedSignId" });
            DropIndex("dbo.TypesAndTestedRoadSigns", new[] { "ClassId" });
            DropTable("dbo.TypesAndTestedRoadSigns");
            CreateIndex("dbo.TestedImages", "PredictedSignId");
            AddForeignKey("dbo.TestedImages", "PredictedSignId", "dbo.TypesRoadSigns", "ClassId");
        }
    }
}
