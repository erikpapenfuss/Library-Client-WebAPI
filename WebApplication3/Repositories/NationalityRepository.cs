using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class NationalityRepository
    {
        public static bool AddNationalityToDB(Nationality nationality)
        {
            //svar connectionString = "Data Source=F64JL82;Initial Catalog=Library;Integrated Security=True";


            var query = "INSERT INTO Nationality(NationalityName,CountryName) VALUES ('@NationalityName','@CountryName')";

            query = query.Replace("@NationalityName", nationality.NationalityName).Replace("@CountryName", nationality.CountryName);

            try
            {
                //Configuration[]
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;
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

        public static string GetNationality(int ID)
        {

            var result = "";

            try
            {
                //Configuration[]
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT * FROM Nationality where ID=@ID", connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);

                        var reader = command.ExecuteReader();

                        var ordNationalityName = reader.GetOrdinal("NationalityName");
                        var ordCountryName= reader.GetOrdinal("CountryName");
                        while (reader.Read())
                        {                            
                            result +=
                            reader.GetString(ordNationalityName) + ", " +
                            reader.GetString(ordCountryName);                                
                        }
                        return result;
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

        public static List<Nationality> GetNationalities(string nationalityName)
        {

            List<Nationality> nationalities = new List<Nationality>();

            try
            {
                //Configuration[]
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT * FROM Nationality where NationalityName like @NationalityName", connection))
                    {
                        command.Parameters.AddWithValue("@NationalityName", "%" + nationalityName + "%");

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Nationality nationality = new Nationality
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                NationalityName = reader.GetString(reader.GetOrdinal("NationalityName")),
                                CountryName = reader.GetString(reader.GetOrdinal("CountryName"))
                            };
                            nationalities.Add(nationality);
                        }
                        return nationalities;
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

        public static int GetNationalityID(string nationalityName)
        {
            var result = 0;
            var i = 0;

            try
            {
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT ID FROM Nationality where NationalityName like @NationalityName", connection))
                    {
                        command.Parameters.AddWithValue("@NationalityName", nationalityName);

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

        public static bool RemoveNationalityFromDB(int ID)
        {

            try
            {
                //Configuration[]
                using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM Nationality where ID=@ID", connection))
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


    public class TestClass : IDisposable
    {
        public void Dispose()
        {
            Debug.WriteLine("Test123!!!!");
        }
    }
}
