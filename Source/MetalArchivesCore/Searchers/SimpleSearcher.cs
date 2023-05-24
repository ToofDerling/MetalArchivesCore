using MetalArchivesCore.Parsers;
using MetalArchivesCore.Searchers.Configurators.Abstract;

namespace MetalArchivesCore.Searchers
{
    /// <summary>
    /// Performs simple search of Items: Band, Album, Song, Lyrical themes, Music genre
    /// </summary>
    /// <typeparam name="T">Search result class. Result will be deserialised as this class</typeparam>
    public class SimpleSearcher<T> where T : class, new()
    {
        private readonly IConfigurator _configurator;

        // MA pages results in chunks of 200 
        private const int _pageSize = 200;

        internal SimpleSearcher(IConfigurator configurator)
        {
            _configurator = configurator;
        }

        /// <summary>
        /// Searches item by name. Shortcut for ByNameAsync(name).Result
        /// </summary>
        /// <param name="name">Item's name</param>
        /// <returns>List of items result - without pagination, all rows at once</returns>
        public IEnumerable<T> ByName(string name)
        {
            return ByNameAsync(name).Result;
        }

        /// <summary>
        /// Searches item by name async.
        /// </summary>
        /// <param name="name">Item's name</param>
        /// <returns>List of items result - without pagination, all rows at once</returns>
        public async Task<IEnumerable<T>> ByNameAsync(string name)
        {
            var items = new List<T>();

            _configurator.Parameters["query"] = name;
            var downloader = new WebDownloader(_configurator.Url, _configurator.Parameters);

            var parser = new ResponseParser<T>();
            var page = 0;

            while (true)
            {
                if (page > 0)
                {
                    var displayStart = (page * _pageSize).ToString();
                    _configurator.Parameters["iDisplayStart"] = displayStart;
                }
                page++;

                var responseData = await downloader.DownloadDataAsync().ConfigureAwait(false);

                var searchResponse = parser.Parse(responseData);
                items.AddRange(searchResponse.Items);

                if (searchResponse.iTotalRecords == 0 || searchResponse.Items.Count == 0 || items.Count >= searchResponse.iTotalRecords)
                {
                    break;
                }
            }

            return items;
        }
    }
}
