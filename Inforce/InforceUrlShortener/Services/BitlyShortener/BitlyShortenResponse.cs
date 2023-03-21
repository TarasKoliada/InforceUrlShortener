using System;
using Newtonsoft.Json;

namespace InforceUrlShortener.Services.BitlyShortener
{
    public class BitlyShortenResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("long_url")]
        public string LongUrl { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
    }
}
