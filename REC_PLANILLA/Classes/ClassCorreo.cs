using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REC_PLANILLA.Classes
{
    class ClassCorreo
    {
        private Classes.ClassPlanilla plani = new Classes.ClassPlanilla();
        private ClassConexion connection = new ClassConexion();
        private static SqlCommand command = new SqlCommand();
        private Classes.ClassParametros parm = new Classes.ClassParametros();
        private static SqlTransaction sqltran;


        public string getcorreodestino()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select mail.correo from tbl_correo as mail INNER JOIN tbl_planilla as plani on mail.codigo_colaborador = plani.codigo WHERE plani.codigo = " + "'" + parm.getcodigo().ToString() + "'";
                
                return command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
               return "";
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public string getcorreoOrigen( string planilla )
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT correo_origen  FROM tbl_parametros WHERE planilla = " + "'" + planilla + "'";

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

        public string getcorreoDominio(string planilla )
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT dominio FROM tbl_parametros WHERE planilla = " + "'" + planilla + "'";

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
