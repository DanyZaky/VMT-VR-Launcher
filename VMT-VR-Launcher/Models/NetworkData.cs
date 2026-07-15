using System.Text.Json.Serialization;

namespace VMT_VR_Launcher.Models
{
    /// <summary>
    /// Model for NetworkData.json found in StreamingAssets.
    /// Contains server connection configuration.
    /// </summary>
    public class NetworkData
    {
        [JsonPropertyName("databaseURL")]
        public string DatabaseURL { get; set; } = string.Empty;

        [JsonPropertyName("server")]
        public string Server { get; set; } = "127.0.0.1";

        [JsonPropertyName("port")]
        public string Port { get; set; } = "7777";

        [JsonPropertyName("isDebug")]
        public bool IsDebug { get; set; } = false;

        [JsonPropertyName("isAPI")]
        public bool IsAPI { get; set; } = false;
    }
}
