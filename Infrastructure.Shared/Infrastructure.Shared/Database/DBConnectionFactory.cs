
using System.Data;
using Microsoft.Data.SqlClient;
using Infrastructure.Shared.Constants;
using Microsoft.Extensions.Options;

namespace Infrastructure.Shared.Database
{
    public interface IDbConnectionFactory : IDisposable
    {
        IDbConnection Connection { get; }
    }

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private IDbConnection? _dbConnection;
        private readonly ConnectionString _connectionString;
   
        public DbConnectionFactory(IOptions<ConnectionString> connectionstring)
        {
            _connectionString = connectionstring.Value!;
        }
     
        public IDbConnection Connection
        {
            get
            {
                if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)
                {
                    _dbConnection = new SqlConnection(_connectionString.DefaultConnection);
                }
                return _dbConnection;
            }
        }
        
        public void Dispose()
        {
            if (_dbConnection != null)
            {
                _dbConnection.Dispose();
            }
        }
    }
}
