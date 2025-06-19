using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CardioCheck.Models;
using CardioCheck.Model;

namespace CardioCheck;

public partial class QuestionarioPage : ContentPage
{
    public QuestionarioPage()
    {
        InitializeComponent();
    }

    private async void OnEnviarQuestionarioClicked(object sender, EventArgs e)
    {
        try
        {
            // Validação simples de entrada
            if (!ValidateInputs())
            {
                ResultadoLabel.Text = "Por favor, preencha todos os campos corretamente.";
                ResultadoLabel.TextColor = Colors.Red;
                return;
            }

            var questionario = new Questionario
            {
                Nome = NomePacienteEntry.Text,
                Age = int.Parse(IdadeEntry.Text),
                Sex = SexoPicker.SelectedIndex,
                ChestPainType = TipoDorPeitoPicker.SelectedIndex + 1,
                RestingBloodPressure = float.Parse(PressaoArterialRepousoEntry.Text),
                SerumCholesterol = float.Parse(ColesterolSericoEntry.Text),
                FastingBloodSugar = GlicemiaJejumPicker.SelectedIndex,
                RestingECG = EletrocardiogramaRepousoPicker.SelectedIndex,
                MaxHeartRate = float.Parse(FrequenciaCardiacaMaximaEntry.Text),
                ExerciseAngina = AnginaExercicioPicker.SelectedIndex,
                Oldpeak = float.Parse(OldpeakEntry.Text),
                StSlope = InclinacaoSTPicker.SelectedIndex
            };

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessaoLogin.Token);
            var url = $"{SessaoLogin.UrlApi}/questionarios";

            var json = JsonSerializer.Serialize(questionario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var resultado = JsonSerializer.Deserialize<Resultado>(responseContent);
                var corResultado = resultado.Predicao == 1 ? Colors.Red : Colors.Green;
                var textoResultado = resultado.Predicao == 1 ? "ALTO RISCO" : "BAIXO RISCO";

                ResultadoLabel.Text = $"Resultado: {textoResultado}\n\nRecomendação: {resultado.Recomendacao}";
                ResultadoLabel.TextColor = corResultado;
            }
            else
            {
                ResultadoLabel.Text = $"Erro ao processar: {responseContent}";
                ResultadoLabel.TextColor = Colors.Red;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
        }
    }

    private bool ValidateInputs()
    {
        return !string.IsNullOrWhiteSpace(NomePacienteEntry.Text) &&
               int.TryParse(IdadeEntry.Text, out _) &&
               float.TryParse(PressaoArterialRepousoEntry.Text, out _) &&
               float.TryParse(ColesterolSericoEntry.Text, out _) &&
               float.TryParse(FrequenciaCardiacaMaximaEntry.Text, out _) &&
               float.TryParse(OldpeakEntry.Text, out _) &&
               SexoPicker.SelectedIndex != -1 &&
               TipoDorPeitoPicker.SelectedIndex != -1 &&
               GlicemiaJejumPicker.SelectedIndex != -1 &&
               EletrocardiogramaRepousoPicker.SelectedIndex != -1 &&
               AnginaExercicioPicker.SelectedIndex != -1 &&
               InclinacaoSTPicker.SelectedIndex != -1;
    }
}