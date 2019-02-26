namespace Warehouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Article : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WareHouseArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Availability = c.Double(nullable: false),
                        Unit = c.String(),
                        LineItemId = c.Int(nullable: false),
                        LineItemAmount = c.Double(nullable: false),
                        LineItemUnit = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WareHouseArticles");
        }
    }
}
