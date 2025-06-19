using System;
using System.Text.Json.Serialization;

namespace CardioCheck.Model
{
    public class Avaliacao
    {

        [JsonPropertyName("data")]
        public DateTime Data { get; set; }

        [JsonPropertyName("resultado")]
        public int Resultado { get; set; }

        [JsonPropertyName("questionario")]
        public QuestionarioInfo Questionario { get; set; }

        // Propriedade auxiliar para exibição na UI
        public string ResultadoTexto => Resultado == 1 ? "ALTO RISCO" : "BAIXO RISCO";
        public Color ResultadoCor => Resultado == 1 ? Color.FromArgb("#DC3545") : Color.FromArgb("#28A745");
        public string DataFormatada => Data.ToLocalTime().ToString("dd/MM/yyyy HH:mm");
    }

    public class QuestionarioInfo
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }
    }
}