using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class FixedWorker : Worker
    {
        private double _FixedPrice;

        public FixedWorker(int Age, string Name, double FixedPrice)
            :base(Age,Name)
        {
            _FixedPrice = FixedPrice;
        }

        protected override void Pay()
        {
            _Price = _FixedPrice;
        }
    }
}
