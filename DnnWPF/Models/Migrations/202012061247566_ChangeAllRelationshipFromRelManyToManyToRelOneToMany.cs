namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAllRelationshipFromRelManyToManyToRelOneToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TypesAndTestedRoadSigns", "ClassId", "dbo.TypesRoadSigns");
            DropForeignKey("dbo.TypesAndTestedRoadSigns", "PredictedSignId", "dbo.TestedImages");
            DropForeignKey("dbo.ValidRoadSigns", "ClassId", "dbo.TypesRoadSigns");
            DropForeignKey("dbo.ValidRoadSigns", "Id", "dbo.ImagesForTests");
            DropIndex("dbo.TypesAndTestedRoadSigns", new[] { "ClassId" });
            DropIndex("dbo.TypesAndTestedRoadSigns", new[] { "PredictedSignId" });
            DropIndex("dbo.ValidRoadSigns", new[] { "ClassId" });
            DropIndex("dbo.ValidRoadSigns", new[] { "Id" });
            CreateIndex("dbo.ImagesForTests", "ValidId");
            CreateIndex("dbo.TestedImages", "PredictedSignId");
            AddForeignKey("dbo.TestedImages", "PredictedSignId", "dbo.TypesRoadSigns", "ClassId", cascadeDelete: true);
            AddForeignKey("dbo.ImagesForTests", "ValidId", "dbo.TypesRoadSigns", "ClassId", cascadeDelete: true);
            DropTable("dbo.TypesAndTestedRoadSigns");
            DropTable("dbo.ValidRoadSigns");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ValidRoadSigns",
                c => new
                    {
                        ClassId = c.Byte(nullable: false),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClassId, t.Id });
            
            CreateTable(
                "dbo.TypesAndTestedRoadSigns",
                c => new
                    {
                        ClassId = c.Byte(nullable: false),
                        PredictedSignId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClassId, t.PredictedSignId });
            
            DropForeignKey("dbo.ImagesForTests", "ValidId", "dbo.TypesRoadSigns");
            DropForeignKey("dbo.TestedImages", "PredictedSignId", "dbo.TypesRoadSigns");
            DropIndex("dbo.TestedImages", new[] { "PredictedSignId" });
            DropIndex("dbo.ImagesForTests", new[] { "ValidId" });
            CreateIndex("dbo.ValidRoadSigns", "Id");
            CreateIndex("dbo.ValidRoadSigns", "ClassId");
            CreateIndex("dbo.TypesAndTestedRoadSigns", "PredictedSignId");
            CreateIndex("dbo.TypesAndTestedRoadSigns", "ClassId");
            AddForeignKey("dbo.ValidRoadSigns", "Id", "dbo.ImagesForTests", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ValidRoadSigns", "ClassId", "dbo.TypesRoadSigns", "ClassId", cascadeDelete: true);
            AddForeignKey("dbo.TypesAndTestedRoadSigns", "PredictedSignId", "dbo.TestedImages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TypesAndTestedRoadSigns", "ClassId", "dbo.TypesRoadSigns", "ClassId", cascadeDelete: true);
        }
    }
}
