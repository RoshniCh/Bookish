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
    public class MembersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public MembersController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult MemberList()
        {

            var context = new BookishContext();
            var memberlist = context.Members
                                
                                   .ToList();
            var list = new MemberListViewModel(memberlist);
            return View(list);
        }
        public IActionResult MemberAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MemberInsert(Member newmember)
        {
            
            // Console.WriteLine (newbook);
            using (var context = new BookishContext())
            {
                // move the values obtained from the form to the variable book
                var member = new Member()
                {
                    Name = newmember.Name,
                    ContactNo = newmember.ContactNo,
                    Email = newmember.Email
                   
                };
                context.Members.Add(member);
                context.SaveChanges();
                
            }
            return RedirectToAction ("MemberList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}