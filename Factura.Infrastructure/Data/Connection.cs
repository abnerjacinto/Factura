using MySql.Data.MySqlClient;

namespace Factura.Infrastructure.Data
{
    public class Connection
    {
        // SINGLETON
        private static Connection connection;
        private MySqlConnection conn;
        private Connection()
        {
            conn = new MySqlConnection("server=localhost;port=3306;user=root;password=;database=invoices;Convert Zero Datetime=True;Allow Zero Datetime=True;connect timeout=1500");
            connection = this;
        }
        public static Connection IsConnected()
        {
            if (connection == null)
            {
                return new Connection();
            }
            return connection;
        }
        public MySqlConnection GetConn()
        {
            return conn;
        }
        public void CloseConnection()
        {
            connection = null;
        }
    }
}
