using System;
using System.Collections.Generic;
using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Processors
{
    public class AuthorProcessor
    {
        public static bool ProcessAuthor(Author author)
        {
            //Processing

            return AuthorRepository.PostAuthor(author);
        }

        public static Author GetAuthor(int authorID)
        {
            return AuthorRepository.GetAuthor(authorID);
        }

        public static List<Author> GetAuthors(string lastName)
        {
            return AuthorRepository.GetAuthors(lastName);
        }

        public static int GetAuthorID(string firstName, string lastName)
        {
            return AuthorRepository.GetAuthorID(firstName, lastName);
        }

        public static bool PutAuthor(Author author)
        {
            if (author.ID == 0)
            {
                return AuthorRepository.PostAuthor(author);
            }
            else
            {
                return AuthorRepository.UpdateAuthor(author.ID, author);
            }
        }

        public static bool DeleteAuthor(int authorID)
        {
            return AuthorRepository.DeleteAuthor(authorID);
        }
    }
}
