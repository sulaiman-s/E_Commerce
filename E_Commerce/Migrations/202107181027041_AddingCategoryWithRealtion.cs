namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCategoryWithRealtion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CardModels", "Category_Id", c => c.Int());
            CreateIndex("dbo.CardModels", "Category_Id");
            AddForeignKey("dbo.CardModels", "Category_Id", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardModels", "Category_Id", "dbo.Categories");
            DropIndex("dbo.CardModels", new[] { "Category_Id" });
            DropColumn("dbo.CardModels", "Category_Id");
            DropTable("dbo.Categories");
        }
    }
}
