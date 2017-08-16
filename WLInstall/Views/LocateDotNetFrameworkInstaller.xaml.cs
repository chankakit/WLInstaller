using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WLInstall.Views
{
    /// <summary>
    /// Interaction logic for LocateDotNetFrameworkInstaller.xaml
    /// </summary>
    public partial class LocateDotNetFrameworkInstaller : Page
    {
        private bool thisPageIsOK = false;

        public LocateDotNetFrameworkInstaller()
        {
            InitializeComponent();
            if (Models.NeedOptions.sqlSeverInstallerLocation != null)
            {
                DotNETInstallerPathTextbox.Text = Models.NeedOptions.dotNETFrameworkInstallerLocation;
            }
            else
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).NxtBtn.IsEnabled = thisPageIsOK;
            }           
        }

        private void DotNETInstallerDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                DotNETInstallerPathTextbox.Text = files[0];
            }
        }

        private void DotNETInstallerPDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        // 點擊 browser 按鈕
        private void DotNETBrowserClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dotNETInstallerDialog = new OpenFileDialog();
            dotNETInstallerDialog.Filter = "Excuteable Files|*.exe";
            dotNETInstallerDialog.Title = "Select .NET Framework 4.0(Full) Installer";
            string filePath = null;
            dotNETInstallerDialog.RestoreDirectory = true;

            if (dotNETInstallerDialog.ShowDialog() == true)
            {
                filePath = dotNETInstallerDialog.FileName;
                DotNETInstallerPathTextbox.Text = filePath;
            }
        }

        // 當 Textbox 的內容變化時，把地址賦值給 Models.NeedOptions.dotNETFrameworkInstallerLocation
        private void DotNETInstallerPathTextboxTextChanged(object sender, TextChangedEventArgs e)
        {           
            if (ViewModels.SHA1Calculator.CalSHAbyChunk(DotNETInstallerPathTextbox.Text) == Models.NeedOptions._10MB_CHUNKS_OF_DOTNET40_SHA1HEX)
            {
                thisPageIsOK = true;
                DotNETVersionLabel.Content = ".NET Framework 4.0 Full";
                Models.NeedOptions.dotNETFrameworkInstallerLocation = DotNETInstallerPathTextbox.Text;
                Console.WriteLine(Models.NeedOptions.dotNETFrameworkInstallerLocation);
                ((MainWindow)System.Windows.Application.Current.MainWindow).NxtBtn.IsEnabled = thisPageIsOK;
            }
            else
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).NxtBtn.IsEnabled = false;
                DotNETVersionLabel.Content = "NOT A .NET Framework 4.0 Full INSTALLER FILE.";
            }           
        }

        private void HyperlinkClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(@"https://download.microsoft.com/download/9/5/A/95A9616B-7A37-4AF6-BC36-D6EA96C8DAAE/dotNetFx40_Full_x86_x64.exe"));
        }

        private void HyperlinkClick1(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(@"https://www.microsoft.com/en-us/download/details.aspx?id=17718"));
        }
    }
}
