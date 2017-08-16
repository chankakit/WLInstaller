using System.Text;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;

namespace WLInstall.Commands
{
    public static class ExecutePS
    {
        public static string ExcuteScript(string scriptText)
        {
            // 創建 PowerShell runspace
            Runspace rs = RunspaceFactory.CreateRunspace();

            rs.Open();

            // 創建 pipline 并將腳本傳遞之
            Pipeline pl = rs.CreatePipeline();
            pl.Commands.AddScript(scriptText);

            // 把Powershell的輸出轉換成字符串
            pl.Commands.Add("Out-String");

            // 執行腳本
            Collection<PSObject> results = pl.Invoke();

            rs.Close();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}
