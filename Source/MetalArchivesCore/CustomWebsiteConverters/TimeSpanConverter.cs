using WebsiteParserCore.Converters.Abstract;

namespace MetalArchivesCore.CustomWebsiteConverters
{
    class TimeSpanConverter : IConverter
    {
        public object Convert(object input)
        {
            var inputStr = (string)input;

            int hours, minutes, seconds;

            var fragments = inputStr.Split(':').Reverse();

            hours = fragments.Count() == 3 ? int.Parse(fragments.ElementAt(2)) : 0;
            minutes = int.Parse(fragments.ElementAt(1));
            seconds = int.Parse(fragments.ElementAt(0));

            return new TimeSpan(hours, minutes, seconds);
        }
    }
}
