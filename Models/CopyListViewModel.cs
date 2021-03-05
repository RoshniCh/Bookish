using System;
using System.Collections.Generic;

namespace Bookish.Models
{
    public class CopyListViewModel
    {
        public int NumberofCopies;
        public int AvailableCopies;
        public List<Copy> TakenList { get; set; }
         public List<Copy> AvailableList { get; set; }

public CopyListViewModel(int num, int aNum, List<Copy> list, List<Copy> list2){
    NumberofCopies = num;
    AvailableCopies = aNum;
    TakenList = list;
    AvailableList = list2;

}
    }
}