using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace FunDooNoteProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        private readonly ILogger<UserController> logger;
        public UserController(IUserBL iuserBL, ILogger<UserController> logger)
        {
            this.iuserBL = iuserBL;
            this.logger = logger;
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = iuserBL.Registration(userRegistrationModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Registration UnSuccessful"
                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser(UserLogin userLogin)
        {
            try
            {
                var result = iuserBL.Login(userLogin);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Login Failed"
                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("Forgot")]
        public IActionResult ForgotPassword(string Email)
        {
            try
            {
                var result = iuserBL.Forgot(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Email sent Successfully", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "email not sent"
                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize]
            [HttpPost]
            [Route("Reset")]
            public IActionResult Reset(string password, string ConfirmPassword)
            {
                try
                {
                    var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                    var result = iuserBL.Reset(Email, password, ConfirmPassword);

                    if (result != null)
                    {
                        return Ok(new { success = true, message = "Password change Successfully", data = result });
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            success = false,
                            message = "Password did not Reset"
                        });
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        }
    }
