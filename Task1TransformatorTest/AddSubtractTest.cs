using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using Task1Transformator;

namespace Task1TransformatorTest
{
    [TestClass]
    public class AddSubtractTest
    {
        [TestMethod]
        public void AddSubtractChangeTest()
        {
            Expression<Func<int, int>> input_exp = (a) => ((a + 1) * (a + 5) * (a - 1) + 1) * (1 + a) * (1 + 1) + (a - a);
            var result_exp = (new AddSubtractTransform().VisitAndConvert(input_exp, ""));

            Console.WriteLine(input_exp + " = " + input_exp.Compile().Invoke(1));
            Console.WriteLine(result_exp + " = " + result_exp.Compile().Invoke(1));
        }

        [TestMethod]
        public void AddSubtractChangeSeveralParamsTest()
        {
            Expression<Func<int, int, int, int>> input_exp = (a, b, c) => ((a + 1) * (b - 1) - 1) * (c + 1);
            var result_exp = (new AddSubtractTransform().VisitAndConvert(input_exp, ""));

            Console.WriteLine(input_exp + " = " + input_exp.Compile().Invoke(1, 1, 1));
            Console.WriteLine(result_exp + " = " + result_exp.Compile().Invoke(1, 1, 1));
        }
    }
}
