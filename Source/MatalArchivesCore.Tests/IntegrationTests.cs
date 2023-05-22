using MetalArchivesCore.Models.Enums;
using MetalArchivesCore.Models.Results.BandResults;
using MetalArchivesCore.Models.Results.FullResults;
using MetalArchivesCore.Models.Results.SearchResults;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace MetalArchivesCore.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void RunQuickBandSearch()
        {
            IEnumerable<SimpleBandSearchResult> results = MetalArchives.Band.ByName("black sabbath");

            Assert.AreNotEqual(null, results);
        }

        [TestMethod]
        public void RunQuickAlbumSearch()
        {
            IEnumerable<SimpleAlbumSearchResult> results = MetalArchives.Album.ByName("13");

            Assert.AreNotEqual(null, results);
        }

        [TestMethod]
        public void RunQuickSongSearch()
        {
            IEnumerable<SimpleSongSearchResult> results = MetalArchives.Song.ByName("beneath the trees");

            Assert.AreNotEqual(null, results);
        }

        [TestMethod]
        public void GetAlbum()
        {
            IEnumerable<SimpleSongSearchResult> results = MetalArchives.Song.ByName("beneath the trees");
            AlbumResult album = results.First().GetFullAlbum();

            Assert.AreNotEqual(null, album);
        }

    }
}
