using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Apex.Api.IntegrationTests
{
    public class FrameworkTestEnvironment<TStartup>
    {
        protected static TestServer staticServer;
        protected TestServer transientServer;
        protected bool useSingletonServer;
        private readonly string lockStr = "lock";

        public FrameworkTestEnvironment(bool useSingletonServer)
        {
            this.useSingletonServer = useSingletonServer;

            if (useSingletonServer)
            {
                if(staticServer == null)
                {
                    lock(lockStr)
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
            return GetServer().CreateClient();
        }

        public HttpClient GetNewAuthenticationTokenClient(string token)
        {
            var client = GetNewClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public Task<HttpResponseMessage> GetWithNewTokenClientAsync(string token, string url)
        {
            var client = GetNewAuthenticationTokenClient(token);
            return client.GetAsync(url);
        }

        public Task<HttpResponseMessage> GetWithNewClientAsync(string url)
        {
            var client = GetNewClient();
            return client.GetAsync(url);
        }

        public Task<(HttpResponseMessage msg, TOutput result)> PostAndReadAsJsonWithNewClientAsync<TOutput, TInput>(string url, TInput data)
        {
            var client = GetNewClient();
            return client.PostAndReadAsJsonAsync<TOutput, TInput>(url, data);
        }

        public Task<(HttpResponseMessage msg, TOutput result)> PostAndReadAsJsonWithNewTokenClientAsync<TOutput, TInput>(string token, string url, TInput data)
        {
            var client = GetNewAuthenticationTokenClient(token);
            return client.PostAndReadAsJsonAsync<TOutput, TInput>(url, data);
        }

        public Task<(HttpResponseMessage msg, TOutput result)> PutAndReadAsJsonWithNewClientAsync<TOutput, TInput>(string url, TInput data)
        {
            var client = GetNewClient();
            return client.PutAndReadAsJsonAsync<TOutput, TInput>(url, data);
        }

        public Task<(HttpResponseMessage msg, TOutput result)> PutAndReadAsJsonWithNewTokenClientAsync<TOutput, TInput>(string token, string url, TInput data)
        {
            var client = GetNewAuthenticationTokenClient(token);
            return client.PutAndReadAsJsonAsync<TOutput, TInput>(url, data);
        }
    }
}
