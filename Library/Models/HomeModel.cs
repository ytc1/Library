using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryData;

namespace Library.Models
{
    public class HomeModel : BaseViewModel
    {
      public IEnumerable<Book> Books { get; set; }

    }
}