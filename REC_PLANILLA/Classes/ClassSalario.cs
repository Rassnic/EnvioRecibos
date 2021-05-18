using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REC_PLANILLA.Classes
{
    class ClassSalario
    {
        private Classes.ClassPlanilla plani = new Classes.ClassPlanilla();
        private ClassConexion connection = new ClassConexion();
        private static SqlCommand command = new SqlCommand();
        private static SqlTransaction sqltran;
        private Classes.ClassParametros parm = new Classes.ClassParametros();


        public string getMontoLetras()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select ([dbo].[numeros_a_letras](saltotal)) AS valor_en_letras from tbl_planilla Where codigo = " + "'" + parm.getcodigo().ToString() + "'";

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

        public string getSalario()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select (CASE WHEN LEN(salbruto) = 7 THEN LEFT(salbruto, 1) + ',' + RIGHT(salbruto,6) WHEN LEN(salbruto) = 8 THEN LEFT(salbruto, 2) + ',' + RIGHT(salbruto,6) WHEN LEN(salbruto) = 9 THEN LEFT(salbruto, 3) + ',' + RIGHT(salbruto,6) else Convert (varchar,salbruto) END) from tbl_planilla WHERE codigo = " + "'" + parm.getcodigo().ToString() + "'";

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

        public string getBoni()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select (CASE WHEN LEN(bonifica) = 7 THEN LEFT(bonifica, 1) + ',' + RIGHT(bonifica,6) WHEN LEN(bonifica) = 8 THEN LEFT(bonifica, 2) + ',' + RIGHT(bonifica,6) WHEN LEN(bonifica) = 9 THEN LEFT(bonifica, 3) + ',' + RIGHT(bonifica,6) else Convert (varchar,bonifica) END) from tbl_planilla WHERE codigo = " + "'" + parm.getcodigo().ToString() + "'";

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

        public string getExtra()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select (CASE WHEN LEN(qextra) = 7 THEN LEFT(qextra, 1) + ',' + RIGHT(qextra,6) WHEN LEN(qextra) = 8 THEN LEFT(qextra, 2) + ',' + RIGHT(qextra,6) WHEN LEN(qextra) = 9 THEN LEFT(qextra, 3) + ',' + RIGHT(qextra,6) else Convert (varchar,qextra) END) from tbl_planilla WHERE codigo = " + "'" + parm.getcodigo().ToString() + "'";

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

        public string getTotalDesc()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "Select (CASE " +
                    "WHEN LEN(otrosdesc - isr + igss + prestamos + seguro + isr) = 7 THEN LEFT((otrosdesc - isr + igss + prestamos + seguro + isr), 1) + ',' + RIGHT((otrosdesc - isr + igss + prestamos + seguro + isr), 6) " +
                    "WHEN LEN(otrosdesc - isr + igss + prestamos + seguro + isr) = 8 THEN LEFT((otrosdesc - isr + igss + prestamos + seguro + isr), 2) + ',' + RIGHT((otrosdesc - isr + igss + prestamos + seguro + isr), 6) " +
                    "else Convert(varchar, (otrosdesc - isr + igss + prestamos + seguro + isr)) END) from tbl_planilla WHERE codigo = " +
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

        public string getBoniRendi()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select (CASE WHEN LEN(bonextra) = 7 THEN LEFT(bonextra, 1) + ',' + RIGHT(bonextra,6) WHEN LEN(bonextra) = 8 THEN LEFT(bonextra, 2) + ',' + RIGHT(bonextra,6) WHEN LEN(bonextra) = 9 THEN LEFT(bonextra, 3) + ',' + RIGHT(bonextra,6) else convert (varchar,bonextra) END) from tbl_planilla WHERE codigo = " + "'" + parm.getcodigo().ToString() + "'";

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

        public string getLiquidoRecibir()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "Select (CASE " +
                    "WHEN LEN(salbruto + bonifica + qextra + bonextra - otrosdesc - igss - prestamos) = 7 THEN LEFT((salbruto + bonifica + qextra + bonextra - otrosdesc - igss - prestamos), 1) + ',' + RIGHT((salbruto + bonifica + qextra + bonextra - otrosdesc - igss - prestamos), 6) " +
                    "WHEN LEN(salbruto + bonifica + qextra + bonextra - otrosdesc - igss - prestamos) = 8 THEN LEFT((salbruto + bonifica + qextra + bonextra - otrosdesc - igss - prestamos), 2) + ',' + RIGHT((salbruto + bonifica + qextra + bonextra - otrosdesc - igss - prestamos), 6) " +
                    "WHEN LEN(salbruto + bonifica + qextra + bonextra - otrosdesc - igss - prestamos) = 9 THEN LEFT((salbruto + bonifica + qextra + bonextra - otrosdesc - igss - prestamos), 3) + ',' + RIGHT((salbruto + bonifica + qextra + bonextra - otrosdesc - igss - prestamos), 6) " +
                    "else Convert(varchar, (salbruto + bonifica + qextra + bonextra - otrosdesc - igss - prestamos)) END) from tbl_planilla WHERE codigo = " +
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

        public string getNombre()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select nombre from tbl_planilla WHERE codigo = " + "'" + parm.getcodigo().ToString() + "'";

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
