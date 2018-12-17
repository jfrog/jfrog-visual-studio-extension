using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace JFrogVSExtension.Xray
{
    class XrayUtil
    {
        public static readonly string MIN_XRAY_VERSION = "2.5.0";

        public static XrayStatus LoadXrayStatus(String output)
        {
            {
                XrayStatus status = JsonConvert.DeserializeObject<XrayStatus>(output);
                return status;
            }
        }

        public static XrayVersion LoadXrayVersion(String output)
        {
            {
                XrayVersion version = JsonConvert.DeserializeObject<XrayVersion>(output);
                return version;
            }
        }

        public static bool IsXrayVersionCompatible(String xrayVersion)
        {
            String[] versionTokens = xrayVersion.Split('.');
            String[] minimumVersionToken = MIN_XRAY_VERSION.Split('.');

            for (int i= 0; i < minimumVersionToken.Length; i++)
            {
                String minVersionToken = minimumVersionToken[i].Trim();
                String versionToken = versionTokens.Length < i + 1 ? "0" : versionTokens[i].Trim();
                int result = compareTokens(minVersionToken, versionToken);
                if (result != 0)
                {
                    return result > 0;
                }
            }
            return true;
        }

        public static String GetErrorMessage(String xrayVersion)
        {
            return "ERROR: Found Xray version: " + xrayVersion + ". This extension version supports Xray " + XrayUtil.MIN_XRAY_VERSION + " or above. For information about using older versions of Xray, please refer to the documentation.";
        }

        private static int compareTokens(String minVersionToken, String versionToken) 
        {
            int versionTokenFirstNumeric = Int32.Parse(Regex.Match(versionToken, @"\d+").Value);
            int minVersionTokenFirstNumeric = Int32.Parse(Regex.Match(minVersionToken, @"\d+").Value);
            return versionTokenFirstNumeric.CompareTo(minVersionTokenFirstNumeric);
        }

    }
}
