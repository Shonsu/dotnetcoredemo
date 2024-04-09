using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebMVCDemo.Controllers
{

    public class WikiController : Controller
    {

        public IActionResult Index(string path)
        {
            ViewData["Path"] = path;
            return View();
        }
    }
}