using System.Data;

namespace PropertyMgmt.Application.Interfaces;

public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();
}
