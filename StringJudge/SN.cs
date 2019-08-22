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
            internal DateTime checkTime { get; set; }
            internal string checkItem { get; set; }
            internal string checkTotal { get; set; }
            internal DateTime firstTime { get; set; }
            
        }
    }

    

    /// <summary>
    /// 单个SN判断过程
    /// </summary>
    class Judge
    {
        static int startInt = Convert.ToInt16(ConfigurationManager.AppSettings["startInt"]);
        static int endInt = Convert.ToInt16(ConfigurationManager.AppSettings["endInt"]);
        static string SNstr = ConfigurationManager.AppSettings["SNstr"];

        public static void judge(string SN)
        {
            AllInfo.SNinfo info = new AllInfo.SNinfo();
            //info.SN = SN=SN.ToUpper();
            info.SN = SN;
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
            //else if (SN.Length < 17)
            else if (SN.Length < 24)
            {
                info.result = "MISS";
                info.detail = "SN format false";
            }
            else
            {

                //info.result = API.SN_Judge_OK2SHIP(SN, ref temp);
                string temp = SN.Substring(startInt, endInt - startInt);
                info.result = temp == SNstr ? "OK" : "NG";
                info.detail = "";
                //API2的结果不是"PASS",是"OK"
                info.checkTotal = info.checkItem = info.result == "PASS"|| info.result == "OK" ? "0" : "1";
            }
            AllInfo.SNlist.Add(info);
        }
    }
}