using BookManagement.DTOs.UserDTOs;
using BookManagement.Models;
using BookManagement.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;
using Org.BouncyCastle.Crypto.Generators;

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


        //public async Task<(bool Success , string Message)> LoginUser (LoginDto dto)
        //{
        //    var existuser = await _repository.GetByEmail(dto.Email);

          
        //}


    }
}
