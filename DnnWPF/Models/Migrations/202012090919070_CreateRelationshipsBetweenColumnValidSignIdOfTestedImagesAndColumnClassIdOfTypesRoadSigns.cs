namespace DnnWPF.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateRelationshipsBetweenColumnValidSignIdOfTestedImagesAndColumnClassIdOfTypesRoadSigns : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TestedImages", "ValidSignId");
            AddForeignKey("dbo.TestedImages", "ValidSignId", "dbo.TypesRoadSigns", "ClassId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestedImages", "ValidSignId", "dbo.TypesRoadSigns");
            DropIndex("dbo.TestedImages", new[] { "ValidSignId" });
        }
    }
}
