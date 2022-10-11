using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JFrogVSExtension.HttpClient
{
    class JfrogHttpClient
    {
        private string xrayUrl = "";
        private string artifactoryUrl = "";
        private string user = "";
        private string password = "";
        private string userAgent = "jfrog-visual-studio-extension";
        private System.Net.Http.HttpClient httpClient = null;
        private bool UseAccessToken { get => string.IsNullOrEmpty(user); }

        public async Task<HttpResponseMessage> PerformXrayGetRequestAsync(string api)
        {
            return await PerformGetRequestAsync(xrayUrl + api);
        }

        public async Task<HttpResponseMessage> PerformArtifactoryGetRequestAsync(string api)
        {
            return await PerformGetRequestAsync(artifactoryUrl + api);
        }

        private async Task<HttpResponseMessage> PerformGetRequestAsync(string url)
        {
            InitClient();
            try
            {
                HttpResponseMessage result = await httpClient.GetAsync(url);
                return result;
            }
            catch (Exception ex)
            {
                Exception innerException = ex.InnerException;
                string message = ex.Message;
                while (innerException != null && innerException.InnerException != null)
                {
                    message = innerException.Message;
                    innerException = innerException.InnerException;
                }
                throw new HttpRequestException(message, innerException);
            }
        }

        public async Task<HttpResponseMessage> PerformXrayPostRequestAsync(string api, string content)
        {
            return await PerformPostRequestAsync(xrayUrl + api,content);
        }

        public async Task<HttpResponseMessage> PerformArtifactoryPostRequestAsync(string api, string content)
        {
            return await PerformPostRequestAsync(artifactoryUrl + api, content);
        }

        private async Task<HttpResponseMessage> PerformPostRequestAsync(string url, string content)
        {
            InitClient();
            try
            {
                return await httpClient.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));
            }
            catch (Exception ex)
            {
                Exception innerException = ex.InnerException;
                string message = ex.Message;
                while (innerException != null && innerException.InnerException != null)
                {
                    message = innerException.Message;
                    innerException = innerException.InnerException;
                }
                throw new HttpRequestException(message, innerException);
            }
        }

        private System.Net.Http.HttpClient AddHeader(string name, string value, System.Net.Http.HttpClient client)
        {
            client.DefaultRequestHeaders.Add(name, value);
            return client;
        }

        private void InitClient()
        {
            if (httpClient == null)
            {
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                if (!UseAccessToken)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", user, password))));
                }
                else
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", password);
                }
                httpClient = AddHeader("User-Agent", userAgent, client);
            }
        }

       // Triggered each time when the options are saved. 
       // Since this being triggered from the options menu, the httpClient need to be init every time.
        public void InitClient(string xrayUrl, string artifactoryUrl, string username, string token)
        {
            httpClient = null;
            user = username;
            password = token;
            this.xrayUrl = xrayUrl;
            this.artifactoryUrl = artifactoryUrl;
            InitClient();
        }

        public void InitClient(string xrayUrl, string artifactoryUrl, string accessToken)
        {
            InitClient(xrayUrl, artifactoryUrl, "", accessToken);
        }
    }
}
