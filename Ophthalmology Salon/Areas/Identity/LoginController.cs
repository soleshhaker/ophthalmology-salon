using DataAccess.Migrations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ophthalmology.Models;
using System.Security.Claims;

namespace OphthalmologySalon.Areas.Identity
{
    public class LoginController
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        /// <param name="email">Admin: admin@admin.com</param>
        /// <param name="password">Admin: Asd123!</param>
        /// <returns>Returns true if login is successful, false otherwise.</returns>
        [HttpPost("Login")]
        public bool Login(string email, string password)
        {
            var result = _signInManager.PasswordSignInAsync(email, password, false, false).GetAwaiter().GetResult();
            return result.Succeeded;
        }
        [HttpPost("Logout")]
        public void Logout()
        {
            _signInManager.SignOutAsync();
        }
    }
}