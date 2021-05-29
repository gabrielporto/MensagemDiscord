using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MensagemDiscord
{
    public partial class Form1 : Form
    {
        private static string configFile = "Config.json";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(txtUrlHook.Text);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(new { content = txtMensagem.Text, username = txtUsuario.Text });
                    streamWriter.Write(json);
                }
                httpWebRequest.GetResponseAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(configFile, JsonConvert.SerializeObject(new Config
                {
                    Usuario = txtUsuario.Text,
                    Url = txtUrlHook.Text,
                    Mensagem = txtMensagem.Text
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show("Atualizado");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(configFile))
            {
                Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFile));

                txtUsuario.Text = config.Usuario;
                txtUrlHook.Text = config.Url;
                txtMensagem.Text = config.Mensagem;

            }
            else
            {
                File.WriteAllText(configFile, JsonConvert.SerializeObject(Config.NewConfig()));
            }
        }
    }
}
