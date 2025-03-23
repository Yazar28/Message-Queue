using System.Windows.Forms;
using System.Drawing;

namespace MessageQueueGUI
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblUsername;
        private TextBox txtUsername;
        private Button btnAccept;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblUsername = new Label();
            txtUsername = new TextBox();
            btnAccept = new Button();
            btnCancel = new Button();
            SuspendLayout();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            lblUsername.Location = new Point(20, 20);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(300, 23);
            lblUsername.Text = "Ingrese su nombre de usuario:";
            txtUsername.Location = new Point(20, 50);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(300, 23);
            btnAccept.Location = new Point(20, 90);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(140, 30);
            btnAccept.Text = "Aceptar";
            btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            btnCancel.Location = new Point(180, 90);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(140, 30);
            btnCancel.Text = "Cancelar";
            btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            ClientSize = new Size(350, 150);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(btnAccept);
            Controls.Add(btnCancel);
            Name = "LoginForm";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
