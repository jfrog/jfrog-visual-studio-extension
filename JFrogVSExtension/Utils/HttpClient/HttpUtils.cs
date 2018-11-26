using JFrogVSExtension.Logger;
using JFrogVSExtension.Utils;
using JFrogVSExtension.Xray;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace JFrogVSExtension.HttpClient
{
    class HttpUtils
    {
        private static XrayHttpClient xray = new XrayHttpClient();

        public static XrayStatus GetPing()
        {
            try
            {
                HttpResponseMessage responseFromXray = xray.PerformGetRequest("system/ping");
                string resultFromXray = parseXrayResponse(responseFromXray);
                return XrayUtil.LoadXrayStatus(resultFromXray);
            } catch (Exception e)
            {
                throw new IOException(e.Message, e);
            }
        }

        public static XrayVersion GetVersion()
        {
            try
            {
                HttpResponseMessage responseFromXray = xray.PerformGetRequest("system/version");
                string resultFromXray = parseXrayResponse(responseFromXray);
                return XrayUtil.LoadXrayVersion(resultFromXray);
            }
            catch (Exception e)
            {
                throw new IOException(e.Message, e);
            }
        }

        public static Artifacts GetCopmonentsFromXray(List<Components> collection)
        {

            HttpResponseMessage componentResponse = getResponseFromXray(collection);
            string componentResult = parseXrayResponse(componentResponse);
            OutputLog.ShowMessage(componentResult);
            Artifacts artifacts = JsonConvert.DeserializeObject<Artifacts>(componentResult);
            return artifacts;
        }

        private static HttpResponseMessage getResponseFromXray(List<Components> collection)
        {
            var collectionWrapper = new
            {
                component_details = collection
            };

            string json = JsonConvert.SerializeObject(collectionWrapper);
            return xray.PerformPostRequest("summary/component ", json);            
        }

        private static string parseXrayResponse(HttpResponseMessage result)
        {
            if (result.StatusCode == HttpStatusCode.OK)
            {
                using (StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
                {
                    return sr.ReadToEnd();
                }
            }
            String message = "Received response status code: " + (int)result.StatusCode + ". Message: " + result.ReasonPhrase;
            throw new HttpRequestException(message);
        }

        public static String PostComponentToXray(Components component)
        {
            var collection = new List<Components>();
            HttpResponseMessage xrayComponentResponse = getResponseFromXray(collection);
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
