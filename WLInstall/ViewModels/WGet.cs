using System;
using System.Net;

namespace WLInstall.ViewModels
{
    public static class WGet
    {
        public static string Web_Get(string scriptURL)
        {
            string scriptText = "";
            try
            {
                using (WebClient client = new WebClient())
                {
                    scriptText = client.DownloadString(scriptURL);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }

            return scriptText;
        }
    }
}
