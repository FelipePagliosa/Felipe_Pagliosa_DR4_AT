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
    public class AuthorRepository
    {
        private DataContext db = new DataContext();

        public IQueryable<Author> ListAuthors()
        {
            return db.Authors;
        }

        public Author GetAuthorById(int id)
        {
            Author author = db.Authors.Find(id);
            return author;
        }

        public void EditAuthorR(Author author)
        {
            db.Entry(author).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void RegistrarAuthor(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();
        }

        public void DeleteAuthorById(int id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public void Disposing()
        {
            db.Dispose();
        }

        public bool CheckAuthorExists(int id)
        {
            return db.Authors.Count(e => e.AuthorId == id) > 0;
        }
    }
}
