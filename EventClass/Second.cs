using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EventClass
{
    // Our subject -- it is this class that other classes
    // will observe. This class publishes one event:
    // SecondChange. The observers subscribe to that event.
    public class Clock
    {
        //Private Fields holding the hour, minute and second
        private int _hour;
        private int _minute;
        private int _second;

        // The delegate named SecondChangeHandler, which will encapsulate
        // any method that takes a clock object and a TimeInfoEventArgs
        // object as the parameter and returns no value. It's the
        // delegate the subscribers must implement.
        public delegate void SecondChangeHandler(
            object clock,
            TimeInfoEventArgs timeInformation
            );

        // The event we publish
        public event SecondChangeHandler SecondChange;

        // The method which fires the Event
        protected void OnSecondChange(
            object clock,
            TimeInfoEventArgs timeInformaiton
            )
        {
            if (SecondChange != null)
            {
                SecondChange(clock, timeInformaiton);
            }
        }

        // Set the clock running, it will raise an
        // event for each new second
        public void Run()
        {
            for (; ; )
            {
                System.Threading.Thread.Sleep(1000);
                System.DateTime dt = System.DateTime.Now;

                // If the second has changed
                // notify the subscribers
                if (dt.Second != _second)
                {
                    // Create the TimeInfoEventArgs object
                    // to pass to the subscribers
                    TimeInfoEventArgs timeInformation = new TimeInfoEventArgs(dt.Hour, dt.Minute, dt.Second);
                    // If anyone has subscribed, notify them
                    OnSecondChange(this, timeInformation);
                }
                // update the state
                _second = dt.Second;
                _minute = dt.Minute;
                _hour = dt.Hour;
            }
        }
    }
    
    // The class to hold the information about the event
    // in this case it will hold only information
    // available in the clock class, but could hold
    // additional state information
    public class TimeInfoEventArgs : EventArgs
    {
        public readonly int hour;
        public readonly int minute;
        public readonly int second;

        public TimeInfoEventArgs(int hour, int minute, int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }
    }
    
    // An observer. DisplayClock subscribes to the
    // clock's events. The job of DisplayClock is
    // to display the current time
    public class DisplayClock
    {
        // The method that implements the
        // delegated functionality
        public void TimeHasChanged(object theClock, TimeInfoEventArgs ti)
        {
            Console.WriteLine("Current Time: {0}:{1}:{2}",
                ti.hour.ToString(),
                ti.minute.ToString(),
                ti.second.ToString());
        }

        // Given a clock, subscribe to
        // its SecondChangeHandler event
        public void Subscribe(Clock theClock)
        {
            theClock.SecondChange += new Clock.SecondChangeHandler(TimeHasChanged);
        }       
    }

    // A second subscriber whose job is to write to a file
    public class LogClock
    {
        // This method should write to a file
        // we write to the console to see the effect
        // this object keeps no state
        public void WriteLogEntry(
            object theClock, TimeInfoEventArgs ti)
        {
            Console.WriteLine("Logging to file: {0}:{1}:{2}",
                ti.hour.ToString(),
                ti.minute.ToString(),
                ti.second.ToString());
        }

        public void Subscribe(Clock theClock)
        {
            theClock.SecondChange += new Clock.SecondChangeHandler(WriteLogEntry);
        }
    }
}
