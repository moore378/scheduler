using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Threading.Tasks;
//using System.ServiceModel;
using System.ServiceProcess;
using Common.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Job;
using Quartz.Impl.Matchers;
using MjhGeneral.ServiceProcess;

namespace SchedulerService
{
    public class SchedulerService
    {
        #region Nested classes to support running as service
        public const string ServiceName = "IPS Job Scheduler";
        public const string CurrentVersion = "0.01";

        // Grab the Scheduler instance from the Factory 
        private static IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

        [WindowsService(Name = SchedulerService.ServiceName, 
                        Description = "Scheduler runs IPS jobs based on configured cron trigger.", 
                        ServiceStartMode = ServiceStartMode.Automatic)]
        public class Service : ServiceBase
        {
            public Service()
            {
                ServiceName = SchedulerService.ServiceName;
            }

            protected override void OnStart(string[] args)
            {
                SchedulerService.Start(args);
            }

            protected override void OnStop()
            {
                SchedulerService.Stop();
            }
        }
        #endregion

        static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
                // running as service
                using (var service = new Service())
                    ServiceBase.Run(service);
            else
            {
                // running as console app
                string mode = args.Length > 0 ? args[0] : null;

                if (mode == "-v" || mode == "--version")
                {
                    Console.WriteLine(SchedulerService.ServiceName + " " + SchedulerService.CurrentVersion + "\n");
                }
                else if(mode == "-h" || mode == "--help")
                {
                    ManPage();
                }
                else if (mode == "install")
                {
                    //string instanceName = null;

                    //if (2 <= args.Length)
                    //{
                    //    instanceName = args[1];
                    //}
                    Console.WriteLine("Installing Service ...");
                    WindowsServiceInstaller.RuntimeInstall<Service>(null);
                }
                else if (mode == "uninstall")
                {
                    //string instanceName = null;

                    //if (2 <= args.Length)
                    //{
                    //    instanceName = args[1];
                    //}
                    Console.WriteLine("Uninstalling Service ...");
                    WindowsServiceInstaller.RuntimeUninstall<Service>(null);
                }
                else if (mode != null)
                {
                    Console.WriteLine("Invalid arguments");
                    Console.WriteLine("Run without arguments to run as a service (requires that it is installed and started from service manager)");
                }

                Start(args);

                Console.WriteLine("Press any key to stop...");
                // Put the menu system here, turn ReadKey into ReadLine for user input
                Console.ReadKey(true);

                 Stop();
            }

            
        }

        private static void Start(string[] args)
        {
            scheduler.Start();

        }

        private static void Stop()
        {
            // and last shut down the scheduler when you are ready to close your program
            scheduler.Shutdown();
        }

        private static void ManPage()
        {
            Console.WriteLine("NAME\n");
            Console.WriteLine("".PadLeft(5) + "SchedulerConsole - IPS Scheduler Console Interface\n");
            Console.WriteLine("SYNOPSIS\n");
            Console.WriteLine("".PadLeft(5) + "SchedulerConsole [OPTION]....\n");
            Console.WriteLine("DESCRIPTION\n");
            Console.WriteLine("".PadLeft(5) + "The IPS Scheduler is a service that schedules and runs");
            Console.WriteLine("".PadLeft(5) + "jobs for IPS. It is very similiar to Cron or Windows Scheduler.\n\n");
            Console.WriteLine("".PadLeft(5) + "-v, --version");
            Console.WriteLine("".PadLeft(10) + "Current version\n");
            Console.WriteLine("".PadLeft(5) + "-h, --help");
            Console.WriteLine("".PadLeft(10) + "Outputs man page\n");
            Console.WriteLine("".PadLeft(5) + "install");
            Console.WriteLine("".PadLeft(10) + "Installs this application as a service. Appears in Services Manager.\n");
            Console.WriteLine("".PadLeft(5) + "uninstall");
            Console.WriteLine("".PadLeft(10) + "Uninstalls this application as a service. Disappears from Services Manager.\n");
        }
    }
}
