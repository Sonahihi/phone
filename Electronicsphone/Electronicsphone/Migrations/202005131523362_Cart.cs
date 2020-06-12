namespace Electronicsphone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IMaSP = c.String(),
                        ITenSP = c.String(),
                        IImage = c.String(),
                        DonGia = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatHangId = c.Int(nullable: false),
                        MonAnId = c.String(maxLength: 128),
                        GiaHienTai = c.Double(nullable: false),
                        Soluong = c.Int(nullable: false),
                        GiamGia = c.Double(nullable: false),
                        Order_Id = c.Int(),
                        Product_Masp = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.Products", t => t.Product_Masp)
                .Index(t => t.Order_Id)
                .Index(t => t.Product_Masp);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NgayDatHang = c.DateTime(nullable: false),
                        TongTien = c.Int(nullable: false),
                        NgayNhanHang = c.DateTime(nullable: false),
                        TenKhachHang = c.String(nullable: false),
                        SDT = c.String(nullable: false),
                        DiaChi = c.String(),
                        MoTa = c.String(),
                        CustomerId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Product_Masp", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Product_Masp" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Carts");
        }
    }
}
