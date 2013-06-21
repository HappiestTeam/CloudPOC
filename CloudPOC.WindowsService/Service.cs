using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Configuration;
using System.Configuration.Install;

namespace Service.Scheduler
{
    [RunInstallerAttribute(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller processInstaller;

        public ProjectInstaller()
        {
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();
            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = "DemoService.Scheduler";
            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }

    partial class Service : ServiceBase
    {
        private string logPath = ConfigManager.LogFile;

        public Service()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "DemoService.Scheduler";
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                System.Threading.Thread.Sleep(10000);
                CommonUtil.WriteLog(logPath, "Process Started");
                Scheduler objManager = new Scheduler();
            }
            catch (Exception e)
            {
                // do nothing
            }
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Process Stopped ... ");
            CommonUtil.WriteLog(logPath, "Process Stopped ...");
        }

        [MTAThread]
        static void Main()
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;
            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new Service() };
            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }
    }
}
