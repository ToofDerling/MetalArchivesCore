using MetalArchivesCore.Attributes;
using MetalArchivesCore.Attributes.Abstract;
using MetalArchivesCore.Models.Responses;
using MetalArchivesCore.Parsers.Abstract;
using System.Reflection;
using System.Text.Json;

namespace MetalArchivesCore.Parsers
{
    /// <summary>
    /// Parses input string by given type
    /// </summary>
    /// <typeparam name="T">Return type</typeparam>
    class ResponseParser<T> : IParser<T> where T : class, new()
    {
        private readonly List<Action<string[], T>> _assignList;

        public ResponseParser()
        {
            _assignList = new List<Action<string[], T>>();

            foreach (var prop in typeof(T).GetProperties())
            {
                var decorators = prop.GetCustomAttributes().OfType<FieldDecoratorBase>().ToList();

                if (decorators.FirstOrDefault(d => d is ColumnAttribute) is ColumnAttribute column)
                {
                    var lastDecorator = (FieldDecoratorBase)column;

                    foreach (var decorator in decorators.Except(new[] { column }))
                    {
                        decorator.SetDecorator(lastDecorator);
                        lastDecorator = decorator;
                    }

                    _assignList.Add((list, model) =>
                    {
                        column.SetBaseValue(list[column.Index]);
                        var value = lastDecorator.GetValue();
                        prop.SetValue(model, value);
                    });
                }
            }
        }

        /// <summary>
        /// Parses class using attributes on it's properties. Props without <see cref="ColumnAttribute">ColumnAttribute</see>  won't be used in parsing engine
        /// </summary>
        /// <param name="content">Json string of <see cref="SearchResponse"/></param>
        /// <returns>Parsed list of element based</returns>
        public SearchResponse<T> Parse(string content)
        {
            var response = JsonSerializer.Deserialize<SearchResponse<T>>(content);

            var items = new List<T>();

            foreach (var respItem in response.aaData)
            {
                var model = new T();

                foreach (var assignOperation in _assignList)
                {
                    assignOperation(respItem, model);
                }

                items.Add(model);
            }

            response.Items = items;
            return response;
        }
    }
}
