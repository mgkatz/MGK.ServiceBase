using System;
using System.Data;

namespace MGK.ServiceBase.DAL.SeedWork
{
    public interface ISqlConnectionFactory : IDisposable
    {
        IDbConnection GetOpenConnection(string database);
        int GetTimeoutForQueries(string database);
    }
}
