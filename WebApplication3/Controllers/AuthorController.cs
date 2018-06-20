using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication3.Models;
using WebApplication3.Processors;

namespace WebApplication3.Controllers
{
    [Route("Author")]
    public class AuthorController : Controller
    {

        [HttpPost]
        public bool AddAuthor(Author author)
        {
            if (author == null)
            {
                return false;
            }
            return AuthorProcessor.ProcessAuthor(author);
        }

        [HttpGet("{id}")]
        public Author GetAuthor(int id)
        {
            return AuthorProcessor.GetAuthor(id);
        }
        [HttpGet]
        public List<Author> GetAuthors(string lastName)
        {
            return AuthorProcessor.GetAuthors(lastName);
        }

        //[HttpGet("getId/{firstName}+{lastName}")]
        //public int ShowAuthorID(string firstName, string lastName)
        //{
        //    return AuthorProcessor.GetAuthorID(firstName, lastName);
        //}

        [HttpPut]
        public bool PutAuthor([FromBody] Author author)
        {
            return AuthorProcessor.PutAuthor(author);
        }

        [HttpDelete("{id}")]
        public bool DeleteAuthor(int id)
        {
            return AuthorProcessor.DeleteAuthor(id);
        }
    }
}