using WebsiteParserCore.Converters.Abstract;

namespace MetalArchivesCore.CustomWebsiteConverters
{
    class SplitConverter : IConverter
    {
        public object Convert(object input)
        {
            var inputStr = (string)input;

            return inputStr.Split(',').Select(i => i.Trim()).ToList();
        }
    }
}
