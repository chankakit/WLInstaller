using System;
using System.Diagnostics;

namespace WLInstall.Commands
{
    public static class RunExternalProgram
    {
        public static int Run(string fileName, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.FileName = fileName;
            startInfo.CreateNoWindow = false;
            if(arguments!=null)
            {
                startInfo.Arguments = arguments;
            }
            
            startInfo.Verb = "runas";

            try
            {
                Process runProcess = Process.Start(startInfo);
                
                runProcess.EnableRaisingEvents = true;
                runProcess.Exited += new EventHandler(ProcessExited);
                Console.WriteLine("Running " + fileName + arguments);
                    
                return 0;
            }
            catch (Exception e)
            {
                Console.Write("The following exception was raised: ");
                Console.WriteLine(e.Message);

                return -1;
            }
        }

        static void ProcessExited(object sender, EventArgs e)
        {
            Process p = (Process)sender;
            int exitCode = p.ExitCode;
            
            Console.WriteLine(exitCode);
        }
    }
}
