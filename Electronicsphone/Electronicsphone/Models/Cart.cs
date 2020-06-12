using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Electronicsphone.Models
{
	public class Cart
	{
		ApplicationDbContext db = new ApplicationDbContext();
		[Key]
		public int ID { get; set; }
		public string IMaSP { get; set; }
		public string ITenSP { get; set; }
		public string IImage { get; set; }
		public int DonGia { get; set; }
		public int SoLuong { get; set; }
		public double ThanhTien
		{
			get { return SoLuong * DonGia; }
		}
		public Cart (string MaSP)
		{
			IMaSP = MaSP;
			Product masp = db.Products.Single(i => i.Masp == IMaSP);
			ITenSP = masp.Tensp;
			IImage = masp.HinhAnh;
			DonGia = int.Parse(masp.Gia.ToString());
			SoLuong = 1;
		}
	}
}