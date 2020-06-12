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
    public class PhuKienController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PhuKien
        public ActionResult Index(string searchString)
        {
            var products = db.Products.Where(p => p.Kind_Product.MaLoaiSP.Equals("L03"));
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Tensp.Contains(searchString));
            }
            return View(products);
        }

        // GET: PhuKien/Details/5
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

        // GET: PhuKien/Create
        public ActionResult Create()
        {
            var ProductModel = new Product()
            {
                Kind_PhuKiens = Kind_PhuKien.GetKind_PhuKien(db),
                Kind_Products = Kind_Product.GetKind_Product(db),
            };
            return View(ProductModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (HttpPostedFileBase Image, FormCollection form)
        {
            string imageLink = "/Content/image/xamomi1.jpg";
            if(Image.ContentLength>0 && Image != null)
            {
                imageLink = "/Content/image/" + Image.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/image"), Image.FileName);
                Image.SaveAs(path);
            }
            var product = new Product()
            {
                Masp = form["Masp"],
                Tensp = form["Tensp"],
                HinhAnh = imageLink,
                Kind_PhuKien=Kind_PhuKien.GetKind_PhuKien_Id(db,form["Kind_PhuKien"]),
                Kind_Product = Kind_Product.GetKind_Product_Id(db, form["Kind_Product"]),
                Gia = int.Parse(form["Gia"]),
                Soluong = int.Parse(form["Soluong"]),
                TrongLuong=form["TrongLuong"],
                TimeSac=form["TimeSac"],
                KetNoiCungLuc=form["KetNoiCungLuc"],
                TimeSuDung=form["TimeSuDung"],
            };
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: PhuKien/Edit/5
        public ActionResult Edit(string id)
        {
            var phukien = Product.GetProductId(db, id);
            Product product = new Product();
            product.Tensp = phukien.Tensp;
            product.Soluong = phukien.Soluong;
            product.Gia = phukien.Gia;
            product.HinhAnh = phukien.HinhAnh;
            product.TrongLuong = phukien.TrongLuong;
            product.TimeSac = phukien.TimeSac;
            product.KetNoiCungLuc = phukien.KetNoiCungLuc;
            product.TimeSuDung = phukien.TimeSuDung;
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form, HttpPostedFileBase Image, string id)
        {
            Product phukien = Product.GetProductId(db, id);
            string imageLink = "/Content/image/xamomi1.jpg";
            if (Image != null && Image.ContentLength > 0)
            {
                imageLink = "/Content/image/" + Image.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/image"), Image.FileName);
                Image.SaveAs(path);
            }
            phukien.Tensp = form["Tensp"];
            phukien.Gia = int.Parse(form["Gia"]);
            phukien.Soluong = int.Parse(form["Soluong"]);
            phukien.HinhAnh = imageLink;
            phukien.TimeSac = form["TimeSac"];
            phukien.TrongLuong = form["TrongLuong"];
            phukien.KetNoiCungLuc = form["KetNoiCungLuc"];
            phukien.TimeSuDung = form["TimeSuDung"];
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: PhuKien/Delete/5
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

        // POST: PhuKien/Delete/5
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
