using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace IISConfiguration
{
    public class AddPermissions
    {

        SecurityIdentifier IIS_IUSRS = new SecurityIdentifier("S-1-5-32-568");
        SecurityIdentifier IUSRS = new SecurityIdentifier("S-1-5-17");

        public int AddIISAccountPermission(string webLinkWebAppFolderPath)
        {
            try
            {
                Console.WriteLine("Adding access control entry for\\ " + webLinkWebAppFolderPath);
                // Add the access control entry to the directory.

                AddDirectorySecurity(webLinkWebAppFolderPath, IUSRS, FileSystemRights.FullControl);
                AddDirectorySecurity(webLinkWebAppFolderPath, IIS_IUSRS, FileSystemRights.FullControl);

                return 0;
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR in AddPermissions.cs");
                Console.WriteLine(e);

                return -1;
            }  
        }

        private void AddDirectorySecurity(string Path, SecurityIdentifier Account, FileSystemRights Rights)
        {

            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(Path);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                                                            PropagationFlags.None,
                                                            AccessControlType.Allow));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }
    }
}
