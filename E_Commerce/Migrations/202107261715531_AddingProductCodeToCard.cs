namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingProductCodeToCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CardModels", "ProductCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CardModels", "ProductCode");
        }
    }
}
