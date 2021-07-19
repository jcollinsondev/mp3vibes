using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IFilterService
    {
        public Expression<Func<TEntity, bool>> GetFilter<TEntity>(Filter filter);
    }

    public class FilterService : IFilterService
    {
        public Expression<Func<TEntity, bool>> GetFilter<TEntity>(Filter filterConfig)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "obj");
            // Initialize exprssion at true && true
            var expression = Expression.And(Expression.Constant(true), Expression.Constant(true));
            foreach (var filter in filterConfig.Filters)
            {
                expression = Expression.And(expression, this.GetFilterExpression<TEntity>(parameter, filter));
            }

            return Expression.Lambda<Func<TEntity, bool>>(expression, new ParameterExpression[] { parameter });
        }

        private Expression GetFilterExpression<TEntity>(ParameterExpression parameter, PropertyFilter propertyFilter)
        {
            var property = typeof(TEntity).GetProperty(propertyFilter.Property);
            if (property == null)
            {
                throw new Exception(String.Format("Cannot filter per {0} because it is not a know property.", propertyFilter.Property));
            }

            var filter = property.GetCustomAttributes(false).OfType<FilterAttribute>().FirstOrDefault();
            if (filter == null)
            {
                throw new Exception(String.Format("Cannot filter per {0} because it is not available as a filter property.", propertyFilter.Property));
            }

            Expression propertyExpression = Expression.PropertyOrField(parameter, propertyFilter.Property);
            Expression valueExpression = Expression.Constant(Convert.ChangeType(propertyFilter.Value, property.PropertyType));

            return filter.GetFilterExpression(propertyFilter.Type, propertyExpression, valueExpression);
        }
    }

    [Flags]
    public enum FilterType { EqualsTo, LowerThan, GreaterThan, Like, All }

    public class FilterAttribute : Attribute
    {
        public FilterType AllowedFilters { get; set; }

        public Expression GetFilterExpression(FilterType type, Expression propertyExpression, Expression valueExpression)
        {
            if ((this.AllowedFilters & FilterType.All) != FilterType.All && (this.AllowedFilters & type) != type)
            {
                throw new Exception("Filter not supported for the property.");
            }

            switch(type)
            {
                case FilterType.EqualsTo:
                    return Expression.Equal(propertyExpression, valueExpression);
                case FilterType.LowerThan:
                    return Expression.LessThan(propertyExpression, valueExpression);
                case FilterType.GreaterThan:
                    return Expression.GreaterThan(propertyExpression, valueExpression);
                case FilterType.Like:
                    return Expression.Call(propertyExpression, "Contains", null, valueExpression);
                default:
                    throw new Exception("Filter not supported.");
            }
        }
    }

    public class Filter
    {
        private List<PropertyFilter> _filters { get; set; }
        public List<PropertyFilter> Filters { get { return this._filters;  } }

        public Filter()
        {
            this._filters = new List<PropertyFilter>();
        }

        public void AddFilter(string property, FilterType type, object value)
        {
            this._filters.Add(new PropertyFilter { Property = property, Type = type, Value = value });
        }
    }

    public class PropertyFilter
    {
        public string Property { get; set; }
        public FilterType Type { get; set; }
        public object Value { get; set; }
    }
}
