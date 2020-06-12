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
    public class Kind_ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Kind_Product
        public ActionResult Index()
        {
            return View(db.Kind_Products.ToList());
        }

        // GET: Kind_Product/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind_Product kind_Product = db.Kind_Products.Find(id);
            if (kind_Product == null)
            {
                return HttpNotFound();
            }
            return View(kind_Product);
        }

        // GET: Kind_Product/Create
        public ActionResult Create()
        {
            var kind = new Kind_Product();
            return View(kind);
        }

        // POST: Kind_Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(FormCollection form, HttpPostedFileBase image)
        {
            string imageLink = "/Content/image/xamomi1.jpg";
            if(image!=null && image.ContentLength > 0)
            {
                imageLink = "/Content/image/" + image.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/image/"), image.FileName);
                image.SaveAs(path);
            }
            var kind = new Kind_Product()
            {
                MaLoaiSP = form["MaLoaiSP"],
                TenLoaiSP =form["TenLoaiSP"],
                HinhLoai=imageLink
            };
            db.Kind_Products.Add(kind);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Kind_Product/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind_Product kind_Product = db.Kind_Products.Find(id);
            if (kind_Product == null)
            {
                return HttpNotFound();
            }
            return View(kind_Product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase image, FormCollection form, string id)
        {
            string imageLink = "/Content/image/xamomi1.jpg";
            if (image != null && image.ContentLength > 0)
            {
                imageLink = "/Content/image/" + image.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/image/"), image.FileName);
                image.SaveAs(path);
            }
            Kind_Product kind = db.Kind_Products.Find(id);
            kind.MaLoaiSP = form["MaLoaiSP"];
            kind.TenLoaiSP = form["TenLoaiSP"];
            kind.HinhLoai = imageLink;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Kind_Product/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind_Product kind_Product = db.Kind_Products.Find(id);
            if (kind_Product == null)
            {
                return HttpNotFound();
            }
            return View(kind_Product);
        }

        // POST: Kind_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Kind_Product kind_Product = db.Kind_Products.Find(id);
            db.Kind_Products.Remove(kind_Product);
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
