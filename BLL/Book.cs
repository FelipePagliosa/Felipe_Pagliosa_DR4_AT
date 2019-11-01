using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string ano { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
