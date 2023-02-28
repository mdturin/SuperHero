using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JwtWebApiTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(
            IConfiguration configuration, 
            IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet, Authorize]
        public ActionResult<object> GetMe()
        {
            //string? username1 = User?.Identity?.Name;
            //string? username2 = User?.FindFirstValue(ClaimTypes.Name);

            string username = _userService.GetMyName();
            string role = _userService.GetMyRole();
            return Ok(new { username, role });
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            CreatePasswordHash(
                request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if (user.Username != request.Username)
                return BadRequest("User not found!");

            if (!VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Wrong Password!");

            return Ok(CreateToken(user));
        }

        #region Private Methods
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Noob"),
            };

            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(
            string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(
            string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(password));

                return computeHash.SequenceEqual(passwordHash);
            }
        }

        #endregion
    }
}
