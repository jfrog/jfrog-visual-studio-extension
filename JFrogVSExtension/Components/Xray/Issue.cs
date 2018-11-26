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
        public String Summary { get; set; }
        public String Component { get; set; } = ""; // This is the name from general

        public Issue() { }
        public Issue(Severity severity, string summery, string issueType, string component)
        {
            Severity = severity;
            Summary = summery;
            IssueType = issueType;
            Component = component;
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
