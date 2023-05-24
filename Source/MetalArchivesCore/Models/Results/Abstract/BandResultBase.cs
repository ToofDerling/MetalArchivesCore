using MetalArchivesCore.Models.Results.FullResults;
using WebsiteParserCore;

namespace MetalArchivesCore.Models.Results.Abstract
{
    /// <summary>
    /// Result's base that contains reference to a band
    /// </summary>
    public abstract class BandResultBase
    {

        /// <summary>
        /// Band's url address
        /// </summary>
        public abstract string BandUrl { get; set; }

        /// <summary>
        /// Get band's page. shortcut for GetFullBandAsync().Result
        /// </summary>
        /// <returns>Parsed band's page</returns>
        public BandResult GetFullBand()
        {
            return GetFullBandAsync().Result;
        }

        /// <summary>
        /// Get band's page async
        /// </summary>
        /// <returns>Parsed band's page</returns>
        public async Task<BandResult> GetFullBandAsync()
        {
            var downloader = new WebDownloader(BandUrl);
            var content = await downloader.DownloadDataAsync().ConfigureAwait(false);

            return WebContentParser.Parse<BandResult>(content);
        }
    }
}
