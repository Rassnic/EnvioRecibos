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
    class ClassDescuentos
    {
        private Classes.ClassPlanilla plani = new Classes.ClassPlanilla();
        private ClassConexion connection = new ClassConexion();
        private static SqlCommand command = new SqlCommand();
        private static SqlTransaction sqltran;
        private Classes.ClassParametros parm = new Classes.ClassParametros();
        private static string ruta_descuento = "";
        string error = "";
        
        public void CargaDatosDescuentos(string planilla)
        {
            bool sin_error = false;
            try
            {
                string ubicacion_error = @":\";
                bool error_ruta = false;

                ruta_descuento = planilla;
                bool ruta_correcta = ubicacion_error.Contains(ruta_descuento);
                if (ruta_correcta)
                {
                    error_ruta = false;
                }
                else
                {
                    string error_a_evaluar = "Error obteniendo ruta: ";
                    ubicacion_error = ruta_descuento;
                    bool error_ruta_p = ubicacion_error.Contains(error_a_evaluar);
                    error_ruta = error_ruta_p;
                }

                if (error_ruta)
                {
                    throw new Exception("Error" + " [" + ubicacion_error + "]");
                }
                else
                {
                    string path = @"" + ubicacion_error + "descuentos.dbf";

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
                            "Codigo, Vivienda, Comida, Tienda, Medicina, Otros, Tiendarec, Bonificacion " +
                            "FROM {0} ", Path.GetFileNameWithoutExtension(path)), oConn);
                        ubicacion_error = "Error obteniendo datos de tabla descuentos.dbf ";
                        YourResultSet.Load(commandDBF.ExecuteReader());


                        ubicacion_error = "Error agregando informacion de tabla descuentos.dbf a datareader.";
                        DataTableReader reader = YourResultSet.CreateDataReader();

                        ubicacion_error = "Error copiando informacion en tabla de SQL.";
                        SqlBulkCopy sqlcpy = new SqlBulkCopy(connection.connection, SqlBulkCopyOptions.Default, sqltran);
                        sqlcpy.DestinationTableName = "tbl_descuentos";

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

        public string getIggs()
        {
            try
            {
                connection.connection.Close();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select(CASE " +
                "WHEN LEN(igss) = 7 THEN LEFT(igss, 1) + ',' + RIGHT(igss, 6) "+
                "WHEN LEN(igss) = 8 THEN LEFT(igss, 2) + ',' + RIGHT(igss, 6) "+
                "WHEN LEN(igss) = 9 THEN LEFT(igss, 3) + ',' + RIGHT(igss, 6) "+ 
                "else Convert(varchar, igss) END) from tbl_planilla WHERE codigo = " +
                "'" + parm.getcodigo().ToString() + "'";
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

        public string getPrestamo()
        {
            try 
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select(CASE " +
                "WHEN LEN(prestamos) = 7 THEN LEFT(prestamos, 1) + ',' + RIGHT(prestamos, 6) " +
                "WHEN LEN(prestamos) = 8 THEN LEFT(prestamos, 2) + ',' + RIGHT(prestamos, 6) " +
                "WHEN LEN(prestamos) = 9 THEN LEFT(prestamos, 3) + ',' + RIGHT(prestamos, 6) " +
                "else Convert(varchar, prestamos) END) from tbl_planilla WHERE codigo = " +
                "'" + parm.getcodigo().ToString() + "'";
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

        public string getSeguro()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select(CASE " +
                "WHEN LEN(seguro) = 7 THEN LEFT(seguro, 1) + ',' + RIGHT(seguro, 6) " +
                "WHEN LEN(seguro) = 8 THEN LEFT(seguro, 2) + ',' + RIGHT(seguro, 6) " +
                "WHEN LEN(seguro) = 9 THEN LEFT(seguro, 3) + ',' + RIGHT(seguro, 6) " +
                "else Convert(varchar, seguro) END) from tbl_planilla WHERE codigo = " +
                "'" + parm.getcodigo().ToString() + "'";
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

        public string getOtros()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select(CASE " +
                "WHEN LEN((otrosdesc - isr)) = 7 THEN LEFT((otrosdesc - isr), 1) + ',' + RIGHT((otrosdesc - isr), 6) " +
                "WHEN LEN((otrosdesc - isr)) = 8 THEN LEFT((otrosdesc - isr), 2) + ',' + RIGHT((otrosdesc - isr), 6) " +
                "WHEN LEN((otrosdesc - isr)) = 9 THEN LEFT((otrosdesc - isr), 3) + ',' + RIGHT((otrosdesc - isr), 6) " +
                "else Convert(varchar, (otrosdesc - isr)) END) from tbl_planilla WHERE codigo = " +
                "'" + parm.getcodigo().ToString() + "'";

                return command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                return "0.00";
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public string getIsr()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select(CASE " +
                "WHEN LEN(isr) = 7 THEN LEFT(isr, 1) + ',' + RIGHT(isr, 6) " +
                "WHEN LEN(isr) = 8 THEN LEFT(isr, 2) + ',' + RIGHT(isr, 6) " +
                "WHEN LEN(isr) = 9 THEN LEFT(isr, 3) + ',' + RIGHT(isr, 6) " +
                "else Convert(varchar, isr) END) from tbl_planilla WHERE codigo = " +
                "'" + parm.getcodigo().ToString() + "'";

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

        public void deleteDescuentos()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "DELETE FROM tbl_descuentos";
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
