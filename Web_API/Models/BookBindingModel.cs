using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_API.Models
{
    public class BookBindingModel
    {
        [Key]
        public int BookId { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string ano { get; set; }

        public virtual ICollection<AuthorBindingModel> Authors { get; set; }
    }
}
