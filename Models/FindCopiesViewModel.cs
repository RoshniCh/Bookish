using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Bookish.Models
{
    public class FindCopy
    {
        public int BookId { get; set; }
        public string Title { get; set; }
    }
}