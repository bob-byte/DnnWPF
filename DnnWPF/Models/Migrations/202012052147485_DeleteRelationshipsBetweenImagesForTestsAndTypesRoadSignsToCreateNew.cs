namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRelationshipsBetweenImagesForTestsAndTypesRoadSignsToCreateNew : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ValidRoadSigns", "ClassId", "dbo.TypesRoadSigns");
            DropForeignKey("dbo.ValidRoadSigns", "ValidId", "dbo.ImagesForTests");
            DropIndex("dbo.ValidRoadSigns", new[] { "ClassId" });
            DropIndex("dbo.ValidRoadSigns", new[] { "ValidId" });
            DropTable("dbo.ValidRoadSigns");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ValidRoadSigns",
                c => new
                    {
                        ClassId = c.Byte(nullable: false),
                        ValidId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClassId, t.ValidId });
            
            CreateIndex("dbo.ValidRoadSigns", "ValidId");
            CreateIndex("dbo.ValidRoadSigns", "ClassId");
            AddForeignKey("dbo.ValidRoadSigns", "ValidId", "dbo.ImagesForTests", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ValidRoadSigns", "ClassId", "dbo.TypesRoadSigns", "ClassId", cascadeDelete: true);
        }
    }
}
