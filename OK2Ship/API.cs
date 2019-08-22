using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
//using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace OK2Ship
{
    class API
    {
        static string API_NTRS = System.Configuration.ConfigurationManager.AppSettings["API_NTRS"];
        static string assy_cd = System.Configuration.ConfigurationManager.AppSettings["assy_cd"];
        static string API_OK2Ship = System.Configuration.ConfigurationManager.AppSettings["API_OK2Ship"];

        public static string SN_Judge_NTRS(string SN, ref string detail)
        {
            //POST参数param
            //assy_cd=COVER&serial_cd=GH98503102ZKPK627
            string POSTparam = "assy_cd=" + assy_cd + "&serial_cd=" + SN;

            // param转换
            byte[] data = Encoding.ASCII.GetBytes(POSTparam);

            ServicePointManager.Expect100Continue = false; //HTTP错误（417）对应

            //创建请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(API_NTRS);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            //POST写入
            try
            {
                using (Stream reqStream = request.GetRequestStream())
                    reqStream.Write(data, 0, data.Length);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); Environment.Exit(0); }

            //取得响应
            WebResponse response = request.GetResponse();

            //读取结果
            string APIstr = "";
            using (Stream resStream = response.GetResponseStream())
            using (var reader = new StreamReader(resStream, Encoding.GetEncoding("UTF-8")))
                APIstr = reader.ReadToEnd();

            //detail输出
            //注意：之前的API地址返还回来的字符串第一个字节是个点（显示不了,是小红点，但是会占字节）
            //APIstr = APIstr.Replace("\ufeff", string.Empty);
            //JObject JO = JObject.Parse(APIstr);


            if (APIstr == "{}")
            {
                MessageBox.Show("接收到空值。\r\n请检查配置文件中的assy_cd与API的值是否正确。\r\n将会关闭程序。", "API接收", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            JObject JO = null;
            try { JO = JObject.Parse(APIstr); }
            catch { MessageBox.Show("返回的Json:\r\n" + APIstr, "解析Json失败", MessageBoxButtons.OK, MessageBoxIcon.Error); Environment.Exit(0); }
            //关闭
            string result = JO["total_judge"].ToString();

            #region detail值写入
            try
            {
                JObject JO_phase2 = JObject.Parse(JO["comments"]["phase2_detail"].ToString());
                foreach (var var in JO_phase2)
                {
                    detail += string.Format("phase2,{0}:{1}\r\n", var.Key, var.Value);
                }
            }
            catch { }
            try
            {
                JObject JO_phase3a = JObject.Parse(JO["comments"]["phase3a_detail"].ToString());
                foreach (var var in JO_phase3a)
                {
                    detail += string.Format("phase3a,{0}:{1}\r\n", var.Key, var.Value);
                }
            }
            catch { }
            try
            {
                JObject JO_phase3b = JObject.Parse(JO["comments"]["phase3b_detail"].ToString());
                foreach (var var in JO_phase3b)
                {
                    detail += string.Format("phase3b,{0}:{1}\r\n", var.Key, var.Value);
                }
            }
            catch { }
            try
            {
                JObject JO_phase3a_fail = JObject.Parse(JO["comments"]["phase3a_fail_count"].ToString());
                foreach (var var in JO_phase3a_fail)
                {
                    detail += string.Format("phase3a_fail,{0}:{1}\r\n", var.Key, var.Value);
                }
            }
            catch { }
            #endregion
            #region
            ////正则表达式解析
            //string[] str = System.Text.RegularExpressions.Regex.Split(APIstr, "_detail\":", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //for (int j= 1; j <= 3; j++)
            //{
            //    str[j] = str[j].Replace("\"", "");
            //    int startInt = str[j].IndexOf("{") + 1;
            //    int endInt = str[j].IndexOf("}");
            //    str[j] = str[j].Substring(startInt, endInt - startInt);
            //    foreach (var var in str[j].Split(','))
            //    {
            //        if (j == 1) { detail += "phase2,"; }
            //        else if (j == 2) { detail += "phase3a,"; }
            //        else { detail += "phase3b,"; }
            //         detail+= var + "\r\n";
            //    }
            //}
            ////总结果输出
            //int i = APIstr.IndexOf(",\"comments");
            //string result = APIstr.Substring(49, i - (49 + 1));
            #endregion
            return result;
        }
        public static string SN_Judge_OK2SHIP(string SN, ref string detail)
        {
            //POST参数param
            string POSTparam = "module_sn=" + SN;

            // param转换
            byte[] data = Encoding.ASCII.GetBytes(POSTparam);

            ServicePointManager.Expect100Continue = false; //HTTP错误（417）对应

            //创建请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(API_OK2Ship);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            //POST写入
            try
            {
                using (Stream reqStream = request.GetRequestStream())
                    reqStream.Write(data, 0, data.Length);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); Environment.Exit(0); }

            //取得响应
            WebResponse response = request.GetResponse();

            //读取结果
            string APIstr = "";
            using (Stream resStream = response.GetResponseStream())
            using (var reader = new StreamReader(resStream, Encoding.GetEncoding("UTF-8")))
                APIstr = reader.ReadToEnd();

            //detail输出
            //注意：之前的API地址返还回来的字符串第一个字节是个点（显示不了,是小红点，但是会占字节）
            //APIstr = APIstr.Replace("\ufeff", string.Empty);
            //JObject JO = JObject.Parse(APIstr);


            if (APIstr == "{}")
            {
                MessageBox.Show("接收到空值。\r\n请检查配置文件中的assy_cd与API的值是否正确。\r\n将会关闭程序。", "API接收", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            JObject JO = null;
            try { JO = JObject.Parse(APIstr); }
            catch { MessageBox.Show("返回的Json:\r\n" + APIstr, "解析Json失败", MessageBoxButtons.OK, MessageBoxIcon.Error); Environment.Exit(0); }
            //关闭
            string result;
            switch ((int)JO["result"])
            {
                case 0:
                    result = "OK";
                    break;
                case 1:
                    result = "NG";
                    break;
                case 2:
                    result = "KEEP";
                    break;
                case 3:
                    result = "DUPLICATE";
                    break;
                case 4:
                    result = "HOLD";
                    break;
                case 5:
                    result = "SCRAP";
                    break;
                case 6:
                    result = "RETEST";
                    break;
                case 7:
                    result = "RECHECK";
                    break;
                default:
                    result = (string)JO["result"];
                    break;
            }
            #region detail值写入
            try
            {                
                detail = string.Join("", JO["comments"]);
            }
            catch { }
            #endregion
            return result;
        }
    }
}