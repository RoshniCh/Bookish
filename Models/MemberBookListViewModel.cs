using System;
using System.Collections.Generic;

namespace Bookish.Models
{
    public class MemberBookListViewModel
    {
         public List<Copy> CopyList { get; set; }
    
        public MemberBookListViewModel(List<Copy> list)
        {
        CopyList = list;
        }
    }
}
