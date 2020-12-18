namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNewRelationshipsBetweenImagesForTestsAndTypesRoadSigns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ValidRoadSigns",
                c => new
                    {
                        ClassId = c.Byte(nullable: false),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClassId, t.Id })
                .ForeignKey("dbo.TypesRoadSigns", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.ImagesForTests", t => t.Id, cascadeDelete: true)
                .Index(t => t.ClassId)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValidRoadSigns", "Id", "dbo.ImagesForTests");
            DropForeignKey("dbo.ValidRoadSigns", "ClassId", "dbo.TypesRoadSigns");
            DropIndex("dbo.ValidRoadSigns", new[] { "Id" });
            DropIndex("dbo.ValidRoadSigns", new[] { "ClassId" });
            DropTable("dbo.ValidRoadSigns");
        }
    }
}
