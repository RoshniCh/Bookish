using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Bookish.Models
{
    public class Copy
    {
        public int CopyId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        
        [ForeignKey("Member")]
        public int? MemberId { get; set; }
        public Member Member { get; set; }
        
        public DateTime? IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
