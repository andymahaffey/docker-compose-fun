using Cassandra;

namespace Api.Data.Providers;
public class CassandraProvider : ICassandraProvider
{
    private readonly Lazy<Task<Cassandra.ISession>> CassandraSession;

    public Cassandra.ISession Session {
        get
        {
            return CassandraSession.Value.Result;
        }
    }
    
    public CassandraProvider(IConfiguration configuration)
    {
        string contactPoint = configuration.GetValue<string>("CassandraSettings:Cluster");
        CassandraSession = new Lazy<Task<Cassandra.ISession>>(() => CreateSession(contactPoint));
    }

    private async Task<Cassandra.ISession>? CreateSession(string contactPoint)
    {
        var cluster = Cluster.Builder().AddContactPoint(contactPoint).Build(); 
        var session = await cluster.ConnectAsync();
        session.CreateKeyspaceIfNotExists("store", new Dictionary<string, string>
        {
            { "class", "SimpleStrategy" },
            { "replication_factor", "1" }
        });
        return session;
    }
}