using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            List<Book> objList = _db.Books
                                    .Include(x => x.Publisher)
                                    .Include(x=> x.BookAuthors)
                                        .ThenInclude(a=>a.Author)
                                    .ToList();

            //List<Book> objList = _db.Books.ToList();

            //foreach (var obj in objList)
            //{
            //    //least efficient
            //    //obj.Publisher = _db.Publishers.FirstOrDefault(x => x.Publisher_Id == obj.Publisher_Id);

            //    //ecplicit loading. more efficient
            //    _db.Entry(obj).Reference(x => x.Publisher).Load();
            //    _db.Entry(obj).Collection (x => x.BookAuthors).Load();
            //    foreach (var bookAuth in obj.BookAuthors)
            //    {
            //        _db.Entry(bookAuth).Reference(b => b.Author).Load();
            //    }

            //}
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
                obj.Book = _db.Books.Include(x=> x.BookDetail).FirstOrDefault(u => u.Book_Id == id);
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
            if (obj.Book.BookDetail.BookDetail_Id == 0)
            {
                _db.BookDetails.Add(obj.Book.BookDetail);
                _db.SaveChanges();
                var bookFromDb = _db.Books.FirstOrDefault(x => x.Book_Id == obj.Book.Book_Id);
                bookFromDb.BookDetail_Id = obj.Book.BookDetail.BookDetail_Id;
                _db.SaveChanges();

            }
            else
            {
                _db.BookDetails.Update(obj.Book.BookDetail);
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int id)
        {
            var obj = _db.Books.FirstOrDefault(u => u.Book_Id == id);
            _db.Books.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ManageAuthors(int id)
        {
            BookAuthorVM obj = new BookAuthorVM
            {
                BookAuthorList = _db.BookAuthors
                    .Include(a => a.Author)
                    .Include(b => b.Book)
                    .Where(b => b.BookId == id)
                    .ToList(),
                BookAuthor = new BookAuthor
                {
                    BookId = id,

                },
                Book = _db.Books.FirstOrDefault(b => b.Book_Id == id)
            };
            List<int> tempListOfAssignedAuthors = obj.BookAuthorList.Select(a => a.AuthorId).ToList();
            //not in clause in LinQ
            //get all the authors whose id is not in tempListOfAsssignedAuthors
            var authorsWhoseIdNotInbook = _db.Authors.Where(a => !tempListOfAssignedAuthors.Contains(a.Author_Id)).ToList();
            obj.AuthorList = authorsWhoseIdNotInbook.Select(sel => new SelectListItem
            {
                Text = sel.FullName,
                Value = sel.Author_Id.ToString(),
            });
            return View(obj);
                
        }
        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if (bookAuthorVM.BookAuthor.BookId != 0 && bookAuthorVM.BookAuthor.AuthorId!=0)
            {
                _db.BookAuthors.Add(bookAuthorVM.BookAuthor);
                _db.SaveChanges();
                   
            }
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.BookId });

        }
        [HttpPost]
        public IActionResult RemoveAuthors(int authorId, BookAuthorVM bookAuthorVM)
        {
            int bookId = bookAuthorVM.Book.Book_Id;
            BookAuthor bookAuthor = _db.BookAuthors.FirstOrDefault(
                b => b.AuthorId == authorId && b.BookId == bookId);
            _db.BookAuthors.Remove(bookAuthor);
            _db.SaveChanges();
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });

        }
        public IActionResult PlayGround()
        {
            //var bookTemp = _db.Books.FirstOrDefault();
            //bookTemp.Price = 100;

            //var bookCollection = _db.Books;
            //double totalPrice = 0;

            //foreach (var book in bookCollection)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = _db.Books.ToList();
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _db.Books;
            //var bookCount1 = bookCollection2.Count();

            //var bookCount2 = _db.Books.Count();

            //IEnumerable<Book> bookList1 = _db.Books;
            //var filteredBook1 = bookList1.Where(b => b.Price > 500).ToList();

            //IQueryable<Book> bookList2 = _db.Books;
            //var filteredBook2 = bookList2.Where(b => b.Price > 500).ToList();

            //var category = _db.Categories.FirstOrDefault();
            //_db.Entry(category).State = EntityState.Modified;
            //_db.SaveChanges();

            //updating related data

            //var bookTemp1 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.Book_Id == 4);
            //bookTemp1.BookDetail.NumberOfChapters = 2222;
            //_db.Books.Update(bookTemp1);
            //_db.SaveChanges();


            //var bookTemp2 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.Book_Id == 4);
            //bookTemp2.BookDetail.Weight = 3333;

            //_db.Books.Attach(bookTemp2);
            //_db.SaveChanges();

            //views
            var vseciewList = _db.BookDetailsFromViews.ToList();
            var viewList1 = _db.BookDetailsFromViews.FirstOrDefault();
            var viewList2 = _db.BookDetailsFromViews.Where(b => b.Price > 500);


            return RedirectToAction(nameof(Index));
        }
    }
}
