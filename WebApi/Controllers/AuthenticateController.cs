using Application.Contracts;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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



        ////KS logowaie z modelem
        //[HttpPost("login")]
        //    public async Task<IActionResult> LoginAsync([FromBody]LoginModel LoginModel)
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

        [HttpPost("recovery/{email}")]
        public async Task<IActionResult> ResetPasswordToken(string email)
        {
            try
            {
                return Ok(await _auth.ResetPasswordToken(email));
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest request)
        {
            try
            {
                return Ok(await _auth.ResetPassword(request));
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("recoveryAuth/{token}")]
        public async Task<IActionResult> CheckToken(string token)
        {
            try
            {
                return Ok(await _auth.CheckPasswordToken(token));
            }
            catch 
            {
                return BadRequest("Token is invalid");
            }
        }
    }
}

