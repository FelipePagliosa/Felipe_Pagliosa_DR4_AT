using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using BLL;

namespace DAL
{
    public class BookRepository
    {
        private DataContext db = new DataContext();

        public IQueryable<Book> ListBooks()
        {
            return db.Books;
        }

        public Book GetBookById(int id)
        {
            Book book = db.Books.Find(id);
            return book;
        }

        public void EditBookR(Book book)
        {
            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void RegistrarLivro(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }

        public void DeleteBookById(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public void Disposing()
        {
            db.Dispose();
        }

        public bool CheckBookExists(int id)
        {
            return db.Books.Count(e => e.BookId == id) > 0;
        }
    }
}
