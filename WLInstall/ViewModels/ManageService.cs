using System;
using Microsoft.SqlServer.Management.Smo.Wmi;
using Microsoft.SqlServer.Management.Smo;

namespace SQLConfiguration
{
    public static class ManageService
    {
        // Set MSSQLSERVER Service to
        // [Start: true]
        // [Stop: false]
        public static int SetMSSQLSERVER(bool value)
        {
            try
            {
                ManagedComputer mc = new ManagedComputer();
                Service Mysvc = mc.Services["MSSQLSERVER"];

                if (value)
                {
                    Mysvc.Start();
                    while (Mysvc.ServiceState != ServiceState.Running)
                    {
                        Console.WriteLine(string.Format("{0} service state is {1}", Mysvc.Name, Mysvc.ServiceState));
                        Mysvc.Refresh();
                    }
                    Console.WriteLine(string.Format("{0} service state is {1}", Mysvc.Name, Mysvc.ServiceState));

                    return 0;
                }
                else
                {
                    Mysvc.Stop();
                    while (Mysvc.ServiceState != ServiceState.Stopped)
                    {
                        Console.WriteLine(string.Format("{0} service state is {1}", Mysvc.Name, Mysvc.ServiceState));
                        Mysvc.Refresh();
                    }
                    Console.WriteLine(string.Format("{0} service state is {1}", Mysvc.Name, Mysvc.ServiceState));

                    return 0;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR in ManageService.cs. - Causeby SetMSSQLSERVER()");
                Console.WriteLine(e);

                return -1;
            }
            
        }

        // Restart MSSQLSERVER Service
        public static int RestartMSSQLSERVER()
        {
            try
            {
                ManagedComputer mc = new ManagedComputer();
                Service Mysvc = mc.Services["MSSQLSERVER"];

                Console.WriteLine(string.Format("{0} service state is {1}", Mysvc.Name, Mysvc.ServiceState));

                if (Mysvc.ServiceState == ServiceState.Running)
                {
                    Mysvc.Stop();
                    while (Mysvc.ServiceState != ServiceState.Stopped)
                    {
                        Console.WriteLine(string.Format("{0} service state is {1}", Mysvc.Name, Mysvc.ServiceState));
                        Mysvc.Refresh();
                    }
                }
                
                Console.WriteLine(string.Format("{0} service state is {1}", Mysvc.Name, Mysvc.ServiceState));

                if (Mysvc.ServiceState == ServiceState.Stopped)
                {
                    Mysvc.Start();
                    while (Mysvc.ServiceState != ServiceState.Running)
                    {
                        Console.WriteLine(string.Format("{0} service state is {1}", Mysvc.Name, Mysvc.ServiceState));
                        Mysvc.Refresh();
                    }
                    Console.WriteLine(string.Format("{0} service state is {1}", Mysvc.Name, Mysvc.ServiceState));
                }

                //Console.WriteLine("MSSQLSERVER is restarted.");

                return 0;
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR in ManageService.cs. - Causeby RestartMSSQLSERVER()");
                Console.WriteLine(e);

                return -1;
            }
            
        }
    }
}
