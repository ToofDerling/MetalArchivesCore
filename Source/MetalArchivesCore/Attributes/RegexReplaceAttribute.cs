using MetalArchivesCore.Attributes.Abstract;
using System.Text.RegularExpressions;

namespace MetalArchivesCore.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    class RegexReplaceAttribute : FieldDecoratorBase
    {
        public RegexReplaceAttribute(string pattern, string replaceTo)
        {
            _pattern = pattern;
            _replaceTo = replaceTo;
        }

        readonly string _pattern, _replaceTo;

        public override object GetValue()
        {
            var value = (string)base.GetValue();

            return Regex.Replace(value, _pattern, _replaceTo);
        }
    }
}
