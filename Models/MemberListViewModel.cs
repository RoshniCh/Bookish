using System;
using System.Collections.Generic;

namespace Bookish.Models
{
    public class MemberListViewModel
    {
        public List<Member> MemberList { get; set; }

public MemberListViewModel(List<Member> list){
    MemberList = list;
}
    }
}
