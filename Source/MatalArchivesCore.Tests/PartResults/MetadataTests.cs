using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using MetalArchivesCore.Models.Results.PartResults;
using MetalArchivesCore.Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteParserCore;

namespace MetalArchivesCore.Tests.PartResults
{
    [TestClass]
    public class MetadataTests
    {
        [TestMethod]
        public void AddedBy()
        {
            var metadata = GetMetadata(Resources.Album_Belzebubs);

            Assert.AreEqual("PaganiusI", metadata.AddedBy);
        }

        [TestMethod]
        public void ModifiedBy()
        {
            var metadata = GetMetadata(Resources.Album_Belzebubs);

            Assert.AreEqual("Dying_Hope", metadata.ModifiedBy);
        }

        [TestMethod]
        public void AddDate()
        {
            var metadata = GetMetadata(Resources.Album_Belzebubs);

            Assert.AreEqual(new DateTime(2019, 2, 22, 13, 55, 18), metadata.AddDate);
        }

        [TestMethod]
        public void ModifiedDate()
        {
            var metadata = GetMetadata(Resources.Album_Belzebubs);

            Assert.AreEqual(new DateTime(2019, 9, 26, 13, 35, 57), metadata.ModifiedDate);
        }

        private Metadata GetMetadata(string resource)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(resource);

            string html = document.DocumentNode.QuerySelector("#auditTrail").InnerHtml;

            return WebContentParser.Parse<Metadata>(html);
        }

    }
}
