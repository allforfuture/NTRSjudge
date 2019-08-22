using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Configuration;

namespace OK2Ship
{
    class Document
    {

        public static List<string> pathList = new List<string>()
        {
            AppDomain.CurrentDomain.BaseDirectory + "log\\",
            AppDomain.CurrentDomain.BaseDirectory + "pqm\\",
            AppDomain.CurrentDomain.BaseDirectory + "sum\\"
        };

        //@YYX不能被子类继承
        public static void CreateDocument()
        {
            foreach (string path in pathList)
            {
                Directory.CreateDirectory(path);
            }
        }
    }

    class Log : Document
    {
        public static void WriteLog(string SN,string detail,string judge)
        {
            string path = Document.pathList[0] + "log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            using (StreamWriter file = new StreamWriter(path, true))//(@"C:\Users\YEYINXU\Desktop\log" + DateTime.Now.ToString("yyyyMMdd") + ".txt", true))
            {
                string str = DateTime.Now.ToString("yyyy/MM/dd,HH:mm:ss,")
                    + SN + ","
                    + detail.Replace("\r\n", "$").Replace(",", "/") + ","
                    + judge;
                file.WriteLine(str);// 直接追加文件末尾，换行
            }
        }

        public static void WriteError(string SN)
        {
            //string path = Document.pathList[0] +  DateTime.Now.ToString("yyyyMMdd") + "ErrorLog.txt";
            string path = Document.pathList[0] + "log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            using (StreamWriter file = new StreamWriter(path, true))
            {
                file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd,HH:mm:ss,@") + SN);// 直接追加文件末尾，换行 
            }
        }
    }

    class Pqm : Document
    {
        public static string type = ConfigurationManager.AppSettings["type"];
        static string factory = ConfigurationManager.AppSettings["factory"];
        static string building = ConfigurationManager.AppSettings["building"];
        public static string line = ConfigurationManager.AppSettings["line"];
        public static string process = ConfigurationManager.AppSettings["process"];
        static string inspect = ConfigurationManager.AppSettings["inspect"];
        static string machineName = ConfigurationManager.AppSettings["ok2ship_MachineName"];

        public static void WriteCSV(AllInfo.SNinfo snInfo)//(string SN,DateTime checkTime)
        {
            //KK07 NSTD E-1 L03 INTRS 20180614201355 GH9814320XDJL4K21.csv
            string fileName = type + factory + building + line + process + DateTime.Now.ToString("yyyyMMddHHmmss") + snInfo.SN;
            string path = Document.pathList[1] + fileName + ".csv";
            using (StreamWriter file = new StreamWriter(path, true))
            {
                string[] csvStr = new string[] { type, factory, building, line, process, snInfo.SN, "", "", snInfo.checkTime.ToString("yy,MM,dd,HH,mm,ss"), "1", inspect, "0.0", snInfo.checkItem, snInfo.checkTotal, "1"
                    , "MACHINE",
                    machineName };

                string str = String.Join(",", csvStr);

                file.WriteLine(str);// 直接追加文件末尾，换行 
            }
        }
    }

    class Sum:Document
    {
        public static int passQty;
        public static int failQty;
        public static int skipQty;
        public static string ReadTotal()
        {
            string path =Document.pathList[2] +"total_yield.txt";
            string totalStr = string.Empty;
            if (File.Exists(path))
            {
                int row = 0;
                string fileStr;
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    while ((fileStr = sr.ReadLine()) != null)
                    {
                        if (row == 0)
                        { totalStr = fileStr + "\r\n"; }
                        else { totalStr += fileStr + "\r\n"; }
                        switch (++row)
                        {
                            case 1:
                                passQty = int.Parse(fileStr);
                                break;
                            case 2:
                                failQty = int.Parse(fileStr);
                                break;
                            case 3:
                                skipQty = int.Parse(fileStr);
                                break;
                        }
                    }
                }
            }
            else
            {
                totalStr += "0\r\n";
                totalStr += "0\r\n";
                totalStr += "0\r\n";
                totalStr += "--\r\n";
                totalStr += "--";
                passQty = failQty = skipQty = 0;
            }
            return  totalStr;
        }

        public static void WriteTotal()
        {
            string path = Document.pathList[2] + "total_yield.txt";
            //覆盖旧文件
            FileStream writeFile = new FileStream(path, FileMode.Create);
            using (StreamWriter sw = new StreamWriter(writeFile))
            {
                string str = null;
                str = passQty.ToString() + "\r\n";
                str += failQty.ToString() + "\r\n";
                str += skipQty.ToString() + "\r\n";
                double sum = passQty + failQty + skipQty;
                str += Math.Round(Convert.ToDouble(sum - failQty) / sum, 4) * 100 + "%\r\n";
                str += Math.Round(Convert.ToDouble(sum - skipQty) / sum, 4) * 100 + "%";
                sw.Write(str);
            }
        }
    }
}
