using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClassLibrary5;  // ✅ Import RegisterRequest

namespace Fifth_Attempt.Controllers

{

    
    [Route("api/users")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private readonly UserService _userService;
        public RegisterController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest(new { Message = "Invalid input. All fields are required." });
            }

            if (!_userService.IsValidEmail(model.Email))
            {
                return BadRequest(new { Message = "Invalid email format." });
            }

            if (!_userService.IsStrongPassword(model.Password))
            {
                return BadRequest(new { Message = "Password must be at least 8 characters long and contain an uppercase letter, a number, and a special character." });
            }

            if (await _userService.UserExists(model.Email))
            {
                return Conflict(new { Message = "Email already registered." });
            }

            model.Password = _userService.HashPassword(model.Password);

            bool isUserCreated = await _userService.CreateUser(model);
            if (!isUserCreated)
            {
                return StatusCode(500, new { Message = "Error saving user to database. Please try again later." });
            }

            return Ok(new { Message = "User registered successfully!", Data = new { model.FirstName, model.LastName, model.Email, model.Role } });
        }

       
    }
}
