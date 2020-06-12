using Electronicsphone.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web;
using PagedList;
using Microsoft.AspNet.Identity;

namespace Electronicsphone.Controllers
{
	public class HomeController : Controller

	{
		
		ApplicationDbContext db = new ApplicationDbContext();
		
		public ActionResult Dienthoai(string SortOrder,string searchString, int? page, string boloc)
		{
			ViewBag.PriceHigh = SortOrder == "Gia_desc" ? "Price" : "Gia_desc";
			ViewBag.PriceLow = SortOrder == "Price_asc" ? "Price" : "Price_asc";
			ViewBag.Price1 = SortOrder == "Priceh" ? "Price" : "Priceh";
			ViewBag.Price2= SortOrder == "Price2" ? "Price" : "Price2";
			ViewBag.Price3 = SortOrder == "Price3" ? "Price" : "Price3";
			ViewBag.Price4 = SortOrder == "Price4" ? "Price" : "Price4";
			ViewBag.Price5 = SortOrder == "Price5" ? "Price" : "Price5";
			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = boloc;
			}
			ViewBag.Boloc = searchString;
			var drs = db.Products.Where(p => p.Kind_Product.MaLoaiSP.Contains("L01"));
			if (!String.IsNullOrEmpty(searchString))
			{
				drs = drs.Where(s => s.Tensp.Contains(searchString));
			}
			switch (SortOrder)
			{
				case "Gia_desc":
					drs = drs.OrderByDescending(p => p.Gia);
					break;
				case "Price_asc":
					drs = drs.OrderBy(p => p.Gia);
					break;
				case "Priceh":
					drs = drs.Where(s=>s.Gia <2000000 );
					break;
				case "Price2":
					drs = drs.Where(s => s.Gia >= 2000000 && s.Gia<=4000000);
					break;
				case "Price3":
					drs = drs.Where(s => s.Gia >= 4000000 && s.Gia <= 7000000);
					break;
				case "Price4":
					drs = drs.Where(s => s.Gia >= 7000000 && s.Gia <= 13000000);
					break;
				case "Price5":
					drs = drs.Where(s => s.Gia >= 13000000);
					break;
				default:
					break;
			}
			int pageSize = 15;
			int pageNumber = (page ?? 1);
			ViewBag.Brand = db.Brand_Products.Where(s=>s.MaNhaSX.Contains("NP"));
			
			return View(drs.ToList().ToPagedList(pageNumber, pageSize));
		}

		public ActionResult Laptop(string SortOrder, string searchString, int? page, string boloc)
		{
			ViewBag.PriceHigh = SortOrder == "Gia_desc" ? "Price" : "Gia_desc";
			ViewBag.PriceLow = SortOrder == "Price_asc" ? "Price" : "Price_asc";
			ViewBag.Price1 = SortOrder == "Priceh" ? "Price" : "Priceh";
			ViewBag.Price3 = SortOrder == "Price3" ? "Price" : "Price3";
			ViewBag.Price4 = SortOrder == "Price4" ? "Price" : "Price4";
			ViewBag.Price5 = SortOrder == "Price5" ? "Price" : "Price5";
			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = boloc;
			}
			ViewBag.Boloc = searchString;
			var drs = db.Products.Where(p => p.Kind_Product.MaLoaiSP.Contains("L02"));
			if (!String.IsNullOrEmpty(searchString))
			{
				drs = drs.Where(s => s.Tensp.Contains(searchString));
			}
			switch (SortOrder)
			{
				case "Gia_desc":
					drs = drs.OrderByDescending(p => p.Gia);
					break;
				case "Price_asc":
					drs = drs.OrderBy(p => p.Gia);
					break;
				case "Priceh":
					drs = drs.Where(s => s.Gia < 5000000);
					break;
				case "Price3":
					drs = drs.Where(s => s.Gia >= 5000000 && s.Gia <= 10000000);
					break;
				case "Price4":
					drs = drs.Where(s => s.Gia >= 10000000 && s.Gia <= 20000000);
					break;
				case "Price5":
					drs = drs.Where(s => s.Gia > 20000000);
					break;
				default:
					break;
			}
			int pageSize = 8;
			int pageNumber = (page ?? 1);
			ViewBag.Brand = db.Brand_Products.Where(s => s.MaNhaSX.Contains("NL"));

			return View(drs.ToList().ToPagedList(pageNumber, pageSize));
		}
		public ActionResult Phukien(string SortOrder, string searchString, int? page, string boloc)
		{
			ViewBag.Price1 = SortOrder == "Priceh" ? "Price" : "Priceh";
			ViewBag.Price3 = SortOrder == "Price3" ? "Price" : "Price3";
			ViewBag.Price4 = SortOrder == "Price4" ? "Price" : "Price4";
			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = boloc;
			}
			ViewBag.Boloc = searchString;
			var drs = db.Products.Where(p => p.Kind_Product.MaLoaiSP.Contains("L03"));
			if (!String.IsNullOrEmpty(searchString))
			{
				drs = drs.Where(s => s.Tensp.Contains(searchString));
			}
			switch (SortOrder)
			{
				case "Priceh":
					drs = drs.Where(s => s.Gia < 300000);
					break;
				case "Price3":
					drs = drs.Where(s => s.Gia >= 300000 && s.Gia <= 500000);
					break;
				case "Price4":
					drs = drs.Where(s => s.Gia >= 500000);
					break;
				default:
					break;
			}
			int pageSize = 8;
			int pageNumber = (page ?? 1);
			ViewBag.Phukien = db.Kind_PhuKiens.Where(s => s.Id_Kind.Contains("K"));
			return View(drs.ToList().ToPagedList(pageNumber, pageSize));
		}
		public ActionResult Thong_tin_san_pham(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}
		public ActionResult Thong_tin_may_tinh(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}
		public ActionResult Thong_tin_phu_kien(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}


		public ActionResult LoaiDienThoai(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Brand_Product brand_Product = db.Brand_Products.Find(id);
			if (brand_Product == null)
			{
				return HttpNotFound();
			}
			ViewBag.Brand = db.Brand_Products.Where(s => s.MaNhaSX.Contains("NP"));

			return View(brand_Product);
		}

		public ActionResult LoaiLaptop(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Brand_Product brand_Product = db.Brand_Products.Find(id);
			if (brand_Product == null)
			{
				return HttpNotFound();
			}

			ViewBag.Brand = db.Brand_Products.Where(s => s.MaNhaSX.Contains("NL"));
			return View(brand_Product);
		}
		public ActionResult LoaiPhukien(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Kind_PhuKien kind_PhuKien = db.Kind_PhuKiens.Find(id);
			if (kind_PhuKien == null)
			{
				return HttpNotFound();
			}
			ViewBag.Phukien = db.Kind_PhuKiens.Where(s => s.Id_Kind.Contains("K"));
			return View(kind_PhuKien);
		}
		
		[Authorize]
		public ActionResult Lich_su_dat_hang()
		{
			string id = User.Identity.GetUserId();
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationUser applicationUser = db.Users.Find(id);
			Order vm = db.Orders.FirstOrDefault(x => x.CustomerId == id);
			if (vm == null)
			{
				return RedirectToAction("Chua_co_lich_su","Home");
			}

			return View(vm);
		}
		public ActionResult Chi_tiet_lich_su_don_hang(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Order Order = db.Orders.Find(id);
			Order.OrderDetails = db.OrderDetails.Where(x => x.DatHangId == id).ToList();

			if (Order.OrderDetails == null || Order.OrderDetails.Count == 0)
			{
				return HttpNotFound();
			}
			Product product = null;
			for (int i = 0; i < Order.OrderDetails.Count; i++)
			{
				string idProduct = Order.OrderDetails.ElementAt(i).MonAnId;
				product = db.Products.Where(x =>
				x.Masp == idProduct).FirstOrDefault();
				if (product == null)
				{
					return HttpNotFound();
				}
				Order.OrderDetails.ElementAt(i).Product = product;
			}

			if (Order == null)
			{
				return HttpNotFound();
			}
			return View(Order);
		}
		public ActionResult Chua_co_lich_su()
		{
			return View();
		}


	}
}