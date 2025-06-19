using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CardioCheck.Model
{
    public class Avaliacao
    {
        [JsonPropertyName("data")]
        public DateTime Data { get; set; }

        [JsonPropertyName("resultado")]
        public int Resultado { get; set; }

        // Propriedade auxiliar para exibição na UI
        public string ResultadoTexto => Resultado == 1 ? "Risco de Doença Cardíaca" : "Normal";
        public Color ResultadoCor => Resultado == 1 ? Colors.Red : Colors.Green;
    }
}
