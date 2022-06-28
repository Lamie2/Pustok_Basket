using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL;
using Pustok.Models;
using Pustok.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            HomeViewModel homeVM = new HomeViewModel
            {
                Sliders = _context.Sliders.ToList(),
                DiscountedBooks = _context.Books
                                 .Include(x => x.BookImages).Include(x => x.Author)
                                 .Where(x => x.DiscountPercent > 0).Take(20).ToList(),
                FeaturedBooks=_context.Books
                              .Include(x => x.BookImages).Include(x => x.Author)
                              .Where(x => x.DiscountPercent > 0).Take(20).ToList(),
                NewBooks = _context.Books
                              .Include(x => x.BookImages).Include(x => x.Author)
                              .Where(x => x.IsNew).Take(20).ToList()
            };
            

            return View(homeVM);
        }

       
    }
}
