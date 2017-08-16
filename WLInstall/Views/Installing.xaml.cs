using System.Windows;
using System.Windows.Controls;
using WLInstall.Models;
using WLInstall.Commands;
using WLInstall.ViewModels;
using System.IO;

namespace WLInstall.Views
{
    /// <summary>
    /// Interaction logic for Installing.xaml
    /// </summary>
    public partial class Installing : Page
    {
        int progressStep = 1;

        public Installing()
        {
            InitializeComponent();
        }

        private void InstallDotNET4(object sender, RoutedEventArgs e)
        {
            progressStep = 1;
            StepLabel.Content = "Ruuning Step: " + progressStep;

            RunExternalProgram.Run(NeedOptions.dotNETFrameworkInstallerLocation, NeedOptions.dotNETFrameworkInstallArguments);
        }

        private void InstallSQL64(object sender, RoutedEventArgs e)
        {
            progressStep = 2;
            StepLabel.Content = "Ruuning Step: " + progressStep;

            RunExternalProgram.Run(NeedOptions.sqlSeverInstallerLocation, NeedOptions.sqlSeverInstallArguments);
        }

        private void EnableTCP(object sender, RoutedEventArgs e)
        {
            progressStep = 3;
            StepLabel.Content = "Ruuning Step: " + progressStep;

            // Set TCP Enabled.
            SQLConfiguration.ManageTCP.SetTCPIsEnable(true);
            // Set Port1433.
            SQLConfiguration.ManageTCP.SetTCPIPAddressProperties();
            // Restart.
            SQLConfiguration.ManageService.RestartMSSQLSERVER();
        }

        private void InstallIIS(object sender, RoutedEventArgs e)
        {
            progressStep = 4;
            StepLabel.Content = "Ruuning Step: " + progressStep;

            IISFeatures iis = new IISFeatures();
            RunExternalProgram.Run(iis.SETUP_IIS_COMMAND, iis.arguments);
        }

        private void SetupWeblink(object sender, RoutedEventArgs e)
        {
            progressStep = 5;
            StepLabel.Content = "Ruuning Step: " + progressStep;

            RunExternalProgram.Run(@"WebLink V1.2.5.exe", null);
        }

        private void AspNetRegIIS(object sender, RoutedEventArgs e)
        {
            progressStep = 6;
            StepLabel.Content = "Ruuning Step: " + progressStep;

            if (Models.NeedOptions.osBit) 
            {
                // 64
                RunExternalProgram.Run(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe", "/i");
            }
            else
            {
                // 32
                RunExternalProgram.Run(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\aspnet_regiis.exe", "/i");
            }
                
        }

        private void ConfigIISWeb(object sender, RoutedEventArgs e)
        {
            progressStep = 7;
            StepLabel.Content = "Ruuning Step: " + progressStep;

            // 此處需要傳入weblink的website app絕對路徑
            string webLinkWebAppFolderPath = @"C:\Program Files (x86)\weblink\WEBAPP";

            IISConfiguration.AddPermissions addPermissions = new IISConfiguration.AddPermissions();
            addPermissions.AddIISAccountPermission(webLinkWebAppFolderPath);

            IISConfiguration.SetAppPool setAppPool = new IISConfiguration.SetAppPool();
            setAppPool.SetApplicationPool("WEBLINK");
        }
    }
}
