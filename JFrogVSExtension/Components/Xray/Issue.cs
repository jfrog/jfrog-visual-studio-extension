using JFrogVSExtension.Data.ViewModels;
using Newtonsoft.Json;
using System;
using Microsoft.VisualStudio.Imaging.Interop;

namespace JFrogVSExtension.Xray
{
    public class Issue : BaseViewModel
    {
        [JsonProperty(PropertyName = "created")]
        public String Created { get; set; }
        [JsonProperty(PropertyName = "description")]
        public String Description { get; set; }
        [JsonProperty(PropertyName = "issue_type")]
        public String IssueType { get; set; } = "N/A";
        [JsonProperty(PropertyName = "provider")]
        public String Provider { get; set; }
        [JsonProperty(PropertyName = "severity")]
        public Severity Severity { get; set; } = Severity.Normal;
        [JsonProperty(PropertyName = "summary")]
        public String Summary { get; set; } = "";
        public string FixedVersions { get; set; } = "";
        public String Component { get; set; } = ""; // This is the name from general

        public Issue() { }
        public Issue(Severity severity, string summary, string issueType, string component, string fixedVersions)
        {
            Severity = severity;
            Summary = summary ?? "";
            IssueType = issueType ?? "";
            Component = component ?? "";
            FixedVersions = fixedVersions ?? "";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var other = obj as Issue;
            if (other == null) return false;
            return this.Severity == other.Severity && this.Summary == other.Summary && this.IssueType == other.IssueType && this.FixedVersions == other.FixedVersions; 
        }

        public override int GetHashCode()
        {
            return Severity.GetHashCode() + Summary.GetHashCode() + IssueType.GetHashCode() + Component.GetHashCode() + FixedVersions.GetHashCode();
        }

        public ImageMoniker SeveretyMoniker
        {
            get
            {
                return JFrogMonikerSelector.SeverityToMoniker(Severity);
            }
        }
    }
}
