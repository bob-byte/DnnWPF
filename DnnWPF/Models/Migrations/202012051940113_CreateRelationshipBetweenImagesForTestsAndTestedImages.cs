namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRelationshipBetweenImagesForTestsAndTestedImages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TypesAndTestedRoadSigns", "PredictedSignId", "dbo.TestedImages");
            DropPrimaryKey("dbo.TestedImages");
            AlterColumn("dbo.TestedImages", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.TestedImages", "Id");
            CreateIndex("dbo.TestedImages", "Id");
            AddForeignKey("dbo.TestedImages", "Id", "dbo.ImagesForTests", "Id");
            AddForeignKey("dbo.TypesAndTestedRoadSigns", "PredictedSignId", "dbo.TestedImages", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypesAndTestedRoadSigns", "PredictedSignId", "dbo.TestedImages");
            DropForeignKey("dbo.TestedImages", "Id", "dbo.ImagesForTests");
            DropIndex("dbo.TestedImages", new[] { "Id" });
            DropPrimaryKey("dbo.TestedImages");
            AlterColumn("dbo.TestedImages", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TestedImages", "Id");
            AddForeignKey("dbo.TypesAndTestedRoadSigns", "PredictedSignId", "dbo.TestedImages", "Id", cascadeDelete: true);
        }
    }
}
