using System.Data.SqlClient;
using System.Data;

namespace ApiB.Comunes
{
    public class ConexionDB
    {
        private static SqlConnection conexion;

        public static SqlConnection abrirConexion()
        {
            conexion = new SqlConnection("Server=JKV\\SQLEXPRESS;Database=BaseB;Trusted_Connection=True;");
            conexion.Open();
            return conexion;
        }
    }
}
