using System.Web;

namespace MetalArchivesCore
{
    class WebDownloader
    {
        private static HttpClient HttpClient { get; set; }

        //private static void CreateHttpClient(CookieContainer cookies)
        //{
        //    var handler = new SocketsHttpHandler() { CookieContainer = cookies };
        //    var client = new HttpClient(handler);

        //    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36");
        //    client.Timeout = TimeSpan.FromMinutes(60);

        //    HttpClient = client;
        //}

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
            var url = PrepareUrl(_url, _parameters);

            var response = await HttpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                throw new Exception($"An error ocurred during performing the request. Response code: {response.StatusCode}");
        }

        private string PrepareUrl(string url, Dictionary<string, string> parameters)
        {
            string p = parameters?.Count > 0
                ? "?" + string.Join("&", parameters.Select(i => $"{i.Key}={HttpUtility.UrlEncode(i.Value)}"))
                : string.Empty;

            return url + p;
        }
    }
}