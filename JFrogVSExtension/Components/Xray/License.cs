using Newtonsoft.Json;
using System.Collections.Generic;

namespace JFrogVSExtension.Xray
{
    public class License
    {
        [JsonProperty(PropertyName = "components")]
        public Dictionary<string, AuditComponent> Components { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}
