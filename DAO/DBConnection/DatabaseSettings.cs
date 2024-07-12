using DAO.DBConnection.SqlServer.Settings;

namespace DAO.DBConnection
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public SqlServerSettings SqlServerSettings { get; set; }
    }

    public interface IDatabaseSettings
    {
        SqlServerSettings SqlServerSettings { get; set; }
    }
}
