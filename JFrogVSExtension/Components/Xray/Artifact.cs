using JFrogVSExtension.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFrogVSExtension.Xray
{
    public class Artifact
    {
        public GeneralInfo general { get; set; } = new GeneralInfo();
        public List<Issue> Issues { get; set; } = new List<Issue>();
        public List<License> licenses { get; set; } = new List<License>();
        public List<string> Dependencies { get; set; } = new List<string>();
        public object Dependency { get; set; }
    }
}
