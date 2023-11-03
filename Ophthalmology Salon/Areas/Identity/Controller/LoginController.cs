using AutoMapper;
using DataAccess.Migrations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using NuGet.Protocol;
using Ophthalmology.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OphthalmologySalon.Areas.Identity.Controller
{
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        public LoginController(SignInManager<IdentityUser> signInManager, IMapper mapper, UserManager<IdentityUser> userManager, JwtHandler jwtHandler)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }
        /// <summary>Log in into an account</summary>
        /// <param name="username">Admin: admin Doctor: doctor Customer: customer</param>
        /// <param name="password">Admin: Asd123! Doctor: Doc123! Customer: Customer123!</param>
        /// <returns>Returns true if login is successful, false otherwise.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

        /// <summary>Log out of an account</summary>
        [HttpPost("Logout")]
        public void Logout()
        {
            _signInManager.SignOutAsync();
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<ApplicationUser>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(errors);
            }

            return StatusCode(201);
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}