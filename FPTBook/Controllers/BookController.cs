using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPTBook.Models;

namespace FPTBook.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        FPTShopEntities6 db = new FPTShopEntities6(); 
        public ActionResult Index()
        {
            List<Book> list = db.Books.ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book b, HttpPostedFileBase UploadImage)
        {
            db.Books.Add(b);
            db.SaveChanges();
            if (UploadImage != null && UploadImage.ContentLength > 0)
            {
                int id = int.Parse(db.Books.ToList().Last().ID.ToString());
                string _FileName = "";
                int index = UploadImage.FileName.IndexOf(".");
                _FileName = "book" + id.ToString() + "." + UploadImage.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/Upload/Book/"), _FileName);
                UploadImage.SaveAs(_path);

                Book book = db.Books.FirstOrDefault(x => x.ID == id);
                book.Image = _FileName;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            Book b = db.Books.FirstOrDefault(x => x.ID == id);
            return View(b);
        }
        [HttpPost]
        public ActionResult Edit(Book b, HttpPostedFileBase UploadImage)
        {
            Book book = db.Books.FirstOrDefault(x=>x.ID == b.ID);
            book.Name = b.Name;
            book.Price = b.Price;
            book.Supplier = b.Supplier;
            book.Publisher = b.Publisher;
            book.Author = b.Author;
            book.Category = b.Category;

            if (UploadImage != null && UploadImage.ContentLength > 0)
            {
                int id = b.ID;

                string _FileName = "";
                int index = UploadImage.FileName.IndexOf(".");
                _FileName = "b" + id.ToString() + "." + UploadImage.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/Upload/Book/"), _FileName);
                UploadImage.SaveAs(_path);
                book.Image = _FileName;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Book book =db.Books.FirstOrDefault(x=>x.ID == id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}