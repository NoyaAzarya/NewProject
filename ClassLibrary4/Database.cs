using MySql.Data.MySqlClient;

namespace ClassLibrary5
{
    public class Database
    {

        private static readonly string _connectionString = "Server=127.0.0.1;Database=studymatch;User Id=root;Password=NOYA8486;";

        public static async Task<MySqlConnection> GetConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

    }
}
