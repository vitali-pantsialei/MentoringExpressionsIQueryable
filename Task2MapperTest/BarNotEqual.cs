using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2MapperTest
{
    public class BarNotEqual
    {
        public int Name { get; set; }
        public string Name2 { get; set; }

        private double privateNumber = 0;
        public double Number { get; set; }
        public double GetFloat
        {
            get
            {
                return this.privateNumber;
            }
        }
    }
}
