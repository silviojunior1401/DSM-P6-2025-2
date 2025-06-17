using System.Text;
using System.Text.Json;
using Microsoft.Maui.Controls;
using CardioCheck.Models;

namespace CardioCheck;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    // Método para fazer login
    private async void OnEntrarClicked(object sender, EventArgs e)
    {
        string email = UsuarioEntry.Text?.Trim();
        string senha = SenhaEntry.Text?.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
        {
            MensagemErro.Text = "Preencha usuário e senha.";
            return;
        }

        try
        {
            var httpClient = new HttpClient();
            var url = $"{SessaoLogin.UrlApi}/auth/login";

            var payload = new
            {
                email = email,
                senha = senha
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                using var document = JsonDocument.Parse(jsonResponse);

                var token = document.RootElement.GetProperty("token").GetString();

                SessaoLogin.Email = email;
                SessaoLogin.Token = token;

                // Vai para o Menu Principal
                Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
            }
            else
            {
                MensagemErro.Text = "Usuário ou senha incorretos.";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Erro ao conectar: {ex.Message}", "OK");
        }
    }

    // Método para ir para tela de cadastro
    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroPage());
    }
}
