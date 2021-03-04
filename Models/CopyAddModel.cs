using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Bookish.Models
{
    public class CopyAdd
    {
        public int CopyId { get; set; }
        public int BookId { get; set; }
        public int NumberofCopies { get; set; }

    }
}
