using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace MessageQueueGUI
{
    public partial class LoginForm : Form
    {
        private readonly string usersFilePath = @"C:\Users\Usuario\OneDrive\Desktop\Programación\Algoritmo y Estructuras de Datos\Message Queue\MessageQueueGUI\Data\users.json";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Debe ingresar un nombre de usuario.");
                return;
            }
            Guid appId = LoadOrCreateAppId(username);
            UserSession.Username = username;
            UserSession.AppId = appId;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private Guid LoadOrCreateAppId(string username)
        {
            Dictionary<string, string> users = new Dictionary<string, string>();
            if (File.Exists(usersFilePath))
            {
                try
                {
                    string json = File.ReadAllText(usersFilePath);
                    users = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
                }
                catch { }
            }
            if (users.ContainsKey(username))
            {
                if (Guid.TryParse(users[username], out Guid savedId))
                    return savedId;
            }
            Guid newId = Guid.NewGuid();
            users[username] = newId.ToString();
            File.WriteAllText(usersFilePath, JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true }));
            return newId;
        }
    }
}
