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
    public static class OrderExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string order)
        {
            if (!order.IsNullOrEmpty())
            {
                var orderData = JsonSerializer.Deserialize<OrderData>(order, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                var parameter = Expression.Parameter(typeof(T), "_");

                string method = string.Empty;
                foreach(var orderModel in orderData.Data)
                {
                    var property = Expression.Property(parameter, orderModel.PropertyName);
                    var lambda = Expression.Lambda(property, parameter);
                    if (method.IsNullOrEmpty())
                    {
                        method = orderModel.Direction == "asc" ? "OrderBy" : "OrderByDescending";
                    }
                    else
                    {
                        method = orderModel.Direction == "asc" ? "ThenBy" : "ThenByDescending";
                    }
                    Type[] types = new Type[] { typeof(T), lambda.Body.Type };
                    var methodCallExpression = Expression.Call(typeof(Queryable), method, types, query.Expression, lambda);

                    query = query.Provider.CreateQuery<T>(methodCallExpression);
                }
                return query;
            }
            return query;
        }
    }
}
