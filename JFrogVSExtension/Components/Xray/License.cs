using Newtonsoft.Json;
using System.Collections.Generic;

namespace JFrogVSExtension.Xray
{
    public class License
    {
        [JsonProperty(PropertyName = "components")]
        public Dictionary<string, AuditComponent> Components { get; set; }

        [JsonProperty(PropertyName = "license_key")]
        public string Name { get; set; }

        public License()
        {
            Name = "Unknown";
            Components = new Dictionary<string, AuditComponent>();
        }

        public License(string name)
        {
            Name = string.IsNullOrEmpty(name) ? "Unknown": name;
        }
    }
}
