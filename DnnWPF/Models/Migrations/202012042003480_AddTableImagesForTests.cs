namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableImagesForTests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImagesForTests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValidId = c.Byte(nullable: false),
                        PathToImage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImagesForTests");
        }
    }
}
