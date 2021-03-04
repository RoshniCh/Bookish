using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Bookish.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
       
        public ICollection<Copy> Copy { get; set; }


    }
}
