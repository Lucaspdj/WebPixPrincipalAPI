using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebPixPrincipalAPI.Controllers
{
    public class TemasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}