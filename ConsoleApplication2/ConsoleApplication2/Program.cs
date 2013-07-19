using System;
using System.Threading;

namespace ConsoleApplication2
{
    
    public class ClockTimerArgs : EventArgs
        //classes for event arguments
    {
        private int tickcount;

        public ClockTimerArgs(int tickcount)    //constructor called
        {
            this.tickcount = tickcount;
        }

        public int Tickcount    //property
        {
        get { return tickcount; }
        }
    
    }

    public delegate void TimerEvent(object sender, ClockTimerArgs e);
    class ClockTimer
        //class to generate the event
    {
        public event TimerEvent Timer;

        public void Start()
        {
            
            for (int i = 0; i < 5; i++)
            {
                Timer(this, new ClockTimerArgs(i+1));
                Thread.Sleep(1000);
            }
        }
    }

    class Program
    //class for event handling
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Event Program");
            Console.WriteLine("");
            ClockTimer clockTimer = new ClockTimer();
            clockTimer.Timer += new TimerEvent(OnClockTick);    //subscribing/registering to the timer event
            clockTimer.Start();
        }
        public static void OnClockTick(object sender, ClockTimerArgs e)
        //method for event handling
        {
            Console.WriteLine("Tick Tock!"+e.Tickcount);
           
        }
    }
}
    

