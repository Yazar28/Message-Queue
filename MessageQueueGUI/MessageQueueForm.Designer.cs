using System.Windows.Forms;
using System.Drawing;

namespace MessageQueueGUI
{
    partial class MessageQueueForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblBrokerIP;
        private TextBox txtBrokerIP;
        private Label lblBrokerPort;
        private TextBox txtBrokerPort;
        private Label lblAppID;
        private TextBox txtAppID;
        private Label lblTema;
        private TextBox txtTema;
        private Button btnSuscribirse;
        private Button btnDesuscribirse;
        private Label lblContenido;
        private TextBox txtContenidoPublicacion;
        private Button btnPublicar;
        private Label lblRecibido;
        private TextBox txtRecibido;
        private Button btnRecibir;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblBrokerIP = new Label();
            txtBrokerIP = new TextBox();
            lblBrokerPort = new Label();
            txtBrokerPort = new TextBox();
            lblAppID = new Label();
            txtAppID = new TextBox();
            lblTema = new Label();
            txtTema = new TextBox();
            btnSuscribirse = new Button();
            btnDesuscribirse = new Button();
            lblContenido = new Label();
            txtContenidoPublicacion = new TextBox();
            btnPublicar = new Button();
            lblRecibido = new Label();
            txtRecibido = new TextBox();
            btnRecibir = new Button();
            SuspendLayout();

            lblBrokerIP.Location = new Point(30, 20);
            lblBrokerIP.Name = "lblBrokerIP";
            lblBrokerIP.Size = new Size(100, 23);
            lblBrokerIP.TabIndex = 0;
            lblBrokerIP.Text = "MQ Broker IP:";
 
            txtBrokerIP.Location = new Point(140, 20);
            txtBrokerIP.Name = "txtBrokerIP";
            txtBrokerIP.Size = new Size(233, 23);
            txtBrokerIP.TabIndex = 1;
            txtBrokerIP.TextAlign = HorizontalAlignment.Center;

            lblBrokerPort.Location = new Point(30, 60);
            lblBrokerPort.Name = "lblBrokerPort";
            lblBrokerPort.Size = new Size(100, 23);
            lblBrokerPort.TabIndex = 2;
            lblBrokerPort.Text = "MQ Broker Port:";

            txtBrokerPort.Location = new Point(140, 60);
            txtBrokerPort.Name = "txtBrokerPort";
            txtBrokerPort.Size = new Size(233, 23);
            txtBrokerPort.TabIndex = 3;
            txtBrokerPort.TextAlign = HorizontalAlignment.Center;

            lblAppID.Location = new Point(30, 100);
            lblAppID.Name = "lblAppID";
            lblAppID.Size = new Size(100, 23);
            lblAppID.TabIndex = 4;
            lblAppID.Text = "AppID:";

            txtAppID.Location = new Point(140, 100);
            txtAppID.Name = "txtAppID";
            txtAppID.ReadOnly = true;
            txtAppID.Size = new Size(233, 23);
            txtAppID.TabIndex = 5;
            txtAppID.TextAlign = HorizontalAlignment.Center;

            lblTema.Location = new Point(30, 140);
            lblTema.Name = "lblTema";
            lblTema.Size = new Size(100, 23);
            lblTema.TabIndex = 6;
            lblTema.Text = "Tema:";

            txtTema.Location = new Point(140, 140);
            txtTema.Name = "txtTema";
            txtTema.Size = new Size(233, 23);
            txtTema.TabIndex = 7;
            txtTema.TextAlign = HorizontalAlignment.Center;
 
            btnSuscribirse.Location = new Point(30, 180);
            btnSuscribirse.Name = "btnSuscribirse";
            btnSuscribirse.Size = new Size(120, 30);
            btnSuscribirse.TabIndex = 8;
            btnSuscribirse.Text = "Suscribirse";
            btnSuscribirse.Click += btnSuscribirse_Click;

            btnDesuscribirse.Location = new Point(253, 180);
            btnDesuscribirse.Name = "btnDesuscribirse";
            btnDesuscribirse.Size = new Size(120, 30);
            btnDesuscribirse.TabIndex = 9;
            btnDesuscribirse.Text = "Desuscribirse";
            btnDesuscribirse.Click += btnDesuscribirse_Click;

            lblContenido.Location = new Point(426, 33);
            lblContenido.Name = "lblContenido";
            lblContenido.Size = new Size(180, 23);
            lblContenido.TabIndex = 10;
            lblContenido.Text = "Contenido de la publicación:";
 
            txtContenidoPublicacion.Location = new Point(426, 63);
            txtContenidoPublicacion.Name = "txtContenidoPublicacion";
            txtContenidoPublicacion.Multiline = true;
            txtContenidoPublicacion.Size = new Size(200, 100);
            txtContenidoPublicacion.TabIndex = 11;

            btnPublicar.Location = new Point(426, 173);
            btnPublicar.Name = "btnPublicar";
            btnPublicar.Size = new Size(100, 30);
            btnPublicar.TabIndex = 12;
            btnPublicar.Text = "Publicar";
            btnPublicar.Click += btnPublicar_Click;
 
            lblRecibido.Location = new Point(658, 33);
            lblRecibido.Name = "lblRecibido";
            lblRecibido.Size = new Size(180, 23);
            lblRecibido.TabIndex = 13;

            txtRecibido.Location = new Point(658, 63);
            txtRecibido.Multiline = true;
            txtRecibido.Name = "txtRecibido";
            txtRecibido.ReadOnly = true;
            txtRecibido.Size = new Size(200, 100);
            txtRecibido.TabIndex = 14;

            btnRecibir.Location = new Point(658, 173);
            btnRecibir.Name = "btnRecibir";
            btnRecibir.Size = new Size(100, 30);
            btnRecibir.TabIndex = 15;
            btnRecibir.Text = "Recibir";
            btnRecibir.Click += btnRecibir_Click;

            ClientSize = new Size(884, 230);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblBrokerIP);
            Controls.Add(txtBrokerIP);
            Controls.Add(lblBrokerPort);
            Controls.Add(txtBrokerPort);
            Controls.Add(lblAppID);
            Controls.Add(txtAppID);
            Controls.Add(lblTema);
            Controls.Add(txtTema);
            Controls.Add(btnSuscribirse);
            Controls.Add(btnDesuscribirse);
            Controls.Add(lblContenido);
            Controls.Add(txtContenidoPublicacion);
            Controls.Add(btnPublicar);
            Controls.Add(lblRecibido);
            Controls.Add(txtRecibido);
            Controls.Add(btnRecibir);
            Name = "MessageQueueForm";
            Text = "Message Queue GUI";
            Load += MessageQueueForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
