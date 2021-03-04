using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Bookish.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
       
        public ICollection<Copy> Copy { get; set; }


    }
}
