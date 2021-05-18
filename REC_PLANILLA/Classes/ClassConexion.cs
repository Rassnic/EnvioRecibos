using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REC_PLANILLA.Classes
{
    class ClassConexion
    {
       

        public SqlConnection connection = new SqlConnection();

        private void connect()
        {
            connection = new SqlConnection(connectionString);
        }

        public ClassConexion()
        {
            connect();
        }

        public SqlConnection getCon()
        {
            return connection;
        }
    }
}
