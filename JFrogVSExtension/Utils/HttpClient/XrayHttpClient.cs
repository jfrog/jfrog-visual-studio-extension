using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace JFrogVSExtension.HttpClient
{
    class XrayHttpClient
    {

        private String url = "";
        private String user = "";
        private String password = "";
        private String userAgent = "jfrog-VS-plugin/1.0.0"; //PUT HERE THE VERSION...
        private System.Net.Http.HttpClient httpClient = null;
        public HttpResponseMessage PerformGetRequest(String usage)
        {
            InitClient();
            try
            {
                HttpResponseMessage result = httpClient.GetAsync(url + usage).Result;
                return result;
            }
            catch (Exception ex)
            {
                Exception innerException = ex.InnerException;
                String message = ex.Message;
                while (innerException != null && innerException.InnerException != null)
                {
                    message = innerException.Message;
                    innerException = innerException.InnerException;
                }
                throw new HttpRequestException(message, innerException);
            }
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

        public HttpResponseMessage PerformPostRequest(String usage, String content)
        {
            InitClient();
            try
            {
                HttpResponseMessage result = httpClient.PostAsync(url + usage, new StringContent(content, Encoding.UTF8,
                    "application/json")).Result;
                return result;
            }
            catch (Exception ex)
            {
                Exception innerException = ex.InnerException;
                String message = ex.Message;
                while (innerException != null && innerException.InnerException != null)
                {
                    message = innerException.Message;
                    innerException = innerException.InnerException;
                }
                throw new HttpRequestException(message, innerException);
            }
        }

        private System.Net.Http.HttpClient AddHeader(String name, String value, System.Net.Http.HttpClient client)
        {
            client.DefaultRequestHeaders.Add(name, value);
            return client;
        }

        private void InitClient()
        {
            if (httpClient == null)
            {
                
                //Create a query
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(
                                                                    string.Format("{0}:{1}", user, password))));
                httpClient = AddHeader("User-Agent", userAgent, client);
            }
        }

       // Triggered each time when the options are saved. 
       // Since this being triggered from the options menu, the httpClient need to be init every time.
        public void InitClient(String url, String username, String password)
        {
            httpClient = null;
            user = username;
            this.password = password;
            this.url = url + "api/v1/";
            InitClient();
        }
    }
}
