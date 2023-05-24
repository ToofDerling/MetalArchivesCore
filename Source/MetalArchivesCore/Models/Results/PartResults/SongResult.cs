using HtmlAgilityPack;
using MetalArchivesCore.CustomWebsiteConverters;
using WebsiteParserCore.Attributes;
using WebsiteParserCore.Attributes.Enums;
using WebsiteParserCore.Attributes.StartAttributes;

namespace MetalArchivesCore.Models.Results.PartResults
{
    /// <summary>
    /// Metal archives' song model
    /// </summary>
    [ListSelector("", ChildSelector = ".even, .odd")]
    public class SongResult
    {
        /// <summary>
        /// Id of lyrics in metal archives databse
        /// </summary>
        [Selector("td:nth-child(4) a", Attribute = "onclick", SkipIfNotFound = true)]
        [Regex(@"toggleLyrics\(\'(\d+)\'\)")]
        [Converter(typeof(ULongConverter))]
        public ulong Id { get; set; }

        /// <summary>
        /// Song's index on album
        /// </summary>
        [Selector("td:first-child")]
        [Regex(@"(\d+).")]
        [Converter(typeof(ByteConverter))]
        public byte Index { get; set; }

        /// <summary>
        /// Song's title
        /// </summary>
        [Selector(".wrapWords")]
        public string Title { get; set; }

        /// <summary>
        /// Duration of the song
        /// </summary>
        [Selector("td:nth-child(3)")]
        [Converter(typeof(TimeSpanConverter))]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// If song is instrumental
        /// </summary>
        [Selector("td:nth-child(4)")]
        [Remove("&nbsp;", RemoverValueType.Text)]
        [CompareValue("instrumental", true)]
        public bool IsInstrumental { get; set; }

        /// <summary>
        /// If song has added lyrics
        /// </summary>
        [Selector("td:nth-child(4)")]
        [Remove("&nbsp;", RemoverValueType.Text)]
        [CompareValue("Show lyrics", true)]
        public bool HasLyrics { get; set; }

        /// <summary>
        /// Gets actual song's lyrics. Shortcut for GetLyricsAsync().Result
        /// </summary>
        /// <returns>Lyrics or string.Empty if not exists</returns>
        public string GetLyrics()
        {
            return GetLyricsAsync().Result;
        }

        /// <summary>
        /// Gets actual song's lyrics async
        /// </summary>
        /// <returns>Lyrics or string.Empty if not exists</returns>
        public async Task<string> GetLyricsAsync()
        {
            var lyrics = string.Empty;

            if (HasLyrics)
            {
                var downloader = new WebDownloader($@"https://www.metal-archives.com/release/ajax-view-lyrics/id/{Id}");
                var content = await downloader.DownloadDataAsync().ConfigureAwait(false);

                var document = new HtmlDocument();
                document.LoadHtml(content);

                lyrics = document.DocumentNode.InnerText.Trim();
            }

            return lyrics;
        }
    }
}
