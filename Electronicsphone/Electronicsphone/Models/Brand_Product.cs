using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Electronicsphone.Models
{
	public class Brand_Product
	{
		[Key]
		public string MaNhaSX { get; set; }
		[Required]
		[Display(Name ="Tên nhà sản xuất")]
		[StringLength(150)]
		public string TenNhaSX { get; set; }
		[Display(Name ="Ảnh nhà sản xuất")]
		public string HinhNhaSX { get; set; }
		public virtual ICollection<Product> Products { get; set; }
		public static Brand_Product GetBrand_Product_Id(ApplicationDbContext db,string id)
		{
			return db.Brand_Products.Where(b => b.MaNhaSX == id).SingleOrDefault();
		}
		public static IEnumerable<Brand_Product> GetBrand_Product(ApplicationDbContext db)
		{
			return db.Brand_Products.ToList();
		}
	}
}