using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AuthenticationAndAuthorization.Data;
using AuthenticationAndAuthorization.Models;
using AuthenticationAndAuthorization.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAndAuthorization.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly Context _context;
        public AuthenticationController(Context context)
        {
            _context = context;
        }

        // GET
        [AllowAnonymous]
        public IActionResult Autenticar()
        {
            //verifica se o usuário já está logado
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Autenticar([Bind("USU_EMAIL, USU_SENHA")] USUARIO usuario)
        {

            //https://medium.com/@lucas.eschechola/autentica%C3%A7%C3%A3o-via-identity-no-asp-net-core-2-2-2a4eb468a8a5

            if (ModelState.IsValid)
            {
                USUARIO user = ConsultarUsuario(usuario);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("UserId", user.USU_ID.ToString()),
                        new Claim("UserName", user.USU_NOME),
                    };

                    foreach (var pu in user.PERFIL_USUARIO)
                    {
                        claims.Add(new Claim("Role", pu.PERFIL.PER_NOME));
                    }

                    ClaimsIdentity identidadeDeUsuario = new ClaimsIdentity(claims, "Usuario");
                    ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

                    var propriedadesDeAutenticacao = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTime.UtcNow.ToLocalTime().AddMinutes(20),
                        IsPersistent = true
                    };

                    HttpContext.SignInAsync
                        (CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorLogin = "Email e/ou senha incorretos";
                }
            }
            else
            {
                if (String.IsNullOrEmpty(usuario.USU_EMAIL))
                    ViewBag.ErrorEmail = "Por favor, informe o email";
                if (String.IsNullOrEmpty(usuario.USU_SENHA))
                    ViewBag.ErrorSenha = "Por favor, informe a senha";
            }

            return View();
        }

        public USUARIO ConsultarUsuario(USUARIO usuario)
        {
            usuario.USU_SENHA = MyUtil.ComputeSha256Hash(usuario.USU_SENHA);
            USUARIO user = _context.USUARIO
                .AsNoTracking()
                .Include(u => u.PERFIL_USUARIO)
                    .ThenInclude(pu => pu.PERFIL)
                .Where(u => u.USU_EMAIL == usuario.USU_EMAIL && u.USU_SENHA == usuario.USU_SENHA)
                .FirstOrDefault();
            return user;
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Autenticar");
        }
    }
}