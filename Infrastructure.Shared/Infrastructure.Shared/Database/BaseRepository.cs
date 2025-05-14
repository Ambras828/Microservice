using Infrastructure.Shared.Database;
using Dapper;
using System.Data;

namespace DataAccess.Repository
{

    public class BaseRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
  
        public BaseRepository(IDbConnectionFactory dbConnectionFatory)
        {
            _dbConnectionFactory = dbConnectionFatory;
        }

        public async Task<IEnumerable<T>> SelectAsync<T>(string sqlString, object parameter = null!)
        {
            return await _dbConnectionFactory.Connection.QueryAsync<T>(sqlString, parameter, commandType:CommandType.StoredProcedure);
        }

        public async Task<T?> SelectFirstOrDefaultAsync<T>(string sqlString, object parameters)
        {
            return await _dbConnectionFactory.Connection.QueryFirstOrDefaultAsync<T>(sqlString, parameters , commandType:CommandType.StoredProcedure);
        }
       
        public async Task<T?> ExecuteScalarAsync<T>(string sqlString, object Parameters = null! )
        {
            return await _dbConnectionFactory.Connection.ExecuteScalarAsync<T>(sqlString, Parameters , commandType:CommandType.StoredProcedure);
        }

        public async Task<int> ExecuteAsync(string sqlString, object parameter = null! )
        {
            return await _dbConnectionFactory.Connection.ExecuteAsync(sqlString, parameter , commandType:CommandType.StoredProcedure);
        }
    }
}
