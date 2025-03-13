using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using ClassLibrary5;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net; // ✅ Import BCrypt for hashing

namespace Fifth_Attempt.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            this._userService = userService;
        }

        // ✅ Get All Users API
        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                
                return Ok(await _userService.GetAllUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("me/{UserId}")]
        public async Task<IActionResult> GetUserById(int UserId)
        {
            try
            {
                var user = await _userService.GetUserById(UserId);
                if(user == null)
                    return NotFound("User with id not found");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
