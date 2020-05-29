using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Framework.TestUtilies
{
    public class FrameworkTestEnvironment<TStartup>
    {
        protected static TestServer staticServer;
        protected TestServer transientServer;
        protected bool useSingletonServer;
        private static readonly object lockStr = "lock";
        protected bool useExternalHttpServer = false;

        public FrameworkTestEnvironment(bool useSingletonServer, bool useExternalHttpServer = false)
        {
            this.useSingletonServer = useSingletonServer;
            this.useExternalHttpServer = useExternalHttpServer;
            if (useSingletonServer)
            {
                if (staticServer == null)
                {
                    lock (lockStr)
                    {
                        if (staticServer == null)
                        {
                            IWebHostBuilder testHost = (new WebHostBuilder()).UseEnvironment("Testing").UseStartup(typeof(TStartup));
                            staticServer = new TestServer(testHost);
                        }
                    }
                }
            }
            else
            {
                IWebHostBuilder testHost = (new WebHostBuilder()).UseEnvironment("Testing").UseStartup(typeof(TStartup));
                transientServer = new TestServer(testHost);
            }
        }

        public TestServer GetServer()
        {
            return useSingletonServer ? staticServer : transientServer;

        }

        public HttpClient GetNewClient()
        {
            HttpClient client;
            if (useExternalHttpServer)
            {
                client = new HttpClient();
            }
            else
            {
                client = GetServer().CreateClient();
            }

            return client;
        }

        public HttpClient GetNewAuthenticationTokenClient(string token)
        {
            var client = GetNewClient();
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public async Task<HttpResponseMessage> GetWithNewTokenClientAsync(string token, string url, object headerObj = null)
        {
            using (var client = GetNewAuthenticationTokenClient(token))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get
                };
                HttpClientUtils.AddToHttpHeaders(request.Headers, headerObj);

                return await client.SendAsync(request);
            }
        }

        public async Task<HttpResponseMessage> GetWithNewClientAsync(string url, object headerObj = null)
        {
            using (var client = GetNewClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get
                };
                HttpClientUtils.AddToHttpHeaders(request.Headers, headerObj);

                return await client.SendAsync(request);
            }
        }

        public async Task<(HttpResponseMessage, TOutput result)> GetAndReadAsJsonWithNewClientAsync<TOutput>(string url, object headerObj = null)
        {
            using (var client = GetNewClient())
                return await client.GetAndReadAsJsonAsync<TOutput>(url, headerObj);
        }

        public async Task<(HttpResponseMessage, TOutput result)> GetAndReadAsJsonWithNewClientAsync<TOutput, TInput>(string url, TInput data, object headerObj = null)
        {
            using (var client = GetNewClient())
                return await client.GetAndReadAsJsonAsync<TOutput>(HttpClientUtils.GetQueryString(data, url), headerObj);
        }

        public async Task<(HttpResponseMessage, TOutput result)> GetAndReadAsJsonWithNewTokenClientAsync<TOutput>(string token, string url, object headerObj = null)
        {
            using (var client = GetNewAuthenticationTokenClient(token))
                return await client.GetAndReadAsJsonAsync<TOutput>(url, headerObj);
        }

        public async Task<(HttpResponseMessage, TOutput result)> GetAndReadAsJsonWithNewTokenClientAsync<TOutput, TInput>(string token, string url, TInput data, object headerObj = null)
        {
            using (var client = GetNewAuthenticationTokenClient(token))
                return await client.GetAndReadAsJsonAsync<TOutput>(HttpClientUtils.GetQueryString(data, url), headerObj);
        }
        public async Task<(HttpResponseMessage msg, TOutput result)> PostAndReadAsJsonWithNewClientAsync<TOutput>(string url, object headerObj = null)
        {
            using (var client = GetNewClient())
                return await client.PostAndReadAsJsonAsync<TOutput, string>(url, "", headerObj);
        }

        public async Task<(HttpResponseMessage msg, TOutput result)> PostAndReadAsJsonWithNewClientAsync<TOutput, TInput>(string url, TInput data, object headerObj = null)
        {
            using (var client = GetNewClient())
                return await client.PostAndReadAsJsonAsync<TOutput, TInput>(url, data, headerObj);
        }

        public async Task<(HttpResponseMessage msg, TOutput result)> PostAndReadAsJsonWithNewTokenClientAsync<TOutput, TInput>(string token, string url, TInput data, object headerObj = null)
        {
            using (var client = GetNewAuthenticationTokenClient(token))
                return await client.PostAndReadAsJsonAsync<TOutput, TInput>(url, data, headerObj);
        }

        public async Task<(HttpResponseMessage msg, TOutput result)> PostAndReadAsJsonWithNewTokenClientAsync<TOutput>(string token, string url, object headerObj = null)
        {
            using (var client = GetNewAuthenticationTokenClient(token))
                return await client.PostAndReadAsJsonAsync<TOutput, string>(url, "", headerObj);
        }

        public async Task<(HttpResponseMessage msg, TOutput result)> PutAndReadAsJsonWithNewClientAsync<TOutput, TInput>(string url, TInput data, object headerObj = null)
        {
            using (var client = GetNewClient())
                return await client.PutAndReadAsJsonAsync<TOutput, TInput>(url, data, headerObj);
        }

        public async Task<(HttpResponseMessage msg, TOutput result)> PutAndReadAsJsonWithNewTokenClientAsync<TOutput, TInput>(string token, string url, TInput data, object headerObj = null)
        {
            using (var client = GetNewAuthenticationTokenClient(token))
                return await client.PutAndReadAsJsonAsync<TOutput, TInput>(url, data, headerObj);
        }

        public async Task<(HttpResponseMessage msg, TOutput result)> DeleteAndReadAsJsonWithNewClientAsync<TOutput>(string url, object headerObj = null)
        {
            using (var client = GetNewClient())
                return await client.DeleteAndReadAsJsonAsync<TOutput>(url, headerObj);
        }

        public async Task<(HttpResponseMessage msg, TOutput result)> DeleteAndReadAsJsonWithNewTokenClientAsync<TOutput>(string token, string url, object obj = null, object headerObj = null)
        {
            using (var client = GetNewAuthenticationTokenClient(token))
                return await client.DeleteAndReadAsJsonAsync<TOutput>(HttpClientUtils.GetQueryString(obj, url), headerObj);
        }
    }
}
