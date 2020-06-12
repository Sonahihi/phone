using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Electronicsphone.Models
{
	public class Kind_PhuKien
	{
		[Key]
		
		public string Id_Kind { get; set; }
		[Required]
		[Display(Name ="Tên loại phụ kiện")]
		public string Kind_Name { get; set; }
		[Display(Name ="Hình ảnh")]
		public string Kind_Phukien_Image { get; set; }
		public virtual ICollection<Product> Products { get; set; }
		public static IEnumerable<Kind_PhuKien> GetKind_PhuKien(ApplicationDbContext db)
		{
			return db.Kind_PhuKiens.ToList();
		}
		public static Kind_PhuKien GetKind_PhuKien_Id (ApplicationDbContext db, string id)
		{
			return db.Kind_PhuKiens.Where(k => k.Id_Kind == id).SingleOrDefault();
		}
	}
}