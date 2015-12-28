namespace DiningPhilosophersWaiter
{
    public class ChopStick
    {
        public int Id { get; set; }
        public bool Available { get; set; }

        public ChopStick()
        {
            Available = true;
        }

        public void Use()
        {
            Available = false;
        }

        public void Release()
        {
            Available = true;
        }
    }
}