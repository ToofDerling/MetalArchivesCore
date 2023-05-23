using MetalArchivesCore.Models.Responses;

namespace MetalArchivesCore.Parsers.Abstract
{
    interface IParser<T> where T : class, new()
    {
        SearchResponse<T> Parse(string content);
    }
}
