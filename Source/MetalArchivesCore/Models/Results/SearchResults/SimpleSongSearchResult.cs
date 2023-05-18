using MetalArchivesCore.Attributes;
using MetalArchivesCore.Models.Enums;
using MetalArchivesCore.Models.Results.Abstract;
using MetalArchivesCore.Models.Results.FullResults;
using MetalArchivesCore.Parsers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebsiteParserCore;

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
        [RegexConverter(@"<a.*?>(.+?)</a>")]
        public string AlbumName { get; set; }

        [Column(1)]
        [RegexConverter("<a href=\"(.*?)\".*>")]
        public override string AlbumUrl { get; set; }

        [Column(2)]
        [EnumConverter(typeof(AlbumType))]
        public AlbumType AlbumType { get; set; }

        // These two must also handle <span title="This band participates on a split, but is not listed on the site."

        [Column(0)]
        [RegexConverter(@"<.*>(.+?)</.*>")] 
        public string BandName { get; set; }

        [Column(0)]
        [RegexConverter("<.*? .*=\"(.*?)\".*>")]
        public override string BandUrl { get; set; }
    }
}
