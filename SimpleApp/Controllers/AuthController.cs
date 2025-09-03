using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleApp.Brokers.Storages;
using SimpleApp.Models.Users;
using SimpleApp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:Controller
    {
        private readonly StorageBroker broker;
        private readonly JwtService jwt;
        private readonly IPasswordHasher<User> passwordHasher;

        public AuthController(
            StorageBroker broker, 
            JwtService jwt, 
            IPasswordHasher<User> passwordHasher)
        {
            this.broker = broker;
            this.jwt = jwt;
            this.passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await broker.Users.AnyAsync(u => u.Username == dto.Username || u.Email == dto.Email))
                return BadRequest(new { message = "Username yoki Email allaqachon mavjud." });

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email
            };
            user.PasswordHash = this.passwordHasher.HashPassword(user, dto.Password);

            broker.Users.Add(user);
            await broker.SaveChangesAsync();

            return Ok(new { message = "Ro‘yxatdan o‘tish muvaffaqiyatli." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await broker.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null) return Unauthorized(new { message = "Login yoki parol noto‘g‘ri." });

            var verify = this.passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (verify == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Login yoki parol noto‘g‘ri." });

            var access = this.jwt.GenerateAccessToken(user);
            var refresh = this.jwt.GenerateRefreshToken();

            user.RefreshTokens.Add(refresh);
            await broker.SaveChangesAsync();

            var tokenResponse = new TokenResponse(access, refresh.Token, DateTimeOffset.UtcNow.AddMinutes(int.Parse(Request.HttpContext.RequestServices.GetService(typeof(Microsoft.Extensions.Configuration.IConfiguration)) is Microsoft.Extensions.Configuration.IConfiguration cfg ? cfg["Jwt:AccessTokenExpirationMinutes"] ?? "15" : "15")).ToUnixTimeSeconds());
            return Ok(tokenResponse);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshRequest req)
        {
            var rt = await broker.RefreshTokens.Include(t => t.User)
                                           .FirstOrDefaultAsync(t => t.Token == req.RefreshToken);

            if (rt == null || rt.IsRevoked || rt.ExpiresAt < DateTime.UtcNow)
                return Unauthorized(new { message = "Invalid refresh token." });

            rt.IsRevoked = true;

            var newRefresh = this.jwt.GenerateRefreshToken();
            rt.User!.RefreshTokens.Add(newRefresh);

            var newAccess = this.jwt.GenerateAccessToken(rt.User);
            await broker.SaveChangesAsync();

            var tokenResponse = new TokenResponse(newAccess, newRefresh.Token, DateTimeOffset.UtcNow.AddMinutes(int.Parse(Request.HttpContext.RequestServices.GetService(typeof(Microsoft.Extensions.Configuration.IConfiguration)) is Microsoft.Extensions.Configuration.IConfiguration cfg ? cfg["Jwt:AccessTokenExpirationMinutes"] ?? "15" : "15")).ToUnixTimeSeconds());
            return Ok(tokenResponse);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (userIdClaim == null) return Unauthorized();
            if (!int.TryParse(userIdClaim, out var userId)) return Unauthorized();

            var user = await broker.Users.FindAsync(userId);
            if (user == null) return NotFound();

            return Ok(new { user.Id, user.Username, user.Email, user.CreatedAt });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (userIdClaim == null) return Unauthorized();
            if (!int.TryParse(userIdClaim, out var userId)) return Unauthorized();

            var rts = await broker.RefreshTokens.Where(rt => rt.UserId == userId && !rt.IsRevoked).ToListAsync();
            foreach (var r in rts) r.IsRevoked = true;

            await broker.SaveChangesAsync();
            return Ok(new { message = "Chiqish bajarildi." });
        }
    }
}

