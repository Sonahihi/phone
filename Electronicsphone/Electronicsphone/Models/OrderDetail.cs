using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Electronicsphone.Models
{
	public class OrderDetail
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[Display(Name = "Mã Đơn Hang:")]
		public int DatHangId { get; set; }
		[Display(Name = "Mã sản phẩm:")]
		[StringLength(128)]
		public string MonAnId { get; set; }
		[Display(Name = "Giá:")]
		public double GiaHienTai { get; set; }
		[Display(Name = "Số lượng:")]
		public int Soluong { get; set; }
		public double GiamGia { get; set; }


		public virtual Order Order { get; set; }
		public virtual Product Product { get; set; }
	}
}