using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OMSv2.Service.Entity;
using System.Security.Cryptography;
using OMSv2.Service.Helpers;
using OMSv2.Service.DataAccess;

namespace OMSv2.Service.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                // Check if username already exists
                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    return BadRequest(new { message = "Username already exists" });
                }

                // Create new user
                var newUser = new User
                {
                    ClientID = model.ClientID,
                    Username = model.Username,
                    PasswordHash = HashPassword(model.Password), // Implement HashPassword method
                    IsActive = true,
                    UserId = Guid.NewGuid(),
                };

                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();

                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (user == null || !VerifyPassword(model.Password, user.PasswordHash)) // Implement VerifyPassword method
            {
                return Unauthorized();
            }
            // Check if user is already logged in
            if (!string.IsNullOrEmpty(user.SessionToken))
            {
                return BadRequest(new { message = "User is already logged in from another session" });
            }

            await _dbContext.SaveChangesAsync();

            // Generate JWT token
            var token = GenerateJwtToken(user.UserId);

            // Store the session token
            user.SessionToken = token;
            await _dbContext.SaveChangesAsync();

            ClientData clientData = new ClientData();
            var apiKey = clientData.GetApiKey(user.ClientID);
            return Ok(new { token, apiKey });
        }

        private string GenerateJwtToken(Guid userId)
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userId.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Other methods for logout, check session, etc.
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(Guid userId)
        {
            //var userId = GetUserIdFromContext();
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            user.SessionToken = null;
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Logged out successfully" });
        }

        private Guid GetUserIdFromContext()
        {
            var user = HttpContext.Items["User"] as User;
            return user?.UserId ?? Guid.Empty;
        }

    }

}