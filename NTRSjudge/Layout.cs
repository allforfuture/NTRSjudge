using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Windows.Forms;
using System.Drawing;

namespace NTRSjudge
{
    class Layout
    {
        public static int row = Convert.ToInt16(ConfigurationManager.AppSettings["row"]);
        public static int col = Convert.ToInt16(ConfigurationManager.AppSettings["col"]);
        public static int sum = row * col;
        public static bool lineSwitch = ConfigurationManager.AppSettings["lineSwitch"]=="1"?true:false;
        public static Panel paint(AllInfo.SNinfo snInfo)
        {
            Color panelColor = new Color();
            switch (snInfo.result)
            {
                case "PASS":
                    //panelColor = Color.Lime;
                    panelColor = ColorTranslator.FromHtml("#008000");
                    break;
                case "MISS":
                    panelColor = Color.Yellow;
                    break;
                case "HOLD":
                    panelColor = ColorTranslator.FromHtml("#0000FF");
                    break;
                default:
                    panelColor = Color.Red;
                    break;
            }
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = panelColor;

            Label l1 = new Label();
            l1.Text = snInfo.result;

            Label l2 = new Label();        
            //绝对位置change动态位置
            l2.Location = new Point(0, 50);
            l2.Text = Regedit.Trajectory[AllInfo.SNlist.Count-1];

            if (snInfo.result == "FAIL")
            {
                Label l3 = new Label();
                l3.AutoSize = true;
                l3.Location = new Point(0, 25);
                //l3.Location = new Point(0, 10);
                //l3.Dock = DockStyle.Fill;
                //l3.Multiline = true;
                //GH945678901TEST00
                //detailTxb.Text = "phase2,AE-1:PASS\r\nphase2,AE-16_01CJ:PASS\r\nphase2,AE-16_01CJ2:FAIL\r\nphase3a,AE-1:PASS\r\nphase3a,AE-2:SKIP\r\nphase3a,AE-3:SKIP\r\nphase3a,AE-40:SKIP\r\nphase3b,AE-16_01CJ#JIG_C_COIL:SKIP\r\n";

                string[] temp = snInfo.detail.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach (string str in temp)
                {
                    if (str.Contains("FAIL") || str.Contains("SKIP"))
                    {
                        //int startInt = str.IndexOf(",");
                        //int endInt = str.Length - 1;
                        //l3.Text = str.Substring(startInt + 1, endInt - startInt);

                        //string str0 = "AE-16_19MA#M_MG_ATTACH: SKIP";
                        int startInt = str.IndexOf(",");
                        int endInt = str.IndexOf("#");
                        if (endInt == -1)
                        { endInt = str.IndexOf(":"); }
                        l3.Text = str.Substring(startInt + 1, endInt - (startInt + 1));
                        //不能用换行显示，单片机模式下长度是非常长的，界面显示能单行显示全部
                        //l3.Text = test.Insert(9,"\r\n");
                        //l3.Height = 40;
                        break;
                    }
                }
                //加上突出效果，能显示的长度变小
                //l3.Font = new Font("微软雅黑", 9, FontStyle.Bold);
                panel.Controls.Add(l3);
                l1.ForeColor = l2.ForeColor = l3.ForeColor = Color.White;
            }
            else if (snInfo.result == "HOLD")
            {
                Label l3 = new Label();
                l3.AutoSize = true;
                l3.Location = new Point(0, 25);
                string[] temp = snInfo.detail.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach (string str in temp)
                {
                    if (str.Contains("phase3a_fail,"))
                    {
                        int startInt = str.IndexOf(":");
                        int endInt = str.Length;
                        int i = int.Parse(str.Substring(startInt + 1, endInt - (startInt + 1)));
                        if (i >= 3)
                        {
                            startInt = str.IndexOf(",");
                            endInt = str.IndexOf("#");
                            if (endInt == -1)
                            { endInt = str.IndexOf(":"); }
                            l3.Text = str.Substring(startInt + 1, endInt - (startInt + 1));
                            break;
                        }
                    }
                }
                panel.Controls.Add(l3);
                l1.ForeColor = l2.ForeColor = l3.ForeColor = Color.White;
            }



            if (lineSwitch && !snInfo.lineOK)
            {
                Label l4 = new Label();
                l4.AutoSize = true;
                l4.Location = new Point(50, 25);
                //l4.Location = new Point(20, 25);
                l4.Text = snInfo.line;
                l4.Font=new Font("Arial", 24, FontStyle.Bold);
                panel.Controls.Add(l4);
            }

            panel.Controls.Add(l1);
            panel.Controls.Add(l2);

            
            return panel;
        }
    }
}
