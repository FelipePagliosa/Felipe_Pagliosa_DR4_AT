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
    [Authorize]
    [RequireHttps]
    public class BookController : ApiController
    {
        BookRepository dal = new BookRepository();

        // GET: api/Book
        public IQueryable<Book> GetBooks()
        {
            return dal.ListBooks();
        }

        // GET: api/Book/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book=dal.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Book/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(BookBindingModel bookbind)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var book = new Book();
            book.BookId = bookbind.BookId;
            book.Isbn = bookbind.Isbn;
            book.Title = bookbind.Title;
            book.ano = bookbind.ano;
            if (bookbind.Authors != null)
            {
                foreach (var p in bookbind.Authors)
                {
                    var author = new Author();
                    author.AuthorId = p.AuthorId;
                    author.DataNascimento = p.DataNascimento;
                    author.Email = p.Email;
                    author.FirstName = p.FirstName;
                    author.LastName = p.LastName;
                    book.Authors.Add(author);
                }
            }
            dal.EditBookR(book);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Book
        //[ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(BookBindingModel bookbind)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = new Book();
            book.BookId = bookbind.BookId;
            book.Isbn = bookbind.Isbn;
            book.Title = bookbind.Title;
            book.ano = bookbind.ano;
            var authors = new List<Author>();
            if (bookbind.Authors != null)
            {
                foreach (var p in bookbind.Authors)
                {
                    var author = new Author();
                    author.AuthorId = p.AuthorId;
                    author.DataNascimento = p.DataNascimento;
                    author.Email = p.Email;
                    author.FirstName = p.FirstName;
                    author.LastName = p.LastName;
                }
            }

            dal.RegistrarLivro(book);

            return Ok(); /*CreatedAtRoute("DefaultApi", new { id = book.BookId }, book);*/
        }

        // DELETE: api/Book/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            dal.DeleteBookById(id);
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

        private bool BookExists(int id)
        {
            bool check = dal.CheckBookExists(id);
            return check;
        }
    }
}