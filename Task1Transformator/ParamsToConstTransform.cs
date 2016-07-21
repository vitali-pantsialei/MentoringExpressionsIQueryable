using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task1Transformator
{
    public class ParamsToConstTransform<TVal> : ExpressionVisitor
    {
        public IDictionary<string, TVal> ParamsToChange { get; set; }
        public ParamsToConstTransform(IDictionary<string, TVal> changeParams) : base()
        {
            this.ParamsToChange = changeParams;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return Expression.Lambda(Visit(node.Body), node.Parameters);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (this.ParamsToChange.ContainsKey(node.Name))
            {
                return Expression.Constant(this.ParamsToChange[node.Name], typeof(TVal));
            }
            return base.VisitParameter(node);
        }
    }
}
