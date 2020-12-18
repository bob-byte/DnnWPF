namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestedImages",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PathToTestedImage = c.String(),
                        ValidSignId = c.Byte(nullable: false),
                        PredictedSignId = c.Byte(nullable: false),
                        PrecisionRecognising = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypesRoadSigns",
                c => new
                    {
                        ClassId = c.Byte(nullable: false),
                        Name = c.String(),
                        PrecisionRecognising = c.Double(nullable: false),
                        PathToTemplateImage = c.String(),
                        DescribeRoadSign = c.String(),
                    })
                .PrimaryKey(t => t.ClassId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TypesRoadSigns");
            DropTable("dbo.TestedImages");
        }
    }
}
