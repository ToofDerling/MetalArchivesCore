using MetalArchivesCore.Searchers.Configurators.Abstract;

namespace MetalArchivesCore.Searchers.Configurators
{
    class SimpleSongConfigurators : IConfigurator
    {
        public string Url => @"https://www.metal-archives.com/search/ajax-song-search/";
        public Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>
            {
                // Only send the parameters that are actually needed (iDisplayStart is set by SimpleSearcher if results >200)
                { "field", "title"},
                //{ "sEcho", "1" },
                //{ "iColumns", "3" },
                //{ "iDisplayStart", "0" },
                //{ "iDisplayLength", "200" }
            };
    }
}
