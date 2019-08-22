using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Update
{
    class Update
    {
        public static bool IsUpdate()
        {
            try
            {
                #region json序列化
                string sPath = AppDomain.CurrentDomain.BaseDirectory + @"Update\Update.json";
                StreamReader file = File.OpenText(sPath);
                JsonTextReader reader = new JsonTextReader(file);
                JObject J = (JObject)JToken.ReadFrom(reader);
                JsonRoot root = JsonConvert.DeserializeObject<JsonRoot>(J.ToString());
                #endregion

                if (root.modeState == 0)
                    return false;

                #region 服务器访问权限
                string str = root.server.Credential;
                string[] credential = str.Split(';');
                string pathStr = credential[0];
                string userStr = credential[1];
                string pwdStr = credential[2];
                GetAccess(pathStr, userStr, pwdStr);
                #endregion                

                #region 对比版本是否打开升级程序
                FileVersionInfo infoServer = FileVersionInfo.GetVersionInfo(root.server.Program);
                string local_program = Process.GetCurrentProcess().MainModule.FileName;
                FileVersionInfo infoLocal = FileVersionInfo.GetVersionInfo(local_program);

                Version versionServer = new Version(infoServer.FileVersion);//服务器版本
                Version versionLocal = new Version(infoLocal.FileVersion);//当前版本
                //Version versionLocal = new Version(Application.ProductVersion);//自身当前版本
                if (infoServer.ProductName == infoLocal.ProductName && versionServer > versionLocal)
                {
                    if (MessageBox.Show("Do you want to upgrade the program?", "Update"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        return true;
                        //Process.Start(@"Update\Update.exe", local_program);
                        //Environment.Exit(0);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Upgrade failure", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            return false;
        }
        static void GetAccess(string path, string user, string pwd)
        {
            Process p = new Process();

            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;

            p.Start();
            //DOS语句net use \\DN2PROD12 "19931001" /user:SAEDGO1\R086706
            p.StandardInput.WriteLine(@"net use {0} /del", path); //必须先删除，否则报错
            p.StandardInput.WriteLine(@"net use {0} ""{1}"" /user:{2}", path, pwd, user);
            p.StandardInput.WriteLine("exit"); //如果不加这句WaitForExit会卡住
            p.WaitForExit();
            p.Close();
        }
    }



    class JsonRoot
    {
        public int modeState { get; set; }
        public Server server { get; set; }
    }
    class Server
    {
        public string Credential { get; set; }
        public string Program { get; set; }
    }
}
