using System;
using System.Collections.Generic;
using System.Threading;

namespace DiningPhilosophersWaiter
{
    public class Waiter
    {
        public IList<ChopStick> ChopSticks = new List<ChopStick>();
        private static object _syncObj = new object();
        public void PickUpChopSticks(IList<ChopStick> incomingSticks )
        {
            foreach (var stick in incomingSticks)
            {
                stick.Release();
                ChopSticks.Add(stick);
            }
        }
        public IList<ChopStick> ServeChopSticks()
        {
            lock (_syncObj)
            {
                IList<ChopStick> availablePair = new List<ChopStick>();
                if (ChopSticks.Count > 2)
                {
                    availablePair.Add(ChopSticks[0]);
                    availablePair.Add(ChopSticks[1]);
                    SanitizeAndSort();
                    foreach (var stick in availablePair)
                    {
                        stick.Use();
                    }
                }
                else
                {
                    Thread.Sleep(new Random().Next(0, 100));
                    ServeChopSticks();
                }
                return availablePair; 
            }
           
        }
        public void SanitizeAndSort()
        {
            ChopSticks.RemoveAt(0);
            ChopSticks.RemoveAt(0);
        }
    }
}