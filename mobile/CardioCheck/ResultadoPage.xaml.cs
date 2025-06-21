using CardioCheck.Model;

namespace CardioCheck;

public partial class ResultadoPage : ContentPage
{
    public ResultadoPage(Questionario questionario, Resultado resultado)
    {
        InitializeComponent();
        PopulateData(questionario, resultado);
    }

    private void PopulateData(Questionario questionario, Resultado resultado)
    {
        // --- Popula o card de resultado em destaque ---
        var corResultado = resultado.Predicao == 1 ? Colors.Red : Colors.Green;
        var textoResultado = resultado.Predicao == 1 ? "ALTO RISCO" : "BAIXO RISCO";

        ResultadoBorder.Stroke = new SolidColorBrush(corResultado);
        ResultadoPredicaoLabel.Text = textoResultado;
        ResultadoPredicaoLabel.TextColor = corResultado;
        ResultadoRecomendacaoLabel.Text = resultado.Recomendacao;

        // --- Popula o resumo dos dados do paciente ---
        NomeLabel.Text = questionario.Nome;
        IdadeLabel.Text = questionario.Age.ToString();
        SexoLabel.Text = questionario.Sex == 1 ? "Masculino" : "Feminino";

        string[] tiposDorPeito = { "1: Angina Típica", "2: Angina Atípica", "3: Dor Não-anginosa", "4: Assintomático" };
        DorPeitoLabel.Text = tiposDorPeito[questionario.ChestPainType - 1]; // O valor é 1-based

        PressaoLabel.Text = $"{questionario.RestingBloodPressure} mm Hg";
        ColesterolLabel.Text = $"{questionario.SerumCholesterol} mg/dl";
        GlicemiaLabel.Text = questionario.FastingBloodSugar == 1 ? "Sim" : "Não";

        string[] resultadosECG = { "0: Normal", "1: Anormalidade da onda ST-T", "2: Hipertrofia ventricular esquerda" };
        EcgLabel.Text = resultadosECG[questionario.RestingECG];

        FreqCardiacaLabel.Text = questionario.MaxHeartRate.ToString();
        AnginaExercicioLabel.Text = questionario.ExerciseAngina == 1 ? "Sim" : "Não";
        OldpeakLabel.Text = questionario.Oldpeak.ToString("F1"); // Formata para uma casa decimal

        string[] inclinacoesST = { "0: Normal", "1: Ascendente", "2: Descendente" };
        InclinacaoStLabel.Text = inclinacoesST[questionario.StSlope];
    }

    private async void OnFinalizarClicked(object sender, EventArgs e)
    {
        // 1. Envia a mensagem para quem estiver ouvindo (neste caso, a QuestionarioPage)
        MessagingCenter.Send<object>(this, "LimparFormulario");

        // 2. Navega de volta para a tela raiz do aplicativo (a tela de abas)
        await Navigation.PopToRootAsync();
    }
}