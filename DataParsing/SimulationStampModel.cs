using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tzup
{
    public class SimulationStampModel
    {
        [JsonProperty("time_mjd2000")]
        public float Time_mjd2000 { get; private set; }

        [JsonProperty("epoch")]
        public string Epoch { get; private set; }

        [JsonProperty("protected_pos")]
        public List<float> Protected_pos { get; private set; }

        [JsonProperty("debris_pos")]
        public List<List<float>> Debris_pos { get; private set; }

        [JsonProperty("alert")]
        public CollisionAlertModel CollisionAlert { get; private set; }

        public SimulationStampModel(float time_mjd2000,
                                    string epoch,
                                    List<float> protected_pos,
                                    List<List<float>> debris_pos,
                                    CollisionAlertModel collisionAlert)
        {
            Time_mjd2000 = time_mjd2000;
            Epoch = epoch;
            Protected_pos = protected_pos;
            Debris_pos = debris_pos;
            CollisionAlert = collisionAlert;
        }
    }
}