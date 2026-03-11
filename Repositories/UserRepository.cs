
using BookManagement.Data;
using BookManagement.Models;
using Dapper;

namespace BookManagement.Repositories
{
    public class UserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(UserModel userModel)
        {
            var query = @"INSERT INTO Users (Name, Email, Password, CreatedAt)
                         VALUES (@Name,@Email, @Password ,@Interests)";

            using var connection = _context.CreateConnection();

            return await connection.ExecuteAsync(query, userModel);

        }

        public async Task<UserModel> GetByEmail(string  email)
        {
            var query = "SELECT * FROM Users WHERE EMAIL = @Email";

            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<UserModel>(query, new { Email = email });
        }

    }
}
