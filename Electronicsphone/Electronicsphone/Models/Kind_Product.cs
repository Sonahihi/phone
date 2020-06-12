using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Electronicsphone.Models
{
	public class Kind_Product
	{
		[Key]
		public string MaLoaiSP { get; set; }
		[Required]
		[Display(Name ="Tên loại sản phẩm")]
		[StringLength(200)]
		public string TenLoaiSP { get; set; }
		[Display(Name ="Hình loại sản phẩm")]
		public string HinhLoai { get; set; }
		public virtual ICollection<Product> Products { get; set; }
		public static IEnumerable<Kind_Product> GetKind_Product(ApplicationDbContext dbContext)
		{
			return dbContext.Kind_Products.ToList();
		}
		public static Kind_Product	GetKind_Product_Id(ApplicationDbContext dbContext,string id)
		{
			return dbContext.Kind_Products.Where(k=>k.MaLoaiSP==id).SingleOrDefault();
		}
	}
}