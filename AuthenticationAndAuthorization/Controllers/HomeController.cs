using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AuthenticationAndAuthorization.Models;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationAndAuthorization.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var identity = HttpContext.User.Identities
                .Where(x => x.AuthenticationType == "Usuario").FirstOrDefault();

            var idUser = identity.Claims.Where(x => x.Type == "UserId")
                .Select(x => x.Value).FirstOrDefault();

            ViewBag.UserName = identity.Claims.Where(x => x.Type == "UserName")
                .Select(x => x.Value).FirstOrDefault();

            ViewBag.Title = "Home";
            
            return View();
        }
    }
}
