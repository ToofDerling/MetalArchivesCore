using MetalArchivesCore.Attributes.Abstract;
using System.Text.RegularExpressions;

namespace MetalArchivesCore.Attributes
{
    /// <summary>
    /// Extracts regex group from string. Throws <see cref="Exception"/> when match won't succeed
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    class RegexConverterAttribute : FieldDecoratorBase
    {
        public RegexConverterAttribute(string regex)
        {
            _regex = regex;
        }

        readonly string _regex;

        public override object GetValue()
        {
            var value = (string)base.GetValue();

            var match = Regex.Match(value, _regex);

            if (!match.Success)
            {
                throw new Exception($"Regex parse error [{_regex}] -> [{value}]");
            }

            return match.Groups[1].Value;
        }
    }
}
