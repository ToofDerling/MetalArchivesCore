using System.Text.RegularExpressions;
using WebsiteParserCore.Converters.Abstract;

namespace MetalArchivesCore.CustomWebsiteConverters
{
    class AlbumDateTimeConverter : IConverter
    {
        private const int _defaultDay = 1;
        private const int _defaultMonth = 7;

        private const string _months = "January|February|March|April|May|June|July|August|September|October|November|December";
        private static readonly string[] _monthsArr = _months.Split('|');

        public object Convert(object input)
        {
            var value = (string)input;

            // This handles "November 29th, 2019", "November, 2019", "November 2019", "2019"
            var match = Regex.Match(value, $"(?<month>{_months})?(,)?( )?((?<day>\\d{{1,2}})(th|st|nd|rd),)?( )?(?<year>\\d{{4}})");

            if (!match.Success)
            {
                return null;
            }

            var year = int.Parse(match.Groups["year"].Value);

            var monthName = match.Groups["month"].Value;
            var idx = string.IsNullOrEmpty(monthName) ? -1 : Array.FindIndex(_monthsArr, s => s == monthName);
            var month = idx == -1 ? _defaultMonth : idx + 1;

            var dayStr = match.Groups["day"].Value;
            var day = string.IsNullOrEmpty(dayStr) ? _defaultDay : int.Parse(dayStr);

            var albumDate = new DateTime(year, month, day);
            return albumDate;
        }
    }
}













