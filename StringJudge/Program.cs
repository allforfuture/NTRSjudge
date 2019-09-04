using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StringJudge
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
        }
    }
}