using BookManagement.DTOs.UserDTOs;
using BookManagement.Models;
using BookManagement.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookManagement.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;
        private readonly IConfiguration _config;


        public UserService(UserRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<(bool Success , string Message)> RegisterUser (RegisterDto dto)
        {
            var existing = await _repository.GetByEmail(dto.Email);
            if (existing != null) return (false, "Email is already exist");

            var user = new UserModel
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCryptNet.HashPassword(dto.Password),
                Interests = dto.Interests,
            };

            await _repository.Create(user);

            return (true, "User registered successfully");

        }


        public async Task<(bool Success, Object? Data, string Message)> LoginUser(LoginDto dto)
        {
            var user = await _repository.GetByEmail(dto.Email);

            if (user == null) return (false, null, "Invalid email");

            if (!BCryptNet.Verify(dto.Password, user.Password))
                return (false, null, "Invalid password");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var responseData = new
            {
                User = new {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Interest = user.Interests,
                    CreatedAt = user.CreatedAt,
                },

                Token = tokenString

            };

            return (true, responseData, "Login successful");
        }


    }
}
