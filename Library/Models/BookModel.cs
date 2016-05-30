using LibraryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BookModel : BaseViewModel
    {
        public Book Book { get; set; } 
    }
}