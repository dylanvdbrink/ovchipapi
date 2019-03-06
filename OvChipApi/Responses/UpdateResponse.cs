using Newtonsoft.Json;

namespace OvChipApi.Responses
{
    public class UpdateResponse
    {
        [JsonProperty("updateAvailable", Required = Required.Always)]
        public bool UpdateAvailable { get; set; }

        [JsonProperty("updateBlocking", Required = Required.Always)]
        public bool UpdateBlocking { get; set; }

        [JsonProperty("appstoreUrl", Required = Required.Always)]
        public string AppstoreUrl { get; set; }

        [JsonProperty("networkTimeout", Required = Required.Always)]
        public int NetworkTimeout { get; set; }

        [JsonProperty("zoomLevel", Required = Required.Always)]
        public int ZoomLevel { get; set; }
    }
}
