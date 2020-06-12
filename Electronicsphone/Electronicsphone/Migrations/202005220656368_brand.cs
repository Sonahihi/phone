namespace Electronicsphone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class brand : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "MaNhaSX", newName: "Brand_Product_MaNhaSX");
            RenameIndex(table: "dbo.Products", name: "IX_MaNhaSX", newName: "IX_Brand_Product_MaNhaSX");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_Brand_Product_MaNhaSX", newName: "IX_MaNhaSX");
            RenameColumn(table: "dbo.Products", name: "Brand_Product_MaNhaSX", newName: "MaNhaSX");
        }
    }
}
