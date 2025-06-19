using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CardioCheck.Model
{
    public class Questionario
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("sex")]
        public int Sex { get; set; }

        [JsonPropertyName("chestPainType")]
        public int ChestPainType { get; set; }

        [JsonPropertyName("restingBloodPressure")]
        public float RestingBloodPressure { get; set; }

        [JsonPropertyName("serumCholesterol")]
        public float SerumCholesterol { get; set; }

        [JsonPropertyName("fastingBloodSugar")]
        public int FastingBloodSugar { get; set; }

        [JsonPropertyName("restingECG")]
        public int RestingECG { get; set; }

        [JsonPropertyName("maxHeartRate")]
        public float MaxHeartRate { get; set; }

        [JsonPropertyName("exerciseAngina")]
        public int ExerciseAngina { get; set; }

        [JsonPropertyName("oldpeak")]
        public float Oldpeak { get; set; }

        [JsonPropertyName("stSlope")]
        public int StSlope { get; set; }
    }
}
