using System.Data.SqlClient;

namespace MagicMirrorApi.Repository
{
    public class DatabaseConnector
    {
        private static string x = "S";
        private static string y = "e2";
        private static string z = "DK";
        private static string w = "ekm";

        private static string datasource = "sql6002.site4now.net";
        private static string userId = "DB_A2EB7E_MagicMirror_admin";
        private static string pw = x + z + y + w;
        private static string initialCatalog = "DB_A2EB7E_MagicMirror";

        public static SqlConnectionStringBuilder getDbConnectionStringBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = datasource;
            builder.UserID = userId;
            builder.Password = pw;
            builder.InitialCatalog = initialCatalog;
            builder.ConnectTimeout = 60; // We'll need a longer timeout for connecting to AzureSqlDb. 30 seconds may not be enough over the internet.

            return builder;
        }
    }
}