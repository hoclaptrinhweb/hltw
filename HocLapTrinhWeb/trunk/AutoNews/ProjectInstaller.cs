using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;

namespace AutoNews
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            InitializeComponent();

            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;

            // Instantiate and configure a ServiceInstaller
            service = new ServiceInstaller();
            service.DisplayName = "AutoNewService";
            service.ServiceName = "AutoNewService";
            service.StartType = ServiceStartMode.Automatic;

            // Add both the ServiceProcessInstaller and ServiceInstaller to
            // the Installer collection, which is inherited from the
            // Installer base class
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
