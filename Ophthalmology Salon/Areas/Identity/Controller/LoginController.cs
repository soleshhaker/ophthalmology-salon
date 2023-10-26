using DataAccess.Migrations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Ophthalmology.Models;
using System.Security.Claims;

namespace OphthalmologySalon.Areas.Identity.Controller
{
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        /// <summary>Log in into an account</summary>
        /// <param name="email">Admin: admin@admin.com Doctor: doctor@doctor.com Customer: customer@customer.com</param>
        /// <param name="password">Admin: Asd123! Doctor: Doc123! Customer: Customer123!</param>
        /// <returns>Returns true if login is successful, false otherwise.</returns>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false).GetAwaiter().GetResult();
            return Ok(result.Succeeded);
        }

        /// <summary>Log out of an account</summary>
        [HttpPost("Logout")]
        public void Logout()
        {
            _signInManager.SignOutAsync();
        }

        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}