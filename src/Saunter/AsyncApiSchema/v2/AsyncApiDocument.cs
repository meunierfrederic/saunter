using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Saunter.AsyncApiSchema.v2
{
    public class AsyncApiDocument
    {
        [JsonProperty("asyncapi")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AsyncApiVersionString AsyncApi { get; } = AsyncApiVersionString.v2;
        
        [JsonProperty("id")]
        [JsonConverter(typeof(Identifier.JsonConverter))]
        public Identifier Id { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("servers")]
        public Servers Servers { get; } = new Servers();

        [JsonProperty("defaultContentType")]
        public string DefaultContentType { get; set; } = "application/json";

        [JsonProperty("channels")]
        public Channels Channels { get; set; } = new Channels();

        [JsonProperty("components")]
        public Components Components { get; } = new Components();

        [JsonProperty("tags")]
        public ISet<Tag> Tags { get; } = new HashSet<Tag>();

        [JsonProperty("externalDocs")]
        public ExternalDocumentation ExternalDocs { get; set; }
    }
}