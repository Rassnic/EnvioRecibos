using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REC_PLANILLA.Classes
{
    class ClassParametros
    {
        private Classes.ClassPlanilla plani = new Classes.ClassPlanilla();
        private ClassConexion connection = new ClassConexion();
        private static SqlCommand command = new SqlCommand();
        private static SqlTransaction sqltran;

        public string getcodigo()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "Select Min(codigo) From tbl_planilla where valida = 1";

                return command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                return "Error obteniendo ruta: " + ex.ToString();
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public string getasunto(string planilla)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT asunto FROM tbl_parametros WHERE planilla = " + "'" + planilla + "'";

                return command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                return "Error obteniendo ruta: " + ex.ToString();
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public string getPassword(string planilla)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT pass_correo_origen FROM tbl_parametros WHERE planilla = " + "'" + planilla + "'";

                return command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                return "Error obteniendo ruta: " + ex.ToString();
            }
            finally
            {
                connection.connection.Close();
            }
        }

    }
}
