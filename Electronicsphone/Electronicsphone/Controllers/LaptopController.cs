using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Electronicsphone.Models;

namespace Electronicsphone.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LaptopController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Laptop
        public ActionResult Index(string searchString)
        {
            var phone = db.Products.Where(p => p.Kind_Product.MaLoaiSP.Contains("L02"));
            if (!String.IsNullOrEmpty(searchString))
            {
                phone = phone.Where(s => s.Tensp.Contains(searchString));
            }
            return View(phone);
        }

        // GET: Laptop/Details/5
        public ActionResult Details(string id)
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

        // GET: Laptop/Create
        public ActionResult Create()
        {
            var ProductModel = new Product()
            {
                Kind_Products = Kind_Product.GetKind_Product(db),
                Brand_Products = Brand_Product.GetBrand_Product(db)
            };
            return View(ProductModel);
        }

        // POST: Laptop/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form, HttpPostedFileBase image)
        {
            string imageLink = "/Content/image/xamomi1.jpg";
            if (image != null && image.ContentLength > 0)
            {
                imageLink = "/Content/image/" + image.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/image"), image.FileName);
                image.SaveAs(path);
            }
            var product = new Product()
            {
                Masp = form["Masp"],
                Tensp = form["Tensp"],
                Kind_Product = Kind_Product.GetKind_Product_Id(db, form["Kind_Product"]),
                Gia = int.Parse(form["Gia"]),
                HinhAnh = imageLink,
                Soluong = int.Parse(form["Soluong"]),
                Brand_Product = Brand_Product.GetBrand_Product_Id(db, form["Brand_Product"]),
                HeDieuHanh = form["HeDieuHanh"],
                CPU = form["CPU"],
                RAM = form["RAM"],
                OCung=form["OCung"],
                CardManHinh = form["CardManHinh"],
                CongKetNoi = form["CongKetNoi"],
                KichThuoc = form["KichThuoc"],
                Manhinh = form["Manhinh"],
            };
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var laptop = Product.GetProductId(db, id);
            Product product = new Product();
            product.Tensp = laptop.Tensp;
            product.Soluong = laptop.Soluong;
            product.Gia = laptop.Gia;
            product.HinhAnh = laptop.HinhAnh;
            product.CardManHinh = laptop.CardManHinh;
            product.HeDieuHanh = laptop.HeDieuHanh;
            product.CPU = laptop.CPU;
            product.RAM = laptop.RAM;
            product.OCung = laptop.OCung;
            product.CongKetNoi = laptop.CongKetNoi;
            product.Manhinh = laptop.Manhinh;
            product.KichThuoc = laptop.KichThuoc;
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, FormCollection form, HttpPostedFileBase Image)
        {
            Product laptop = Product.GetProductId(db, id);
            string imageLink = "/Content/image/xamomi1.jpg";
            if (Image != null && Image.ContentLength > 0)
            {
                imageLink = "/Content/image/" + Image.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/image"), Image.FileName);
                Image.SaveAs(path);
            }
            laptop.Tensp = form["Tensp"];
            laptop.Gia = int.Parse(form["Gia"]);
            laptop.Soluong = int.Parse(form["Soluong"]);
            laptop.RAM = form["RAM"];
            laptop.CPU = form["CPU"];
            laptop.CongKetNoi = form["Congketnoi"];
            laptop.CardManHinh = form["CardManHinh"];
            laptop.Manhinh = form["Manhinh"];
            laptop.KichThuoc = form["Kichthuoc"];
            laptop.HeDieuHanh = form["HeDieuHanh"];
            laptop.OCung = form["OCung"];
            laptop.HinhAnh = imageLink;
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Laptop/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Laptop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
