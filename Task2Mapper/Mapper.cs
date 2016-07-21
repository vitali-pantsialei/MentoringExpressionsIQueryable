using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2Mapper
{
    public class Mapper<TSource, TDestination>
    {
        Func<TSource, TDestination> mapFunction;
        internal Mapper(Func<TSource, TDestination> func)
        {
            this.mapFunction = func;
        }

        public TDestination Map(TSource source)
        {
            return this.mapFunction(source);
        }
    }
}
