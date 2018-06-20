using System.Collections.Generic;
using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Processors
{
    public class BookProcessor
    {
        public static bool PostBook(Book book)
        {
            return BookRepository.PostBook(book);
        }

        public static Book GetBook(int ID)
        {
            return BookRepository.GetBook(ID);                        
        }

        public static List<Book> GetBooks(string title)
        {
            return BookRepository.GetBooks(title);
        }

        public static int GetBookID(string title)
        {
            return BookRepository.GetBookID(title);

        }

        public static bool PutBook(Book book)
        {
            if(book.ID == 0)
            {
                return PostBook(book);
            }
            else
            {
                return BookRepository.UpdateBook(book.ID,book);
            } 
        }

        public static bool DeleteBook(int ID)
        {
            return BookRepository.DeleteBook(ID);
        }
    }
}
