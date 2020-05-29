/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Framework.Infrastructure.Attributes;
using Framework.Infrastructure.Utils;

namespace AspNetCore.Http.Extensions
{
    public static class HttpClientUtils
    {
        public static async Task<(HttpResponseMessage msg, T result)> DeleteAndReadAsJsonAsync<T>(
            this HttpClient httpClient, string url, object headerObj = null)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Delete,
            };
            AddToHttpHeaders(request.Headers, headerObj);

            var msg = await httpClient.SendAsync(request);
            if (!msg.IsSuccessStatusCode)
                return (msg, default(T));

            var t = await ReadAsJsonAsync<T>(msg.Content);

            return (msg, t);
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data, object headerObj = null)
        {
            var dataAsString = JsonUtils.Serialize(data);
            var content = new StringContent(dataAsString);
            AddToHttpHeaders(content.Headers, headerObj);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PostAsync(url, content);
        }

        public static async Task<(HttpResponseMessage msg, T result)> PostAndReadAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data, object headerObj = null)
        {
            var msg = await PostAsJsonAsync(httpClient, url, data, headerObj);
            if (!msg.IsSuccessStatusCode)
                return (msg, default(T));

            var t = await ReadAsJsonAsync<T>(msg.Content);

            return (msg, t);
        }

        public static async Task<(HttpResponseMessage msg, TOutput result)> PostAndReadAsJsonAsync<TOutput, TInput>(
            this HttpClient httpClient, string url, TInput data, object headerObj = null)
        {
            var msg = await PostAsJsonAsync(httpClient, url, data, headerObj);
            if (!msg.IsSuccessStatusCode)
                return (msg, default(TOutput));

            var t = await ReadAsJsonAsync<TOutput>(msg.Content);

            return (msg, t);
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data, object headerObj = null)
        {
            var dataAsString = JsonUtils.Serialize(data);
            var content = new StringContent(dataAsString);
            AddToHttpHeaders(content.Headers, headerObj);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PutAsync(url, content);
        }

        public static async Task<(HttpResponseMessage msg, T result)> PutAndReadAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data, object headerObj = null)
        {
            var msg = await PutAsJsonAsync(httpClient, url, data, headerObj);
            if (!msg.IsSuccessStatusCode)
                return (msg, default(T));

            var t = await ReadAsJsonAsync<T>(msg.Content);

            return (msg, t);
        }

        public static async Task<(HttpResponseMessage msg, TOutput result)> PutAndReadAsJsonAsync<TOutput, TInput>(
            this HttpClient httpClient, string url, TInput data, object headerObj = null)
        {
            var msg = await PutAsJsonAsync(httpClient, url, data, headerObj);
            if (!msg.IsSuccessStatusCode)
                return (msg, default(TOutput));

            var t = await ReadAsJsonAsync<TOutput>(msg.Content);

            return (msg, t);
        }

        public static async Task<(HttpResponseMessage msg, T result)> GetAndReadAsJsonAsync<T>(
            this HttpClient httpClient, string url, object headerObj = null)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get,
            };
            AddToHttpHeaders(request.Headers, headerObj);

            var msg = await httpClient.SendAsync(request);
            if (!msg.IsSuccessStatusCode)
                return (msg, default(T));

            var t = await ReadAsJsonAsync<T>(msg.Content);

            return (msg, t);
        }

        public static async Task<(HttpResponseMessage msg, string result)> GetAndReadAsStringAsync(
            this HttpClient httpClient, string url, object headerObj = null)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get,
            };
            AddToHttpHeaders(request.Headers, headerObj);

            var msg = await httpClient.SendAsync(request);
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

        public static void AddToHttpHeaders(HttpHeaders headers, object obj)
        {
            if (headers == null || obj == null)
            {
                return;
            }

            foreach (var p in obj.GetType().GetProperties())
            {
                var alst = p.GetCustomAttributes(typeof(AlternateNameAttribute), true);
                var name = p.Name;
                if (alst != null && alst.Length > 0)
                {
                    name = (alst[0] as AlternateNameAttribute).Name;
                }

                headers.Add(name, p.GetValue(obj, null)?.ToString());
            }
        }

        public static string GetQueryString(object obj, string baseUrl = null)
        {
            if (obj == null)
            {
                return (baseUrl != null) ? baseUrl : "";
            }

            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + WebUtility.UrlEncode(p.GetValue(obj, null).ToString());

            var qry = string.Join("&", properties.ToArray());

            if (baseUrl == null)
                return qry;
            if (baseUrl.IndexOf("?") == -1)
                qry = "?" + qry;

            return baseUrl + qry;
        }
    }
}