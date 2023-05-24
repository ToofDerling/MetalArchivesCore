using MetalArchivesCore.Models.Results.FullResults;
using WebsiteParserCore;

namespace MetalArchivesCore.Models.Results.Abstract
{
    public abstract class BandAlbumResultBase : BandResultBase
    {
        /// <summary>
        /// Url address to album
        /// </summary>
        public abstract string AlbumUrl { get; set; }

        /// <summary>
        /// Get album's page. Shortcut for GetFullAlbumAsync().Result
        /// </summary>
        /// <returns>Parsed album's page</returns>
        public AlbumResult GetFullAlbum()
        {
            return GetFullAlbumAsync().Result;
        }

        /// <summary>
        /// Get album's page async
        /// </summary>
        /// <returns>Parsed album's page</returns>
        public async Task<AlbumResult> GetFullAlbumAsync()
        {
            var downloader = new WebDownloader(AlbumUrl);
            var content = await downloader.DownloadDataAsync().ConfigureAwait(false);

            return WebContentParser.Parse<AlbumResult>(content);
        }
    }
}
