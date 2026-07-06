using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyPractice2.Data;
using MyPractice2.DTO.Auth;
using MyPractice2.DTO.User;
using MyPractice2.Migrations;
using MyPractice2.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace MyPractice2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly APIDbContext db;
        private readonly IConfiguration _configuration;

        public AuthController(APIDbContext context, IConfiguration configuration)
        {
            db = context;
            _configuration = configuration;
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterDTO dto) {
            var emailExists = await db.Users
              .AnyAsync(x => x.Email == dto.Email);

            if (emailExists)
            {
                return BadRequest("Email already exists.");
            }
            var hasher = new PasswordHasher<User>();

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
            };

            user.Password = hasher.HashPassword(user,dto.Password);
            db.Users.Add(user);

            await db.SaveChangesAsync();
            return Ok(new
            {
                Message = "Registered",
                Data = user
            });

        }


        [HttpPost("login")]
        //[Route("login")]

        public async Task<IActionResult> Login(LoginDTO dto) {
            var user = await db.Users.
               FirstOrDefaultAsync(x => x.Email == dto.EmailDTO);
            if (user == null) {
                return BadRequest("Email not found!");
            }
            var hasher = new PasswordHasher<User>();

            var verify = hasher.VerifyHashedPassword(
            user,
            user.Password,
            dto.PasswordDTO
            );
            var showUserData = new {
            user.FullName, user.Email,
            };

            if (verify == PasswordVerificationResult.Failed)
            {
                return BadRequest("Invalid Email or Password");
            }

            var token = GenerateToken(user);

            return Ok(new {
            Message="Login Sucess!",
            Token = token,
            Data = showUserData,
            });

        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAll() {
            var user = await db.Users
            .Include(x => x.UserColors)
            .ThenInclude(x => x.Color)
            .Select(x => new
            {
                x.Id,
                x.FullName,
                Colors = x.UserColors.Select(c => c.Color.ColorName)
            }
            ).ToListAsync();
            return Ok(new { 
            Data= user
            });
        
        }


        [HttpPut("updateuser/{id}")]

        public async Task<IActionResult> Put(int id, UpdateUserDTO dto)
        {
            var user = await db.Users
             .Include(x=>x.UserColors)
             .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            user.FullName = dto.FullName;
            db.UserColors.RemoveRange(user.UserColors);

            foreach (var colorId in dto.ColorsIds)
            {
                user.UserColors.Add(new UserColor
                {
                    UserId = id,
                    ColorId = colorId
                });
            }
            await db.SaveChangesAsync();


//view color belongs to user after updated
            var result = await db.Users
              .Include(x => x.UserColors)
              .ThenInclude(x => x.Color)
              .Where(x => x.Id == id)
              .Select(x => new
              {
                  x.Id,
                  x.FullName,
                  Colors = x.UserColors.Select(c => c.Color.ColorName)
              }).FirstOrDefaultAsync();
            return Ok(new
            {
                Message = "User updated!",
                Data = result
            });

        }
    }
}
