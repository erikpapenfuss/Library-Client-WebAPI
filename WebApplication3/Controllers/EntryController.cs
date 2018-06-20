using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Processors;

//namespace WebApplication3.Controllers
//{
//    [Route("entry")]
//    public class EntryController : Controller
//    {
//        [HttpPut("put")]
//        public bool PutEntry(Entry entry)
//        {
//            if (entry == null)
//            {
//                return false;
//            }
//            return EntryProcessor.PutEntry(entry);
//        }

//        [HttpGet("get/{bookTitle}")]
//        public Entry GetEntry(string bookTitle)
//        {
//            return EntryProcessor.GetEntry(bookTitle);
//        }

//        [HttpDelete("{bookTitle}")]
//        public bool DeleteEntry(string bookTitle)
//        {
//            return EntryProcessor.DeleteEntry(bookTitle);
//        }
//    }
//}