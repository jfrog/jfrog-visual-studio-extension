using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JFrogVSExtension.HttpClient
{
    class XrayHttpClient
    {

        private String url = "";
        private String user = "";
        private String password = "";
        // Put the version here:
        private String userAgent = "jfrog-visual-studio-extension/1.0.1";
        private System.Net.Http.HttpClient httpClient = null;
        public async Task<HttpResponseMessage> PerformGetRequestAsync(String usage)
        {
            InitClient();
            try
            {
                HttpResponseMessage result = await httpClient.GetAsync(url + usage);
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

        private static async Task<string> ParseXrayResponseAsync(HttpResponseMessage result)
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

        public async Task<HttpResponseMessage> PerformPostRequestAsync(String usage, String content)
        {
            InitClient();
            try
            {
                return await httpClient.PostAsync(url + usage, new StringContent(content, Encoding.UTF8, "application/json"));
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
