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
    class ClassPlanilla
    {
        private ClassConexion connection = new ClassConexion();
        private static SqlCommand command = new SqlCommand();
        private static SqlTransaction sqltran;
        
        private static string ruta_planilla = "";
        string error = "";

        public void CargaDatosPlanilla(string ruta)
        {
            bool sin_error = false;
            try
            {
                string ubicacion_error = @":\";
                bool error_ruta = false;

                ruta_planilla = ruta;
                bool ruta_correcta = ubicacion_error.Contains(ruta_planilla);
                if (ruta_correcta)
                {
                    error_ruta = false;
                }
                else
                {
                    string error_a_evaluar = "Error obteniendo ruta: ";
                    ubicacion_error = ruta_planilla;
                    bool error_ruta_p = ubicacion_error.Contains(error_a_evaluar);
                    error_ruta = error_ruta_p;
                }

                if (error_ruta)
                {
                    throw new Exception("Error" + " [" + ubicacion_error + "]");
                }
                else
                {
                    string path = @"" + ubicacion_error + "planilla.dbf";

                    DataTable YourResultSet = new DataTable();

                    OleDbConnection oConn = new OleDbConnection("Provider=VFPOLEDB; Data Source=" + Path.GetDirectoryName(path));

                    try
                    {
                        ubicacion_error = "Abriendo la conexion a la base de datos SQL ";
                        connection.connection.Open();
                        ubicacion_error = "Abriendo la conexion con base de datos DBF ";
                        oConn.Open();

                        ubicacion_error = "Error dandole conexion a command.connection";
                        command.Connection = connection.connection;
                        ubicacion_error = "Error iniciando begin transaction a command.connection";

                        sqltran = connection.connection.BeginTransaction();
                        command.Transaction = sqltran;

                        // Para obtener un numero maximo////
                        //command.CommandText = "SELECT ISNULL(MAX(), 0) AS  FROM ";
                        //ubicacion_error = "Error obteniendo vale maximo. ";
                        //max_ = "'" + command.ExecuteScalar().ToString() + "'";

                        OleDbCommand commandDBF = new OleDbCommand(
                        string.Format("SELECT " +
                            "Codigo, Nombre, Cedula, Areatrab, Numigss, Salario, Diastrab, Salnom, Qextra, Nextra, Nextrad, Qextrad, Salbruto, Igss, Prestamos, OtrosDesc, Seguro, Isr, Salarioliq, Bonifica, Saltotal, Bonextra " +
                            "FROM {0} ", Path.GetFileNameWithoutExtension(path)), oConn);
                        ubicacion_error = "Error obteniendo informacion de tabla planilla.dbf ";
                        YourResultSet.Load(commandDBF.ExecuteReader());

                        ubicacion_error = "Error agregando informacion de tabla planilla.dbf a datareader.";
                        DataTableReader reader = YourResultSet.CreateDataReader();

                        ubicacion_error = "Error copiando informacion en tabla de SQL.";
                        SqlBulkCopy sqlcpy = new SqlBulkCopy(connection.connection, SqlBulkCopyOptions.Default, sqltran);
                        sqlcpy.DestinationTableName = "tbl_planilla";

                        ubicacion_error = "Error copiando informacion en tabla de SQL.";
                        sqlcpy.WriteToServer(YourResultSet);

                        ubicacion_error = "Error cerrando datareader.";
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        error = ubicacion_error + " " + ex.ToString();
                        sqltran.Rollback();
                        command.Transaction.Rollback();
                        sin_error = false;
                    }
                    finally
                    {
                        sin_error = true;
                        sqltran.Commit();
                        oConn.Close();
                        connection.connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                error += "Error general " + ex.ToString();
            }
        }

        public string getRutaPlanilla(string planilla)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT ruta FROM tbl_parametros WHERE planilla = " + "'" + planilla + "'";

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

        public string getCountPlanilla()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "Select COUNT(codigo) from tbl_planilla";

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

        public void deletePlanilla()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "DELETE FROM tbl_planilla";
                command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                error = "Error obteniendo ruta: " + ex.ToString();
            }
            finally
            {
                connection.connection.Close();
            }
        }

    }
}
