using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Routes
{
    public static class AuthEndpoints
    {
        public static void RegisterAuthEndpoints(this WebApplication app)
        {
            var jwtKey = "SuperSecretKey12345!";
            var key = Encoding.UTF8.GetBytes(jwtKey);

            app.MapPost("/register", async (IUserRepository userRepository, string username, string password) =>
            {
                var existingUser = await userRepository.GetUserByUsernameAsync(username);
                if (existingUser != null)
                {
                    return Results.BadRequest("User already exists.");
                }

                var newUser = new User { Username = username, PasswordHash = password };
                var success = await userRepository.RegisterUserAsync(newUser);

                return success ? Results.Ok("User registered successfully!") : Results.BadRequest("Failed to register user.");
            });

            app.MapPost("/login", async (IUserRepository userRepository, string username, string password) =>
            {
                var user = await userRepository.GetUserByUsernameAsync(username);
                if (user == null || user.PasswordHash != password)
                {
                    return Results.Unauthorized();
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, "User")
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Results.Ok(new { Token = tokenHandler.WriteToken(token) });
            });
        }
    }
}
