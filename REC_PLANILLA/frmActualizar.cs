using REC_PLANILLA.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REC_PLANILLA
{
    public partial class frmActualizar : Form
    {        
        private Classes.ClassParametros parm = new Classes.ClassParametros();
        private ClassConexion connection = new ClassConexion();
        private static SqlCommand command = new SqlCommand();
        private static SqlTransaction sqltran;
        private static string error = "";
        private int actualizaestado;
        DataTable estados = new DataTable();

        public frmActualizar()
        {
            InitializeComponent();
            lblid.Visible = false;
        }


        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtCodigo.Text == "" || txtCorreo.Text == "")
                {
                    MessageBox.Show("Por favor llenar los campos de Codigo y Correo");
                }
                else
                {
                    connection.connection.Open();
                    command.Connection = connection.connection;
                    command.Parameters.Clear();
                    command.CommandText = "INSERT into tbl_correo values ( " + "'" + txtCodigo.Text + "'" + "," + "'" + txtCorreo.Text + "'" + "," + 1 + ")";
                    command.ExecuteScalar().ToString();
                    MessageBox.Show("Correo: " + txtCorreo.Text + " Asignado a Codigo: " + txtCodigo.Text);
                }
            }
            catch (Exception ex)
            {
                string error = "Error obteniendo ruta: " + ex.ToString();
            }
            finally
            {
                connection.connection.Close();
                txtCodigo.Text = "";
                txtCorreo.Text = "";
            }

        }


        private string getid()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select id_correo from tbl_correo WHERE codigo_colaborador = " + "'" + txtCodigo.Text + "'";

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


        public string getcorreodestino()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select correo from tbl_correo WHERE codigo_colaborador = " + "'" + txtCodigo.Text + "'";

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

        public string getestado()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT (CASE WHEN estado = 1 THEN 'Activo' ELSE 'Inactivo' END) FROM tbl_correo WHERE id_correo = " + lblid.Text;

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

        private void obtenreestado()
        {
            try
            {
                estados = new DataTable("stat");
                estados = DatosEstado(ref error);
            }
            catch (Exception ex)
            {
                //showWarning(error + ex.ToString());
            }

        }


        public DataTable DatosEstado(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("datos");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT estado FROM tbl_estado";
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



        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            if (cmbestado.Text == "Activo")
            {
                actualizaestado = 1;
            }
            else { actualizaestado = 0; }


            try
            {
                if (txtCodigo.Text == "" || txtCorreo.Text == "")
                {
                    MessageBox.Show("Por favor llenar los campos de Codigo y Correo");
                }
                else
                {
                    connection.connection.Open();
                    command.Connection = connection.connection;
                    command.Parameters.Clear();
                    command.CommandText = "UPDATE tbl_correo SET codigo_colaborador = " + "'" + txtCodigo.Text + "'" + "," + " correo = " + "'" + txtCorreo.Text + "'" + "," + " estado = " + actualizaestado + "WHERE id_correo = " + lblid.Text;
                    command.ExecuteScalar().ToString();                    
                }
            }
            catch (Exception ex)
            {
                string error = "Error obteniendo ruta: " + ex.ToString();
            }
            finally
            {
                MessageBox.Show("Correo: " + txtCorreo.Text + " Asignado a Codigo: " + txtCodigo.Text);
                connection.connection.Close();
                txtCodigo.Text = "";
                txtCorreo.Text = "";
                cmbestado.Text = "";
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {

            txtCorreo.Text = getcorreodestino().ToString();
            lblid.Text = getid().ToString();
            obtenreestado();
            cmbestado.DataSource = estados;
            cmbestado.DisplayMember = "estado";
            cmbestado.ValueMember = "estado";
            cmbestado.SelectedValue = getestado().ToString();
        }

        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            frmNuevoCorreo inicio = new frmNuevoCorreo();
            this.Visible = false;
            inicio.Visible = true;

        }
    }
    
    }

