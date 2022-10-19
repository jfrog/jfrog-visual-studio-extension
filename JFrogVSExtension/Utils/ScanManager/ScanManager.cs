using JFrogVSExtension.Xray;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static JFrogVSExtension.OptionsMenu.JFrogXrayOptions;

namespace JFrogVSExtension.Utils.ScanManager
{
    public class ScanManager
    {
        #region Private fields
        private const string CliServerId = "jfrog-visual-studio-extension";
        private const string userAgent = "jfrog-visual-studio-extension";
        private readonly Dictionary<string, string> cliEnv = new Dictionary<string, string> { { "CI", "TRUE" }, { "JFROG_CLI_USER_AGENT", userAgent } };
        private string project;
        private string watches;
        private static ScanManager instance = null;
        private ScanManager() { }
        #endregion
        #region Public Properties
        public static ScanManager Instance
        {
            get => instance ?? (instance = new ScanManager());
        }
        public ScanPolicy Policy { get; private set; }
        #endregion

        public async Task InitializeAsync(string xrayUrl,string artifactoryUrl,string user,string password,string accessToken, ScanPolicy policy ,string project="", string watches="")
        {
            await ConfigCLIAsync(xrayUrl, artifactoryUrl, user, password, accessToken);
            this.project = project;
            this.watches = watches;
            Policy = policy;
        }

        private async Task ConfigCLIAsync(string xrayUrl, string artifactoryUrl, string user, string password, string accessToken)
        {
            var cliConfigCommand = $"config add \"{CliServerId}\" --xray-url=\"{xrayUrl}\" --artifactory-url=\"{artifactoryUrl}\" --user=\"{user}\" --password=\"{password}\" --access-token=\"{accessToken}\" --interactive=false --overwrite";
            _ = await Util.GetCLIOutputAsync(cliConfigCommand, "", true, cliEnv).ConfigureAwait(true);
        }

        public async Task<string> PreformScanAsync(List<string> workingDirs)
        {
            var workingDirsString = workingDirs.Count() > 1 ? string.Join(", ", workingDirs) : workingDirs.First();
            var cliAuditCommand = $"audit --format=\"json\" --server-id=\"{CliServerId}\" --licenses --fail=\"false\" --working-dirs=\"{workingDirsString}\"";

            switch (Policy)
            {
                case ScanPolicy.Project:
                    cliAuditCommand += $" --project=\"{project}\"";
                    break;
                case ScanPolicy.Watches:
                    cliAuditCommand += $" --watches=\"{watches}\"";
                    break;
            }
            return await  Util.GetCLIOutputAsync(cliAuditCommand, workingDirs.First(), false, cliEnv);
        }
    }
}
