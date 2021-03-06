using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using CourseProj.Data;
using CourseProj.Views_Models;
using CourseProj.Data.Models;
using System.Linq;
using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CourseProj.Controllers
{
    public class AccountController:Controller
    {
        private readonly DBContent dBContent;
        public AccountController(DBContent dBContent)
        {
            this.dBContent = dBContent;
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = dBContent.User.FirstOrDefault(u => u.Email == model.Email);
                if (user == null)
                {
                    user = new User();
                    user.salt = GenerateSalt();
                    user.Password = HashUserPassword(model.Password, user.salt);
                    user.Unblocked = true;
                    user.Email = model.Email;
                    Role userRole = dBContent.Role.FirstOrDefault(r => r.Name == "user");
                    if (userRole != null) user.Role = userRole;
                    dBContent.User.Add(user);
                    dBContent.SaveChanges();
                    await Authenticate(user); 
                    return RedirectToAction("MainPage", "Users", new { userID = user.ID });
                }
                else
                    ModelState.AddModelError("", "Wrong mail or password");
            }
            return View(model);
        }

        private byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            var salt = new byte[64 / 8];
            rng.GetBytes(salt);
            return salt;
        }
        private string HashUserPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 128 / 8));
                return hashed;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = dBContent.User.Include(u => u.Role).FirstOrDefault(u => u.Email == model.Email);
                if (user!=null && user.Password == HashUserPassword(model.Password,user.salt))
                {
                    await Authenticate(user);
                    return RedirectToAction("MainPage", "Users", new {userID = user.ID });
                }
                ModelState.AddModelError("", "Wrong login or password");
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)};
            ClaimsIdentity id = new(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn", "Account");
        }
    }
}
