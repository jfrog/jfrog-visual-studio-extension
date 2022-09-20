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
        private static JfrogHttpClient jfrog = new JfrogHttpClient();

        public static async Task<ServerStatus> GetXrayPingAsync()
        {
            try
            {
                HttpResponseMessage responseFromXray = await jfrog.PerformXrayGetRequestAsync("api/v1/system/ping");
                string resultFromXray = await ParseResponseAsync(responseFromXray);
                return XrayUtil.LoadServerStatus(resultFromXray);
            }
            catch (Exception e)
            {
                throw new IOException(e.Message, e);
            }
        }
        public static async Task GetArtifactoryPingAsync()
        {
            try
            {
                HttpResponseMessage responseFromArtifactory = await jfrog.PerformArtifactoryGetRequestAsync("api/system/ping");
                string resultFromXray = await ParseResponseAsync(responseFromArtifactory);
            }
            catch (Exception e)
            {
                throw new IOException(e.Message, e);
            }
        }
        public static async Task<XrayVersion> GetXrayVersionAsync()
        {
            try
            {
                HttpResponseMessage responseFromXray = await jfrog.PerformXrayGetRequestAsync("api/v1/system/version");
                string resultFromXray = await ParseResponseAsync(responseFromXray);
                return XrayUtil.LoadXrayVersion(resultFromXray);
            }
            catch (Exception e)
            {
                throw new IOException(e.Message, e);
            }
        }

        public static async Task<ArtifactoryVersion> GetArtifactoryVersionAsync()
        {
            try
            {
                HttpResponseMessage responseFromArtifactory = await jfrog.PerformArtifactoryGetRequestAsync("api/system/version");
                string resultFromXray = await ParseResponseAsync(responseFromArtifactory);
                return XrayUtil.LoadArtifactoryVersion(resultFromXray);
            }
            catch (Exception e)
            {
                throw new IOException(e.Message, e);
            }
        }

        public static async Task<Artifacts> GetCopmonentsFromXrayAsync(List<Components> collection)
        {
            HttpResponseMessage componentResponse = await getResponseFromXrayAsync(collection);
            string componentResult = await ParseResponseAsync(componentResponse);
            await OutputLog.ShowMessageAsync(componentResult);
            try
            {
                Artifacts artifacts = JsonConvert.DeserializeObject<Artifacts>(componentResult);
                return artifacts;
            }
            catch (Exception e)
            {
                await OutputLog.ShowMessageAsync("Failed deserializing component result in Xray response.");
                throw new IOException(e.Message, e);
            }
        }

        private static async Task<HttpResponseMessage> getResponseFromXrayAsync(List<Components> collection)
        {
            var collectionWrapper = new
            {
                component_details = collection
            };

            string json = JsonConvert.SerializeObject(collectionWrapper);
            return await jfrog.PerformXrayPostRequestAsync("summary/component ", json);            
        }

        private static async Task<string> ParseResponseAsync(HttpResponseMessage result)
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

        public static async Task<HttpResponseMessage> TestConnectionAndPermissionsAsync()
        {
            return await getResponseFromXrayAsync(new List<Components>());
        }

        public static void InitClient(string xrayUrl,string artifactoryUrl, string username, string password, string accessToken="")
        {
            if (!string.IsNullOrEmpty(accessToken)) 
            {
                jfrog.InitClient(xrayUrl, artifactoryUrl,accessToken);

            }
            else
            {
                jfrog.InitClient(xrayUrl, artifactoryUrl, username, password);
            }
        }
    }
}
