namespace Electronicsphone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Masp = c.String(nullable: false, maxLength: 128),
                        Tensp = c.String(nullable: false, maxLength: 255),
                        Gia = c.Int(nullable: false),
                        Soluong = c.Int(nullable: false),
                        HinhAnh = c.String(),
                        Manhinh = c.String(maxLength: 255),
                        HeDieuHanh = c.String(maxLength: 255),
                        CPU = c.String(maxLength: 50),
                        RAM = c.String(maxLength: 50),
                        BoNhoTrong = c.String(maxLength: 50),
                        DungLuongPin = c.String(maxLength: 50),
                        CameraTruoc = c.String(maxLength: 50),
                        CameraSau = c.String(maxLength: 50),
                        HieuXuatSac = c.String(maxLength: 50),
                        NguonVao = c.String(maxLength: 50),
                        NguonRa = c.String(maxLength: 50),
                        TrongLuong = c.String(maxLength: 50),
                        DauRa = c.String(maxLength: 50),
                        DoDaiDay = c.String(maxLength: 50),
                        CardManHinh = c.String(maxLength: 50),
                        CongKetNoi = c.String(maxLength: 50),
                        OCung = c.String(maxLength: 50),
                        KichThuoc = c.String(maxLength: 50),
                        DungLuong = c.String(maxLength: 50),
                        TocDoGhi = c.String(maxLength: 50),
                        TocDoDoc = c.String(maxLength: 50),
                        LoaiOCung = c.String(maxLength: 50),
                        JackCam = c.String(maxLength: 50),
                        KetNoiCungLuc = c.String(maxLength: 50),
                        CongXuat = c.String(maxLength: 50),
                        CachKetNoi = c.String(maxLength: 100),
                        TimeSuDung = c.String(maxLength: 50),
                        TimeSac = c.String(maxLength: 50),
                        MaLoai = c.String(),
                        MaNhaSX = c.String(maxLength: 128),
                        Kind_Product_MaLoaiSP = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Masp)
                .ForeignKey("dbo.Brand_Product", t => t.MaNhaSX)
                .ForeignKey("dbo.Kind_Product", t => t.Kind_Product_MaLoaiSP)
                .Index(t => t.MaNhaSX)
                .Index(t => t.Kind_Product_MaLoaiSP);
            
            CreateTable(
                "dbo.Brand_Product",
                c => new
                    {
                        MaNhaSX = c.String(nullable: false, maxLength: 128),
                        TenNhaSX = c.String(nullable: false, maxLength: 150),
                        HinhNhaSX = c.String(),
                    })
                .PrimaryKey(t => t.MaNhaSX);
            
            CreateTable(
                "dbo.Kind_Product",
                c => new
                    {
                        MaLoaiSP = c.String(nullable: false, maxLength: 128),
                        TenLoaiSP = c.String(nullable: false, maxLength: 200),
                        HinhLoai = c.String(),
                    })
                .PrimaryKey(t => t.MaLoaiSP);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Kind_Product_MaLoaiSP", "dbo.Kind_Product");
            DropForeignKey("dbo.Products", "MaNhaSX", "dbo.Brand_Product");
            DropIndex("dbo.Products", new[] { "Kind_Product_MaLoaiSP" });
            DropIndex("dbo.Products", new[] { "MaNhaSX" });
            DropTable("dbo.Kind_Product");
            DropTable("dbo.Brand_Product");
            DropTable("dbo.Products");
        }
    }
}
