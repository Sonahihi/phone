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
    public class Kind_PhuKienController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Kind_PhuKien
        public ActionResult Index()
        {
            return View(db.Kind_PhuKiens.ToList());
        }

        // GET: Kind_PhuKien/Details/5
        public ActionResult Details(string id)
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
            return View(kind_PhuKien);
        }

        // GET: Kind_PhuKien/Create
        public ActionResult Create()
        {
            var kind_pk = new Kind_PhuKien();
            return View(kind_pk);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form, HttpPostedFileBase image)
        {
            string imageLink = "/Content/image/xamomi1.jpg";
            if (image != null && image.ContentLength > 0)
            {
                imageLink = "/Content/image/" + image.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/image/"), image.FileName);
                image.SaveAs(path);
            }
            var kind = new Kind_PhuKien()
            {
                Id_Kind = form["Id_Kind"],
                Kind_Name = form["Kind_Name"],
                Kind_Phukien_Image = imageLink
            };
            db.Kind_PhuKiens.Add(kind);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Kind_PhuKien/Edit/5
        public ActionResult Edit(string id)
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
            return View(kind_PhuKien);
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
            Kind_PhuKien kind = db.Kind_PhuKiens.Find(id);
            kind.Id_Kind = form["Id_Kind"];
            kind.Kind_Phukien_Image = form["Kind_Phukien_Image"];
            kind.Kind_Phukien_Image = imageLink;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Kind_PhuKien/Delete/5
        public ActionResult Delete(string id)
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
            return View(kind_PhuKien);
        }

        // POST: Kind_PhuKien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Kind_PhuKien kind_PhuKien = db.Kind_PhuKiens.Find(id);
            db.Kind_PhuKiens.Remove(kind_PhuKien);
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
