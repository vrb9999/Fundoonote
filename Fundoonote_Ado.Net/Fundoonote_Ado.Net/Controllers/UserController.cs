using BusinessLayer.Interface;
using DatabaseLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fundoonote_Ado.Net.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult AddUser(UsersModel users)
        {
            try
            {
                this.userBL.AddUser(users);
                return Ok(new { success = true, Message = "User Registration Sucessfull" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<UserResponseModel> users = new List<UserResponseModel>();
                users = this.userBL.GetAllUsers();
                return Ok(new { success = true, Message = "All Users fetch successfully", data = users });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("Login")]
        public IActionResult LoginUser(LoginUserModel user)
        {
            try
            {
                string result = this.userBL.LoginUser(user);
                return Ok(new { success = true, Message = "Token Generated successfully", data = result });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("ForgetPasswordUser")]
        public IActionResult ForgetPasswordUser(string email)
        {
            try
            {
                bool result = this.userBL.ForgetPasswordUser(email);
                return Ok(new { success = true, Message = "Reset Password Link Send successfully", data = result });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(PasswordModel modelPassword)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                //var email = claims.Where(p => p.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").FirstOrDefault()?.Value;
                var email = claims.Where(p => p.Type == @"Email").FirstOrDefault()?.Value;
                bool result = this.userBL.ResetPassoword(email, modelPassword);
                return Ok(new { success = true, Message = $"{email} your Password Updated successfully!" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
