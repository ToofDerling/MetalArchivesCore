﻿using MetalArchivesCore.Models.Responses;
using System.Net;
using System.Net.Http.Json;

namespace MetalArchivesCore
{
    class WebDownloader
    {
        private static HttpClient HttpClient { get; set; }

        public static void DisposeHttpClient()
        {
            try
            {
                HttpClient?.Dispose();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Creates instance of WebDownloader.
        /// </summary>
        /// <param name="url">As string format</param>
        /// <param name="parameters">name-value GET parameters</param>
        public WebDownloader(string url, Dictionary<string, string> parameters = null)
        {
            _url = url;
            _parameters = parameters ?? new Dictionary<string, string>();

            HttpClient ??= new HttpClient();
        }

        readonly string _url;
        readonly Dictionary<string, string> _parameters;

        /// <summary>
        /// Shortcut for DownloadDataAsync().Result
        /// </summary>
        /// <returns></returns>
        public string DownloadData()
        {
            return DownloadDataAsync().Result;
        }

        public async Task<string> DownloadDataAsync()
        {
            var url = $"{_url}{GetParameters()}";

            var responseStr = await HttpClient.GetStringAsync(url).ConfigureAwait(false);
            return responseStr;
        }

        public async Task<SearchResponse<T>> DownloadJsonAsync<T>()
        {
            var url = $"{_url}{GetParameters()}";

            var searchResponse = await HttpClient.GetFromJsonAsync<SearchResponse<T>>(url).ConfigureAwait(false);
            return searchResponse;
        }

        private string GetParameters()
        {
            return _parameters.Count > 0
                ? $"?{string.Join("&", _parameters.Select(p => $"{p.Key}={WebUtility.UrlEncode(p.Value)}"))}"
                : string.Empty;
        }
    }
}