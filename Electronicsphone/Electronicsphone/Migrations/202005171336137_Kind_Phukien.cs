namespace Electronicsphone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kind_Phukien : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kind_PhuKien",
                c => new
                    {
                        Id_Kind = c.String(nullable: false, maxLength: 128),
                        Kind_Name = c.String(nullable: false),
                        Kind_Phukien_Image = c.String(),
                    })
                .PrimaryKey(t => t.Id_Kind);
            
            AddColumn("dbo.Products", "Kind_PhuKien_Id_Kind", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "Kind_PhuKien_Id_Kind");
            AddForeignKey("dbo.Products", "Kind_PhuKien_Id_Kind", "dbo.Kind_PhuKien", "Id_Kind");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Kind_PhuKien_Id_Kind", "dbo.Kind_PhuKien");
            DropIndex("dbo.Products", new[] { "Kind_PhuKien_Id_Kind" });
            DropColumn("dbo.Products", "Kind_PhuKien_Id_Kind");
            DropTable("dbo.Kind_PhuKien");
        }
    }
}
