using Newtonsoft.Json;

namespace OvChipApi.Responses.OAuth
{
    internal class TokenErrorResponse
    {
        [JsonProperty("error", Required = Required.Always)]
        public string Error { get; set; }

        [JsonProperty("error_description", Required = Required.Always)]
        public string ErrorDescription { get; set; }
    }
}
