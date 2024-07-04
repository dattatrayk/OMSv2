using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using OMSv2.Service.Entity;
using System.Security.Cryptography;
using OMSv2.Service.Helpers;
using OMSv2.Service.DataAccess;
using System.IdentityModel.Tokens.Jwt;

namespace OMSv2.Service.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public AccountController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {

                var apiKeyHelper = new ApiKeyHelper();
                var apiKey = apiKeyHelper.GetApiKey(Request);
                var isValidApiKey = apiKeyHelper.IsValidAPI(apiKey);

                if (!isValidApiKey)
                {
                    return BadRequest(new { message = "Invalid API Key" });
                }
                // Check if username already exists
                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.ClientID == apiKeyHelper.ClientID);
                if (existingUser != null)
                {
                    return BadRequest(new { message = "Username already exists" });
                }
                // Create new user
                var newUser = new User
                {
                    ClientID = apiKeyHelper.ClientID,
                    Username = model.Username,
                    PasswordHash = HashPassword(model.Password),
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
        public async Task<IActionResult> Login(Login model)
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
            return Ok(new { token, apiKey, user.UserId, user.ClientID });
        }

        private string GenerateJwtToken(Guid userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingProvider.GetSecretKey()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: AppSettingProvider.GetIssuer(),
                audience: AppSettingProvider.GetAudience(),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
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
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            user.SessionToken = null;
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Logged out successfully" });
        }

       

    }

}