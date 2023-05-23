using MetalArchivesCore.Attributes;
using MetalArchivesCore.Models.Enums;
using MetalArchivesCore.Models.Results.Abstract;

namespace MetalArchivesCore.Models.Results.SearchResults
{
    /// <summary>
    /// Representation of song result
    /// </summary>
    public class SimpleSongSearchResult : BandAlbumResultBase
    {
        [Column(3)]
        public string SongTitle { get; set; }

        [Column(1)]
        [RegexConverter(@"(?#AlbumName)<.*>(.+?)<.*>")]
        public string AlbumName { get; set; }

        [Column(1)]
        [RegexConverter("(?#AlbumUrl)<a href=\"(.*?)\".*>")]
        public override string AlbumUrl { get; set; }

        [Column(2)]
        [EnumConverter(typeof(AlbumType))]
        public AlbumType AlbumType { get; set; }

        // Must also handle the "This band participates on a split, but is not listed on the site." span (see below)
        [Column(0)]
        [RegexConverter(@"(?#BandName)<.*>(.+?)</.*>")]
        public string BandName { get; set; }

        // <a title=\"Dismal Reverie (US)\" href=\"https://www.metal-archives.com/bands/Dismal_Reverie/3540458856\">Dismal Reverie</a>
        // <a href=\"https://www.metal-archives.com/bands/Dismal_Reverie/3540458856\" title=\"Dismal Reverie (US)\">Dismal Reverie</a>
        // <a href=\"https://www.metal-archives.com/bands/Dismal_Reverie/3540458856\">Dismal Reverie</a>
        // <span title =\"This band participates on a split, but is not listed on the site.\">Tsubaki</span>

        [Column(0)]
        [RegexConverter(@"(?#BandUrl)(?:(?:<a title=.*href="")|(?:<a href="")|(?:<span title=""))([^""]*)")]
        public override string BandUrl { get; set; }
    }
}
