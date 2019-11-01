using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_API.Models
{
    public class AuthorBindingModel
    {
        [Key]
        public int AuthorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }

        public virtual ICollection<BookBindingModel> Books { get; set; }
    }
}