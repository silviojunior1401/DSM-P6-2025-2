using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CardioCheck.Models;
using CardioCheck.Model;
using System.Threading.Tasks;

namespace CardioCheck;

// Classes auxiliares para mapear a resposta do Backend
public class ApiResponseCoracao
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string Message { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("avaliacao")]
    public AvaliacaoResponseItem Avaliacao { get; set; }
}

public class AvaliacaoResponseItem
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    public string Id { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("resultado")]
    public int Resultado { get; set; }
}

public partial class QuestionarioPage : ContentPage
{
    public QuestionarioPage()
    {
        InitializeComponent();

        MessagingCenter.Subscribe<object>(this, "LimparFormulario", async (sender) => // Adiciona o "async" aqui
        {
            await ClearForm(); // Agora o await funciona
        });
    }
    private void OnAnginaSwitchToggled(object sender, ToggledEventArgs e)
    {
        bool isToggled = e.Value;
        if (isToggled)
        {
            AnginaExercicioLabel.Text = "Sim";
            AnginaExercicioLabel.TextColor = Colors.Green;
        }
        else
        {
            AnginaExercicioLabel.Text = "Não";
            AnginaExercicioLabel.TextColor = Colors.Gray; // Ou Colors.Red
        }
    }
    private void OnGlicemiaSwitchToggled(object sender, ToggledEventArgs e)
    {
        bool isToggled = e.Value;
        if (isToggled)
        {
            GlicemiaLabel.Text = "Sim";
            GlicemiaLabel.TextColor = Colors.Green;
        }
        else
        {
            GlicemiaLabel.Text = "Não";
            GlicemiaLabel.TextColor = Colors.Gray;
        }
    }

    private void OnIdadeEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        // Garante que o sender é um Entry e que o texto não está vazio
        if (sender is not Entry idadeEntry || string.IsNullOrEmpty(e.NewTextValue))
        {
            return;
        }


        if (int.TryParse(e.NewTextValue, out int idade))
        {
            if (idade < 1 || idade > 110)
            {

                idadeEntry.Text = e.OldTextValue ?? "";
            }
        }
        else
        {
          
            idadeEntry.Text = e.OldTextValue ?? "";
        }
    }

    private async void OnInfoEcgTapped(object sender, TappedEventArgs e)
    {
        // Crie aqui o seu pop-up com as informações detalhadas sobre o ECG
        await DisplayAlert(
            "Eletrocardiograma em Repouso",
            "Resultados:\n\n" +
            "• Normal: Sem anormalidades significativas.\n\n" +
            "• Anormalidade da onda ST-T: Pode indicar problemas de repolarização ventricular, isquemia, entre outros.\n\n" +
            "• Hipertrofia ventricular esquerda: Sugere um aumento da massa muscular do ventrículo esquerdo, frequentemente associado à hipertensão.",
            "OK"
        );
    }

    private async void OnInfoDorPeitoTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert(
            "Tipos de Dor no Peito",
            "• Angina Típica: Dor torácica clássica relacionada ao esforço ou estresse, aliviada com repouso.\n\n" +
            "• Angina Atípica: Dor que não preenche todos os critérios da angina típica.\n\n" +
            "• Dor Não-anginosa: Dor no peito que provavelmente não é de origem cardíaca.\n\n" +
            "• Assintomático: Ausência de dor no peito.",
            "OK"
        );
    }

    private async void OnInfoOldpeakTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert(
            "Oldpeak",
            "Refere-se à depressão do segmento ST induzida pelo exercício em relação ao estado de repouso.\n\n" +
            "É um importante indicador eletrocardiográfico de isquemia miocárdica.",
            "OK"
        );
    }


    private async void OnInfoInclinacaoSTTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert(
            "Inclinação do Pico do Segmento ST", // Título do Pop-up
            "Este campo descreve a inclinação do segmento ST no eletrocardiograma durante o pico do exercício.\n\n" +
            "• Normal (Flat): Risco intermediário.\n\n" +
            "• Ascendente (Upsloping): Geralmente considerado de baixo risco.\n\n" +
            "• Descendente (Downsloping): Geralmente indica um maior risco de isquemia miocárdica (fluxo sanguíneo inadequado para o coração).", // Mensagem explicativa
            "Entendi" // Texto do botão para fechar
        );
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

            await SetLoadingState(true);

            // Ativa o loader e esconde o formulário
            SetLoadingState(true);

            var questionario = new Questionario
            {
                Nome = NomePacienteEntry.Text,
                Age = int.Parse(IdadeEntry.Text),
                Sex = SexoMasculinoRadio.IsChecked ? 1 : 0,
                ChestPainType = TipoDorPeitoPicker.SelectedIndex + 1,
                RestingBloodPressure = float.Parse(PressaoArterialRepousoEntry.Text),
                SerumCholesterol = float.Parse(ColesterolSericoEntry.Text),
                FastingBloodSugar = GlicemiaSwitch.IsToggled ? 1 : 0,
                RestingECG = EletrocardiogramaRepousoPicker.SelectedIndex,
                MaxHeartRate = float.Parse(FrequenciaCardiacaMaximaEntry.Text),
                ExerciseAngina = AnginaExercicioSwitch.IsToggled ? 1 : 0,
                Oldpeak = float.Parse(OldpeakEntry.Text),
                StSlope = InclinacaoSTPicker.SelectedIndex +1
            };

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessaoLogin.Token);
            var url = $"{SessaoLogin.UrlApi}/questionarios/coracao";

            var json = JsonSerializer.Serialize(questionario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);

            var responseContent = await response.Content.ReadAsStringAsync();



            // ================== ALTERAÇÃO PRINCIPAL AQUI ==================
            if (response.IsSuccessStatusCode)
            {
                // 1. Desserializa a resposta estruturada (Wrapper)
                var apiResponse = JsonSerializer.Deserialize<ApiResponseCoracao>(responseContent);

                if (apiResponse?.Avaliacao == null)
                {
                    throw new Exception("Resposta inválida do servidor.");
                }

                // 2. Verifica se já temos o resultado ou se precisa de Polling
                if (apiResponse.Avaliacao.Resultado != -1)
                {
                    // Resultado imediato
                    var resultadoObj = new Resultado
                    {
                        Predicao = apiResponse.Avaliacao.Resultado,
                        Recomendacao = apiResponse.Avaliacao.Resultado == 1
                            ? "Paciente apresenta alto risco cardiovascular..." // (Texto completo igual ao frontend)
                            : "Paciente apresenta baixo risco cardiovascular..."
                    };

                    await Navigation.PushAsync(new ResultadoPage(questionario, resultadoObj));
                }
                else
                {
                    // 3. Resultado pendente (-1): Inicia Polling
                    // Atualiza a UI para informar o usuário
                    ResultadoLabel.Text = "Processando IA... Aguarde.";
                    ResultadoLabel.TextColor = Colors.Orange;

                    // Chama o polling passando o ID recebido
                    var resultadoFinal = await PollResultado(apiResponse.Avaliacao.Id);

                    await Navigation.PushAsync(new ResultadoPage(questionario, resultadoFinal));
                }

                ResultadoLabel.Text = string.Empty;
            }
            // ==============================================================



        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
        }
        finally
        {
            // Desativa o loader e mostra o formulário, independentemente do resultado
            await SetLoadingState(false);
        }
    }

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
                MainContentScrollView.FadeTo(0.3, duration, easing), // Deixa o formulário semitransparente
                LoaderGrid.FadeTo(1, duration, easing) // Deixa o loader totalmente visível
            );

            // Esconde o formulário do layout para não ser clicável por baixo do loader
            MainContentScrollView.IsVisible = false;
        }
        else
        {
            // Prepara o formulário para a animação de FadeIn
            MainContentScrollView.Opacity = 0;
            MainContentScrollView.IsVisible = true;

            // Inicia as duas animações de volta ao mesmo tempo
            await Task.WhenAll(
                LoaderGrid.FadeTo(0, duration, easing), // Deixa o loader transparente
                MainContentScrollView.FadeTo(1, duration, easing) // Deixa o formulário totalmente visível
            );

            // Esconde o loader do layout após a animação
            LoaderGrid.IsVisible = false;
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
               TipoDorPeitoPicker.SelectedIndex != -1 &&             
               EletrocardiogramaRepousoPicker.SelectedIndex != -1 &&
               InclinacaoSTPicker.SelectedIndex != -1;
    }
    public async Task ClearForm()
    {
        // Limpa os campos de texto
        NomePacienteEntry.Text = string.Empty;
        IdadeEntry.Text = string.Empty;

        // Redefine os RadioButtons
        SexoMasculinoRadio.IsChecked = true;

        // Redefine os Pickers
        TipoDorPeitoPicker.SelectedIndex = -1;
        EletrocardiogramaRepousoPicker.SelectedIndex = -1;
        InclinacaoSTPicker.SelectedIndex = -1;

        // Redefine os Switches
        GlicemiaSwitch.IsToggled = false;
        AnginaExercicioSwitch.IsToggled = false;

        // Redefine os Sliders para seus valores padrão
        PressaoSlider.Value = 120;
        ColesterolSlider.Value = 200;
        FreqCardiacaSlider.Value = 150;
        OldpeakSlider.Value = 1.0;

        await MainContentScrollView.ScrollToAsync(0, 0, true);
    }


    private async Task<Resultado> PollResultado(string id)
    {
        int maxAttempts = 15;
        int attempts = 0;
        string url = $"{SessaoLogin.UrlApi}/historico/coracao"; // Endpoint de histórico

        using (var pollingClient = new HttpClient())
        {
            pollingClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessaoLogin.Token);

            while (attempts < maxAttempts)
            {
                // Lógica de espera (Backoff) igual ao Frontend
                int delay = 15000;
                if (attempts < 3) delay = 3000;
                else if (attempts < 6) delay = 10000;

                await Task.Delay(delay);
                attempts++;

                try
                {
                    var response = await pollingClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        // O endpoint retorna uma lista de avaliações. Precisamos encontrar a nossa pelo ID.
                        var historico = JsonSerializer.Deserialize<List<Avaliacao>>(content);
                        var avaliacao = historico?.FirstOrDefault(a => a.Data != null);
                        // Nota: O modelo Avaliacao do mobile não parece ter o campo ID mapeado explicitamente no arquivo enviado,
                        // mas precisaremos identificar a avaliação correta. 
                        // SUGESTÃO: O ideal é que o Model/Avaliacao.cs tenha o campo Id. 
                        // Se não tiver, assumiremos que a mais recente (First) é a nossa se a lista vier ordenada, 
                        // mas para ser robusto como o frontend, adicione "public string Id { get; set; }" no Model/Avaliacao.cs.

                        // Assumindo que você adicionou o ID no Model/Avaliacao.cs ou usando lógica de busca:
                        // var item = historico.Find(x => x.Id == id); // Se ID existir no model

                        // Lógica alternativa se não houver ID no Model Avaliacao.cs atual:
                        // Pegar a última avaliação processada se confiarmos na ordem.
                        // Mas vamos seguir a lógica do frontend que busca por ID.

                        // *IMPORTANTE*: Para este código funcionar 100%, adicione [JsonPropertyName("id")] public string Id { get; set; } na classe Avaliacao.cs

                        // Usando dynamic ou JsonElement para contornar caso o Model não tenha ID ainda:
                        var jsonDoc = JsonDocument.Parse(content);
                        foreach (var element in jsonDoc.RootElement.EnumerateArray())
                        {
                            if (element.GetProperty("id").GetString() == id)
                            {
                                int resultadoValor = element.GetProperty("resultado").GetInt32();
                                if (resultadoValor != -1)
                                {
                                    // Sucesso! Converter para o objeto Resultado esperado pela tela
                                    // Recriamos o objeto Avaliacao para usar o método helper ToResultado
                                    var av = JsonSerializer.Deserialize<Avaliacao>(element.GetRawText());
                                    return av.ToResultado();
                                }
                                break; // Encontrou o ID, mas ainda é -1, sai do foreach e continua o while
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro no polling: {ex.Message}");
                    // Continua tentando se for erro de rede
                }
            }
        }

        throw new Exception("Tempo limite excedido. O processamento está demorando mais que o esperado.");
    }




}