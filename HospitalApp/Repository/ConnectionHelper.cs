using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace HospitalApp.Repository
{
    public class ConnectionHelper
    {
        private readonly string _connectionString;

        public ConnectionHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
