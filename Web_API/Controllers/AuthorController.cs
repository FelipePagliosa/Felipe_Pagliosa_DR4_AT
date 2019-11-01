using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BLL;
using DAL;
using Web_API.Models;


namespace Web_API.Controllers
{
    public class AuthorController : ApiController
    {
        AuthorRepository dal = new AuthorRepository();
        // GET: api/Author
        public IQueryable<Author> GetAuthors()
        {
            return dal.ListAuthors();
        }

        // GET: api/Author/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult GetAuthor(int id)
        {
            Author author = dal.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/Author/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuthor(AuthorBindingModel authorbind)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var author = new Author();
            author.AuthorId = authorbind.AuthorId;
            author.FirstName = authorbind.FirstName;
            author.LastName = authorbind.LastName;
            author.Email = authorbind.Email;
            author.DataNascimento = authorbind.DataNascimento;
            if (authorbind.Books != null)
            {
                foreach(var p in authorbind.Books)
                {
                    var book = new Book();
                    book.BookId = p.BookId;
                    book.Isbn = p.Isbn;
                    book.Title = p.Title;
                    book.ano = p.ano;
                    author.Books.Add(book);
                }                     
            }

            dal.EditAuthorR(author);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Author
        [ResponseType(typeof(Author))]
        public IHttpActionResult PostAuthor(AuthorBindingModel authorbind)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = new Author();
            author.AuthorId = authorbind.AuthorId;
            author.FirstName = authorbind.FirstName;
            author.LastName = authorbind.LastName;
            author.Email = authorbind.Email;
            author.DataNascimento = authorbind.DataNascimento;
            if (authorbind.Books != null)
            {
                foreach (var p in authorbind.Books)
                {
                    var book = new Book();
                    book.BookId = p.BookId;
                    book.Isbn = p.Isbn;
                    book.Title = p.Title;
                    book.ano = p.ano;
                    author.Books.Add(book);
                }
            }

            dal.RegistrarAuthor(author);

            return Ok(); /*CreatedAtRoute("DefaultApi", new { id = author.AuthorId }, author);*/
        }

        // DELETE: api/Author/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult DeleteAuthor(int id)
        {
            dal.DeleteAuthorById(id);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dal.Disposing();
            }
            base.Dispose(disposing);
        }

        private bool AuthorExists(int id)
        {
            bool check = dal.CheckAuthorExists(id);
            return check;
        }
    }
}