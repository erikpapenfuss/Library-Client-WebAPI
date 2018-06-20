using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication3.Models;
using WebApplication3.Processors;

namespace WebApplication3.Controllers
{
    [Route("Nationality")]
    public class NationalityController : Controller
    {
        [HttpPost]
        public bool SaveNationality(Nationality nationality)
        {
            if (nationality == null)
            {
                return false;
            }
            return NationalityProcessor.ProcessNationality(nationality);
        }

        [HttpGet("{id}")]
        public string ShowNationality(int id)
        {
            return NationalityProcessor.GetNationality(id);
        }

        [HttpGet]
        public List<Nationality> ShowNationalities(string nationalityName)
        {
            return NationalityProcessor.GetNationalities(nationalityName);
        }

        [HttpDelete("{id}")]
        public bool DeleteNationality(int id)
        {
            return NationalityProcessor.DeleteNationality(id);
        }       
    }
}