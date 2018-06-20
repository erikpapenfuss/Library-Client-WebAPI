using System;
using System.Collections.Generic;
using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Processors
{
    public class NationalityProcessor
    {
        public static bool ProcessNationality(Nationality country)
        {
            //Processing

            return NationalityRepository.AddNationalityToDB(country);
        }

        public static String GetNationality(int countryID)
        {
            return NationalityRepository.GetNationality(countryID);
        }

        public static List<Nationality> GetNationalities(string nationalityName)
        {
            return NationalityRepository.GetNationalities(nationalityName);
        }

        public static bool DeleteNationality(int countryID)
        {
            return NationalityRepository.RemoveNationalityFromDB(countryID);
        }
    }
}
