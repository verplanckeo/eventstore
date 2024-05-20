using System;
using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EventStore.Infrastructure.Persistence.Factories
{
    public class SqlConnectionManager : ISqlConnectionManager
    {
        private readonly IConfiguration _configuration;

        private SqlConnection _sqlConnection;

        public SqlConnectionManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection Get()
        {
            if (_sqlConnection != null) return _sqlConnection;

            var tenantId =  _configuration[Constants.Security.Azure.TenantId];;
            var clientId = _configuration[Constants.Security.Azure.ClientId];
            var clientSecret = _configuration[Constants.Security.Azure.ClientSecret];
            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            var token = credential.GetToken(new TokenRequestContext(new[]{"https://database.windows.net/.default"})).Token;
            
            var connectionString = _configuration.GetConnectionString(Constants.Database.ConnectionStringName);
            _sqlConnection = new SqlConnection(connectionString)
            {
                AccessToken = token
            };

            return _sqlConnection;
        }

        #region IDisposable

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SqlConnectionManager()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing && _sqlConnection != null)
            {
                _sqlConnection.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}