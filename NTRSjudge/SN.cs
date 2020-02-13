using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Windows.Forms;

namespace NTRSjudge
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
            internal bool lineOK { get; set; }
            internal string line { get; set; }
            internal string result { get; set; }
            internal string detail { get; set; }
            internal DateTime checkTime { get; set; }
            internal string checkItem { get; set; }
            internal string checkTotal { get; set; }
            internal DateTime firstTime { get; set; }            
        }
    }

    /// <summary>
    /// PPP和EEEE的检查(配置文件)
    /// </summary>
    class Config
    {
        static List<string> PPP = new List<string>(ConfigurationManager.AppSettings["checkPPP"].Split(','));
        static List<string> EEEE = new List<string>(ConfigurationManager.AppSettings["checkEEEE"].Split(','));
        protected static bool checkPPP(string SN)
        {
            SN = SN.Substring(0, 3);
            foreach (string PPPstr in PPP)
            {
                if (SN == PPPstr)
                {
                    return true;
                }
            }
            return false;
        }

        protected static bool checkEEEE(string SN)
        {
            if (EEEE[0] == "N/A")
                return true;
            SN =SN.Substring(11, 4);
            foreach (string EEEEstr in EEEE)
            {
                if (SN == EEEEstr)
                {
                    return true;
                }
            }
            return false;
        }
    }
    

    /// <summary>
    /// 单个SN判断过程
    /// </summary>
    class Judge:Config 
    {
        public static void judge(string SN)
        {
            AllInfo.SNinfo info = new AllInfo.SNinfo();
            info.SN = SN=SN.ToUpper();
            string line = "L??";
            if (Layout.lineSwitch&& SN!="ERROR")
                info.lineOK = Check.Check.checkLine(SN, ref line);
            info.line = line;



            #region log记录的一盘里的第一个时间
            if (AllInfo.SNlist.Count == 0)
            { info.firstTime = DateTime.Now; }
            else
            { info.firstTime = AllInfo.SNlist[0].firstTime; }
            info.checkTime = DateTime.Now;
            #endregion

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
            else if (SN.Length < 17)
            {
                info.result = "MISS";
                info.detail = "SN format false";
            }
            else if (!Config.checkPPP(SN) || !Config.checkEEEE(SN))
            {
                if (!Config.checkPPP(SN))
                {
                    info.result = "MISS";
                    info.detail = "Miss PPP";
                }
                if (!Config.checkEEEE(SN))
                {
                    info.result = "MISS";
                    info.detail += "\r\nMiss EEEE";
                }
            }
            else
            {
                string temp = string.Empty;
                info.result = API.SN_Judge(SN, ref temp);
                info.detail = temp;
                info.checkTotal = info.checkItem = info.result == "PASS" ? "0" : "1";
            }
            AllInfo.SNlist.Add(info);
        }
    }
}