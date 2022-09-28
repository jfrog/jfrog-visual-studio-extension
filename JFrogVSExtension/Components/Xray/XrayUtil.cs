using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace JFrogVSExtension.Xray
{
    class XrayUtil
    {
        public static readonly string MIN_XRAY_VERSION = "3.29.0";

        public static ServerStatus LoadServerStatus(string output)
        {
            return JsonConvert.DeserializeObject<ServerStatus>(output);
        }

        public static XrayVersion LoadXrayVersion(string output)
        {
            return JsonConvert.DeserializeObject<XrayVersion>(output);
        }

        public static ArtifactoryVersion LoadArtifactoryVersion(string output)
        {
            return JsonConvert.DeserializeObject<ArtifactoryVersion>(output);
        }

        public static bool IsXrayVersionCompatible(string xrayVersion)
        {
            string[] versionTokens = xrayVersion.Split('.');
            string[] minimumVersionToken = MIN_XRAY_VERSION.Split('.');

            for (int i= 0; i < minimumVersionToken.Length; i++)
            {
                string minVersionToken = minimumVersionToken[i].Trim();
                string versionToken = versionTokens.Length < i + 1 ? "0" : versionTokens[i].Trim();
                int result = compareTokens(minVersionToken, versionToken);
                if (result != 0)
                {
                    return result > 0;
                }
            }
            return true;
        }

        public static string GetMinimumXrayVersionErrorMessage(string xrayVersion)
        {
            return "ERROR: Found Xray version: " + xrayVersion + ". This extension version supports Xray " + XrayUtil.MIN_XRAY_VERSION + " or above. For information about using older versions of Xray, please refer to the documentation.";
        }

        private static int compareTokens(string minVersionToken, string versionToken) 
        {
            int versionTokenFirstNumeric = int.Parse(Regex.Match(versionToken, @"\d+").Value);
            int minVersionTokenFirstNumeric = int.Parse(Regex.Match(minVersionToken, @"\d+").Value);
            return versionTokenFirstNumeric.CompareTo(minVersionTokenFirstNumeric);
        }

    }
}
