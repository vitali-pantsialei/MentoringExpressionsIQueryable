using System.Linq.Expressions;

namespace Task1Transformator
{
    public class AddSubtractTransform : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if ((node.NodeType == ExpressionType.Add || node.NodeType == ExpressionType.Subtract) &&
                node.Right.NodeType == ExpressionType.Constant && node.Left.NodeType == ExpressionType.Parameter)
            {
                ConstantExpression constant = (ConstantExpression)node.Right;
                if (constant.Type == typeof(int) && (int)(constant.Value) == 1)
                {
                    if (node.NodeType == ExpressionType.Add)
                    {
                        return Expression.Increment(node.Left);
                    }
                    else
                    {
                        return Expression.Decrement(node.Left);
                    }
                }
            }
            return base.VisitBinary(node);
        }
    }
}
