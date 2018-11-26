using Newtonsoft.Json;
using System.Collections.Generic;

namespace JFrogVSExtension.Xray
{
    public class License
    {
        [JsonProperty(PropertyName = "components")]
        public List<string> Components { get; set; }

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "modern_info_url")]
        public List<string> MoreInfoUrl { get; set; }
    }
}
