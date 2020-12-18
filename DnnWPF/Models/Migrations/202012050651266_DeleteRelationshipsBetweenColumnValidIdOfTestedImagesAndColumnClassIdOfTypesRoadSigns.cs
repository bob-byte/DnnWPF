namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRelationshipsBetweenColumnValidIdOfTestedImagesAndColumnClassIdOfTypesRoadSigns : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestedImages", "ValidSignId", "dbo.TypesRoadSigns");
            DropIndex("dbo.TestedImages", new[] { "ValidSignId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.TestedImages", "ValidSignId");
            AddForeignKey("dbo.TestedImages", "ValidSignId", "dbo.TypesRoadSigns", "ClassId");
        }
    }
}
