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
        public string ArtifactId { get; set; } = "";
        public List<Issue> Issues { get; set; } = new List<Issue>();
        public List<License> Licenses { get; set; } = new List<License>();
        public List<string> Dependencies { get; set; } = new List<string>();
        public object Dependency { get; set; }
    }

    public class AuditResults
    {
        [JsonProperty(PropertyName = "violations")]
        public List<SecurityIssue> Violations { get; set; } = new List<SecurityIssue>();

        [JsonProperty(PropertyName = "vulnerabilities")]
        public List<SecurityIssue> Vulnerabilities { get; set; } = new List<SecurityIssue>();

        [JsonProperty(PropertyName = "licenses")]
        public List<License> Licenses { get; set; } = new List<License>();

        // Results will contain only violation OR only vulnerabilites dependes on the given scan context.
        public List<SecurityIssue> AllSecurityIssues { get => Violations.Count > 0 ? Violations : Vulnerabilities; }

    }

    public class SecurityIssue
    {
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }
        [JsonProperty(PropertyName = "severity")]
        public Severity Severity { get; set; } = Severity.Normal;
        [JsonProperty(PropertyName = "components")]
        public Dictionary<string, AuditComponent> Components { get; set; } = new Dictionary<string, AuditComponent>();
        [JsonProperty(PropertyName = "type")]
        public string IssueType { get; set; }
    }

    public class AuditComponent
    {
        [JsonProperty(PropertyName = "fixed_versions")]
        public string[] FixedVersions { get; set; }
        [JsonProperty(PropertyName = "impact_paths")]
        public ImpactPath[][] ImpactPaths { get; set; }
    }

    public class ImpactPath
    {
        [JsonProperty(PropertyName = "component_id")]
        public string ComponentId { get; set; }

        [JsonProperty(PropertyName = "full_path")]
        public string FullPath { get; set; }
    }

}
