using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bookish.Models;


namespace Bookish.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public BooksController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult BookList()
        {

            var context = new BookishContext();
            var booklist = context.Books
                                //    .Where(s => s.Author == "George RR Martin")
                                   .ToList();
            var list = new BookListViewModel(booklist);
            return View(list);
        }
        public IActionResult BookAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BookInsert(Book newbook)
        {
            
            // Console.WriteLine (newbook);
            using (var context = new BookishContext())
            {
                // move the values obtained from the form to the variable book
                var book = new Book()
                {
                    Title = newbook.Title,
                    Author = newbook.Author,
                    Year = newbook.Year
                   
                };
                context.Books.Add(book);
                context.SaveChanges();
                
            }
            return RedirectToAction ("BookList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}