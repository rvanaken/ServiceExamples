using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyNewMyNewService1
{
    public partial class MyNewService1 : ServiceBase
    {
        System.Timers.Timer workTimer;

        public MyNewService1(string[] args)
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource("MyNewServiceEventSource", "MyNewServiceEventLog");
                eventLog1.Source = "MyNewServiceEventSource";
                eventLog1.Log = "MyNewServiceEventLog";
            }
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("OnStart method called.");
            workTimer = new System.Timers.Timer();
            workTimer.Interval = 60000;
            workTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnWorkTimer);
            workTimer.Start();
        }

        public void OnWorkTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            eventLog1.WriteEntry("WorkTimer fired, Sender: " + sender.ToString() + " at time: " + args.SignalTime.ToString(), EventLogEntryType.Information);
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("OnStop method called.");
            workTimer.Stop();
        }

        protected override void OnContinue()
        {
            //base.OnContinue();
            eventLog1.WriteEntry("OnStop method called.");
        }
    }
}
