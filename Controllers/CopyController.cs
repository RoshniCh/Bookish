using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bookish.Models;
using Microsoft.EntityFrameworkCore;


namespace Bookish.Controllers
{
    public class CopyController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public CopyController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
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

        public IActionResult CopyDelete(String CopyId)
        {
            var copy = new Copy(){CopyId = Int32.Parse(CopyId)};
            using (var context = new BookishContext())
            {
                context.Remove<Copy>(copy);
                context.SaveChanges(); 
            }
            
            return RedirectToAction("CopyAdd");
        }

        public IActionResult FindCopies()
        {
            return View();
        }
        
        public IActionResult CopiesList(FindCopy find) 
        {
            if (find.Title != null)
            {
                var bookTitle = find.Title;
                var context = new BookishContext();
                var availableList = context.Copies_Book_Member
                                    .Where(s => s.Book.Title.Contains(bookTitle))
                                    .Where(s => s.MemberId == null)
                                    .Include(c => c.Book)
                                    .ToList();

                var takenList = context.Copies_Book_Member
                                    .Where(s => s.Book.Title.Contains(bookTitle))
                                    .Where(s => s.MemberId != null)
                                    .Include(c => c.Book)
                                    .Include(m => m.Member)
                                    .ToList();                      

                var numofcopies = availableList.Count + takenList.Count;
                var numAvailable = availableList.Count;
                var list = new CopyListViewModel(numofcopies, numAvailable, takenList, availableList);
                return View(list);
            } 
            else 
              { 
                var bookId = find.BookId;
                var context = new BookishContext();
                var availableList = context.Copies_Book_Member
                                    .Where(s => s.BookId == bookId)
                                    .Where(s => s.MemberId == null)
                                    .Include(c => c.Book)
                                    .ToList();

                var takenList = context.Copies_Book_Member
                                    .Where(s => s.BookId == bookId)
                                    .Where(s => s.MemberId != null)
                                    .Include(c => c.Book)
                                    .Include(m => m.Member)
                                    .ToList();                      

                var numofcopies = availableList.Count + takenList.Count;
                var numAvailable = availableList.Count;
                var list = new CopyListViewModel(numofcopies, numAvailable, takenList, availableList);
                return View(list);
            }

        }


        public IActionResult Checkout(String CopyId, String BookId, String MemberId)
        {
            //copy has to be populated
                // var context = new BookishContext();
                // var copy = context.Copies_Book_Member
                //                     .Where(s => s.CopyId == Int32.Parse(CopyId))
                //                     .ToList()

            var copy = new Copy(){CopyId = Int32.Parse(CopyId), BookId = Int32.Parse(BookId)};
            // MemberId = Int32.Parse(MemberId), IssueDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14)
        
            copy.MemberId = Int32.Parse(MemberId);
            copy.IssueDate = DateTime.Now;
            copy.DueDate = DateTime.Now.AddDays(14);
            
            using (var context = new BookishContext())
            {
                context.Update<Copy>(copy);
                context.SaveChanges(); 
            }

            return RedirectToAction ("FindCopies");
        } 

        public IActionResult ReturnB(String CopyId, String BookId)
        {
            var copy = new Copy(){CopyId = Int32.Parse(CopyId), BookId = Int32.Parse(BookId)};
        
            copy.MemberId = null;
            copy.IssueDate = null;
            copy.DueDate = null;
            
            using (var context = new BookishContext())
            {
                context.Update<Copy>(copy);
                context.SaveChanges(); 
            }

            return RedirectToAction ("FindCopies");
        } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}