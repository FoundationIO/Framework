using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using Flurl.Http;
using Framework.Infrastructure.Models.Result;

namespace Framework.Infrastructure.Utils
{
    public static class RestClientUtils
    {
        public static IFlurlClient FClient(string url, HttpClient httpClient = null)
        {
            if (httpClient == null)
            {
                if (string.IsNullOrEmpty(url))
                    return new FlurlClient();

                return new FlurlClient(url);
            }

            if (!string.IsNullOrEmpty(url))
                httpClient.BaseAddress = new Uri(url);

            return new FlurlClient(httpClient);
        }

        public static string Url(string site, string urlFragment = null)
        {
            if (site == null)
                return site;

            if (urlFragment == null || urlFragment == "")
                return site;

            if (!site.EndsWith("/"))
                site = site + "/";

            if (urlFragment.StartsWith("/"))
                urlFragment = urlFragment.Substring(1);

            return site + urlFragment;
        }

        public static ReturnModel<T> Get<T>(string url, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            try
            {
                using (var client = GetClient(url, queryModel, headers, token, httpClient))
                {
                    return client.Request().GetAsync().ReceiveJson<ReturnModel<T>>().Result;
                }
            }
            catch (FlurlHttpTimeoutException tEx)
            {
                //log error
                return ReturnModel<T>.Error($"Request Timeout when connecting to {url} ", tEx);
            }
            catch (FlurlHttpException hEx)
            {
                //log error

                var r = hEx.Call.Response.GetJsonAsync<ReturnModel<T>>().Result;
                if (r != null)
                {
                    return r;
                }

                return ReturnModel<T>.Error($"Http Exception when connecting to {url}", hEx, $"status = {hEx.Call.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                //log error
                return ReturnModel<T>.Error($"Unexpected error when connecting to {url} ", ex);
            }
        }

        public static ReturnListModel<T> GetList<T>(string url, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            try
            {
                using (var client = GetClient(url, queryModel, headers, token, httpClient))
                {
                    return client.Request().GetAsync().ReceiveJson<ReturnListModel<T>>().Result;
                }
            }
            catch (FlurlHttpTimeoutException tEx)
            {
                //log error
                return ReturnListModel<T>.Error($"Request Timeout when connecting to {url} ", tEx);
            }
            catch (FlurlHttpException hEx)
            {
                //log error

                var r = hEx.Call.Response.GetJsonAsync<ReturnListModel<T>>().Result;
                if (r != null)
                {
                    return r;
                }

                return ReturnListModel<T>.Error($"Http Exception when connecting to {url}", hEx, $"status = {hEx.Call.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                //log error
                return ReturnListModel<T>.Error($"Unexpected error when connecting to {url} ", ex);
            }
        }

        public static ReturnModel<T> Put<T>(string url, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            try
            {
                using (var client = GetClient(url, queryModel, headers, token, httpClient))
                {
                    return client.Request().PutJsonAsync("").ReceiveJson<ReturnModel<T>>().Result;
                }
            }
            catch (FlurlHttpTimeoutException tEx)
            {
                //log error
                return ReturnModel<T>.Error($"Request Timeout when connecting to {url} ", tEx);
            }
            catch (FlurlHttpException hEx)
            {
                //log error

                var r = hEx.Call.Response.GetJsonAsync<ReturnModel<T>>().Result;
                if (r != null)
                {
                    return r;
                }

                return ReturnModel<T>.Error($"Http Exception when connecting to {url}", hEx, $"status = {hEx.Call.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                //log error
                return ReturnModel<T>.Error($"Unexpected error when connecting to {url} ", ex);
            }
        }

        public static ReturnListModel<T> PutList<T>(string url, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            try
            {
                using (var client = GetClient(url, queryModel, headers, token, httpClient))
                {
                    return client.Request().PutJsonAsync("").ReceiveJson<ReturnListModel<T>>().Result;
                }
            }
            catch (FlurlHttpTimeoutException tEx)
            {
                //log error
                return ReturnListModel<T>.Error($"Request Timeout when connecting to {url} ", tEx);
            }
            catch (FlurlHttpException hEx)
            {
                //log error

                var r = hEx.Call.Response.GetJsonAsync<ReturnListModel<T>>().Result;
                if (r != null)
                {
                    return r;
                }

                return ReturnListModel<T>.Error($"Http Exception when connecting to {url}", hEx, $"status = {hEx.Call.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                //log error
                return ReturnListModel<T>.Error($"Unexpected error when connecting to {url} ", ex);
            }
        }

        public static ReturnModel<T> PutWithModel<T, TModel>(string url, TModel model, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            try
            {
                using (var client = GetClient(url, queryModel, headers, token, httpClient))
                {
                    return client.Request().PutJsonAsync(model).ReceiveJson<ReturnModel<T>>().Result;
                }
            }
            catch (FlurlHttpTimeoutException tEx)
            {
                //log error
                return ReturnModel<T>.Error($"Request Timeout when connecting to {url} ", tEx);
            }
            catch (FlurlHttpException hEx)
            {
                //log error

                var r = hEx.Call.Response.GetJsonAsync<ReturnModel<T>>().Result;
                if (r != null)
                {
                    return r;
                }

                return ReturnModel<T>.Error($"Http Exception when connecting to {url}", hEx, $"status = {hEx.Call.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                //log error
                return ReturnModel<T>.Error($"Unexpected error when connecting to {url} ", ex);
            }
        }

        public static ReturnListModel<T> PutListWithModel<T, TModel>(string url, TModel model, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            try
            {
                using (var client = GetClient(url, queryModel, headers, token, httpClient))
                {
                    return client.Request().PutJsonAsync(model).ReceiveJson<ReturnListModel<T>>().Result;
                }
            }
            catch (FlurlHttpTimeoutException tEx)
            {
                //log error
                return ReturnListModel<T>.Error($"Request Timeout when connecting to {url} ", tEx);
            }
            catch (FlurlHttpException hEx)
            {
                //log error

                var r = hEx.Call.Response.GetJsonAsync<ReturnListModel<T>>().Result;
                if (r != null)
                {
                    return r;
                }

                return ReturnListModel<T>.Error($"Http Exception when connecting to {url}", hEx, $"status = {hEx.Call.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                //log error
                return ReturnListModel<T>.Error($"Unexpected error when connecting to {url} ", ex);
            }
        }

        public static ReturnModel<T> Post<T>(string url, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            try
            {
                using (var client = GetClient(url, queryModel, headers, token, httpClient))
                {
                    return client.Request().PostJsonAsync("").ReceiveJson<ReturnModel<T>>().Result;
                }
            }
            catch (FlurlHttpTimeoutException tEx)
            {
                //log error
                return ReturnModel<T>.Error($"Request Timeout when connecting to {url} ", tEx);
            }
            catch (FlurlHttpException hEx)
            {
                //log error

                var r = hEx.Call.Response.GetJsonAsync<ReturnModel<T>>().Result;
                if (r != null)
                {
                    return r;
                }

                return ReturnModel<T>.Error($"Http Exception when connecting to {url}", hEx, $"status = {hEx.Call.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                //log error
                return ReturnModel<T>.Error($"Unexpected error when connecting to {url} ", ex);
            }
        }

        public static ReturnListModel<T> PostList<T>(string url, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            try
            {
                using (var client = GetClient(url, queryModel, headers, token, httpClient))
                {
                    return client.Request().PostJsonAsync("").ReceiveJson<ReturnListModel<T>>().Result;
                }
            }
            catch (FlurlHttpTimeoutException tEx)
            {
                //log error
                return ReturnListModel<T>.Error($"Request Timeout when connecting to {url} ", tEx);
            }
            catch (FlurlHttpException hEx)
            {
                //log error

                var r = hEx.Call.Response.GetJsonAsync<ReturnListModel<T>>().Result;
                if (r != null)
                {
                    return r;
                }

                return ReturnListModel<T>.Error($"Http Exception when connecting to {url}", hEx, $"status = {hEx.Call.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                //log error
                return ReturnListModel<T>.Error($"Unexpected error when connecting to {url} ", ex);
            }
        }

        public static ReturnModel<T> PostWithModel<T, TModel>(string url, TModel model, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            try
            {
                using (var client = GetClient(url, queryModel, headers, token, httpClient))
                {
                    return client.Request().PostJsonAsync(model).ReceiveJson<ReturnModel<T>>().Result;
                }
            }
            catch (FlurlHttpTimeoutException tEx)
            {
                //log error
                return ReturnModel<T>.Error($"Request Timeout when connecting to {url} ", tEx);
            }
            catch (FlurlHttpException hEx)
            {
                //log error

                var r = hEx.Call.Response.GetJsonAsync<ReturnModel<T>>().Result;
                if (r != null)
                {
                    return r;
                }

                return ReturnModel<T>.Error($"Http Exception when connecting to {url}", hEx, $"status = {hEx.Call.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                //log error
                return ReturnModel<T>.Error($"Unexpected error when connecting to {url} ", ex);
            }
        }

        public static ReturnListModel<T> PostListWithModel<T, TModel>(string url, TModel model, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            try
            {
                using (var client = GetClient(url, queryModel, headers, token, httpClient))
                {
                    return client.Request().PostJsonAsync(model).ReceiveJson<ReturnListModel<T>>().Result;
                }
            }
            catch (FlurlHttpTimeoutException tEx)
            {
                //log error
                return ReturnListModel<T>.Error($"Request Timeout when connecting to {url} ", tEx);
            }
            catch (FlurlHttpException hEx)
            {
                //log error

                var r = hEx.Call.Response.GetJsonAsync<ReturnListModel<T>>().Result;
                if (r != null)
                {
                    return r;
                }

                return ReturnListModel<T>.Error($"Http Exception when connecting to {url}", hEx, $"status = {hEx.Call.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                //log error
                return ReturnListModel<T>.Error($"Unexpected error when connecting to {url} ", ex);
            }
        }

        public static ReturnModel<T> Delete<T>(string url, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            try
            {
                using (var client = GetClient(url, queryModel, headers, token, httpClient))
                {
                    return client.Request().DeleteAsync().ReceiveJson<ReturnModel<T>>().Result;
                }
            }
            catch (FlurlHttpTimeoutException tEx)
            {
                //log error
                return ReturnModel<T>.Error($"Request Timeout when connecting to {url} ", tEx);
            }
            catch (FlurlHttpException hEx)
            {
                //log error

                var r = hEx.Call.Response.GetJsonAsync<ReturnModel<T>>().Result;
                if (r != null)
                {
                    return r;
                }

                return ReturnModel<T>.Error($"Http Exception when connecting to {url}", hEx, $"status = {hEx.Call.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                //log error
                return ReturnModel<T>.Error($"Unexpected error when connecting to {url} ", ex);
            }
        }

        public static ReturnListModel<T> DeleteList<T>(string url, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            try
            {
                using (var client = GetClient(url, queryModel, headers, token, httpClient))
                {
                    return client.Request().DeleteAsync().ReceiveJson<ReturnListModel<T>>().Result;
                }
            }
            catch (FlurlHttpTimeoutException tEx)
            {
                //log error
                return ReturnListModel<T>.Error($"Request Timeout when connecting to {url} ", tEx);
            }
            catch (FlurlHttpException hEx)
            {
                //log error

                var r = hEx.Call.Response.GetJsonAsync<ReturnListModel<T>>().Result;
                if (r != null)
                {
                    return r;
                }

                return ReturnListModel<T>.Error($"Http Exception when connecting to {url}", hEx, $"status = {hEx.Call.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                //log error
                return ReturnListModel<T>.Error($"Unexpected error when connecting to {url} ", ex);
            }
        }

        private static IFlurlClient GetClient(string url, object queryModel = null, List<KeyValuePair<string, string>> headers = null, string token = null, HttpClient httpClient = null)
        {
            if (url == null)
                throw new Exception("Url is not valid");

            var fClient = FClient(url, httpClient);

            var result = fClient.Request();

            //var resultUrl = new Url(url);
            if (queryModel != null)
            {
                foreach (var p in queryModel.GetType().GetProperties())
                {
                    result.SetQueryParam(p.Name, p.GetValue(queryModel));
                }
            }

            result = result.WithOAuthBearerToken(token);

            if (headers != null && headers.Count > 0)
            {
                var eo = new ExpandoObject();
                var eoColl = (ICollection<KeyValuePair<string, object>>)eo;

                foreach (var kvp in headers)
                {
                    eoColl.Add(new KeyValuePair<string, object>(kvp.Key, kvp.Value));
                }

                dynamic eoDynamic = eo;

                result.WithHeaders((object)eoDynamic);
            }

            return fClient;
        }
    }
}
