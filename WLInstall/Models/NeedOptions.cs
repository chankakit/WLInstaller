using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WLInstall.Models
{
    public static class NeedOptions
    {
        public const string _10MB_CHUNKS_OF_SQL2012E_SHA1HEX = "fd05a2281aa625a1f110eabcf073add30cc56733";
        public const string _10MB_CHUNKS_OF_SQL2012E_SHA1HEX_32 = "46bb2785f362e4e8ea2bd07746597a1eba093bd6";
        public const string _10MB_CHUNKS_OF_DOTNET40_SHA1HEX = "7bbbbdd8a1c6e3722cf5553ecd80d0530d835c51";     

        public static bool osBit; // True: 64 | False: 32
        public static bool executePolicy = false; // True: Unrestricted | False: Restricted

        public static bool isInstalledSQL2012Express;
        public static bool isInstalledDotNet;

        public static string computerName = Environment.MachineName;

        public static string dotNETFrameworkInstallArguments = " /passive /norestart";
        public static string dotNETFrameworkInstallerLocation;

        public static string sqlSeverInstallArguments = " /ConfigurationFile=" + Directory.GetCurrentDirectory() + "\\MySQLConfiguration.ini";
        public static string sqlSeverInstallerLocation;

        public static string sqlSAPassword;

        public static string sqlInstallCommandText;
        public static string dotNET4InstallCommandText;

        public static void init()
        {
            // 檢查系統是否 64位
            osBit = ViewModels.Wow.Is64BitOperatingSystem;
            Console.WriteLine("The OS is 64bit? - " + Models.NeedOptions.osBit.ToString());
            Console.WriteLine(Environment.MachineName + " - " + Environment.UserName);


            // 檢查是否已安裝.NET
            RegistryKey netKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\");
            if (netKey != null)
            {
                if (netKey.GetValue("Install").ToString() == "1")
                {
                    isInstalledDotNet = true;
                    Console.WriteLine(".NET Framework 4.0(Full) was Installed.");
                }
            }
            else
            {
                isInstalledDotNet = false;
            }
        }
        // 生成SQL安裝命令
        public static void SetSQLInstallCommandText()
        {
            sqlInstallCommandText = sqlSeverInstallerLocation + sqlSeverInstallArguments;
            Console.WriteLine(sqlInstallCommandText);
        }
        // 生成.NET 4.0安裝命令
        public static void SetDotNET4InstallCommandText()
        {
            dotNET4InstallCommandText = dotNETFrameworkInstallerLocation + dotNETFrameworkInstallArguments;
            Console.WriteLine(dotNET4InstallCommandText);
        }
    }
}
