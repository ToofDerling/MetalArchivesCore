using MetalArchivesCore.Attributes;
using MetalArchivesCore.Attributes.Abstract;
using MetalArchivesCore.Models.Responses;
using MetalArchivesCore.Parsers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MetalArchivesCore.Parsers
{
    /// <summary>
    /// Parses input string by given typ
    /// </summary>
    /// <typeparam name="T">Return type</typeparam>
    class ResponseParser<T> : IParser<List<T>> where T : class, new()
    {
        /// <summary>
        /// Parses class using attributes on it's properties. Props without <see cref="ColumnAttribute">ColumnAttribute</see>  won't be used in parsing engine
        /// </summary>
        /// <param name="content">Json string of <see cref="SearchResponse"/></param>
        /// <returns>Parsed list of element based</returns>
        public List<T> Parse(string content)
        {
            var response = JsonSerializer.Deserialize<SearchResponse>(content);

            List<Action<string[], T>> assignList = new List<Action<string[], T>>();

            foreach (var prop in typeof(T).GetProperties())
            {
                var column = prop.GetCustomAttribute<ColumnAttribute>();
                if (column != null)
                {

                    FieldDecoratorBase lastDecorator = column;
                    List<FieldDecoratorBase> decorators = prop.GetCustomAttributes().Except(new List<Attribute> { column }).OfType<FieldDecoratorBase>().ToList();

                    foreach (var dec in decorators)
                    {
                        dec.SetDecorator(lastDecorator);
                        lastDecorator = dec;
                    }

                    assignList.Add((list, model) =>
                    {
                        column.SetBaseValue(list[column.Index]);
                        object val = lastDecorator.GetValue();
                        prop.SetValue(model, val);
                    });
                }
            }

            List<T> items = new List<T>();

            foreach (var respItem in response.aaData)
            {
                T model = new T();

                foreach (var assignOperation in assignList)
                    assignOperation(respItem, model);

                items.Add(model);
            }

            return items;
        }

    }
}
