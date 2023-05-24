using MetalArchivesCore.Searchers.Configurators.Abstract;
using System.Collections.Generic;

namespace MetalArchivesCore.Searchers.Configurators
{
    /// <summary>
    /// Configuration for simple band search
    /// </summary>
    class SimpleBandConfigurators : IConfigurator
    {
        public string Url => @"https://www.metal-archives.com/search/ajax-band-search/";
        public Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>
            {
                // Only send the parameters that are actually needed (iDisplayStart is set by SimpleSearcher if results >200)
                { "field", "name"},
                //{ "sEcho", "1" },
                //{ "iColumns", "3" },
                //{ "iDisplayStart", "0" },
                //{ "iDisplayLength", "200" }
            };
    }
}
