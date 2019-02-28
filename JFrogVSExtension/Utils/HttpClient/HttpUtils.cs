using JFrogVSExtension.Logger;
using JFrogVSExtension.Utils;
using JFrogVSExtension.Xray;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JFrogVSExtension.HttpClient
{
    class HttpUtils
    {
        private static XrayHttpClient xray = new XrayHttpClient();

        public static async Task<XrayStatus> GetPingAsync()
        {
            try
            {
                HttpResponseMessage responseFromXray = await xray.PerformGetRequestAsync("system/ping");
                string resultFromXray = await parseXrayResponseAsync(responseFromXray);
                return XrayUtil.LoadXrayStatus(resultFromXray);
            } catch (Exception e)
            {
                throw new IOException(e.Message, e);
            }
        }

        public static async Task<XrayVersion> GetVersionAsync()
        {
            try
            {
                HttpResponseMessage responseFromXray = await xray.PerformGetRequestAsync("system/version");
                string resultFromXray = await parseXrayResponseAsync(responseFromXray);
                return XrayUtil.LoadXrayVersion(resultFromXray);
            }
            catch (Exception e)
            {
                throw new IOException(e.Message, e);
            }
        }

        public static async Task<Artifacts> GetCopmonentsFromXrayAsync(List<Components> collection)
        {
            HttpResponseMessage componentResponse = await getResponseFromXrayAsync(collection);
            string componentResult = await parseXrayResponseAsync(componentResponse);
            await OutputLog.ShowMessageAsync(componentResult);
            Artifacts artifacts = JsonConvert.DeserializeObject<Artifacts>(componentResult);
            return artifacts;
        }

        private static async Task<HttpResponseMessage> getResponseFromXrayAsync(List<Components> collection)
        {
            var collectionWrapper = new
            {
                component_details = collection
            };

            string json = JsonConvert.SerializeObject(collectionWrapper);
            return await xray.PerformPostRequestAsync("summary/component ", json);            
        }

        private static async Task<string> parseXrayResponseAsync(HttpResponseMessage result)
        {
            if (result.StatusCode == HttpStatusCode.OK)
            {
                using (StreamReader sr = new StreamReader(await result.Content.ReadAsStreamAsync()))
                {
                    return await sr.ReadToEndAsync();
                }
            }
            String message = "Received response status code: " + (int)result.StatusCode + ". Message: " + result.ReasonPhrase;
            throw new HttpRequestException(message);
        }

        public static async Task<String> PostComponentToXrayAsync(Components component)
        {
            var collection = new List<Components>();
            HttpResponseMessage xrayComponentResponse = await getResponseFromXrayAsync(collection);
            if (xrayComponentResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                return "Received " + HttpStatusCode.Unauthorized + " from Xray. Plesae check your credentials.";
            }

            if (xrayComponentResponse.StatusCode == HttpStatusCode.Forbidden)
            {
                return "Received " + HttpStatusCode.Forbidden + " from Xray. Please make sure that the user has 'View Components' permission in Xray.";
            }
            return "";
        }

        public static void InitClient(String url, String username, String password)
        {
            xray.InitClient(url, username, password);
        }
    }
}
