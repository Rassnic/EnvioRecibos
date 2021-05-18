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
    class ClassQuincena
    {
        private Classes.ClassPlanilla plani = new Classes.ClassPlanilla();
        private ClassConexion connection = new ClassConexion();
        private static SqlCommand command = new SqlCommand();
        private static SqlTransaction sqltran;

        private static string ruta_quincena = "";
        public int qsiguiente;
        public int qactual;
        string error = "";

        public void CargaDatosQuincena(string ruta)
        {
            bool sin_error = false;
            try
            {
                string ubicacion_error = @":\";
                bool error_ruta = false;

                ruta_quincena = ruta;
                bool ruta_correcta = ubicacion_error.Contains(ruta_quincena);
                if (ruta_correcta)
                {
                    error_ruta = false;
                }
                else
                {
                    string error_a_evaluar = "Error obteniendo ruta: ";
                    ubicacion_error = ruta_quincena;
                    bool error_ruta_p = ubicacion_error.Contains(error_a_evaluar);
                    error_ruta = error_ruta_p;
                }

                if (error_ruta)
                {
                    throw new Exception("Error" + " [" + ubicacion_error + "]");
                }
                else
                {
                    string path = @"" + ubicacion_error + "quincenas.dbf";

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

                        //////Para obtener un numero maximo///////
                        //command.CommandText = "SELECT ISNULL(MAX(quincena), 0) AS quincena FROM tbl_quincenas";
                        //ubicacion_error = "Error obteniendo vale maximo. ";
                        //max_quincena = command.ExecuteScalar().ToString();

                        OleDbCommand commandDBF = new OleDbCommand(
                        string.Format("SELECT " +
                            "Empresa, Quincena, Del, Al, Igsspat, Igsslab, Intecap, Irtra, Actual, Terminada, Generada, Sabado, Numigss, Encarga, Encarga, Bonificacion, Bonificac2, Bonificac3, Bonificac4 " +
                            "FROM {0} ", Path.GetFileNameWithoutExtension(path)), oConn);
                        ubicacion_error = "Error obteniendo informacion de tabla QUINCENAS.dbf ";
                        YourResultSet.Load(commandDBF.ExecuteReader());
                        
                        ubicacion_error = "Error agregando informacion de tabla QUINCENAS.dbf a datareader.";
                        DataTableReader reader = YourResultSet.CreateDataReader();

                        ubicacion_error = "Error copiando informacion en tabla de SQL.";
                        SqlBulkCopy sqlcpy = new SqlBulkCopy(connection.connection, SqlBulkCopyOptions.Default, sqltran);
                        sqlcpy.DestinationTableName = "tbl_quincenas";

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

        public void deleteQuincena()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "DELETE FROM tbl_quincenas";
                command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                error =  "Error obteniendo ruta: " + ex.ToString();
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public void UpdateQuincena()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "Update tbl_quincenas set valida = CAST (RIGHT(del, 2) AS int) FROM tbl_quincena";
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


        //PARA OTRAS PLANILLA//

        #region Obtener Datos Quincena
        //public string getQuincena()
        //{
        //    try
        //    {
        //        connection.connection.Open();
        //        command.Connection = connection.connection;
        //        command.Parameters.Clear();
        //        command.CommandText = "SELECT ( CASE WHEN valida = 1 THEN 'Primera' else 'Segunda' END) as quincena FROM tbl_quincena where actual = 'true'";
        //        return command.ExecuteScalar().ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Primera";
        //    }
        //    finally
        //    {
        //        connection.connection.Close();
        //    }
        //}

        //public string getMes()
        //{
        //    try
        //    {
        //        connection.connection.Open();
        //        command.Connection = connection.connection;
        //        command.Parameters.Clear();
        //        command.CommandText = "SELECT (CASE WHEN MONTH(del) = 1 THEN 'Enero' " +
        //            "WHEN MONTH(del) = 2 THEN 'Febrero' " +
        //            "WHEN MONTH(del) = 3 THEN 'Marzo' " +
        //            "WHEN MONTH(del) = 4 THEN 'Abril' " +
        //            "WHEN MONTH(del) = 5 THEN 'Mayo' " +
        //            "WHEN MONTH(del) = 6 THEN 'Junio' " +
        //            "WHEN MONTH(del) = 7 THEN 'Julio' " +
        //            "WHEN MONTH(del) = 8 THEN 'Agosto' " +
        //            "WHEN MONTH(del) = 9 THEN 'Septiembre' " +
        //            "WHEN MONTH(del) = 10 THEN 'Octubre' " +
        //            "WHEN MONTH(del) = 11 THEN 'Noviembre' " +
        //            "ELSE 'Diciembre' " +
        //            "END) as mes from tbl_quincenas where actual = 'true'";
        //        return command.ExecuteScalar().ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error obteniendo ruta: " + ex.ToString();
        //    }
        //    finally
        //    {
        //        connection.connection.Close();
        //    }
        //}

        //public string getYear()
        //{
        //    try
        //    {
        //        connection.connection.Open();
        //        command.Connection = connection.connection;
        //        command.Parameters.Clear();
        //        command.CommandText = "SELECT YEAR(del) from tbl_quincenas as anio where actual = 'true'";
        //        return command.ExecuteScalar().ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error obteniendo ruta: " + ex.ToString();
        //    }
        //    finally
        //    {
        //        connection.connection.Close();
        //    }
        //}

        //public string getUltimoDiaMes()
        //{
        //    try
        //    {
        //        connection.connection.Open();
        //        command.Connection = connection.connection;
        //        command.Parameters.Clear();
        //        command.CommandText =
        //            "Select" +
        //                "(CASE WHEN LEN(CAST(day(al) AS varchar(2))) = 1 THEN '0' + " +
        //                "CAST(day(al)AS varchar(2))" +
        //                "ELSE CAST(day(al)AS varchar(2))END) + '/' + " +

        //                "(CASE WHEN LEN(CAST(MONTH(al)AS varchar(2))) = 1 THEN '0' + " +
        //                "CAST(MONTH(al)AS varchar(2)) " +
        //                "ELSE CAST(MONTH(al)AS varchar(2))END) +'/' + " +

        //                "CAST(YEAR(al) AS varchar(4)) " +

        //                "AS[FECHA DE DOCUMENTO] from tbl_quincenas where actual = 'true' ";

        //        return command.ExecuteScalar().ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error obteniendo ruta: " + ex.ToString();
        //    }
        //    finally
        //    {
        //        connection.connection.Close();
        //    }
        //}
        #endregion

        //PARA ADMINISTRACION//

        #region Obtener Quincena

        public string getQuincena()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "Select (CASE WHEN DAY(getdate()) <= 15 then 'Primera' else 'Segunda' END)";
                return command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                return "Primera";
            }
            finally
            {
                connection.connection.Close();
            }
        } 

        public string getMes()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT (CASE WHEN MONTH(getdate()) = 1 THEN 'Enero' " +
                    "WHEN MONTH(getdate()) = 2 THEN 'Febrero' " +
                    "WHEN MONTH(getdate()) = 3 THEN 'Marzo' " +
                    "WHEN MONTH(getdate()) = 4 THEN 'Abril' " +
                    "WHEN MONTH(getdate()) = 5 THEN 'Mayo' " +
                    "WHEN MONTH(getdate()) = 6 THEN 'Junio' " +
                    "WHEN MONTH(getdate()) = 7 THEN 'Julio' " +
                    "WHEN MONTH(getdate()) = 8 THEN 'Agosto' " +
                    "WHEN MONTH(getdate()) = 9 THEN 'Septiembre' " +
                    "WHEN MONTH(getdate()) = 10 THEN 'Octubre' " +
                    "WHEN MONTH(getdate()) = 11 THEN 'Noviembre' " +
                    "ELSE 'Diciembre' " +
                    "END)";
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


        public DataTable Mes(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("datos");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT mes FROM tbl_mes";
                returnTable.Load(command.ExecuteReader());

                return returnTable;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
            finally
            {
                connection.connection.Close();
            }
        }


        public string getYear()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "Select YEAR(getdate())";
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

        public string getUltimoDiaMes()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT CONVERT(VARCHAR(10), GETDATE(), 103)";
                    //"DECLARE @mydate DATETIME " +
                    //"SELECT @mydate = GETDATE() " +
                    //"SELECT CONVERT(VARCHAR(25),DATEADD(dd,-(DAY(DATEADD(mm,1,@mydate))),DATEADD(mm,1,@mydate)),103) ";
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

        #endregion

        #region Actulizar Quincena
        //public string getQuincenaSiguiente()
        //{
        //    try
        //    {
        //        connection.connection.Open();
        //        command.Connection = connection.connection;
        //        command.Parameters.Clear();
        //        command.CommandText =
        //            "Select Max(id_quincena)+1 from tbl_quincena Where actual = 't'";

        //        return command.ExecuteScalar().ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error obteniendo ruta: " + ex.ToString();
        //    }
        //    finally
        //    {
        //        connection.connection.Close();
        //    }
        //}

        //public void UpdateQuincenaSiguiente()
        //{
        //    qsiguiente = Convert.ToInt32(getQuincenaSiguiente());

        //    try
        //    {
        //        connection.connection.Open();
        //        command.Connection = connection.connection;
        //        command.Parameters.Clear();
        //        command.CommandText = "update tbl_quincena set actual = 't' Where id_quincena = " + qsiguiente;
        //        command.ExecuteScalar().ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        error = "Error obteniendo ruta: " + ex.ToString();
        //    }
        //    finally
        //    {
        //        connection.connection.Close();
        //    }
        //}

        //public void UpdateQuincenaActual()
        //{
        //    qactual = Convert.ToInt32(qsiguiente) - 1;

        //    try
        //    {
        //        connection.connection.Open();
        //        command.Connection = connection.connection;
        //        command.Parameters.Clear();
        //        command.CommandText = "Update tbl_quincena set actual = 'f' Where id_quincena = " + qactual;
        //        command.ExecuteScalar().ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        error = "Error obteniendo ruta: " + ex.ToString();
        //    }
        //    finally
        //    {
        //        connection.connection.Close();
        //    }
        //}
        #endregion
    }
}
