using System;
using System.IO;

namespace EventClass
{
    public class Program
    {
        static void Logger(string s)
        {
            Console.WriteLine(s);
        }
        static void Main(string[] args)
        {
            #region first
            //FileLogger f1 = new FileLogger("process.log");
            //PublisherOfEvent p1 = new PublisherOfEvent();

            ////Subscribe the Functions Logger and fl.Logger
            //p1.LogEvent1 += new PublisherOfEvent.LogHandler1(Logger);
            //p1.LogEvent1 += new PublisherOfEvent.LogHandler1(f1.Logger);

            ////The Event will now be triggered in the Process() Method
            //p1.Process();

            //f1.Close();
            #endregion

            #region second
            Clock theClock = new Clock();

            // Create the display and tell it to
            // subscribe to the clock just created
            DisplayClock dc = new DisplayClock();
            dc.Subscribe(theClock);

            // Create a Log object and tell it
            // to subscribe to the clock
            LogClock lc = new LogClock();
            lc.Subscribe(theClock);

            // Get the clock started
            theClock.Run();
            #endregion

            Console.Write("Press any key...");
            Console.ReadKey();
        }
    }
}
