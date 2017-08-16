using System;
using System.Collections.Generic;
using System.IO;

namespace WLInstall.ViewModels
{
    public class GenerateSQLConfigurationFile
    {
        Dictionary<string, string> configurations = new Dictionary<string, string>();

        private void InitSomeConfigurations()
        {
            string X86_value = "\"" + (!Models.NeedOptions.osBit).ToString() + "\"";
            string SQLSYSADMINACCOUNTS_value = "\"" + Environment.MachineName + "\\" + Environment.UserName + "\"";

            configurations.Add("X86=", X86_value);
            configurations.Add("SQLSYSADMINACCOUNTS=", SQLSYSADMINACCOUNTS_value);
            configurations.Add("INSTALLSHAREDDIR=", "\"C:\\Program Files\\Microsoft SQL Server\"");
            configurations.Add("INSTALLSHAREDWOWDIR=", "\"C:\\Program Files (x86)\\Microsoft SQL Server\"");
            configurations.Add("INSTANCEDIR=", "\"C:\\Program Files\\Microsoft SQL Server\"");
            configurations.Add("SAPWD=", "");
            configurations.Add("AGTSVCSTARTUPTYPE=", "\"Disabled\"");
        }

        private void SetPassword(string userPassword)
        {
            configurations["SAPWD="] = "\"" + userPassword + "\"";
            Console.WriteLine(configurations["SAPWD="] + "  - Password was set.");
        }

        public void GenerateFile(string userPassword)
        {
            string fuckingOptions = @"[OPTIONS]
ACTION=""Install""
ROLE=""AllFeatures_WithDefaults"" 
ENU=""True""
QUIET=""False""
QUIETSIMPLE=""True""
UpdateEnabled=""False""
FEATURES=SQLENGINE,REPLICATION,FULLTEXT,RS,BIDS,CONN,BC,SDK,BOL,SSMS,ADV_SSMS,SNAC_SDK,LOCALDB
UpdateSource=""MU""
HELP=""False""
INDICATEPROGRESS=""True""
INSTANCENAME=""MSSQLSERVER""
INSTANCEID=""MSSQLSERVER""
SQMREPORTING=""False""
RSINSTALLMODE=""DefaultNativeMode""
ERRORREPORTING=""False""
AGTSVCACCOUNT=""NT AUTHORITY\NETWORK SERVICE""
COMMFABRICPORT=""0""
COMMFABRICNETWORKLEVEL=""0""
COMMFABRICENCRYPTION=""0""
MATRIXCMBRICKCOMMPORT=""0""
SQLSVCSTARTUPTYPE=""Automatic""
FILESTREAMLEVEL=""0""
ENABLERANU=""True""
SQLCOLLATION=""SQL_Latin1_General_CP1_CI_AS""
SQLSVCACCOUNT=""NT Service\MSSQLSERVER""
SECURITYMODE=""SQL""
ADDCURRENTUSERASSQLADMIN=""True""
TCPENABLED=""0""
NPENABLED=""0""
BROWSERSVCSTARTUPTYPE=""Disabled""
RSSVCACCOUNT=""NT Service\ReportServer""
RSSVCSTARTUPTYPE=""Automatic""
FTSVCACCOUNT=""NT Service\MSSQLFDLauncher""
IACCEPTSQLSERVERLICENSETERMS=""True""
";

            InitSomeConfigurations();
            SetPassword(userPassword);
            File.WriteAllText(@"MySQLConfiguration.ini", fuckingOptions);
            foreach (KeyValuePair<string, string> configuration in configurations)
            {
                File.AppendAllText(@"MySQLConfiguration.ini", configuration.Key + configuration.Value + Environment.NewLine);
            }
        }
    }
}
