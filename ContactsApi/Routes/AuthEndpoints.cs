using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ContactsApi.Domain.Entities;
using ContactsApi.Application.Interfaces;

namespace ContactsApi.ContactsApi.Routes
{
    public static class AuthEndpoints
    {
        public static void RegisterAuthEndpoints(this WebApplication app)
        {
            var jwtKey = "a-string-secret-that-is-longer-than-256-bits!";
            var keyBytes = Encoding.UTF8.GetBytes(jwtKey);
            var securityKey = new SymmetricSecurityKey(keyBytes);

            app.MapPost("/register", async (IUserRepository userRepository, [FromBody] User user) =>
            {
                var existingUser = await userRepository.GetByUsernameAsync(user.Username);
                if (existingUser != null) return Results.BadRequest("User already exists");

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                if (string.IsNullOrEmpty(user.Role)) user.Role = "User";

                await userRepository.CreateAsync(user);
                return Results.Ok("User registered successfully!");
            });

            app.MapPost("/login", async (IUserRepository userRepository, [FromBody] User loginUser) =>
            {
                var user = await userRepository.GetByUsernameAsync(loginUser.Username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.PasswordHash, user.PasswordHash))
                    return Results.Unauthorized();

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role) 
                    };

                var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(24),
                    signingCredentials: creds
                );

                return Results.Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            });
        }
    }
}
