using System.Text;
using System.Text.Json;
using Microsoft.Maui.Controls;
using CardioCheck.Models;
using System.Net.Http.Headers;

namespace CardioCheck;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

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
            var jsonResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine(jsonResponse);  // 🔧 Para debug

            if (response.IsSuccessStatusCode)
            {
                using var document = JsonDocument.Parse(jsonResponse);

                if (document.RootElement.TryGetProperty("accessToken", out var tokenElement))
                {
                    var token = tokenElement.GetString();

                    SessaoLogin.Email = email;
                    SessaoLogin.Token = token;

                    Application.Current.MainPage = new NavigationPage(new MenuPrincipal());
                }
                else
                {
                    await DisplayAlert("Erro", "Token não encontrado na resposta. Verifique usuário e senha.", "OK");
                    MensagemErro.Text = "Usuário ou senha incorretos.";
                }
            }
            else
            {
                await DisplayAlert("Erro", $"Erro ao fazer login:\n{jsonResponse}", "OK");
                MensagemErro.Text = "Falha no login. Verifique os dados.";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Erro ao conectar: {ex.Message}", "OK");
        }
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroPage());
    }
}
