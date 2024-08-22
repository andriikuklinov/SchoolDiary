using Microsoft.IdentityModel.Tokens;
using SchoolDiary.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolDiary.DAL.Extensions
{
    public static class FilterExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, string filter)
        {
            if (!filter.IsNullOrEmpty())
            {
                var filterData = JsonSerializer.Deserialize<FilterData>(filter, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                var parameter = Expression.Parameter(typeof(T), "_");

                Expression filterExpression = null;
                foreach (var filterModel in filterData.Data)
                {
                    var property = Expression.Property(parameter, filterModel.PropertyName);
                    var constant = Expression.Constant(filterModel.Value);
                    Expression comparison = null;
                    if (property.Type == typeof(string))
                    {
                        comparison = Expression.Call(property, "Contains", Type.EmptyTypes, constant);
                    }
                    else
                    {
                        comparison = Expression.Equal(property, constant);
                    }
                    filterExpression = filterExpression == null ? comparison : Expression.Add(filterExpression, comparison);
                }
                var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);

                return query.Where(lambda);
            }
            return query;
        }
    }
}
