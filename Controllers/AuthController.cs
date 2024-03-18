using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageLeaveAplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using BC = BCrypt.Net.BCrypt;

namespace ManageLeaveAplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly ManageLeaveAplicationContext _context;

        public AuthController(ManageLeaveAplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        public async Task<ActionResult<Employee>> PostEmployee(LoginModel model)
        {
            var user = await _context.Employees
            .FirstOrDefaultAsync(u => u.Name == model.Username)??throw new ArgumentException();;
            
            // Dummy authentication logic, replace it with your actual authentication logic
            if (model.Username == user.Name && BC.Verify(model.Password,user.Password))
            {
                HttpContext.Session.SetString("userLogin",model.ToJson());
                // Authentication successful
                return  user;
            }
            else
            {
                // Authentication failed
                return Unauthorized(new { Message = "Invalid username or password" });
            }
        }

        [HttpGet("userinfo")]
        [Consumes("application/json")]
        public IActionResult GetUserInfo()
        {
            var username = HttpContext.Session.GetString("userLogin");
            if (username != null)
            {
                return Ok(new { Username = username });
            }
            else
            {
                return Unauthorized(new { Message = "User not logged in" });
            }
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { Message = "Logout successful" });
        }


    }
}