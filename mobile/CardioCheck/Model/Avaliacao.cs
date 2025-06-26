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
        public Questionario Questionario { get; set; }

        // Propriedade auxiliar para exibição na UI
        public string ResultadoTexto => Resultado == 1 ? "ALTO RISCO" : "BAIXO RISCO";
        public Color ResultadoCor => Resultado == 1 ? Color.FromArgb("#DC3545") : Color.FromArgb("#28A745");
        public string DataFormatada => Data.ToLocalTime().ToString("dd/MM/yyyy HH:mm");

        public Resultado ToResultado()
        {
            return new Resultado
            {
                Predicao = Resultado,
                Recomendacao = Resultado == 1 ? "Paciente apresenta alto risco de doença cardíaca. "
                 + "Recomenda-se avaliação cardiológica completa, incluindo exames complementares como ecocardiograma e teste ergométrico. "
                 + "Considerar ajustes no estilo de vida e medicação preventiva." : "Paciente apresenta baixo risco de doença cardíaca. "
                 + "Considerar ajustes na dieta para controle do colesterol. Manter hábitos saudáveis e atividade física regular."
            };
        }
    }
}