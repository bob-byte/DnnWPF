namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnCountTestToTypesRoadSignsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypesRoadSigns", "CountTest", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypesRoadSigns", "CountTest");
        }
    }
}
