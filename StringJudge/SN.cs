using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Windows.Forms;

namespace StringJudge
{
    /// <summary>
    /// 全部SN清单
    /// </summary>
    class AllInfo
    {
        internal static List<SNinfo> SNlist = new List<SNinfo>();
        public class SNinfo
        {
            internal string SN { get; set; }
            internal string result { get; set; }
            internal string detail { get; set; }
        }
    }

    

    /// <summary>
    /// 单个SN判断过程
    /// </summary>
    class Judge
    {
        static int startInt = Convert.ToInt16(ConfigurationManager.AppSettings["startInt"]);
        static int endInt = Convert.ToInt16(ConfigurationManager.AppSettings["endInt"]);
        static string[] strArr = ConfigurationManager.AppSettings["SNstr"].Split('|');
        public static void judge(string SN)
        {
            AllInfo.SNinfo info = new AllInfo.SNinfo();
            //info.SN = SN=SN.ToUpper();
            info.SN = SN;
            
            #region SN是否重复
            foreach (AllInfo.SNinfo var in AllInfo.SNlist)
            {
                if (SN == var.SN&&SN!="ERROR")
                {
                    info.result = "MISS";
                    info.detail = "Duplicate SN";

                    AllInfo.SNlist.Add(info);
                    return;
                }
            }
            #endregion

            if (SN == "ERROR")
            {
                info.result = "MISS";
                info.detail = "ERROR";
            }
            else if (SN.Length < 24)
            {
                info.result = "MISS";
                info.detail = "SN format false";
            }
            else
            {
                string temp = SN.Substring(startInt, endInt - startInt);
                info.result = strArr.Contains(temp) ? "OK" : "NG";
                info.detail = "";
            }
            AllInfo.SNlist.Add(info);
        }
    }
}