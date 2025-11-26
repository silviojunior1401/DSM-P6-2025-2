using CardioCheck.Models;
using CardioCheck.Model;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers; // NECESSÁRIO
using System.Globalization;     // NECESSÁRIO
using System.Threading.Tasks;

namespace CardioCheck;




public partial class SonoPage : ContentPage
{
    // Use um HttpClient estático para melhor performance
    private static readonly HttpClient client = new HttpClient();

    public SonoPage()
    {
        InitializeComponent();

        // 1. Configura a subscrição para a limpeza do formulário (Chamado por ResultadoSonoPage)
        MessagingCenter.Subscribe<object>(this, "LimparFormularioSono", async (sender) =>
        {
            await ClearFormSono();
        });

        // Define os valores iniciais dos labels dos sliders
        OnSliderValueChanged(DuracaoSonoSlider, new ValueChangedEventArgs(DuracaoSonoSlider.Value, DuracaoSonoSlider.Value));
        OnSliderValueChanged(AtividadeSlider, new ValueChangedEventArgs(AtividadeSlider.Value, AtividadeSlider.Value));
        OnSliderValueChanged(PassosSlider, new ValueChangedEventArgs(PassosSlider.Value, PassosSlider.Value));
        OnSliderValueChanged(StressSlider, new ValueChangedEventArgs(StressSlider.Value, StressSlider.Value));
        OnSliderValueChanged(QualidadeSonoSlider, new ValueChangedEventArgs(QualidadeSonoSlider.Value, QualidadeSonoSlider.Value));
    }

    private async void OnAnalisarClicked(object sender, EventArgs e)
    {
        // 1. Validação
        if (!ValidateInputs())
        {
            await DisplayAlert("Erro de Validação", "Por favor, preencha todos os campos obrigatórios corretamente.", "OK");
            return;
        }

        // 2. Ativa o loader antes de começar o processamento pesado
        await SetLoadingState(true);

        try
        {
            // Cria o modelo de requisição
            var requestData = new SonoRequestModel
            {
                nome = NomeEntry.Text,
                gender = TraduzirGenero(SexoPicker.SelectedItem as string),
                age = int.TryParse(IdadeEntry.Text, out int i) ? i : 0,
                occupation = TraduzirOcupacao(OcupacaoPicker.SelectedItem as string),
                sleepDuration = double.Parse(DuracaoSonoSlider.Value.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                qualityOfSleep = (int)QualidadeSonoSlider.Value,
                physicalActivityLevel = (int)AtividadeSlider.Value,
                stressLevel = (int)StressSlider.Value,
                bmiCategory = TraduzirImc(ImcPicker.SelectedItem as string),
                bloodPressure = PressaoEntry.Text,
                heartRate = int.TryParse(FreqCardiacaEntry.Text, out int f) ? f : 0,
                dailySteps = (int)PassosSlider.Value
            };

            // Configurações da API
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessaoLogin.Token);
            var url = $"{SessaoLogin.UrlApi}/questionarios/sono";

            string jsonPayload = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Envia a requisição
            HttpResponseMessage response = await client.PostAsync(url, content);
            string responseBody = await response.Content.ReadAsStringAsync();



            if (response.IsSuccessStatusCode)
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponseSono>(responseBody);

                if (apiResponse?.Avaliacao == null) throw new Exception("Resposta inválida.");

                if (apiResponse.Avaliacao.Resultado != -1)
                {
                    // Resultado imediato
                    var resultado = new Resultado
                    {
                        Predicao = apiResponse.Avaliacao.Resultado,
                        Recomendacao = apiResponse.Avaliacao.Resultado == 1
                            ? "Indícios de distúrbio do sono identificados."
                            : "Padrões de sono normais."
                    };
                    await Navigation.PushAsync(new ResultadoSonoPage(requestData, resultado));
                }
                else
                {
                    // Polling necessário
                    // Como o loader já está ativo (SetLoadingState(true) foi chamado no início),
                    // apenas aguardamos o polling.

                    var resultadoFinal = await PollResultado(apiResponse.Avaliacao.Id);

                    await Navigation.PushAsync(new ResultadoSonoPage(requestData, resultadoFinal));
                }
            }



        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Crítico", $"Ocorreu um erro: {ex.Message}", "OK");
        }
        finally
        {
            // 3. Desativa o loader SEMPRE, independente de sucesso ou erro
            await SetLoadingState(false);
        }
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Atualiza os labels dos sliders
        if (sender == DuracaoSonoSlider)
        {
            DuracaoSonoLabel.Text = $"{e.NewValue:F1} horas";
        }
        else if (sender == AtividadeSlider)
        {
            AtividadeLabel.Text = $"{(int)e.NewValue}";
        }
        else if (sender == PassosSlider)
        {
            PassosLabel.Text = $"{(int)e.NewValue} passos";
        }
        else if (sender == StressSlider)
        {
            StressLabel.Text = $"{(int)e.NewValue}";
        }
        else if (sender == QualidadeSonoSlider)
        {
            QualidadeSonoLabel.Text = $"{(int)e.NewValue}";
        }
    }

    // --- MÉTODOS DE SUPORTE ---

    private bool ValidateInputs()
    {
        // Adiciona validação simples para campos obrigatórios
        return !string.IsNullOrWhiteSpace(NomeEntry.Text) &&
               int.TryParse(IdadeEntry.Text, out _) &&
               !string.IsNullOrWhiteSpace(PressaoEntry.Text) &&
               int.TryParse(FreqCardiacaEntry.Text, out _) &&
               SexoPicker.SelectedIndex != -1 &&
               OcupacaoPicker.SelectedIndex != -1 &&
               ImcPicker.SelectedIndex != -1;
    }

    // Adicione este método na classe SonoPage (pode ser logo antes ou depois dos métodos de validação)
    private async Task SetLoadingState(bool isLoading)
    {
        uint duration = 250; // Duração da animação em milissegundos
        Easing easing = Easing.CubicInOut; // Efeito de aceleração/desaceleração suave

        if (isLoading)
        {
            // Prepara o loader para a animação de FadeIn
            LoaderGrid.Opacity = 0;
            LoaderGrid.IsVisible = true;

            // Inicia as duas animações ao mesmo tempo
            await Task.WhenAll(
                MainScrollView.FadeTo(0.3, duration, easing), // Deixa o formulário semitransparente
                LoaderGrid.FadeTo(1, duration, easing) // Deixa o loader totalmente visível
            );

            // Esconde o formulário do layout para não ser clicável por baixo do loader
            MainScrollView.IsVisible = false;
        }
        else
        {
            // Prepara o formulário para a animação de FadeIn
            MainScrollView.Opacity = 0;
            MainScrollView.IsVisible = true;

            // Inicia as duas animações de volta ao mesmo tempo
            await Task.WhenAll(
                LoaderGrid.FadeTo(0, duration, easing), // Deixa o loader transparente
                MainScrollView.FadeTo(1, duration, easing) // Deixa o formulário totalmente visível
            );

            // Esconde o loader do layout após a animação
            LoaderGrid.IsVisible = false;
        }
    }

    public async Task ClearFormSono()
    {
        // Implementação da limpeza do formulário (chamado via MessagingCenter)
        NomeEntry.Text = string.Empty;
        IdadeEntry.Text = string.Empty;
        PressaoEntry.Text = string.Empty;
        FreqCardiacaEntry.Text = string.Empty;

        // Reseta Pickers
        SexoPicker.SelectedIndex = -1;
        OcupacaoPicker.SelectedIndex = -1;
        ImcPicker.SelectedIndex = -1;

        // Reseta Sliders para os valores iniciais
        DuracaoSonoSlider.Value = 8.0;
        AtividadeSlider.Value = 50;
        PassosSlider.Value = 5000;
        StressSlider.Value = 4;
        QualidadeSonoSlider.Value = 7;

        // CORREÇÃO: Usa o nome da instância da ScrollView
        await MainScrollView.ScrollToAsync(0, 0, true);
    }

    // --- MÉTODOS DE TRADUÇÃO ---

    private int? TraduzirGenero(string generoPt)
    {
        return generoPt switch
        {
            "Feminino" => 0,
            "Masculino" => 1,
            _ => null,
        };
    }

    private string TraduzirImc(string imcPt)
    {
        return imcPt switch
        {
            "Abaixo do peso" => "Underweight",
            "Peso normal" => "Normal",
            "Sobrepeso" => "Overweight",
            "Obesidade" => "Obese",
            _ => null,
        };
    }

    private string TraduzirOcupacao(string ocupacaoPt)
    {
        return ocupacaoPt switch
        {
            "Médico(a)" => "Doctor",
            "Engenheiro(a)" => "Engineer",
            "Enfermeiro(a)" => "Nurse",
            "Professor(a)" => "Teacher",
            "Advogado(a)" => "Lawyer",
            "Engenheiro(a) de Software" => "Software Engineer",
            "Cientista" => "Scientist",
            "Contador(a)" => "Accountant",
            "Gerente" => "Manager",
            "Representante de Vendas" => "Sales Representative",
            "Vendedor(a)" => "Salesperson",
            _ => null,
        };
    }

    private async Task<Resultado> PollResultado(string id)
    {
        int maxAttempts = 15; // Tenta por aprox. 2 minutos (ajustado para ser mais tolerante)
        int attempts = 0;
        string url = $"{SessaoLogin.UrlApi}/historico/sono";

        // CONFIGURAÇÃO CRÍTICA: Ignora se é maiúscula ou minúscula (id vs Id)
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        while (attempts < maxAttempts)
        {
            // Backoff progressivo
            int delay = 3000; // 3 segundos
            if (attempts >= 3) delay = 5000;
            if (attempts >= 6) delay = 10000; // 10 segundos

            await Task.Delay(delay);
            attempts++;

            try
            {
                // Nota: O header de Authorization já está no client estático, 
                // mas se der erro de 401, reatribua aqui:
                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessaoLogin.Token);

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    // Desserializa usando as opções CaseInsensitive
                    var historico = JsonSerializer.Deserialize<List<AvaliacaoSono>>(content, jsonOptions);

                    // Procura o ID na lista
                    var avaliacao = historico?.FirstOrDefault(a => a.Id == id);

                    // Debug: Ajuda a ver se encontrou
                    if (avaliacao == null)
                        System.Diagnostics.Debug.WriteLine($"[Polling] Tentativa {attempts}: ID {id} não encontrado na lista.");
                    else
                        System.Diagnostics.Debug.WriteLine($"[Polling] Tentativa {attempts}: ID encontrado. Resultado: {avaliacao.Resultado}");

                    // Se encontrou e o resultado mudou de -1
                    if (avaliacao != null && avaliacao.Resultado != -1)
                    {
                        return avaliacao.ToResultado();
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[Polling] Erro API: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Apenas loga o erro e continua tentando
                System.Diagnostics.Debug.WriteLine($"[Polling] Erro de exceção: {ex.Message}");
            }
        }

        throw new Exception("O servidor demorou muito para responder. Tente consultar o histórico mais tarde.");
    }





}


public class ApiResponseSono
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string Message { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("avaliacao")]
    public AvaliacaoSonoResponseItem Avaliacao { get; set; }
}

public class AvaliacaoSonoResponseItem
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    public string Id { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("resultado")]
    public int Resultado { get; set; }
}