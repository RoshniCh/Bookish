using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace Bookish.Models
{
    public class BookishContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Copy> Copies_Book_Member { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=BookishDB;Trusted_Connection=True;");
        }
    }
}

