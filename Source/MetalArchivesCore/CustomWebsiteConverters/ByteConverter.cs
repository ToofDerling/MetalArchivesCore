using WebsiteParserCore.Converters.Abstract;

namespace MetalArchivesCore.CustomWebsiteConverters
{
    class ByteConverter : IConverter
    {
        public object Convert(object input)
        {
            return byte.Parse((string)input);
        }
    }
}
