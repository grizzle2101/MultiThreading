using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiningPhilosophersWaiter
{
    public class Philosopher
    {
        public string Name { get; set; }
        public IList<ChopStick> ChopSticks = new List<ChopStick>();
        public int WaitingTime;
        private static object _synObj = new object();

        public void Eat(IList<ChopStick> incomingPair)
        {
            ChopSticks = incomingPair;
            WaitingTime = new Random().Next(1, 10);
            Console.WriteLine("{0} is Eating with {1} & {2}", Name, ChopSticks[0].Id, ChopSticks[1].Id);
            Thread.Sleep(WaitingTime);

        }

        public IList<ChopStick> Finish()
        {
            lock (_synObj)
            {
                Console.WriteLine("{0} Finished Eating in {1}ms", Name, WaitingTime);
                return ChopSticks;
            }
        }
    }
}
