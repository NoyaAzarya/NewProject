using ClassLibrary5;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;



[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly UserService _userService;

    public LoginController(UserService userService)
    {
        _userService = userService;
    }
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] ClassLibrary5LoginRequest request)
    {
        Console.WriteLine($"🔹 Login Request Received: {request.Email}");

        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Email and password are required." });
        }


        var user = await _userService.AuthenticateUser(request);

        if (user == null)
        {
            Console.WriteLine($"❌ Login Failed: User '{request.Email}' not found or password incorrect.");
            return Unauthorized(new { message = "אימייל או סיסמה שגויים!" });
        }

        Console.WriteLine($"✅ Login Successful: {user.Email}");
        return Ok(user);
    }


}
