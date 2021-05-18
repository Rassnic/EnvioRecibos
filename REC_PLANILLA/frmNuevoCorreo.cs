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
    public partial class frmNuevoCorreo : Form
    {
        private Classes.ClassParametros parm = new Classes.ClassParametros();
        private ClassConexion connection = new ClassConexion();
        private static SqlCommand command = new SqlCommand();
        private static SqlTransaction sqltran;



        public frmNuevoCorreo()
        {
            InitializeComponent();
            lblid.Visible = false;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtCodigo.Text == "" || txtCorreo.Text == "") {
                    MessageBox.Show("Por favor llenar los campos de Codigo y Correo");
                } else { 
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "INSERT into tbl_correo values ( " + "'" + txtCodigo.Text + "'" + "," +  "'" + txtCorreo.Text + "'" +","+ 1 +")";
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
            }
                        
        }

        private void Actualizar()
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
                    command.CommandText = "UPDATE tbl_correo SET codigo = " + "'" + txtCodigo.Text + "'" + "," + " correo = " + "'" + txtCorreo.Text + "'" + "," + " estado = " + " 1 " +  "WHERE id_correo = " + lblid.Text;
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




        private string getid() {
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


        private void btnRegresar_Click(object sender, EventArgs e)
        {
            frmInicio inicio = new frmInicio();
            this.Visible = false;
            inicio.Visible = true;
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



        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmActualizar act = new frmActualizar();
            this.Visible = false;
            act.Visible = true;

        }
    }
}
