namespace DnnWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteColumnDescribeRoadSignFromTypesRoadSignsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TypesRoadSigns", "DescribeRoadSign");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TypesRoadSigns", "DescribeRoadSign", c => c.String());
        }
    }
}
