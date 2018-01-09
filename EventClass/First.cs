using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EventClass
{
    class PublisherOfEvent
    {
        // Define a delegate named LogHandler1, which will encapsulate
        // any method that takes a string as the parameter and returns no value
        public delegate void LogHandler1(string msg);

        // Define an Event based on the above Delegate
        public event LogHandler1 LogEvent1;

        // Instead of having the Process() function take a delegate
        // as a parameter, we've declared a Log event. Call the Event,
        // using the OnXXXX Method, where XXXX is the name of the Event.
        public void Process()
        {
            OnLog("Process() begin");
            OnLog("Process() end");
        }

        protected void OnLog(string msg)
        {
            if (LogEvent1 != null)
            {
                LogEvent1(msg);
            }
        }
    }

    class FileLogger
    {
        FileStream fs;
        StreamWriter sw;

        public FileLogger(string filename)
        {
            fs = new FileStream(filename, FileMode.Create);
            sw = new StreamWriter(fs);
        }
        //Member Function which is used in the Delegate
        public void Logger(string s)
        {
            sw.WriteLine(s);
        }

        public void Close()
        {
            sw.Close();
            fs.Close();
        }
    }
}
