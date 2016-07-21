using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1Transformator;
using System.Linq.Expressions;

namespace Task1TransformatorTest
{
    [TestClass]
    public class ParamsToConstTest
    {
        [TestMethod]
        public void ChangeParamsToConstTest()
        {
            Expression<Func<int, int, int, int>> input_exp = (a, b, c) => ((a + 1) * (b - 1) - 1) * (c + 1);
            ParamsToConstTransform<int> transform = new ParamsToConstTransform<int>(new Dictionary<string, int>() 
            {
                {"a", 1},
                {"b", 2}
            });
            var result_exp = transform.VisitAndConvert(input_exp, "");

            Console.WriteLine(input_exp + " = " + input_exp.Compile().Invoke(1, 1, 1));
            Console.WriteLine(result_exp + " = " + result_exp.Compile().Invoke(1, 1, 1));
        }
    }
}
