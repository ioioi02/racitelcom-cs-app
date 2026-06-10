using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RACITELCOM_CS_APP
{
    class MyConnectionString
    {
        public static MySqlConnection mysql_connection()
        {
            string connection_string = "server=localhost;user=root;password=;database=db_cs_app;";
            // string connection_string = "server=192.168.253.218;user=user123;password=user123;database=db_cs_app;";
            return new MySqlConnection(connection_string);
        }
    }
}
