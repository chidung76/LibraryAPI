using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LibraryAPI.Models;
using System.Data.Entity;

namespace LibraryAPI.Controllers
{
    public class BooksController : ApiController
    {
        private LibraryContext db = new LibraryContext();
        // Get all Book
        [HttpGet()]
        public IHttpActionResult GetBook()
        {
            IHttpActionResult ret = null;
            List<Book> list = new List<Book>();
            list = db.Books.ToList();

            if (list.Count > 0)
            {
                ret = Ok(list);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        // Get sigle Book
        [HttpGet()]
        public IHttpActionResult GetBook(string id)
        {
            IHttpActionResult ret;
            Book book = new Book();
            book = db.Books.Where(p => p.CallNumber == id).First();

            if (book == null)
            {
                ret = NotFound();
            }
            else
            {
                ret = Ok(book);
            }

            return ret;
        }

        // Add new Book
        [HttpPost()]
        public IHttpActionResult Post(Book book)
        {
            IHttpActionResult ret = null;
            book = db.Books.Add(book);
            db.SaveChanges();

            if (book != null)
            {
                ret = Created<Book>(Request.RequestUri +
                     book.CallNumber.ToString(), book);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        // Update Book
        [HttpPut()]
        public IHttpActionResult Put(string id, Book book)
        {
            IHttpActionResult ret = null;
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();

            if (book != null)
            {
                ret = Ok(book);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        // Delete Book
        [HttpDelete()]
        public IHttpActionResult Delete(string id)
        {
            IHttpActionResult ret = null;
            Book book = new Book();

            book = db.Books.Where(p => p.CallNumber == id).First();
            db.Books.Remove(book);
            db.SaveChanges();

            if (book != null)
            {
                ret = Ok(true);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
