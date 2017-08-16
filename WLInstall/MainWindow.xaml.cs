using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WLInstall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///     /// 以下是一個死美工在網上各處抄到的代碼集合體
    /// 不要跟我說什麼面向過程面向對象MVCMVVM數據結構編譯原理亂七八糟的
    /// 打開 Google 一個字就是
    /// 「幹(chao)！」
    /// 
    public partial class MainWindow : Window
    {
        public static int pageCount = 0;
        
        public MainWindow()
        {
            InitializeComponent();
            Models.NeedOptions.init();
                      
            WhichPage(pageCount, 0);
            this.DataContext = new ViewModels.ButtonIsEnable();

        }
        

        void WhichPage(int p, int action)
        {
            switch (p)
            {
                case 0:
                    Main.Content = new Views.Welcome();
                    PreBtn.IsEnabled = false;
                    break;
                case 1:
                    PreBtn.IsEnabled = true;
                    //if(false)
                    if (Models.NeedOptions.isInstalledDotNet)
                    {
                        pageCount += action;
                        WhichPage(pageCount, action);
                        break;
                    }
                    else
                    {
                        Main.Content = new Views.LocateDotNetFrameworkInstaller();
                        break;
                    }                                                    
                case 2:
                    Main.Content = new Views.LocateSQLInstaller();
                    NxtBtn.Visibility = Visibility.Visible;
                    PwConfirmBtn.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    Main.Content = new Views.SetSAPassword();
                    NxtBtn.Visibility = Visibility.Hidden;
                    InstallBtn.Visibility = Visibility.Hidden;
                    PwConfirmBtn.Visibility = Visibility.Visible;
                    break;
                case 4:
                    Main.Content = new Views.ReadyToInstall();
                    PwConfirmBtn.Visibility = Visibility.Hidden;
                    NxtBtn.Visibility = Visibility.Hidden;
                    InstallBtn.Visibility = Visibility.Visible;
                    break;
                case 5:
                    Main.Content = new Views.Installing();
                    PreBtn.Visibility = Visibility.Hidden;
                    InstallBtn.Visibility = Visibility.Hidden;
                    break;
            }
        }


        private void NextPage(object sender, RoutedEventArgs e)
        {
            int action = 1;
            pageCount += action;
            WhichPage(pageCount, action);
        }

        private void PreviousPage(object sender, RoutedEventArgs e)
        {
            int action = -1;
            pageCount += action;
            WhichPage(pageCount, action);
        }


        private void ConfirmInfo(object sender, RoutedEventArgs e)
        {
            // 生成 .NET 4.0 安裝命令
            if(Models.NeedOptions.dotNETFrameworkInstallerLocation != null)
            {
                Models.NeedOptions.SetDotNET4InstallCommandText();
            }
            
            // 生成 SQL Server 2012 Express 安裝配置文件
            ViewModels.GenerateSQLConfigurationFile genFile = new ViewModels.GenerateSQLConfigurationFile();
            genFile.GenerateFile(Models.NeedOptions.sqlSAPassword);
            // 生成 SQL Server 2012 Express 安裝命令
            Models.NeedOptions.SetSQLInstallCommandText();

            int action = 1;
            // 翻頁
            pageCount += action;
            WhichPage(pageCount, action);
        }


        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            CloseProgram cpWindow = new CloseProgram();
            cpWindow.ShowDialog();
        }

        private void Install(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("FUCKIT");

            int action = 1;
            // 翻頁
            pageCount += action;
            WhichPage(pageCount, action);
        }
    }
}
