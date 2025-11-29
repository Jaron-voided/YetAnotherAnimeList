using System.Data;

namespace AnimeList.Persistence.Database;

// using an interface here so I can swap out databases later, or mock tests
public interface IDbConnectionFactory
{
    IDbConnection  CreateConnection();
}