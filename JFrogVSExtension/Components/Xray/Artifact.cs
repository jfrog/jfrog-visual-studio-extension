using JFrogVSExtension.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

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

    public class AuditResults
    {
        [JsonProperty(PropertyName = "violations")]
        public List<SecurtiyIssue> Violations { get; set; } = new List<SecurtiyIssue>();
        [JsonProperty(PropertyName = "vulnerabilities")]

        public List<SecurtiyIssue> Vulnerabilities { get; set; } = new List<SecurtiyIssue>();
        [JsonProperty(PropertyName = "licenses")]

        public List<License> License { get; set; } = new List<License>();

        // Results will contain only violation OR only vulnerabilites dependes on the given scan context.
        public List<SecurtiyIssue> AllSecurityIssues { get => Violations.Count > 0 ? Violations : Vulnerabilities; }

    }

    public class SecurtiyIssue
    {
        [JsonProperty(PropertyName = "summary")]
        public string Summery { get; set; }
        [JsonProperty(PropertyName = "severity")]
        public Severity Severity { get; set; } = Severity.Normal;
        [JsonProperty(PropertyName = "components")]

        public Dictionary<string, AuditComponent> Components { get; set; } = new Dictionary<string, AuditComponent>();
    }

    public class AuditComponent
    {
        [JsonProperty(PropertyName = "fixed_versions")]
        public string[] FixedVersions { get; set; }
        [JsonProperty(PropertyName = "impact_paths")]
        public ImpcatPath[][] ImpcatPAths { get; set; }
    }

    public class ImpcatPath
    {
        [JsonProperty(PropertyName = "component_id")]
        public string ComponentId { get; set; }

        [JsonProperty(PropertyName = "full_path")]
        public string FullPath { get; set; }
    }

}
