using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using REC_PLANILLA.Classes;
using System.Data.SqlClient;

namespace REC_PLANILLA
{
    public partial class frmInicio : Form
    {

        private ClassConexion connection = new ClassConexion();
        private static SqlCommand command = new SqlCommand();
        private Classes.ClassPlanilla plani = new Classes.ClassPlanilla();
        private Classes.ClassDescuentos desc = new Classes.ClassDescuentos();
        private Classes.ClassQuincena quin = new Classes.ClassQuincena();
        private Classes.ClassCorreo mail = new Classes.ClassCorreo();
        private Classes.ClassParametros parm = new Classes.ClassParametros();
        private Classes.ClassSalario sal = new Classes.ClassSalario();
        private static string error = "";
        public string planilla;
        public int contador;
        public string cont;
        public int contarcheck = 0;
        public string ruta;
        public string asunto, quincena, mes, anio, ultimodia, Q;
        DataTable month = new DataTable();
        
        public frmInicio()
        {
            
            InitializeComponent();
            CheKarQuincena();
            getmes();
            cmbMes.DataSource = month;
            cmbMes.DisplayMember = "mes";
            cmbMes.ValueMember = "mes";
            cmbMes.SelectedValue = quin.getMes().ToString();
            //chkadmon.Enabled = false;
            //chkSaladmon.Checked = true;
            lblanio.Text = quin.getYear().ToString();
            chkadmon.Checked = true;
            chkSaladmon.Enabled = false;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            // obtener planilla //
            if (chkSaladmon.Checked)
            {
                 planilla = "SALADMON";
            }
            else if (chkadmon.Checked)
            {
                 planilla = "ADMON";
            }

            if (chkprimera.Checked)
            {
                quincena = "Primera";
            }
            else if (chksegunda.Checked)
            {
                quincena = "Segunda";
            }

            
            cmbMes.DataSource = month;


            ruta = plani.getRutaPlanilla(planilla);
            quin.CargaDatosQuincena(ruta);
            desc.CargaDatosDescuentos(ruta);
            plani.CargaDatosPlanilla(ruta);
            updateValidaPlani();
            cont = plani.getCountPlanilla();
            contador = Convert.ToInt32(cont);
            quin.UpdateQuincena();

            // Obtener Asunto //
            asunto = parm.getasunto(planilla).ToString();
            mes = cmbMes.Text;
            anio = quin.getYear().ToString();
            

            for (int i = 0; i < contador; i++) {
                parm.getcodigo();
                if (mail.getcorreodestino().ToString() == "")
            {
                MessageBox.Show("COLABORADOR " + parm.getcodigo().ToString() + " " + sal.getNombre().ToString() + " SIN CORREO");
                updatePlanilla();
           }
            else
            {
               EnviarCorreo();
               updatePlanilla();
             }
            }
            //quin.getQuincenaSiguiente();
            //quin.UpdateQuincenaSiguiente();
            //quin.UpdateQuincenaActual();
            quin.deleteQuincena();
            plani.deletePlanilla();
            desc.deleteDescuentos();
            MessageBox.Show("CORREOS ENVIADOS");
        }
        
        private void EnviarCorreo() {


                string mailOrigen = mail.getcorreoOrigen(planilla).ToString();
                string mailDestino = mail.getcorreodestino().ToString();
                string pass = parm.getPassword(planilla).ToString();
                string subject = asunto + " " + quincena + " Quincena " + mes + " " + anio;
                string body = "<p></p> " + " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " +
                                           " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " +
                                           " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " +
                                           " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " +
                                           " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  " +
                                           "Guatemala, " + quin.getUltimoDiaMes().ToString() +
                                // Estilos para color de las letras
                              "<style> .greytext {color: #838080;} .whitetext {color: #FFFFFF;} </ style > " +

                               "<p></p>" + "<H2>" + sal.getNombre().ToString() +
                               "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" + parm.getcodigo().ToString() + "</H2>" +

                               "<b><p>Recibi de PALKI, S.A. (GUATEMALA)</p></b> " +
                               sal.getMontoLetras().ToString() +
                              "<p></p> " + "Correspondiente a mi salario y bonificacion de la " + quincena + " Quincena " + " Del mes de " + mes + " de " + anio +
                              "<br> " + " " +
                              "<br> " + " " +
                              "<table>" +
                              "<tr>" +
                              "<td colspan='4' class = 'whitetext'>" + "_________________________________" + "</td>" +
                                "<td class = 'whitetext'>" + "Detalle de Desc" + "</td>" +
                                "<td colspan='3' >" + "<b>Detalle de Descuentos:</b>" + "</td>"
                              + "</tr>" +
                              "<tr>" + // SALARIO
                                "<td>" + "+" + "</td>" +
                                "<td>" + "Salario &nbsp; " + "</td>" +
                                "<td>" + "Q. " + "</td>" +
                                "<td align='right'>" + sal.getSalario().ToString() + "</td>" +
                                // DESCUENTOS IGSSS
                                "<td class = 'whitetext'>" + "Detalle de Desc" + "</td>" +
                                "<td class = 'greytext'>" + "IGSS &nbsp; " + "</td>" +
                                "<td class = 'greytext'>" + "Q. " + "</td>" +
                                "<td class = 'greytext' align='right'>" + desc.getIggs().ToString() + "</td>" +
                              "</tr>" +
                              "<tr>" +// BONIFICACION
                                "<td>" + "+" + "</td>" +
                                "<td>" + "Bonificacion &nbsp; " + "</td>" +
                                "<td>" + "Q. " + "</td>" +
                                "<td align='right'>" + sal.getBoni().ToString() + "</td>" +
                                // DESCUENTOS PRESTAMOS
                                "<td class = 'whitetext'>" + "Detalle de Desc" + "</td>" +
                                "<td class = 'greytext'>" + "Prestamo &nbsp; " + "</td>" +
                                "<td class = 'greytext'>" + "Q. " + "</td>" +
                                "<td class = 'greytext' align='right'>" + desc.getPrestamo().ToString() + "</td>" +
                              "</tr>" +
                              "<tr>" +// EXTRAS
                                "<td>" + "+" + "</td>" +
                                "<td>" + "Extras &nbsp; " + "</td>" +
                                "<td>" + "Q. " + "</td>" +
                                "<td align='right'>" + sal.getExtra().ToString() + "</td>" +
                              // DESCUENTOS SEGURO
                              "<td class = 'whitetext'>" + "Detalle de Desc" + "</td>" +
                              "<td class = 'greytext'>" + "Seguro &nbsp; " + "</td>" +
                              "<td class = 'greytext'>" + "Q. " + "</td>" +
                              "<td class = 'greytext' align='right'>" + desc.getSeguro().ToString() + "</td>" +
                              "</tr>" +
                              "<tr>" + //BONIF. RENDIMIENTO
                                "<td>" + "+" + "</td>" +
                                "<td>" + "Bonif. Rendimiento &nbsp; " + "</td>" +
                                "<td>" + "Q. " + "</td>" +
                                "<td align='right'>" + sal.getBoniRendi().ToString() + "</td>" +
                              //DESCUENTOS OTROS
                              "<td class = 'whitetext'>" + "Detalle de Desc" + "</td>" +
                              "<td class = 'greytext'>" + "Otros &nbsp; " + "</td>" +
                              "<td class = 'greytext'>" + "Q. " + "</td>" +
                              "<td class = 'greytext' align='right'>" + desc.getOtros().ToString() + "</td>" +
                              "</tr>" +
                              "<tr>" + // TOTAL DESCUENTOS
                                "<td class = 'greytext'>" + "-" + "</td>" +
                                "<td class = 'greytext'>" + "Descuentos &nbsp; " + "</td>" +
                                "<td class = 'greytext'>" + "Q. " + "</td>" +
                                "<td align='right' class = 'greytext'>" + sal.getTotalDesc().ToString() + "</td>" +
                                //DESCUENTOS ISR
                                "<td class = 'whitetext'>" + "Detalle de Desc" + "</td>" +
                                "<td class = 'greytext'>" + "ISR &nbsp; " + "</td>" +
                                "<td class = 'greytext'>" + "Q. " + "</td>" +
                                "<td class = 'greytext' align='right'>" + desc.getIsr().ToString() + "</td>" +
                              "</tr>" +
                              "<tr>" +
                                "<td colspan='4'>" + "_________________________________" + "</td>" +
                                "<td>" + " " + "</td>" +
                                "<td colspan='3'>" + "_____________________" + "</td>" +
                              "</tr>" +
                              "<tr>" +
                                "<td>" + " " + "</td>" + //TOTAL SALARIO
                                "<td>" + "<b>TOTAL &nbsp; " + " </b> " + " </td> " +
                                "<td>" + "<b>" + "Q. " + "</b>" + "</td>" +
                                "<td align='right' >" + "<b>" + sal.getLiquidoRecibir().ToString() + "</b>" + "</td>" +
                              //TOTAL DESCUENTOS
                              "<td class = 'whitetext'>" + "Detalle de Desc" + "</td>" +
                              "<td class = 'greytext'>" + "<b>TOTAL &nbsp; " + "</b>" + "</td>" +
                              "<td class = 'greytext'>" + "<b>" + "Q. " + "</b>" + "</td>" +
                              "<td class = 'greytext' align='right'>" + "<b>" + sal.getTotalDesc().ToString() + "</b>" + "</td>" +
                              "</tr>" +
                              "</table>" +
                              "<br> " + " " +
                              "<br> " + " " +
                              "<b><p>--- Correo generado automaticamente por favor no responda a esta cuenta de correo ---</p></b> " ;
                string dominio = mail.getcorreoDominio(planilla).ToString();
                MailMessage msng = new MailMessage(mailOrigen, mailDestino, subject, body);
                msng.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(dominio);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;

                smtp.Credentials = new System.Net.NetworkCredential(mailOrigen, pass);
                
            try
                {
                    smtp.Send(msng);
                }
                catch
                {

                    MessageBox.Show("Error al Enviar");
                }
            

        }

        private void getmes()
        {
            try
            {
               month  = new DataTable("stat");
               month = quin.Mes(ref error);
            }
            catch (Exception ex)
            {
                //showWarning(error + ex.ToString());
            }

        }

        private void frmInicio_Load(object sender, EventArgs e)
        {

        }

        private void chkprimera_CheckedChanged(object sender, EventArgs e)
        {
            if (chkprimera.Checked && chksegunda.Checked)
            {
                MessageBox.Show("DEBE SELECCIONAR UNA QUINCENA A LA VEZ");
                chkprimera.Checked = false;
            }
            else
            {

            }

        }

        private void chksegunda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkprimera.Checked && chksegunda.Checked)
            {
                MessageBox.Show("DEBE SELECCIONAR UNA QUINCENA A LA VEZ");
                chksegunda.Checked = false;
            }
            else
            {

            }
        }

        private void btnNuevoCorreo_Click(object sender, EventArgs e)
        {
            frmNuevoCorreo Nc = new frmNuevoCorreo();
            this.Visible = false;
            Nc.Visible = true;

        }

        public void updatePlanilla()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "Update tbl_planilla set valida = 0 WHERE codigo = " + "'" + parm.getcodigo().ToString() + "'" + " and valida = 1";
                command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                string error = "Error obteniendo ruta: " + ex.ToString();
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public void updateValidaPlani()
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "Update tbl_planilla set valida = 1 where valida is Null";
                command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                string error = "Error obteniendo ruta: " + ex.ToString();
            }
            finally
            {
                connection.connection.Close();
            }
        }


        private void CheKarQuincena() {
            Q = quin.getQuincena().ToString();
            if (Q == "Primera")
            {
                chkprimera.Checked = true;
            }
            else {
                chksegunda.Checked = true;
            }
        }


        private void chkadmon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkadmon.Checked && chkSaladmon.Checked)
            {
                MessageBox.Show("DEBE SELECCIONAR UNA PLANILLA A LA VEZ");
                chkadmon.Checked = false;
            }
            else
            {
                
            }
        }
        
        private void chkSaladmon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkadmon.Checked && chkSaladmon.Checked)
            {
                MessageBox.Show("DEBE SELECCIONAR UNA PLANILLA A LA VEZ");
                chkSaladmon.Checked = false;
            }
            else
            {

            }
        }
    }
}
