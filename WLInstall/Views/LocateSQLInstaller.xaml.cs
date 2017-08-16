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
    /// Interaction logic for LocateSQLInstaller.xaml
    /// </summary>
    public partial class LocateSQLInstaller : Page
    {
        private bool thisPageIsOK = false;

        public LocateSQLInstaller()
        {
            InitializeComponent();

            if(Models.NeedOptions.sqlSeverInstallerLocation != null)
            {
                SqlInstallerPathTextbox.Text = Models.NeedOptions.sqlSeverInstallerLocation;
            }
            else
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).NxtBtn.IsEnabled = thisPageIsOK;
            }          
        }

        private void SQLInstallerPDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void SQLInstallerDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                SqlInstallerPathTextbox.Text = files[0];
            }
        }

        // 點擊 browser 按鈕
        private void SQLBrowserClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog sqlInstallerDialog = new OpenFileDialog();
            sqlInstallerDialog.Filter = "Excuteable Files|*.exe";
            sqlInstallerDialog.Title = "Select SQL Server 2012 Express Installer";
            string filePath = null;
            sqlInstallerDialog.RestoreDirectory = true;

            if (sqlInstallerDialog.ShowDialog() == true)
            {
                filePath = sqlInstallerDialog.FileName;
                SqlInstallerPathTextbox.Text = filePath;
            }
        }

        // 當 Textbox 的內容變化時, 更新安裝器地址
        private void SqlInstallerPathTextboxTextChanged(object sender, TextChangedEventArgs e)
        {            
            if(Models.NeedOptions.osBit)
            {
                Console.WriteLine("It's 64bit OS.");
                Console.WriteLine("File sha1: " + ViewModels.SHA1Calculator.CalSHAbyChunk(SqlInstallerPathTextbox.Text));
                if (ViewModels.SHA1Calculator.CalSHAbyChunk(SqlInstallerPathTextbox.Text) == Models.NeedOptions._10MB_CHUNKS_OF_SQL2012E_SHA1HEX)
                {
                    thisPageIsOK = true;
                    SQLVersionLabel.Content = "SQL Server 2012(SP2) Express 64bit";
                    Models.NeedOptions.sqlSeverInstallerLocation = SqlInstallerPathTextbox.Text;
                    Console.WriteLine(Models.NeedOptions.sqlSeverInstallerLocation);
                    ((MainWindow)System.Windows.Application.Current.MainWindow).NxtBtn.IsEnabled = thisPageIsOK;
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).NxtBtn.IsEnabled = false;
                    SQLVersionLabel.Content = "NOT A SQL Server 2012(SP2) Express 64bit INSTALLER FILE.";
                }
            }
            else
            {
                Console.WriteLine("It's 32bit OS.");
                Console.WriteLine("File sha1: " + ViewModels.SHA1Calculator.CalSHAbyChunk(SqlInstallerPathTextbox.Text));
                if (ViewModels.SHA1Calculator.CalSHAbyChunk(SqlInstallerPathTextbox.Text) == Models.NeedOptions._10MB_CHUNKS_OF_SQL2012E_SHA1HEX_32)
                {
                    thisPageIsOK = true;
                    SQLVersionLabel.Content = "SQL Server 2012(SP2) Express 32bit";
                    Models.NeedOptions.sqlSeverInstallerLocation = SqlInstallerPathTextbox.Text;
                    Console.WriteLine(Models.NeedOptions.sqlSeverInstallerLocation);
                    ((MainWindow)System.Windows.Application.Current.MainWindow).NxtBtn.IsEnabled = thisPageIsOK;
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).NxtBtn.IsEnabled = false;
                    SQLVersionLabel.Content = "NOT A SQL Server 2012(SP2) Express 32bit INSTALLER FILE.";
                }
            }
            
        }




        private void HyperlinkClick(object sender, RoutedEventArgs e)
        {
            string uri;

            if (Models.NeedOptions.osBit)
            {
                string SQLEXPRADV_x64_ENU_URI = @"https://download.microsoft.com/download/0/1/E/01E0D693-2B4F-4442-9713-27A796B327BD/SQLEXPRADV_x64_ENU.exe";
                uri = SQLEXPRADV_x64_ENU_URI;
            }
            else
            {
                string SQLEXPRADV_X86_URI = @"https://download.microsoft.com/download/0/1/E/01E0D693-2B4F-4442-9713-27A796B327BD/SQLEXPRADV_x86_ENU.exe";
                uri = SQLEXPRADV_X86_URI;
            }

            Process.Start(new ProcessStartInfo(uri));
        }

        private void HyperlinkClick1(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(@"https://www.microsoft.com/en-us/download/details.aspx?id=43351"));
        }
    }
}
