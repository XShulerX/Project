using System;
using System.Collections.Generic;

namespace TestConsole
{
    class Program
    {
        private static Worker[] _Workers;
        static void Main(string[] args)
        {
            var workers = new List<Worker>();
            var rnd = new Random();

            const int fixed_workers_count = 30;
            for (int i = 0; i < fixed_workers_count; i++)
                workers.Add(new FixedWorker(
                    rnd.Next(20, 35),
                    "FixedWorker" + i,
                    rnd.Next(70000, 150000)
                    ));
            const int time_workers_count = 30;
            for (int i = 0; i < time_workers_count; i++)
                workers.Add(new TimeWorker(
                    rnd.Next(20, 35),
                    "TimeWorker" + i,
                    rnd.Next(465, 490)
                    ));
            _Workers = workers.ToArray();
            Pause();
        }

        static void Pause()
        {
            Console.ReadKey();
        }
    }
}
