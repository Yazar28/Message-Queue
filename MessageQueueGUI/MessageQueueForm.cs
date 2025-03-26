using System;
using System.Windows.Forms;
using MQClient.Library;
using MQClient.Models;

namespace MessageQueueGUI
{
    public partial class MessageQueueForm : Form
    {
        private MQClient.Library.MQClient? mqClient;

        public MessageQueueForm()
        {
            InitializeComponent();
        }

        private void MessageQueueForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBrokerIP.Text))
                txtBrokerIP.Text = "127.0.0.1";
            if (string.IsNullOrWhiteSpace(txtBrokerPort.Text))
                txtBrokerPort.Text = "5000";
            txtAppID.Text = UserSession.AppId.ToString();
            mqClient = new MQClient.Library.MQClient(txtBrokerIP.Text, int.Parse(txtBrokerPort.Text), UserSession.AppId);
        }

        private void btnSuscribirse_Click(object sender, EventArgs e)
        {
            if (mqClient == null)
            {
                MessageBox.Show("El cliente no está inicializado.");
                return;
            }
            string tema = txtTema.Text.Trim();
            if (string.IsNullOrEmpty(tema))
            {
                MessageBox.Show("Ingrese un nombre de tema.");
                return;
            }
            Topic topic = new Topic(tema);
            bool result = mqClient.Subscribe(topic);
            MessageBox.Show(result ? $"Suscrito al tema '{tema}'." : $"Ya es suscrito al tema '{tema}'.");
        }

        private void btnDesuscribirse_Click(object sender, EventArgs e)
        {
            if (mqClient == null)
            {
                MessageBox.Show("El cliente no está inicializado.");
                return;
            }
            string tema = txtTema.Text.Trim();
            if (string.IsNullOrEmpty(tema))
            {
                MessageBox.Show("Ingrese un nombre de tema.");
                return;
            }
            bool result = mqClient.Unsubscribe(new Topic(tema));
            MessageBox.Show(result ? $"Desuscrito del tema '{tema}'." : $"No se pudo desuscribir al tema '{tema}' porque no existe.");
        }

        private void btnPublicar_Click(object sender, EventArgs e)
        {
            if (mqClient == null)
            {
                MessageBox.Show("El cliente no está inicializado.");
                return;
            }
            string tema = txtTema.Text.Trim();
            if (string.IsNullOrEmpty(tema))
            {
                MessageBox.Show("Ingrese un nombre de tema.");
                return;
            }
            string contenido = txtContenidoPublicacion.Text.Trim();
            if (string.IsNullOrEmpty(contenido))
            {
                MessageBox.Show("Ingrese el contenido de la publicación.");
                return;
            }
            Topic topic = new Topic(tema);
            bool success = mqClient.Publish(topic, contenido);
            MessageBox.Show(success ? "Mensaje publicado correctamente." : "Error al publicar el mensaje.");
        }

        private void btnRecibir_Click(object sender, EventArgs e)
        {
            if (mqClient == null)
            {
                MessageBox.Show("El cliente no está inicializado.");
                return;
            }
            string tema = txtTema.Text.Trim();
            if (string.IsNullOrEmpty(tema))
            {
                MessageBox.Show("Ingrese un nombre de tema.");
                return;
            }
            Topic topic = new Topic(tema);
            string? mensaje = mqClient.Receive(topic);
            if (mensaje != null)
                txtRecibido.Text = mensaje;
            else
                MessageBox.Show($"No hay mensajes pendientes para el tema '{tema}'.");
        }
    }
}
