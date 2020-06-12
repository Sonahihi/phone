using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Electronicsphone.Models
{
	public class Order
	{
		[Key]

		public int Id { get; set; }
		[Required]
		[Display(Name = "Ngày đặt hàng")]
		public DateTime NgayDatHang { get; set; }
		[Required(ErrorMessage = "Bạn chưa nhập số tiền:")]
		[Display(Name = "Tổng tiền : ")]
		public int TongTien { get; set; }
		[Display(Name = "Ngày nhận hàng : ")]
		[Required(ErrorMessage = "Bạn chưa nhập ngày nhận:")]
		public DateTime NgayNhanHang { get; set; }
		[Display(Name = "Tên Khách Hàng : ")]
		[Required(ErrorMessage = "Bạn chưa nhập họ và tên:")]
		public string TenKhachHang { get; set; }

		[Display(Name = "Số điện thoại : ")]
		[Required(ErrorMessage = "Bạn chưa nhập số điện thoại:")]
		public string SDT { get; set; }

		public string DiaChi { get; set; }
		[Display(Name = "Chi tiết : ")]
		public string MoTa { get; set; }
		public string CustomerId { get; set; }
		
		public  ApplicationUser ApplicationUser { get; set; }
		public virtual ICollection<OrderDetail> OrderDetails
		{
			get; set;
		}
		
	}
}