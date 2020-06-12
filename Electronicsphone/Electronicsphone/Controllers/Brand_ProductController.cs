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
    public class Brand_ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Brand_Product
        public ActionResult Index()
        {
            return View(db.Brand_Products.ToList());
        }

        // GET: Brand_Product/Details/5
        public ActionResult Details(string id)
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
            return View(brand_Product);
        }

        // GET: Brand_Product/Create
        public ActionResult Create()
        {
            var Brand = new Brand_Product();
            return View(Brand);
        }

        // POST: Brand_Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form, HttpPostedFileBase image)
        {
            string imageLink = "/Content/image/xamomi1.jpg";
            if(image!=null && image.ContentLength > 0)
            {
                imageLink = "/Content/image/" + image.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/image/"), image.FileName);
                image.SaveAs(path); 
            }
            var Brand = new Brand_Product()
            {
                MaNhaSX = form["MaNhaSX"],
                TenNhaSX = form["TenNhaSX"],
                HinhNhaSX = imageLink,
            };
            db.Brand_Products.Add(Brand);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Brand_Product/Edit/5
        public ActionResult Edit(string id)
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
            return View(brand_Product);
        }

        // POST: Brand_Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form, HttpPostedFileBase image, string id)
        {
            string imageLink = "/Content/image/xamomi1.jpg";
            if(image!=null && image.ContentLength > 0)
            {
                imageLink = "/Content/image/" + image.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/image/"), image.FileName);
                image.SaveAs(path);
            }
            Brand_Product brand_Product = db.Brand_Products.Find(id);
            brand_Product.MaNhaSX = form["MaNhaSX"];
            brand_Product.TenNhaSX = form["TenNhaSX"];
            brand_Product.HinhNhaSX = imageLink;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Brand_Product/Delete/5
        public ActionResult Delete(string id)
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
            return View(brand_Product);
        }

        // POST: Brand_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Brand_Product brand_Product = db.Brand_Products.Find(id);
            db.Brand_Products.Remove(brand_Product);
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
