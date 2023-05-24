using MetalArchivesCore.Models.Results.SearchResults;
using MetalArchivesCore.Searchers;
using MetalArchivesCore.Searchers.Configurators;

namespace MetalArchivesCore
{
    public static class MetalArchives
    {
        /// <summary>
        /// Search band
        /// </summary>
        public static SimpleSearcher<SimpleBandSearchResult> Band => new(new SimpleBandConfigurators());

        /// <summary>
        /// Search album
        /// </summary>
        public static SimpleSearcher<SimpleAlbumSearchResult> Album => new(new SimpleAlbumConfigurators());

        /// <summary>
        /// Search song
        /// </summary>
        public static SimpleSearcher<SimpleSongSearchResult> Song => new(new SimpleSongConfigurators());

        /// <summary>
        /// Close the Metal Archives downloader. 
        /// </summary>
        public static void CloseWebDownloader() => WebDownloader.DisposeHttpClient();
    }
}
