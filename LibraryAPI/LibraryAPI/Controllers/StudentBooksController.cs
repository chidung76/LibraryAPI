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
    public class StudentBooksController : ApiController
    {
        private LibraryContext db = new LibraryContext();
        // Get all Student Book
        [HttpGet()]
        public IHttpActionResult GetStdBook()
        {
            IHttpActionResult ret = null;
            List<StudentBook> list = new List<StudentBook>();
            list = db.StudentBooks.ToList();

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

        // Get sigle Student Book
        [HttpGet()]
        public IHttpActionResult GetStdBook(int id)
        {
            IHttpActionResult ret;
            StudentBook stdBook = new StudentBook();
            stdBook = db.StudentBooks.Where(p => p.BorrowId == id).First();

            if (stdBook == null)
            {
                ret = NotFound();
            }
            else
            {
                ret = Ok(stdBook);
            }

            return ret;
        }

        // Add new Student Book
        [HttpPost()]
        public IHttpActionResult Post(StudentBook stdBook)
        {
            IHttpActionResult ret = null;
            stdBook = db.StudentBooks.Add(stdBook);
            db.SaveChanges();

            if (stdBook != null)
            {
                ret = Created<StudentBook>(Request.RequestUri +
                     stdBook.BorrowId.ToString(), stdBook);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        // Update Book
        [HttpPut()]
        public IHttpActionResult Put(int id, StudentBook stdBook)
        {
            IHttpActionResult ret = null;
            db.Entry(stdBook).State = EntityState.Modified;
            db.SaveChanges();

            if (stdBook != null)
            {
                ret = Ok(stdBook);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        // Delete Book
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult ret = null;
            StudentBook stdBook = new StudentBook();

            stdBook = db.StudentBooks.Where(p => p.BorrowId == id).First();
            db.StudentBooks.Remove(stdBook);
            db.SaveChanges();

            if (stdBook != null)
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
