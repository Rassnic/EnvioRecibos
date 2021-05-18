namespace REC_PLANILLA
{
    partial class frmInicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEnviar = new System.Windows.Forms.Button();
            this.chkSaladmon = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkadmon = new System.Windows.Forms.CheckBox();
            this.chkprimera = new System.Windows.Forms.CheckBox();
            this.chksegunda = new System.Windows.Forms.CheckBox();
            this.cmbMes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnNuevoCorreo = new System.Windows.Forms.Button();
            this.lblanio = new System.Windows.Forms.Label();
            this.lblaniotext = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(166, 206);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(91, 23);
            this.btnEnviar.TabIndex = 0;
            this.btnEnviar.Text = "Enviar Correos";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // chkSaladmon
            // 
            this.chkSaladmon.AutoSize = true;
            this.chkSaladmon.Location = new System.Drawing.Point(103, 74);
            this.chkSaladmon.Name = "chkSaladmon";
            this.chkSaladmon.Size = new System.Drawing.Size(75, 17);
            this.chkSaladmon.TabIndex = 1;
            this.chkSaladmon.Text = "ADMON II";
            this.chkSaladmon.UseVisualStyleBackColor = true;
            this.chkSaladmon.CheckedChanged += new System.EventHandler(this.chkSaladmon_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ENVIO DE RECIBOS DE PLANILLA";
            // 
            // chkadmon
            // 
            this.chkadmon.AutoSize = true;
            this.chkadmon.Location = new System.Drawing.Point(103, 39);
            this.chkadmon.Name = "chkadmon";
            this.chkadmon.Size = new System.Drawing.Size(119, 17);
            this.chkadmon.TabIndex = 3;
            this.chkadmon.Text = "ADMINISTRACION";
            this.chkadmon.UseVisualStyleBackColor = true;
            this.chkadmon.CheckedChanged += new System.EventHandler(this.chkadmon_CheckedChanged);
            // 
            // chkprimera
            // 
            this.chkprimera.AutoSize = true;
            this.chkprimera.Location = new System.Drawing.Point(38, 149);
            this.chkprimera.Name = "chkprimera";
            this.chkprimera.Size = new System.Drawing.Size(61, 17);
            this.chkprimera.TabIndex = 4;
            this.chkprimera.Text = "Primera";
            this.chkprimera.UseVisualStyleBackColor = true;
            this.chkprimera.CheckedChanged += new System.EventHandler(this.chkprimera_CheckedChanged);
            // 
            // chksegunda
            // 
            this.chksegunda.AutoSize = true;
            this.chksegunda.Location = new System.Drawing.Point(38, 173);
            this.chksegunda.Name = "chksegunda";
            this.chksegunda.Size = new System.Drawing.Size(69, 17);
            this.chksegunda.TabIndex = 5;
            this.chksegunda.Text = "Segunda";
            this.chksegunda.UseVisualStyleBackColor = true;
            this.chksegunda.CheckedChanged += new System.EventHandler(this.chksegunda_CheckedChanged);
            // 
            // cmbMes
            // 
            this.cmbMes.FormattingEnabled = true;
            this.cmbMes.Location = new System.Drawing.Point(188, 147);
            this.cmbMes.Name = "cmbMes";
            this.cmbMes.Size = new System.Drawing.Size(91, 21);
            this.cmbMes.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Quincena";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(185, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Mes";
            // 
            // btnNuevoCorreo
            // 
            this.btnNuevoCorreo.Location = new System.Drawing.Point(38, 206);
            this.btnNuevoCorreo.Name = "btnNuevoCorreo";
            this.btnNuevoCorreo.Size = new System.Drawing.Size(112, 23);
            this.btnNuevoCorreo.TabIndex = 9;
            this.btnNuevoCorreo.Text = "Crear Nuevo Correo";
            this.btnNuevoCorreo.UseVisualStyleBackColor = true;
            this.btnNuevoCorreo.Click += new System.EventHandler(this.btnNuevoCorreo_Click);
            // 
            // lblanio
            // 
            this.lblanio.AutoSize = true;
            this.lblanio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblanio.Location = new System.Drawing.Point(243, 177);
            this.lblanio.Name = "lblanio";
            this.lblanio.Size = new System.Drawing.Size(31, 15);
            this.lblanio.TabIndex = 10;
            this.lblanio.Text = "anio";
            // 
            // lblaniotext
            // 
            this.lblaniotext.AutoSize = true;
            this.lblaniotext.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblaniotext.Location = new System.Drawing.Point(185, 175);
            this.lblaniotext.Name = "lblaniotext";
            this.lblaniotext.Size = new System.Drawing.Size(36, 17);
            this.lblaniotext.TabIndex = 11;
            this.lblaniotext.Text = "Año";
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 238);
            this.Controls.Add(this.lblaniotext);
            this.Controls.Add(this.lblanio);
            this.Controls.Add(this.btnNuevoCorreo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbMes);
            this.Controls.Add(this.chksegunda);
            this.Controls.Add(this.chkprimera);
            this.Controls.Add(this.chkadmon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSaladmon);
            this.Controls.Add(this.btnEnviar);
            this.Name = "frmInicio";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmInicio_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.CheckBox chkSaladmon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkadmon;
        private System.Windows.Forms.CheckBox chkprimera;
        private System.Windows.Forms.CheckBox chksegunda;
        private System.Windows.Forms.ComboBox cmbMes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnNuevoCorreo;
        private System.Windows.Forms.Label lblanio;
        private System.Windows.Forms.Label lblaniotext;
    }
}

