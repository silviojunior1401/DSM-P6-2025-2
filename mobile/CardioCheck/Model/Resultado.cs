using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CardioCheck.Model
{
    public class Resultado
    {
        [JsonPropertyName("resultado")]
        public int Predicao { get; set; }

        [JsonPropertyName("recomendacao")]
        public string Recomendacao { get; set; }
    }
}
