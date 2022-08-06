namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingyCategoryAttributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Image", c => c.String(nullable: false));
        }
    }
}
