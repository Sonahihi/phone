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
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(string searchString)
        {
            var products = db.Products.Where(p => p.Kind_Product.MaLoaiSP.Contains("L01"));
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Tensp.Contains(searchString));
            }
            return View(products);
        }

        // GET: Products/Details/5
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

        // GET: Products/Create
      
        public ActionResult Create()
        {
            var ProductModel = new Product()
            {
                Kind_Products = Kind_Product.GetKind_Product(db),
                Brand_Products=Brand_Product.GetBrand_Product(db)
            };
            return View(ProductModel);
         }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase image, FormCollection form)
        {
            string imageLink = "/Content/image/xamomi1.jpg";
            if(image!=null && image.ContentLength > 0)
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
                Manhinh = form["Manhinh"],
                        HinhAnh = imageLink,
                        Soluong = int.Parse(form["Soluong"]),
                        Brand_Product=Brand_Product.GetBrand_Product_Id(db,form["Brand_Product"]),
                        HeDieuHanh = form["HeDieuHanh"],
                        CPU = form["CPU"],
                        RAM = form["RAM"],
                        BoNhoTrong = form["BoNhoTrong"],
                        DungLuongPin = form["DungLuongPin"],
                        CameraTruoc = form["CameraTruoc"],
                        CameraSau = form["CameraSau"],
            };
              db.Products.Add(product);
              db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        [HttpGet]

        public ActionResult Edit(string id)
        {
            var product = Product.GetProductId(db, id);
            Product product1 = new Product();
            product1.Tensp = product.Tensp;
            product1.Gia = product.Gia;
            product1.Manhinh = product.Manhinh;
            product1.HinhAnh = product.HinhAnh;
            product1.CPU = product.CPU;
            product1.RAM = product.RAM;
            product1.Soluong = product.Soluong;
            product1.CameraSau = product.CameraSau;
            product1.CameraTruoc = product.CameraTruoc;
            product1.HeDieuHanh = product.HeDieuHanh;
            product1.BoNhoTrong = product.BoNhoTrong;
            product1.DungLuongPin = product.DungLuongPin;
            return View(product1);
        }

        public ActionResult Edit(string id, HttpPostedFileBase Image, FormCollection form)
        {
            Product product = Product.GetProductId(db, id);
            string imageLink = "/Content/image/xamomi1.jpg";
            if (Image != null && Image.ContentLength > 0)
            {
                imageLink = "/Content/image/" + Image.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/image"), Image.FileName);
                Image.SaveAs(path);
            }
            product.Tensp = form["Tensp"];
            
            product.HinhAnh = imageLink;
            product.Manhinh = form["Manhinh"];
            product.Soluong = int.Parse(form["Soluong"]);
            product.HeDieuHanh = form["HeDieuHanh"];
            product.CPU = form["CPU"];
            product.RAM = form["RAM"];
            product.BoNhoTrong = form["BoNhoTrong"];
            product.DungLuongPin = form["DungLuongPin"];
            product.CameraTruoc = form["CameraTruoc"];
            product.CameraSau = form["CameraSau"]; 
            product.Gia = int.Parse(form["Gia"]);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

       
        
        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
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
