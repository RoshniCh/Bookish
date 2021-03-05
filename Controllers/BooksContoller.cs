using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bookish.Models;
// using System.Data;
// using System.Data.Entity;
// using System.Data.Entity.Infrastructure;
// using System.Data.Entity.Core.Objects;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public BooksController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult BookSearch()
        {
            return View();
        }

        public IActionResult BookSearchList(String BookId, String TitleP, String Title, String Author, String Year)
        {
            // Console.WriteLine(book.Title.GetType());
            var context = new BookishContext();
            var booklist = context.Books.AsQueryable();
            if (!String.IsNullOrEmpty(BookId)){
                booklist = booklist.Where(s => s.BookId == Int32.Parse(BookId));
            }
            if (!String.IsNullOrEmpty(TitleP)){
                booklist = booklist.Where(s => s.Title.Contains(TitleP));
            }
            if (!String.IsNullOrEmpty(Title)){
                booklist = booklist.Where(s => s.Title == Title);
            }
            if (!String.IsNullOrEmpty(Author)){
                booklist =  booklist.Where(s => s.Author.Contains(Author));
            }
            if (!String.IsNullOrEmpty(Year)){
                booklist = booklist.Where(s => s.Year == Int32.Parse(Year));
            }
            var books = booklist.OrderBy(x => x.Title).ToList();
            var list = new BookListViewModel(books);
            return View(list);
            // return RedirectToAction ("BookList");
        }


        public IActionResult BookList()
        {

            var context = new BookishContext();
            var booklist = context.Books
                                   .OrderBy(x => x.Title)
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
            
            using (var context = new BookishContext())
            {
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