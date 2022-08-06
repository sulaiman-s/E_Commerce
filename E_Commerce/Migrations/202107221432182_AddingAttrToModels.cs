namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAttrToModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CardModels", "Category_Id", "dbo.Categories");
            DropIndex("dbo.CardModels", new[] { "Category_Id" });
            AlterColumn("dbo.Banners", "RelatedName", c => c.String(nullable: false));
            AlterColumn("dbo.Banners", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Banners", "Ban_Img", c => c.String(nullable: false));
            AlterColumn("dbo.CardModels", "ItemName", c => c.String(nullable: false));
            AlterColumn("dbo.CardModels", "ItemImage", c => c.String(nullable: false));
            AlterColumn("dbo.CardModels", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.CardModels", "Price", c => c.String(nullable: false));
            AlterColumn("dbo.CardModels", "Category_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Image", c => c.String(nullable: false));
            CreateIndex("dbo.CardModels", "Category_Id");
            AddForeignKey("dbo.CardModels", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardModels", "Category_Id", "dbo.Categories");
            DropIndex("dbo.CardModels", new[] { "Category_Id" });
            AlterColumn("dbo.Categories", "Image", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.CardModels", "Category_Id", c => c.Int());
            AlterColumn("dbo.CardModels", "Price", c => c.String());
            AlterColumn("dbo.CardModels", "Description", c => c.String());
            AlterColumn("dbo.CardModels", "ItemImage", c => c.String());
            AlterColumn("dbo.CardModels", "ItemName", c => c.String());
            AlterColumn("dbo.Banners", "Ban_Img", c => c.String());
            AlterColumn("dbo.Banners", "Description", c => c.String());
            AlterColumn("dbo.Banners", "RelatedName", c => c.String());
            CreateIndex("dbo.CardModels", "Category_Id");
            AddForeignKey("dbo.CardModels", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
