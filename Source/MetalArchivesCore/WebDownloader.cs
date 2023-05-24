using System.Net;

namespace MetalArchivesCore
{
    class WebDownloader
    {
        private static HttpClient HttpClient { get; set; }

        public static void DisposeHttpClient()
        {
            if (HttpClient != null)
            {
                try
                {
                    HttpClient.Dispose();
                }
                catch
                {
                }
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

        private string GetParameters()
        {
            return _parameters.Count > 0
                ? $"?{string.Join("&", _parameters.Select(p => $"{p.Key}={WebUtility.UrlEncode(p.Value)}"))}"
                : string.Empty;
        }
    }
}