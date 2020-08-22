using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net5_DataAccess.Data;
using Net5_Model.Models;

namespace Net5_Preview7.Controllers
{ 
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> obj = _db.Categories.ToList();
            return View();
        }
    }
}
