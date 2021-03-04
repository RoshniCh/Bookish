using System;
using System.Collections.Generic;

namespace Bookish.Models
{
    public class BookListViewModel
    {
        public List<Book> BookList { get; set; }

public BookListViewModel(List<Book> list){
    BookList = list;
}
    }
}
