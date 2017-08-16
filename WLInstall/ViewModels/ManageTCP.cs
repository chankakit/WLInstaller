using System;
using Microsoft.SqlServer.Management.Smo.Wmi;
using Microsoft.SqlServer.Management.Smo;

namespace SQLConfiguration
{
    public static class ManageTCP
    {

        // Get the TCP/IP is enabled? value
        // [false: Disabled]
        // [true: Enabled]
        public static bool GetTCPIsEnabled()
        {
            ManagedComputer mc = new ManagedComputer();
            return mc.ServerInstances[0].ServerProtocols[2].IsEnabled;
        }

        // Set TCP/IP to
        // [Disabled: false] 
        // [Enabled: true]
        public static int SetTCPIsEnable(bool value)
        {
            try
            {
                //Declare and create an instance of the ManagedComputer   
                //object that represents the WMI Provider services.  
                ManagedComputer mc = new ManagedComputer();
                ServerProtocol tcpSp = mc.ServerInstances[0].ServerProtocols[2];

                Console.WriteLine(tcpSp.DisplayName + ": " + tcpSp.IsEnabled);

                // Ser tcp to enable
                tcpSp.IsEnabled = value;

                // Upadte Value
                tcpSp.Alter();
                tcpSp.Refresh();

                Console.WriteLine("Set " + tcpSp.DisplayName + ": " + tcpSp.IsEnabled);

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR in ManageTCP.cs! - Causeby SetTCPIsEnable()");
                Console.WriteLine(e);

                return -1;
            }
            
        }

        // ManagedComputer.ServerInstances[i].ServerProtocols[2].IPAddresses[i].IPAddressProperties[]
        // Set All the IP Addresses property to 
        // [Enabled: Yes]
        // [TCP Dynamic Ports: 0]
        // [TCP Port: 1433]
        //
        // IPAll proper to
        // [TCP Dynamic Ports: 0]
        // [TCP Port: 1433]
        public static int SetTCPIPAddressProperties()
        {
            try
            {
                ManagedComputer mc = new ManagedComputer();
                ServerProtocol tcpSp = mc.ServerInstances[0].ServerProtocols[2];

                // Set all IP Address exclude IPAll
                for (int i = 0; i < tcpSp.IPAddresses.Count - 1; i++)
                {
                    tcpSp.IPAddresses[i].IPAddressProperties[1].Value = true;
                    tcpSp.IPAddresses[i].IPAddressProperties[3].Value = "0";
                    tcpSp.IPAddresses[i].IPAddressProperties[4].Value = "1433";
                }

                // Set IP All
                tcpSp.IPAddresses[tcpSp.IPAddresses.Count - 1].IPAddressProperties[0].Value = "0";
                tcpSp.IPAddresses[tcpSp.IPAddresses.Count - 1].IPAddressProperties[1].Value = "1433";

                // Update 
                tcpSp.Alter();
                tcpSp.Refresh();

                return 0;
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR in ManageTCP.cs! - Causeby SetTCPIPAddressProperties()");
                Console.WriteLine(e);

                return -1;
            }
        }
    }
}
