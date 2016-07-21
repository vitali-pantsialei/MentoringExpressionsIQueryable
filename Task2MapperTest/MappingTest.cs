using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2Mapper;

namespace Task2MapperTest
{
    [TestClass]
    public class MappingTest
    {
        [TestMethod]
        public void TestWithEqualFields()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();
            var expected = new Foo()
            {
                Name = "test",
                Number = 23
            };
            var actual = mapper.Map(expected);

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Number, actual.Number);
            Assert.AreEqual(0, actual.GetFloat);
        }

        [TestMethod]
        public void TestWithNotEqualFields()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, BarNotEqual>();
            var expected = new Foo()
            {
                Name = "test",
                Number = 23
            };
            var actual = mapper.Map(expected);

            Assert.IsNotNull(actual);
        }
    }
}
