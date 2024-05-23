using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car_Project.Models;
using System.Net.Http;

namespace Car_Project.Controllers
{
    public class Car : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }
        public IActionResult image()
        {
            return View();
        }
        public IActionResult car_Bookking()
        {
            var deloreans = new List<Model>
        {
                new Model { DeloreanModel = "--select--",  },
            new Model { DeloreanModel = "DMC-12", Deloreanprice = 35000000 },
            new Model { DeloreanModel = "DMC-12 (Gold Plated)", Deloreanprice = 85000000 },
            new Model { DeloreanModel = "DMC-12 (Time Machine)", Deloreanprice = 15000000 },
             new Model { DeloreanModel = " Alpha5 Plasmatail", Deloreanprice = 25000000 }
        };
            //ViewBag.Deloreans = deloreans;
            return View(deloreans);
        }
       public IActionResult Login()
        {
            
            return View();
        }
        [Route("Login_user_details")]
        public IActionResult Login_user_details(string email)
        {
            ViewData["eamil"] = email.ToString();
            return View();
        }
    }
}
