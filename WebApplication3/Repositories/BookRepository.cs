using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class BookRepository
    {
        public static bool PostBook(Book book)
        {
            try
            {
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("INSERT INTO Book(Title,Description,AuthorID,IsRented) VALUES (@Title,@Description,@AuthorID,@IsRented)", connection))
                    {
                        command.Parameters.AddWithValue("@Title", book.Title.Replace("'", "\'"));
                        command.Parameters.AddWithValue("@Description", book.Description.Replace("'", "\'"));
                        command.Parameters.AddWithValue("@AuthorID", book.AuthorID);
                        command.Parameters.AddWithValue("@IsRented", book.IsRented);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw;
                return false;
            }
        }

        public static Book GetBook(int ID)
        {

            var result = "";

            try
            {
                //Configuration[]
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT * FROM Book where ID=@ID", connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);

                        var reader = command.ExecuteReader();

                        var ordID = reader.GetOrdinal("ID");
                        var ordTitle = reader.GetOrdinal("Title");
                        var ordDescription = reader.GetOrdinal("Description");
                        var ordAuthorID = reader.GetOrdinal("AuthorID");
                        var ordIsRented = reader.GetOrdinal("IsRented");
                        Book book = new Book();
                        while (reader.Read())
                        {
                            result +=
                                book.ID = reader.GetInt32(ordID); 
                                book.Title = reader.GetString(ordTitle);
                                book.Description = reader.GetString(ordDescription);
                                book.IsRented = reader.GetBoolean(ordIsRented);
                                book.AuthorID = reader.GetInt32(ordAuthorID);
                        }

                        return book;
                    }
                }
                //return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw;
                return null;
            }
        }

        public static List<Book> GetBooks(string title)
        {
            List<Book> books = new List<Book>();

            try
            {
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT * FROM Book where Title like @Title", connection))
                    {
                        command.Parameters.AddWithValue("@Title", "%" + title + "%");

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Book book = new Book();

                            book.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                            book.Title = reader.GetString(reader.GetOrdinal("Title"));
                            book.Description = reader.GetString(reader.GetOrdinal("Description"));
                            book.IsRented = reader.GetBoolean(reader.GetOrdinal("IsRented"));
                            book.AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID"));

                            books.Add(book);
                        }
                        return books;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw;
                return null;
            }
        }

        public static int GetBookID(string title)
        {
            var result = 0;
            var i = 0;

            try
            {
                //Configuration[]
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT ID FROM Book where Title like @Title", connection))
                    {
                        command.Parameters.AddWithValue("@Title", title.Replace("'", "''"));

                        var reader = command.ExecuteReader();
                        //var ordID = reader.GetOrdinal("ID");

                        while (reader.Read() && i < 1)
                        {
                                result += reader.GetInt32(0);
                                i++;
                        }
                        
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw;
                return 0;
            }
        }

        public static bool UpdateBook(int ID, Book book)
        {
             try
            {
                //Configuration[]
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE Book SET Title=@Title,Description=@Description,AuthorID=@AuthorID,IsRented=@IsRented where ID=@ID", connection))
                    {
                        command.Parameters.AddWithValue("@Title", book.Title.Replace("'", "\'"));
                        command.Parameters.AddWithValue("@Description", book.Description.Replace("'", "\'"));
                        command.Parameters.AddWithValue("@AuthorID", book.AuthorID);
                        command.Parameters.AddWithValue("@IsRented", book.IsRented);
                        command.Parameters.AddWithValue("@ID", ID);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw;
                return false;
            }
        }

        public static bool DeleteBook(int ID)
        {
            try
            {
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM Book where ID=@ID", connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        command.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        public class TestClass : IDisposable
        {
            public void Dispose()
            {
                Debug.WriteLine("Test123!!!!");
            }
        }
    }
}
