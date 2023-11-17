namespace Api.Data.Providers;
public interface ICassandraProvider
{
    Cassandra.ISession Session { get; }
}