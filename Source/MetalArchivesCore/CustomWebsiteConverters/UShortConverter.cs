using WebsiteParserCore.Converters.Abstract;

namespace MetalArchivesCore.CustomWebsiteConverters
{
    class UShortConverter : IConverter
    {
        public object Convert(object input)
        {
            return ushort.TryParse((string)input, out ushort value) ? value : null;
        }
    }
}
