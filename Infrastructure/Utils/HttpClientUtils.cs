/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Framework.Infrastructure.Utils;

namespace AspNetCore.Http.Extensions
{
    public static class HttpClientUtils
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonUtils.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PostAsync(url, content);
        }

        public static async Task<(HttpResponseMessage msg, T result)> PostAndReadAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var msg = await PostAsJsonAsync(httpClient, url, data);
            if (!msg.IsSuccessStatusCode)
                return (msg, default(T));

            var t = await ReadAsJsonAsync<T>(msg.Content);

            return (msg,t);
        }

        public static async Task<(HttpResponseMessage msg, TOutput result)> PostAndReadAsJsonAsync<TOutput,TInput>(
            this HttpClient httpClient, string url, TInput data)
        {
            var msg = await PostAsJsonAsync(httpClient, url, data);
            if (!msg.IsSuccessStatusCode)
                return (msg, default(TOutput));

            var t = await ReadAsJsonAsync<TOutput>(msg.Content);

            return (msg, t);
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonUtils.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PutAsync(url, content);
        }

        public static async Task<(HttpResponseMessage msg, T result)> PutAndReadAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var msg = await PutAsJsonAsync(httpClient, url, data);
            if (!msg.IsSuccessStatusCode)
                return (msg, default(T));

            var t = await ReadAsJsonAsync<T>(msg.Content);

            return (msg, t);
        }

        public static async Task<(HttpResponseMessage msg, TOutput result)> PutAndReadAsJsonAsync<TOutput, TInput>(
            this HttpClient httpClient, string url, TInput data)
        {
            var msg = await PutAsJsonAsync(httpClient, url, data);
            if (!msg.IsSuccessStatusCode)
                return (msg, default(TOutput));

            var t = await ReadAsJsonAsync<TOutput>(msg.Content);

            return (msg, t);
        }

        public static async Task<(HttpResponseMessage msg, T result)> GetAndReadAsJsonAsync<T>(
            this HttpClient httpClient, string url)
        {
            var msg = await httpClient.GetAsync(url);
            if (!msg.IsSuccessStatusCode)
                return (msg, default(T));

            var t = await ReadAsJsonAsync<T>(msg.Content);

            return (msg, t);
        }

        public static async Task<(HttpResponseMessage msg, string result)> GetAndReadAsStringAsync(
            this HttpClient httpClient, string url)
        {
            var msg = await httpClient.GetAsync(url);
            if (!msg.IsSuccessStatusCode)
                return (msg, null);

            var t = await msg.Content.ReadAsStringAsync();

            return (msg, t);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return JsonUtils.Deserialize<T>(dataAsString);
        }
    }
}