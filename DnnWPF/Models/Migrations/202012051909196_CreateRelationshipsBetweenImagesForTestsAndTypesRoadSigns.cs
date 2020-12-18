namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRelationshipsBetweenImagesForTestsAndTypesRoadSigns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ValidRoadSigns",
                c => new
                    {
                        ClassId = c.Byte(nullable: false),
                        ValidId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClassId, t.ValidId })
                .ForeignKey("dbo.TypesRoadSigns", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.ImagesForTests", t => t.ValidId, cascadeDelete: true)
                .Index(t => t.ClassId)
                .Index(t => t.ValidId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValidRoadSigns", "ValidId", "dbo.ImagesForTests");
            DropForeignKey("dbo.ValidRoadSigns", "ClassId", "dbo.TypesRoadSigns");
            DropIndex("dbo.ValidRoadSigns", new[] { "ValidId" });
            DropIndex("dbo.ValidRoadSigns", new[] { "ClassId" });
            DropTable("dbo.ValidRoadSigns");
        }
    }
}
