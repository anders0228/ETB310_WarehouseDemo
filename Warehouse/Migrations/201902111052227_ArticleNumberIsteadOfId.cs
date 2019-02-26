namespace Warehouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleNumberIsteadOfId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WareHouseArticles", "ArticleNr", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WareHouseArticles", "ArticleNr");
        }
    }
}
