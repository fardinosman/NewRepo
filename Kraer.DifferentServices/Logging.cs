using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Kraer.DifferentServices
{
    public class Logging
    {
        //public static void LogAction(string source, string logname, string errorMessage)
        //{
        //    //Create the source, if it does not already exist
        //    if (!EventLog.SourceExists(source))
        //    {
        //        //An Event log source should not be created and immdiately used.//
        //        //There is a latency time to enable the source, it should be created
        //        // prior to excuting the application that uses the source
        //        //Executes this sample a second time to use the new source.
        //        EventLog.CreateEventSource(source, logname);
        //        Console.WriteLine("CreatedEventSource");
        //        Console.WriteLine("Existing, execute the application to allow to be registered");
        //        return;
        //    }
        //    //
        //    EventLog eventLog = new EventLog();//
        //    eventLog.Source = source;
        //    //Write an informational entry to the event log.
        //    eventLog.WriteEntry(errorMessage);
        //}
    }
}

