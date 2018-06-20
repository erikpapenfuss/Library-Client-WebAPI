using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class AuthorRepository
    {
        public static bool PostAuthor(Author author)
        {
            try
            {
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();

                    if(author.MiddleName != null)
                        using (var command = new SqlCommand("INSERT INTO Author(FirstName,MiddleName,LastName,NationalityID) VALUES (@FirstName,@MiddleName,@LastName,@NationalityID)", connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", author.FirstName.Replace("'", "\'"));
                            command.Parameters.AddWithValue("@MiddleName", author.MiddleName.Replace("'", "\'"));
                            command.Parameters.AddWithValue("@LastName", author.LastName.Replace("'", "\'"));
                            command.Parameters.AddWithValue("@NationalityID", author.NationalityID);
                            command.ExecuteNonQuery();
                            return true;
                        }
                    else
                        using (var command = new SqlCommand("INSERT INTO Author(FirstName,LastName,NationalityID) VALUES (@FirstName,@LastName,@NationalityID)", connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", author.FirstName.Replace("'", "\'"));
                            command.Parameters.AddWithValue("@LastName", author.LastName.Replace("'", "\'"));
                            command.Parameters.AddWithValue("@NationalityID", author.NationalityID);
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


        public static List<Author> GetAuthors(string lastName)
        {
            List<Author> authors = new List<Author>();

            try
            {
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT * FROM Author where LastName like @LastName", connection))
                    {
                        command.Parameters.AddWithValue("@LastName", "%" + lastName + "%");

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader.IsDBNull(reader.GetOrdinal("MiddleName")))
                            {
                                Author author = new Author();
                                author.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                                author.FirstName = reader.GetString(reader.GetOrdinal("FirstName"))
                                    //.Replace("''", "'")
                                    ;
                                author.MiddleName = null;
                                author.LastName = reader.GetString(reader.GetOrdinal("LastName"))
                                    //.Replace("''", "'")
                                    ;
                                author.NationalityID = reader.GetInt32(reader.GetOrdinal("NationalityID"));
                                authors.Add(author);
                            }
                            else
                            {
                                Author author = new Author();
                                author.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                                author.FirstName = reader.GetString(reader.GetOrdinal("FirstName"))
                                    //.Replace("''","'")
                                    ;
                                author.MiddleName = reader.GetString(reader.GetOrdinal("MiddleName"))
                                    //.Replace("''", "'")
                                    ;
                                author.LastName = reader.GetString(reader.GetOrdinal("LastName"))
                                    //.Replace("''", "'")
                                    ;
                                author.NationalityID = reader.GetInt32(reader.GetOrdinal("NationalityID"));
                                authors.Add(author);
                            }                            
                        }
                    }
                }
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw;
                return null;
            }

            return authors;
        }

        public static Author GetAuthor(int ID)
        {
            try
            {
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT * FROM Author where ID=@ID", connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);

                        var reader = command.ExecuteReader();
                        Author author = new Author();
                        while (reader.Read())
                        {

                            if (reader.IsDBNull(reader.GetOrdinal("MiddleName")))
                            {
                                author.ID = ID;
                                author.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                author.MiddleName = "";
                                author.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                author.NationalityID = reader.GetInt32(reader.GetOrdinal("NationalityID"));
                            }
                            else
                            {
                                author.ID = ID;
                                author.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                author.MiddleName = reader.GetString(reader.GetOrdinal("MiddleName"));
                                author.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                author.NationalityID = reader.GetInt32(reader.GetOrdinal("NationalityID"));
                            }
                                
                        }

                        return author;
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

        public static int GetAuthorID(string firstName, string lastName)
        {
            var result = 0;
            var i = 0;

            try
            {
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT ID FROM Author where FirstName like @FirstName and LastName like @LastName", connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName.Replace("'","''"));
                        command.Parameters.AddWithValue("@LastName", lastName.Replace("'", "''"));

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

        public static bool UpdateAuthor(int ID, Author author)
        {
            try
            {
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    if (author.MiddleName != null)
                    {
                        using (var command = new SqlCommand("UPDATE Author SET FirstName=@FirstName,MiddleName=@MiddleName,LastName=@LastName,NationalityID=@NationalityID where ID=@ID", connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", author.FirstName.Replace("'", "''"));
                            command.Parameters.AddWithValue("@MiddleName", author.MiddleName.Replace("'", "''"));
                            command.Parameters.AddWithValue("@LastName", author.LastName.Replace("'", "''"));
                            command.Parameters.AddWithValue("@NationalityID", author.NationalityID);
                            command.Parameters.AddWithValue("@ID", ID);
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (var command = new SqlCommand("UPDATE Author SET FirstName=@FirstName,LastName=@LastName,NationalityID=@NationalityID where ID=@ID", connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", author.FirstName.Replace("'", "''"));
                            command.Parameters.AddWithValue("@LastName", author.LastName.Replace("'", "''"));
                            command.Parameters.AddWithValue("@NationalityID", author.NationalityID);
                            command.Parameters.AddWithValue("@ID", ID);
                            command.ExecuteNonQuery();
                        }
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

        public static bool DeleteAuthor(int ID)
        {

            try
            {
                //Configuration[]
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM Author where ID=@ID", connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        command.ExecuteNonQuery();

                        return true;
                    }
                }
                //return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw;
                return false;
            }
        }
    }
}
