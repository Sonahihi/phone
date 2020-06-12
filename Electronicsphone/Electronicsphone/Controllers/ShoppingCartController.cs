using Electronicsphone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;

namespace Electronicsphone.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
       
        ApplicationDbContext db = new ApplicationDbContext();
        public List<Cart> TaoGioHang()
        {
            List<Cart> carts = Session["Cart"] as List<Cart>;
            if (carts == null)
            {
                carts =new List<Cart>();
                Session["Cart"] = carts;
            }
            return carts;
        }
        public ActionResult ThemHang(string IMaSP, string chuoi)
        {
            List<Cart> cart = TaoGioHang();
            Cart item = cart.Find(i => i.IMaSP == IMaSP);
            if (item == null)
            {
                item = new Cart(IMaSP);
                cart.Add(item);
                return Redirect(chuoi);
            }
            else
            {
                item.SoLuong++;
                return Redirect(chuoi);
            }
        }
        private int TongSoLuong()
        {
            int Itong = 0;
            List<Cart> carts = Session["Cart"] as List<Cart>;
            if (carts != null)
            {
                Itong = carts.Sum(i => i.SoLuong);
            }
            return Itong;
        }
        private double TongTien()
        {
            double Itien = 0;
            List<Cart> carts = Session["Cart"] as List<Cart>;
            if (carts != null)
            {
                Itien = carts.Sum(i => i.ThanhTien);
            }
            return Itien;
        }
        public ActionResult GioHang()
        {
            List<Cart> carts = TaoGioHang();
            if (carts.Count == 0)
            {
                return RedirectToAction("Empty", "ShoppingCart");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(carts);
        }
        public ActionResult XoaHang(string IMaSP)
        {
            List<Cart> carts = TaoGioHang();
            Cart item = carts.SingleOrDefault(i => i.IMaSP==IMaSP);
            if (item != null)
            {
                carts.RemoveAll(i => i.IMaSP == IMaSP);
                return RedirectToAction("GioHang");
            }
            if (carts.Count == 0)
            {
                return RedirectToAction("Empty", "ShoppingCart");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult Update(string IMaSP, FormCollection form)
        {
            List<Cart> carts = TaoGioHang();
            Cart item = carts.SingleOrDefault(i => i.IMaSP == IMaSP);
            try
            {
                if (item != null)
                {
                    item.SoLuong = int.Parse(form["SoLuong"].ToString());
                }
            }
            catch(Exception e)
            {
                ViewBag.Message("So luong khac khong" + e);
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaAll()
        {
            List<Cart> carts = TaoGioHang();
            carts.Clear();
            return RedirectToAction("Dienthoai", "Home");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            List<Cart> gioHangs = TaoGioHang();
            ViewBag.TongTien = TongTien();
            var donhang = new Order();
            return View(donhang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection form)
        {
            try
            {
                List<Cart> gioHangs = TaoGioHang();
                string id = User.Identity.GetUserId();
                string name = "Chưa xử lý";
                Order donDatHang = new Order()
                {
                    CustomerId = id,
                    MoTa=name,
                    TenKhachHang = Convert.ToString(form["TenKhachHang"]),
                    SDT = Convert.ToString(form["SDT"]),
                    TongTien = int.Parse(form["TongTien"]),
                    DiaChi = Convert.ToString(form["DiaChi"]),
                    NgayDatHang = DateTime.Now,
                    NgayNhanHang = DateTime.Parse(String.Format("{0:MM/dd/yyyy}", form["NgayNhanHang"])),

                };
                db.Orders.Add(donDatHang);
                db.SaveChanges();
                foreach (var item in gioHangs)
                {
                    OrderDetail CT = new OrderDetail()
                    {
                        DatHangId = donDatHang.Id,
                        Soluong = item.SoLuong,
                        MonAnId = item.IMaSP,
                        GiaHienTai = item.ThanhTien,
                    };
                    db.OrderDetails.Add(CT);
                    db.SaveChanges();
                }
                gioHangs.Clear();
                return RedirectToAction("XacNhanDathang", "ShoppingCart");
            }
            catch (Exception e)
            {
                Console.WriteLine("Chua nhap du thong tin");
                Console.WriteLine(e.Message);
            };

            return View();
        }
        
        public ActionResult ChiTietDatHang(int? id)
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
        
        public ActionResult Empty()
        {
            return View();
        }
        public ActionResult XacNhanDathang()
        {
            return View();
        }
        public ActionResult Index()
        {
            var show = db.Orders.ToList();
            return View(show);
        }
        public ActionResult DetailStatus(FormCollection form, int? id)
        {
            Order order = db.Orders.SingleOrDefault(x => x.Id == id);

            if (order != null)
            {
                order.MoTa = Convert.ToString(form["Gender"]);  
            }
            
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}