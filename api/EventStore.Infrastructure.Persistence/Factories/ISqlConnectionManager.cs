using System;
using Microsoft.Data.SqlClient;

namespace EventStore.Infrastructure.Persistence.Factories
{
    public interface ISqlConnectionManager : IDisposable
    {
        SqlConnection Get();
    }
}