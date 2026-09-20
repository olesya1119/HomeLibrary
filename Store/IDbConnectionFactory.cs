using System.Data;

namespace Store;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
