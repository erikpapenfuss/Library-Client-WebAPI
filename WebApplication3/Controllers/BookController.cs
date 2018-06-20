using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication3.Models;
using WebApplication3.Processors;

namespace WebApplication3.Controllers
{
    [Route("Book")]
    public class BookController : Controller
    {

        [HttpPost]
        public bool PostBook(Book book)
        {
            if (book == null)
            {
                return false;
            }
            return BookProcessor.PostBook(book);
        }

        [HttpGet("{id}")]
        public Book GetBook(int id)
        {
            return BookProcessor.GetBook(id);
        }

        [HttpGet]
        public List<Book> GetBooks(string title)
        {
            return BookProcessor.GetBooks(title);
        }

        //[HttpGet("{title}")]
        //public int GetBookID(string title)
        //{
        //    return BookProcessor.GetBookID(title);
        //}

        [HttpPut]
        public bool PutBook([FromBody] Book book)
        {
            return BookProcessor.PutBook(book);
        }

        //[HttpPut("updateRentStatus/")]
        //public bool UpdateRentStatus(string title)
        //{
        //    return BookProcessor.UpdateRentStatus(title);
        //}

        [HttpDelete("{id}")]
        public bool DeleteBook(int id)
        {
            return BookProcessor.DeleteBook(id);
        }
    }
}