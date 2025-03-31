using System;
using System.Windows.Forms;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace MicrowaveApp.UI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private readonly HttpClient _apiClient = new HttpClient
        {
            // Webapi Base address
            BaseAddress = new Uri("https://localhost:7292")
        };

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Porfavor insira senha e login", "Erro de Validação");
                    return;
                }

                // Create the login data object
                var loginData = new
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text
                };

                // Serialize the object to JSON
                string jsonContent = JsonConvert.SerializeObject(loginData);

                // Create StringContent with JSON data
                var content = new StringContent(
                    jsonContent,
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _apiClient.PostAsync("/api/Auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    //Login successfull
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(string.IsNullOrEmpty(errorContent) ? "Credenciais inválidas" : errorContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
