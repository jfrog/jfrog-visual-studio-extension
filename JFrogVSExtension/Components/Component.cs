using JFrogVSExtension.Data.ViewModels;
using JFrogVSExtension.Utils;
using JFrogVSExtension.Xray;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JFrogVSExtension.Data
{
    public class Component : BaseViewModel
    {
        public string Key { get; set; } = "";
        public string Group { get; set; } = "";
        public string Package { get; set; } = "";
        public string Artifact { get; set; } = "";
        public string Name { get; set; } = "";
        public string Version { get; set; } = "";
        public string Type { get; set; } = "";
        public List<License> Licenses { get; set; } = new List<License>();
        public Severity TopSeverity { get; set; } = Severity.Normal;
        public List<string> Dependencies { get; set; }
        public List<Issue> Issues { get; set; } = new List<Issue>();

        public Component() { }
        public Component(string artifactId, string packageType)
        {
            string[] elements = artifactId.Split(':');
            if (elements.Length == 2)
            {
                Name = elements[0];
                Version = elements[1];
                Key = artifactId;
                Group = elements[0];
            }
            Type = packageType;
        }
        public override bool Equals(object obj)
        {
            Component comp = new Component();
            if (obj is Component)
            {
                comp = (Component)obj;
            }
            else
            {
                return false;
            }

            // Dependencies hash code
            int hashCodeDepSum = GetDepHashCode(this.Dependencies);
            int hashCodeDepSumObj = GetDepHashCode(comp.Dependencies);

            if (hashCodeDepSum != hashCodeDepSumObj)
            {
                return false;
            }

            // Issues hash code
            int hashCodeIssuesSum = GetIssuesHashCode(this.Issues);
            int hashCodeIssuesSumObj = GetIssuesHashCode(comp.Issues);

            if (hashCodeIssuesSum != hashCodeIssuesSumObj)
            {
                return false;
            }
            // Licenses hash code
            int hashCodeLicensesSum = GetLicensesHashCode(this.Licenses);
            int hashCodeLicensesSumObj = GetLicensesHashCode(comp.Licenses);

            if (hashCodeLicensesSum != hashCodeLicensesSumObj)
            {
                return false;
            }

            if (this.TopSeverity.GetHashCode() != comp.TopSeverity.GetHashCode())
            {
                return false;
            }

            if (!this.Key.Equals(comp.Key))
            {
                return false;
            }
            if (!this.Group.Equals(comp.Group))
            {
                return false;
            }
            if (!this.Package.Equals(comp.Package))
            {
                return false;
            }
            if (!this.Artifact.Equals(comp.Artifact))
            {
                return false;
            }
            if (!this.Name.Equals(comp.Name))
            {
                return false;
            }
            if (!this.Version.Equals(comp.Version))
            {
                return false;
            }
            if (!this.Type.Equals(comp.Type))
            {
                return false;
            }
            return true;

        }

        public override int GetHashCode()
        {
            int hashCodeSum = 0;
            hashCodeSum += GetDepHashCode(Dependencies);

            hashCodeSum += GetIssuesHashCode(Issues);
            hashCodeSum += GetLicensesHashCode(Licenses);

            hashCodeSum += TopSeverity.GetHashCode();

            return Key.GetHashCode() + Group.GetHashCode() + Package.GetHashCode() + Artifact.GetHashCode() + Name.GetHashCode() + Version.GetHashCode() + Type.GetHashCode() + hashCodeSum;

        }

        private int GetLicensesHashCode(List<License> Licenses)
        {
            if (Licenses == null || Licenses.Count == 0)
            {
                return 0;
            }
            int hashCodeSum = 0;
            foreach (License license in Licenses)
            {
                hashCodeSum += license.GetHashCode();
            }

            return hashCodeSum;
        }

        private int GetIssuesHashCode(List<Issue> Issues)
        {
            if (Issues == null || Issues.Count == 0)
            {
                return 0;
            }
            int hashCodeSum = 0;
            foreach (Issue issue in Issues)
            {
                hashCodeSum += issue.GetHashCode();
            }

            return hashCodeSum;
        }

        private int GetDepHashCode(List<string> dependencies)
        {
            if (dependencies == null || dependencies.Count == 0)
            {
                return 0;
            }
            int hashCodeSum = 0;
            foreach (string dep in dependencies)
            {
                hashCodeSum += dep.GetHashCode();
            }

            return hashCodeSum;
        }
    }
}
