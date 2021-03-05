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

        public IActionResult FindMemberBooks()
        {
            return View();
        }

        public IActionResult MemberBookList(FindMemberBook find) 
        {
                var memberId = find.MemberId;
                var context = new BookishContext();
                var copyList = context.Copies_Book_Member
                                    .Where(s => s.MemberId == memberId)
                                    .Include(c => c.Book)
                                    .Include(m => m.Member)
                                    .ToList();

                var list = new MemberBookListViewModel(copyList);
                return View(list);
            
        }
        public IActionResult UpdateMemberSearch()
        {
            return View();
        }

        public IActionResult UpdateMemberInfo(String MemberId)
        
        {
            var context = new BookishContext();
            var memberlist = context.Members
                            .Where(s => s.MemberId == Int32.Parse(MemberId))    
                            .ToList();
            var member = memberlist[0];
            return View(member);
            
        }

        public IActionResult MemberChange(String MemberId, String Name, String ContactNo, String Email)
        
        {
            var member = new Member(){MemberId = Int32.Parse(MemberId)};
            
            member.Name = Name;
            member.ContactNo = ContactNo;
            member.Email = Email;
           
            
            using (var context = new BookishContext())
            {
                context.Update<Member>(member);
                context.SaveChanges(); 
            }

            return RedirectToAction ("MemberList");
            
        }

        public IActionResult MemberSearch()
        {
            return View();
        }

        public IActionResult MemberSearchList(String MemberId, String Name)
        {
            // Console.WriteLine(book.Title.GetType());
            var context = new BookishContext();
            var memberlist = context.Members.AsQueryable();
            if (!String.IsNullOrEmpty(MemberId)){
                memberlist = memberlist.Where(s => s.MemberId == Int32.Parse(MemberId));
            }
            if (!String.IsNullOrEmpty(Name)){
                memberlist = memberlist.Where(s => s.Name.Contains(Name));
            }
            var members = memberlist.OrderBy(x => x.Name).ToList();
            var list = new MemberListViewModel(members);
            return View(list);
            // return RedirectToAction ("BookList");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}