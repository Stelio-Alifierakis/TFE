using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace wsProxy
{
    public partial class wsProxy : ServiceBase
    {
        public wsProxy()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("testLog"))
            {
                System.Diagnostics.EventLog.CreateEventSource("testLog","newLog");
            }
            eventLog1.Source = "testLog";
            eventLog1.Log = "newLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("est démarré");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("est éteint");
        }
    }
}
