using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LatihanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Pokedex()
        {
            return View();
        }
        public IActionResult Employee()
        {
            return View();
        }
    }
}
