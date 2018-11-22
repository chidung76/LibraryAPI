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
    public class StudentsController : ApiController
    {
        private LibraryContext db = new LibraryContext();
        // Get all Student
        [HttpGet()]
        public IHttpActionResult GetStudent()
        {
            IHttpActionResult ret = null;
            List<Student> list = new List<Student>();
            list = db.Students.ToList();

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
        public IHttpActionResult GetStudent(string id)
        {
            IHttpActionResult ret;
            Student student = new Student();
            student = db.Students.Where(p => p.StudentId == id).First();

            if (student == null)
            {
                ret = NotFound();
            }
            else
            {
                ret = Ok(student);
            }

            return ret;
        }

        // Add new Book
        [HttpPost()]
        public IHttpActionResult Post(Student student)
        {
            IHttpActionResult ret = null;
            student = db.Students.Add(student);
            db.SaveChanges();

            if (student != null)
            {
                ret = Created<Student>(Request.RequestUri +
                     student.StudentId.ToString(), student);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        // Update Book
        [HttpPut()]
        public IHttpActionResult Put(string id, Student student)
        {
            IHttpActionResult ret = null;
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();

            if (student != null)
            {
                ret = Ok(student);
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
            Student student = new Student();

            student = db.Students.Where(p => p.StudentId == id).First();
            db.Students.Remove(student);
            db.SaveChanges();

            if (student != null)
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
