using System;
using System.Collections.Generic;
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

namespace WLInstall.Views
{
    /// <summary>
    /// Interaction logic for SetSAPassword.xaml
    /// </summary>
    public partial class SetSAPassword : Page
    {
        private bool thisPageIsOK = false;

        public SetSAPassword()
        {
            InitializeComponent();

            thisPageIsOK = false;
            ((MainWindow)System.Windows.Application.Current.MainWindow).PwConfirmBtn.IsEnabled = thisPageIsOK;

        }

        private void PasswordBoxPWChanged(object sender, RoutedEventArgs e)
        {
            if((PasswordBox1.Password == PasswordBox2.Password) && (PasswordBox1.Password != ""))
            {
                thisPageIsOK = true;
                Models.NeedOptions.sqlSAPassword = PasswordBox1.Password;
                Console.WriteLine(Models.NeedOptions.sqlSAPassword);
                PasswordCheckLabel.Content = "Password is OK.";
                PasswordCheckLabel.Foreground = Brushes.Green;
                ((MainWindow)System.Windows.Application.Current.MainWindow).PwConfirmBtn.IsEnabled = thisPageIsOK;
            }
            else
            {
                thisPageIsOK = false;
                PasswordCheckLabel.Content = "Password does not match the confirm password. Type both passwords again.";
                PasswordCheckLabel.Foreground = Brushes.Red;
                ((MainWindow)System.Windows.Application.Current.MainWindow).PwConfirmBtn.IsEnabled = thisPageIsOK;
            }
        }
    }
}
