using Newtonsoft.Json;

namespace OvChipApi.Responses
{
    public class Response<T>
    {
        [JsonProperty("c", Required = Required.Always)]
        public int StatusCode { get; set; }

        [JsonProperty("o", Required = Required.Always)]
        public T Data { get; set; }

        [JsonProperty("e", Required = Required.AllowNull)]
        public string Unknown { get; set; }
    }
}
