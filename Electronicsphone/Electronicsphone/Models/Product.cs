using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Electronicsphone.Models
{
	public class Product
	{
		[Key]
		[Display(Name ="Mã sản phẩm")]
		public string Masp { get; set; }
		[Required]
		[StringLength(255)]
		[Display(Name ="Tên sản phẩm")]
		public string Tensp { get; set; }
		[Display(Name = "Giá sản phẩm")]
		public int Gia { get; set; }
		[Display(Name = "Số lượng sản phẩm")]
		public int Soluong { get; set; }
		[Display(Name = "Hình ảnh sản phẩm")]
		public string HinhAnh { get; set; }
		[Display(Name = "Màn hình")]
		[StringLength(255)]
		public string Manhinh { get; set; }
		[Display(Name = "Hệ điều hành")]
		[StringLength(255)]
		public string HeDieuHanh { get; set; }
		[Display(Name = "CPU")]
		[StringLength(50)]
		public string CPU { get; set; }
		[Display(Name = "RAM")]
		[StringLength(50)]
		public string RAM { get; set; }
		[Display(Name = "Bộ nhớ trong")]
		[StringLength(50)]
		public string BoNhoTrong { get; set; }
		[Display(Name = "Dung lượng pin")]
		[StringLength(50)]
		public string DungLuongPin { get; set; }
		[Display(Name = "Camera trước")]
		[StringLength(50)]
		public string CameraTruoc { get; set; }
		[Display(Name = "Camera sau")]
		[StringLength(50)]
		public string CameraSau { get; set; }
		[Display(Name = "Hiệu xuất sạc")]
		[StringLength(50)]
		public string HieuXuatSac { get; set; }
		[Display(Name = "Nguồn vào")]
		[StringLength(50)]
		public string NguonVao { get; set; }
		[Display(Name = "Nguồn ra")]
		[StringLength(50)]
		public string NguonRa { get; set; }
		[Display(Name = "Trọng lượng")]
		[StringLength(50)]
		public string TrongLuong { get; set; }
		[Display(Name = "Đầu ra")]
		[StringLength(50)]
		public string DauRa { get; set; }
		[Display(Name = "Độ dài dây")]
		[StringLength(50)]
		public string DoDaiDay { get; set; }
		[Display(Name = "Card màn hình")]
		[StringLength(50)]
		public string CardManHinh { get; set; }
		[Display(Name = "Cổng kết nối")]
		[StringLength(50)]
		public string CongKetNoi { get; set; }
		[Display(Name = "Ổ cứng")]
		[StringLength(50)]
		public string OCung { get; set; }
		[Display(Name = "Kích thước")]
		[StringLength(50)]
		public string KichThuoc { get; set; }
		[Display(Name = "Dung lượng")]
		[StringLength(50)]
		public string DungLuong { get; set; }
		[Display(Name = "Tốc độ ghi")]
		[StringLength(50)]
		public string TocDoGhi { get; set; }
		[Display(Name = "Tốc độ đọc")]
		[StringLength(50)]
		public string TocDoDoc { get; set; }
		[Display(Name = "Loại ổ cứng")]
		[StringLength(50)]
		public string LoaiOCung { get; set; }
		[Display(Name = "Jack cắm tay nghe")]
		[StringLength(50)]
		public string JackCam { get; set; }
		[Display(Name = "Kết nối cùng lúc")]
		[StringLength(50)]
		public string KetNoiCungLuc { get; set; }
		[Display(Name = "Cổng xuất")]
		[StringLength(50)]
		public string CongXuat { get; set; }
		[Display(Name = "Cách kết nối")]
		[StringLength(100)]
		public string CachKetNoi { get; set; }
		[Display(Name = "Thời gian sử dụng")]
		[StringLength(50)]
		public string TimeSuDung { get; set; }
		[Display(Name = "Thời gian sạc")]
		[StringLength(50)]
		public string TimeSac { get; set; }
		[Display(Name = "Loai sản phẩm")]
		public string MaLoai { get; set; }
		public Kind_Product Kind_Product { get; set; }
		public Brand_Product Brand_Product { get; set; }
		public Kind_PhuKien Kind_PhuKien { get; set; }
		public virtual IEnumerable<Kind_Product> Kind_Products { get; set; }
		public virtual IEnumerable<Brand_Product> Brand_Products { get; set; }
		public virtual IEnumerable<Kind_PhuKien> Kind_PhuKiens { get; set; }
		public static Product GetProductId(ApplicationDbContext dbContext,string id)
		{
			var product= dbContext.Products.Include("Kind_Product").Where(p => p.Masp == id).SingleOrDefault();
			return product;
		}


	}
}