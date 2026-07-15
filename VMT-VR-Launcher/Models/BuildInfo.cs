using System.Text.Json.Serialization;

namespace VMT_VR_Launcher.Models
{
    /// <summary>
    /// Model for BuildInfo.json found in StreamingAssets.
    /// Contains game name, version, and build date.
    /// </summary>
    public class BuildInfo
    {
        [JsonPropertyName("gameName")]
        public string GameName { get; set; } = "Unknown";

        [JsonPropertyName("version")]
        public string Version { get; set; } = "0.0.0";

        [JsonPropertyName("buildDate")]
        public string BuildDate { get; set; } = "N/A";
    }
}
