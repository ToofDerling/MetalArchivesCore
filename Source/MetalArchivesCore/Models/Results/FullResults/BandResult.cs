using HtmlAgilityPack;
using MetalArchivesCore.CustomWebsiteConverters;
using MetalArchivesCore.Models.Enums;
using MetalArchivesCore.Models.Results.BandResults;
using MetalArchivesCore.Models.Results.PartResults;
using WebsiteParserCore;
using WebsiteParserCore.Attributes;
using WebsiteParserCore.Attributes.Enums;
using WebsiteParserCore.Attributes.StartAttributes;

namespace MetalArchivesCore.Models.Results.FullResults
{
    /// <summary>
    /// Representation of band's page
    /// </summary>
    public class BandResult
    {
        /// <summary>
        /// Id of the band
        /// </summary>
        [Selector(".band_name a", Attribute = "href")]
        [Regex(@"/(\d+)$")]
        [Converter(typeof(ULongConverter))]
        public ulong Id { get; set; }

        /// <summary>
        /// Band's name
        /// </summary>
        [Selector(@".band_name")]
        public string Name { get; set; }

        /// <summary>
        /// Country of origin
        /// </summary>
        [Selector(@"#band_stats dl:first-child dd:nth-child(2)")]
        [Converter(typeof(EnumDescriptionConverter<Country>))]
        public Country CountryOfOrigin { get; set; }

        /// <summary>
        /// Bands location. Usually city and country
        /// </summary>
        [Selector(@"#band_stats dl:first-child dd:nth-child(4)", EmptyValues = new string[] { "N/A" })]
        public string Location { get; set; }

        /// <summary>
        /// Band's status
        /// </summary>
        [Selector(@"#band_stats dl:first-child dd:nth-child(6)")]
        [Converter(typeof(EnumDescriptionConverter<BandStatus>))]
        public BandStatus Status { get; set; }

        /// <summary>
        /// Yer when the band was formed
        /// </summary>
        [Selector(@"#band_stats dl:first-child dd:nth-child(8)", EmptyValues = new string[] { "N/A" })]
        [Converter(typeof(UShortConverter))]
        public ushort? FormedInYear { get; set; }

        /// <summary>
        /// Genres
        /// </summary>
        [Selector("#band_stats dl:nth-child(2) dd:nth-child(2)", EmptyValues = new string[] { "N/A" })]
        public string GenresString { get; set; }

        /// <summary>
        /// Genres splited from <see cref="GenresString"/>. May not be correctly parsed.
        /// </summary>
        [Selector("#band_stats dl:nth-child(2) dd:nth-child(2)", EmptyValues = new string[] { "N/A" })]
        [Converter(typeof(SplitConverter))]
        public IEnumerable<string> Genres { get; set; }

        /// <summary>
        /// Lyrical themes
        /// </summary>
        [Selector("#band_stats dl:nth-child(2) dd:nth-child(4)", EmptyValues = new string[] { "N/A" })]
        public string LyricalThemesString { get; set; }

        /// <summary>
        /// Lyrical themes splited from <see cref="GenresString"/>. May not be correctly parsed.
        /// </summary>
        [Selector("#band_stats dl:nth-child(2) dd:nth-child(4)", EmptyValues = new string[] { "N/A" })]
        [Converter(typeof(SplitConverter))]
        public IEnumerable<string> LyricalThemes { get; set; }

        /// <summary>
        /// Last label
        /// </summary>
        [Selector("#band_stats dl:nth-child(2) dd:nth-child(6)", EmptyValues = new string[] { "N/A" })]
        public string LastLabel { get; set; }

        /// <summary>
        /// Notes of band
        /// </summary>
        [Selector(".band_comment", SkipIfNotFound = true)]
        [Remove("Read more", RemoverValueType.Text)]
        public string NotesShort { get; set; }

        /// <summary>
        /// Url of full notes if exists otherwise null
        /// </summary>
        [Selector(".btn_read_more", Attribute = "onclick", SkipIfNotFound = true)]
        [Regex(@"readMore\('(.*?)'\);")]
        [Format(@"https://metal-archives.com/{0}")]
        public string NotesFullUrl { get; set; }

        /// <summary>
        /// Informations about page's add and update
        /// </summary>
        [WebsiteParserModel(Selector = "#auditTrail")]
        public Metadata Metadata { get; set; }

        /// <summary>
        /// If <see cref="NotesFullUrl"/> is not null, it returns full band's notes. Shortcut for GetFullNotesAsync().Result
        /// </summary>
        /// <returns>Band's notes</returns>
        public string GetFullNotes()
        {
            return GetFullNotesAsync().Result;
        }

        /// <summary>
        /// If <see cref="NotesFullUrl"/> is not null, it returns full band's notes async
        /// </summary>
        /// <returns>Band's notes</returns>
        public async Task<string> GetFullNotesAsync()
        {
            if (string.IsNullOrEmpty(NotesFullUrl))
            {
                return string.Empty;
            }

            var downloader = new WebDownloader(NotesFullUrl);
            var content = await downloader.DownloadDataAsync().ConfigureAwait(false);

            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            return doc.DocumentNode.InnerText;
        }

        /// <summary>
        /// Gets list of band's albums simple list. Shortcut for GetAlbumsAsync(type).Result
        /// </summary>
        public IEnumerable<AlbumBandResult> GetAlbums(AlbumListType type)
        {
            return GetAlbumsAsync(type).Result;
        }

        /// <summary>
        /// Gets list of band's albums simple list async
        /// </summary>
        public async Task<IEnumerable<AlbumBandResult>> GetAlbumsAsync(AlbumListType type)
        {
            var downloader = new WebDownloader($"{$@"https://www.metal-archives.com/band/discography/id/{Id}/tab/"}{type.ToString().ToLower()}");
            var content = await downloader.DownloadDataAsync().ConfigureAwait(false);

            return WebContentParser.ParseList<AlbumBandResult>(content);
        }

        //TODO: images
        //TODO: years active
    }
}
