using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTRSjudge
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //try
            //{
            //    bool isUpDATE = Update.Update.IsUpdate();
            //    if (isUpDATE)
            //    {
            //        //命令行参数的" "是向数组添加元素
            //        //例如：传输"a b"，会接收到的数组{"a","b"}
            //        //所以要传" "的话，先转换成另外的字符串，然后在接收段解析
            //        string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Replace(" ", "|_|");
            //        System.Diagnostics.Process.Start(@"Update\Update.exe", exePath);
            //        Environment.Exit(0);
            //    }
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message, "Check for updates", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            Application.Run(new Main());
            

            #region 当前用户是管理员的时候，直接启动应用程序。如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
            ////获得当前登录的Windows用户标示
            //System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            //System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            ////判断当前登录用户是否为管理员
            //if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            //{
            //    //如果是管理员，则直接运行
            //    Application.Run(new Main());
            //}
            //else
            //{
            //    //创建启动对象
            //    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //    startInfo.UseShellExecute = true;
            //    startInfo.WorkingDirectory = Environment.CurrentDirectory;
            //    startInfo.FileName = Application.ExecutablePath;
            //    //设置启动动作,确保以管理员身份运行
            //    startInfo.Verb = "runas";
            //    try
            //    {
            //        System.Diagnostics.Process.Start(startInfo);
            //    }
            //    catch
            //    {
            //        return;
            //    }
            //    //退出
            //    //Application.Exit();
            //    //彻底退出
            //    Environment.Exit(0);
            //}
            #endregion
        }
    }
}