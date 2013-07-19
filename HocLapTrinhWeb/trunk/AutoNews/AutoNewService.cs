using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace AutoNews
{
    partial class AutoNewService : ServiceBase
    {
        static void Main(string[] args)
        {
#if(!DEBUG)
            {
                ServiceBase.Run(new AutoNewService());
            }
#else
            {
                AutoNewService service = new AutoNewService();
                service.OnStart(null);
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            }
#endif
        }

        public AutoNewService()
        {
            InitializeComponent();

            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.ServiceName = "AutoNewService";
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                var path = ConfigurationManager.AppSettings["Uploads"];
                path += "\\nhatnam.txt";
                var file = new StreamWriter(path, true);
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
