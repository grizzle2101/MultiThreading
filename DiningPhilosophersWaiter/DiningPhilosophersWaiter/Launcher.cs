using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiningPhilosophersWaiter
{
    public class Launcher
    {
        private static object _syncObj = new object();

        public static void Main(string[] args)
        {
            Console.WriteLine("Dining Philosophers Solution: ");

            //Create ChopSticks
            var chopOne = new ChopStick(){Id = 1};
            var chopTwo = new ChopStick() { Id = 2 };
            var chopthree = new ChopStick() { Id = 3 };
            var chopFour = new ChopStick() { Id = 4 };
            var chopFive = new ChopStick() { Id = 5 };
            IList<ChopStick> chopSticks = new List<ChopStick>(){chopOne,chopTwo, chopthree, chopFour, chopFive};

            //Waiter
            var waiter = new Waiter();
            waiter.PickUpChopSticks(chopSticks);

            //Philosophers
            var socrates = new Philosopher(){Name = "Socrates"};
            var plato = new Philosopher(){Name = "Plato"};
            var confucius = new Philosopher(){Name = "Confucius"};
            var aristotle = new Philosopher(){Name = "Aristotle"};
            var marx = new Philosopher(){Name = "Marx"};

            //Multi-Threaded Dining
            Thread t1 = new Thread(() => Dining(socrates, waiter));
            t1.Start();

            Thread t2 = new Thread(() => Dining(plato, waiter));
            t2.Start();

            Thread t3 = new Thread(() => Dining(confucius, waiter));
            t3.Start();

            Thread t4 = new Thread(() => Dining(aristotle, waiter));
            t4.Start();

            Thread t5 = new Thread(() => Dining(marx, waiter));
            t5.Start();

            Console.ReadLine();
        }

        public static void Dining(Philosopher p, Waiter w)
        {
            bool lockTaken = false;
            Monitor.TryEnter(_syncObj, TimeSpan.FromMilliseconds(30), ref lockTaken);
            try
            {
                Console.WriteLine("---------------------------------");
                p.Eat(w.ServeChopSticks());
                w.PickUpChopSticks(p.Finish());
                Console.WriteLine("---------------------------------");
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(_syncObj);
                }
            }
        }
    }
}
