using System.ComponentModel;
using WebsiteParserCore.Converters.Abstract;

namespace MetalArchivesCore.CustomWebsiteConverters
{
    class EnumDescriptionConverter<T> : IConverter where T : Enum
    {
        public object Convert(object input)
        {
            var inputStr = (string)input;

            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == inputStr)
                    {
                        return field.GetValue(null);
                    }
                }
                else if (field.Name == inputStr)
                {
                    return field.GetValue(null);
                }
            }

            return null;
        }
    }
}
