using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationAndAuthorization.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult StatusCode(int code)
        {
            switch(code)
            {
                case 404:
                    return View("NotFound");
                default:
                    return RedirectToAction("Logout", "Authentication");
            }
        }
    }
}