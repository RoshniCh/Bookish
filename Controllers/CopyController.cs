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
    public class CopyController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public CopyController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // public IActionResult CopyList()
        // {

        //     var context = new BookishContext();
        //     var booklist = context.Books
        //                         //    .Where(s => s.Author == "George RR Martin")
        //                            .ToList();
        //     var list = new BookListViewModel(booklist);
        //     return View(list);
        // }
        public IActionResult CopyAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CopyInsert(CopyAdd newCopies)
        {


            // select from book where book id is the value entered by the user
            var contextBook = new BookishContext();
            var booklist = contextBook.Books
                                .Where(s => s.BookId == newCopies.BookId )
                                .ToList();

            // if you get a value do the insert below
            if (booklist.Count == 1)
            {
                using (var context = new BookishContext())
                {
                    // move the values obtained from the form to the variable book
                    
                    for (var i = 0; i < newCopies.NumberofCopies; i++)
                    {
                        var copies = new Copy()
                        {
                        BookId = newCopies.BookId
                        
                        };
                        context.Copies_Book_Member.Add(copies);
                        context.SaveChanges();
                    }
                    
                }
                return RedirectToAction ("CopyAdd");
            }
            else
            {
                return RedirectToAction ("CopyAddError");
            }
        }

        public IActionResult CopyAddError()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}