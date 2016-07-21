using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2MapperTest
{
    public class Foo
    {
        public string Name { get; set; }

        private double privateNumber = 19.9;
        public int Number { get; set; }
        public double GetFloat
        {
            get
            {
                return this.privateNumber;
            }
        }
    }
}
