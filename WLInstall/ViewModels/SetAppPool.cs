using System;
using Microsoft.Web.Administration;

namespace IISConfiguration
{
    public class SetAppPool
    {
        // siteName: "WEBLINK"
        public int SetApplicationPool(string siteName)
        {
            try
            {
                ServerManager serverManager = new ServerManager();

                ApplicationPool appPool = serverManager.ApplicationPools[0];
                if (serverManager.ApplicationPools["ASP.NET v4.0"] != null)
                {
                    appPool = serverManager.ApplicationPools["ASP.NET v4.0"];
                }

                if (serverManager.Sites[siteName] != null)
                {
                    Site site = serverManager.Sites[siteName];

                    site.Stop();

                    site.Applications[0].ApplicationPoolName = appPool.Name;
                    Console.WriteLine(site.Applications[0].ApplicationPoolName);

                    serverManager.CommitChanges();

                    site.Start();
                    appPool.Recycle();
                }
                else if(serverManager.Sites[siteName] == null)
                {
                    appPool.Recycle();
                    Console.WriteLine("NO WEBLINK SITE!");

                    return 1;
                }
                
                return 0;
            }
            catch(Exception e)
            {
                Console.WriteLine("BUG on SetAppPool.cs");
                Console.WriteLine(e);

                return -1;
            }
            
        }
        
    }
}
