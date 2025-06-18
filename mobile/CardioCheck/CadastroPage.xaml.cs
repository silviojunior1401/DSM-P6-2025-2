
using System.Text;
using System.Text.Json;
using Microsoft.Maui.Controls;
using CardioCheck.Models;

namespace CardioCheck;

public partial class CadastroPage : ContentPage
{
    public CadastroPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        string nome = NomeEntry.Text?.Trim();
        string crm = CrmEntry.Text?.Trim();
        string email = EmailEntry.Text?.Trim();
        string senha = SenhaEntry.Text?.Trim();
        string especialidade = EspecialidadeEntry.Text?.Trim();

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(crm) ||
            string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha) ||
            string.IsNullOrEmpty(especialidade))
        {
            MensagemLabel.Text = "Preencha todos os campos.";
            return;
        }

        try
        {
            var httpClient = new HttpClient();
            var url = $"{SessaoLogin.UrlApi}/medicos";

            var payload = new
            {
                nome,
                crm,
                email,
                senha,
                especialidade
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            

            var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                MensagemLabel.TextColor = Colors.Green;
                MensagemLabel.Text = "Cadastro realizado com sucesso!";
                LimparCampos();
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                var erro = await response.Content.ReadAsStringAsync();
                MensagemLabel.TextColor = Colors.Red;
                MensagemLabel.Text = $"Erro no cadastro: {erro}";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Erro ao cadastrar: {ex.Message}", "OK");
        }
    }

    private void LimparCampos()
    {
        NomeEntry.Text = string.Empty;
        CrmEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
        SenhaEntry.Text = string.Empty;
        EspecialidadeEntry.Text = string.Empty;
    }
}
