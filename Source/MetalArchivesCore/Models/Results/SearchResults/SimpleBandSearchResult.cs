using MetalArchivesCore.Attributes;
using MetalArchivesCore.Models.Results.Abstract;

namespace MetalArchivesCore.Models.Results.SearchResults
{
    /// <summary>
    /// Representation of band result
    /// </summary>
    public class SimpleBandSearchResult : BandResultBase
    {
        [Column(0)]
        [RegexConverter(@"<a.*?>(.+?)</a>")]
        public string BandName { get; set; }

        [Column(0)]
        [RegexConverter("<a href=\"(.*?)\".*>")]
        public override string BandUrl { get; set; }

        [Column(1)]
        public string BandGenre { get; set; }

        [Column(2)]
        [EnumConverter(typeof(Country))]
        public Country BandCountry { get; set; }

    }
}
