using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Processors
{
    public class EntryProcessor
    {
        public static bool PutEntry(Entry entry)
        {
            var authorSuccess = false;
            var bookSuccess = false;
            if(entry.AuthorMiddleName == "")
            {
                entry.AuthorMiddleName = null;
            }

            Author author = new Author();

                author.FirstName = entry.AuthorFirstName;
                author.MiddleName = entry.AuthorMiddleName;
                author.LastName = entry.AuthorLastName;
                author.NationalityID = NationalityRepository.GetNationalityID(entry.AuthorNationality);            

            if (AuthorProcessor.PutAuthor(author))
            {
                authorSuccess = true;
            }

            Book book = new Book();
                book.Title = entry.Title;
                book.Description = entry.Description;
                book.AuthorID = AuthorRepository.GetAuthorID(entry.AuthorFirstName, entry.AuthorLastName);
                book.IsRented = entry.IsRented;

            if (BookProcessor.PutBook(book))
            {
                bookSuccess = true;
            }

            if(authorSuccess == true && bookSuccess == true)
            {
                return true;
            }
            else
            {
                return false;
            }

             
        }

        public static Entry GetEntry(string bookTitle)
        {
            Entry entry = new Entry();

            var bookID = BookRepository.GetBookID(bookTitle);

            Book book = BookRepository.GetBook(bookID);
            Author author = AuthorRepository.GetAuthor(book.AuthorID);

            entry.Title = bookTitle;
            entry.Description = book.Description;
            entry.IsRented = book.IsRented;
            entry.AuthorFirstName = author.FirstName;
            if(author.MiddleName.Length > 0)
            entry.AuthorMiddleName = author.MiddleName;
            entry.AuthorLastName = author.LastName;
            entry.AuthorNationality = NationalityRepository.GetNationality(author.NationalityID);

            return entry;
        }


        public static bool DeleteEntry(string bookTitle)
        {
            var BookId = BookRepository.GetBookID(bookTitle);
            var AuthorId = BookRepository.GetBook(BookId).AuthorID;

            if(BookRepository.DeleteBook(BookId));
                return true;
        }
    }
}
