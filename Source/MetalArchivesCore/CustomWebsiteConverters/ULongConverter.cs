using WebsiteParserCore.Converters.Abstract;

namespace MetalArchivesCore.CustomWebsiteConverters
{
    class ULongConverter : IConverter
    {
        public object Convert(object input)
        {
            return ulong.Parse((string)input);
        }
    }
}
