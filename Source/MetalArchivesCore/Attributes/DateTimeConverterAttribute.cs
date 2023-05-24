using MetalArchivesCore.Attributes.Abstract;

namespace MetalArchivesCore.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    class DateTimeConverterAttribute : FieldDecoratorBase
    {
        public DateTimeConverterAttribute(string dateFormat)
        {
            _format = dateFormat;
        }

        readonly string _format;

        public override object GetValue()
        {
            var value = (string)base.GetValue();

            return DateTime.ParseExact(value, _format, null);
        }
    }
}
