using Application.Contracts;
using Application.Models;
using Application.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {

        private readonly IAuthenticateService _auth;
        public AuthenticateController(IAuthenticateService auth)
        {
            _auth = auth;
        }


        //KS Model na potrzeby logowania
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(string email, string password)
        {
            try
            {
                var request = new AuthenticationRequest(email, password);
                return Ok(await _auth.AuthenticateAsync(request));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            }



            //KS logowaie z modelem
            //[HttpPost("login")]
            //public async Task<IActionResult> LoginAsync(LoginModel LoginModel)
            //{
            //    try
            //    {
            //        var request = new AuthenticationRequest(LoginModel.Username, LoginModel.Password);
            //        return Ok(await _auth.AuthenticateAsync(request));
            //    }
            //    catch (Exception e)
            //    {
            //        return BadRequest(e.Message);
            //    }
            //}


            [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest user)
        {
            try
            {
                return Ok(await _auth.RegisterAsync(user));
             } catch (Exception e)
            {
                return BadRequest(e.Message);
    }
}


    }
}

