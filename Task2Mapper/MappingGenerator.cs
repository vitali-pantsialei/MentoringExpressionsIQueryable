using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Task2Mapper
{
    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>() 
            where TSource : new() 
            where TDestination : new()
        {
            var sourceParam = Expression.Parameter(typeof(TSource));
            var bindings = new List<MemberBinding>();
            foreach (PropertyInfo sourceProperty in typeof(TSource).GetProperties())
            {
                if (!sourceProperty.CanRead)
                {
                    continue;
                }
                PropertyInfo targetProperty = typeof(TDestination).GetProperty(sourceProperty.Name);
                if (targetProperty == null || !targetProperty.CanWrite || !targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                {
                    continue;
                }
                bindings.Add(Expression.Bind(targetProperty, Expression.Property(sourceParam, sourceProperty)));
            }
            Expression initializer = Expression.MemberInit(Expression.New(typeof(TDestination)), bindings);
            return new Mapper<TSource, TDestination>(Expression.Lambda<Func<TSource, TDestination>>(initializer, sourceParam).Compile());
        }
    }
}
