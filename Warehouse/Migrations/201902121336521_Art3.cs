namespace Warehouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Art3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WareHouseArticles", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WareHouseArticles", "Note");
        }
    }
}
