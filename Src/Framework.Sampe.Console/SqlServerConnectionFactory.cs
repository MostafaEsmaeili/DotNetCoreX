using System.Data;
using System.Data.SqlClient;
using Dapper.AmbientContext;

namespace Framework.Sampe.Console
{
	public class SqlServerConnectionFactory : IDbConnectionFactory
	{
		private readonly string _connectionString;

		public SqlServerConnectionFactory(string connectionString)
		{
			_connectionString = connectionString;
		}

		public IDbConnection Create()
		{
			return new SqlConnection(_connectionString);
		}
	}
}