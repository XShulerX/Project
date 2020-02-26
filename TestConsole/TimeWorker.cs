using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class TimeWorker : Worker
    {
        private double _PricePerHour;

        public TimeWorker(int Age, string Name, double PricePerHour)
            : base(Age, Name)
        {
            _PricePerHour = PricePerHour;
            Pay();
        }

        protected override void Pay()
        {
            _Price = 20.8 * 8 * _PricePerHour;
        }
    }
}
