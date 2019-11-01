using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_MVC.Models
{
    public class AuthorViewModel
    {
        [Key]
        public int AuthorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }

        public virtual ICollection<BookViewModel> Books { get; set; }
    }
}