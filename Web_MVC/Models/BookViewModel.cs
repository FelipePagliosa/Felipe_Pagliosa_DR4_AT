using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_MVC.Models
{
    public class BookViewModel
    {
        [Key]
        public int BookId { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string ano { get; set; }

        public virtual ICollection<AuthorViewModel> Authors { get; set; }
    }
}