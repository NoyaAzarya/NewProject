using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary5
{
    public class UserService(IConfiguration configuration)
    {

        private readonly IConfiguration _configuration = configuration;

        // ✅ Check if Email Exists in Database
        public async Task<bool> UserExists(string email)
        {

            using var connection = await Database.GetConnection();

            string query = "SELECT COUNT(*) FROM users WHERE email = @Email";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);

            var count = (long)await command.ExecuteScalarAsync();
            return count > 0;
        }

        // ✅ Insert New User into Database
        public async Task<bool> CreateUser(RegisterRequest model)
        {
            using var connection = await Database.GetConnection();

            string query = "INSERT INTO users (first_name, last_name, email, password_hash, role) VALUES (@FirstName, @LastName, @Email, @Password, @Role)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", model.FirstName);
            command.Parameters.AddWithValue("@LastName", model.LastName);
            command.Parameters.AddWithValue("@Email", model.Email);
            command.Parameters.AddWithValue("@Password", model.Password);
            command.Parameters.AddWithValue("@Role", model.Role);

            int result = await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
            return result > 0;
        }

        // ✅ Secure Password Hashing
        public string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // ✅ Email Validation
        public bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // ✅ Password Strength Validation
        public bool IsStrongPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }

        public async Task<User?> GetUserAsync(string Email)
        {
            Console.WriteLine("🔹 Checking if a user is logged in...");
            using var connection = await Database.GetConnection();

            string query = "SELECT * from users where Email = @Email";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", Email);
            DbDataReader reader = await command.ExecuteReaderAsync();
            User? user = null;
            if (reader.Read())
            {
                user = new User
                {
                    UserId = Convert.ToInt32(reader["user_id"]),
                    FirstName = reader["first_name"].ToString(),
                    LastName = reader["last_name"].ToString(),
                    Email = reader["email"].ToString(),
                    Password = reader["password_hash"].ToString(),
                    Role = reader["role"].ToString()
                };
            }
            return await Task.FromResult(user);
        }

        public async Task<User?> AuthenticateUser(ClassLibrary5LoginRequest request)
        {
            request.Password = HashPassword(request.Password);
            var user = await GetUserAsync(request.Email);
            if (user == null)
            {
                Console.WriteLine("❌ Authentication Failed! User not found.");
                return null;
            }
            else if (user.Password != request.Password)
            {
                Console.WriteLine("❌ Authentication Failed! User passwords don't match.");
                return null;
            }
            return user;
        }



        public async Task<User?> GetUserById(int userId)
        {

            using var connection = await Database.GetConnection();

            const string query = "SELECT * from users where user_id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", userId);
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var user = new User
                {
                    UserId = Convert.ToInt32(reader["user_id"]),
                    FirstName = reader["first_name"].ToString(),
                    LastName = reader["last_name"].ToString(),
                    Password = reader["password_hash"].ToString(),
                    Email = reader["email"].ToString(),
                    Role = reader["role"].ToString()
                };

                return user;
            }
            return null;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = new List<User>();

            using var connection = await Database.GetConnection();

            const string query = "SELECT user_id, first_name, last_name, email, role FROM users";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var user = new User
                {
                    UserId = Convert.ToInt32(reader["user_id"]),
                    FirstName = reader["first_name"].ToString(),
                    LastName = reader["last_name"].ToString(),
                    Password = reader["password_hash"].ToString(),
                    Email = reader["email"].ToString(),
                    Role = reader["role"].ToString()
                };

                users.Add(user);
            }
            return users;
        }
    }
}