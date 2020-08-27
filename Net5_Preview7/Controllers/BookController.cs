using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Net5_DataAccess.Data;
using Net5_Model.Models;
using Net5_Model.ViewModels;

namespace Net5_Preview7.Controllers
{ 
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Book> objList = _db.Books.ToList();
            foreach (var obj in objList)
            {
                //least efficient
                //obj.Publisher = _db.Publishers.FirstOrDefault(x => x.Publisher_Id == obj.Publisher_Id);
                
                //ecplicit loading. more efficient
                _db.Entry(obj).Reference(x => x.Publisher).Load();
            }
            return View(objList);
        }
        public IActionResult Upsert(int? id)
        {
            BookVM obj = new BookVM();

            obj.PublisherList = _db.Publishers.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Publisher_Id.ToString(),
            });

            if (id != null)
                obj.Book = _db.Books.FirstOrDefault(u => u.Book_Id == id);
            if (obj == null)
                return NotFound();

            return View(obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM obj)
        {
            if (obj.Book.Book_Id == 0) _db.Books.Add(obj.Book);

            else _db.Books.Update(obj.Book);

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }

        public IActionResult Details(int? id)
        {
            BookVM obj = new BookVM();

            if (id != null)
            {
                obj.Book = _db.Books.FirstOrDefault(u => u.Book_Id == id);
                obj.Book.BookDetail = _db.BookDetails.FirstOrDefault(u => u.BookDetail_Id == obj.Book.BookDetail_Id);

            }
            if (obj == null)
                return NotFound();

            return View(obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookVM obj)
        {
            if (obj.Book.Book_Id == 0) _db.Books.Add(obj.Book);

            else _db.Books.Update(obj.Book);

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int id)
        {
            var obj = _db.Books.FirstOrDefault(u => u.Book_Id == id);
            _db.Books.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult PlayGround()
        {
            var bookTemp = _db.Books.FirstOrDefault();
            bookTemp.Price = 100;

            var bookCollection = _db.Books;
            double totalPrice = 0;

            foreach (var book in bookCollection)
            {
                totalPrice += book.Price;
            }

            var bookList = _db.Books.ToList();
            foreach (var book in bookList)
            {
                totalPrice += book.Price;
            }

            var bookCollection2 = _db.Books;
            var bookCount1 = bookCollection2.Count();

            var bookCount2 = _db.Books.Count();
            return RedirectToAction(nameof(Index));
        }
    }
}
