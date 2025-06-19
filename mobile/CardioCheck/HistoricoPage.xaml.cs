using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text.Json;
using CardioCheck.Models;
using CardioCheck.Model;


namespace CardioCheck;

public partial class HistoricoPage : ContentPage
{
    public ObservableCollection<Avaliacao> Historico { get; set; } = new();

    public HistoricoPage()
    {
        InitializeComponent();
        HistoricoCollectionView.ItemsSource = Historico;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadHistorico();
    }

    private async Task LoadHistorico()
    {
        LoadingIndicator.IsRunning = true;
        try
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessaoLogin.Token);
            var url = $"{SessaoLogin.UrlApi}/historico";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var historicoList = JsonSerializer.Deserialize<List<Avaliacao>>(content);

                Historico.Clear();
                foreach (var item in historicoList)
                {
                    Historico.Add(item);
                }
            }
            else
            {
                await DisplayAlert("Erro", "Não foi possível carregar o histórico.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro de conexão: {ex.Message}", "OK");
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
        }
    }
}