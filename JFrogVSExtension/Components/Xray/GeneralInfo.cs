using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFrogVSExtension.Xray
{
    public class GeneralInfo
    {
        [JsonProperty(PropertyName = "component_id")]
        public String ComponentId { get; set; } = "";
        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; } = "";
        public String Path { get; set; } = "";
        [JsonProperty(PropertyName = "pkg_type")]
        public String PkgType { get; set; } = "";
        public String Sha256 { get; set; } = "";
    }
}
