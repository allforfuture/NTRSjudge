﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Windows.Forms;
using System.Drawing;

namespace OK2Ship
{
    class Layout
    {
        public static int row = Convert.ToInt16(ConfigurationManager.AppSettings["row"]);
        public static int col = Convert.ToInt16(ConfigurationManager.AppSettings["col"]);
        public static int sum = row * col;
        
        public static Panel paint(AllInfo.SNinfo snInfo)
        {            
            Color panelColor = new Color();
            Color strColor = new Color();
            switch (snInfo.result)
            {
                //case "PASS":
                case "OK":
                    //panelColor = ColorTranslator.FromHtml("#008000");
                    panelColor = Color.Green;
                    strColor = Color.White;
                    break;
                case "MISS":
                    panelColor = Color.Yellow;
                    strColor = Color.Black;
                    break;
                case "HOLD":
                    //panelColor = ColorTranslator.FromHtml("#0000FF");
                    panelColor = Color.Blue;
                    strColor = Color.White;
                    break;
                case "NG":
                    panelColor = Color.Crimson;
                    strColor = Color.White;
                    break;
                case "KEEP":
                    panelColor = Color.Orange;
                    strColor = Color.Black;
                    break;
                case "DUPLICATE":
                    panelColor = Color.Red;
                    strColor = Color.Black;
                    break;
                case "SCRAP":
                    panelColor = Color.Red;
                    strColor = Color.Black;
                    break;
                case "RETEST":
                    panelColor = Color.Magenta;
                    strColor = Color.Black;
                    break;
                case "RECHECK":
                    panelColor = Color.Magenta;
                    strColor = Color.Black;
                    break;
                default:
                    panelColor = Color.Red;
                    strColor = Color.Black;
                    break;
            }            
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = panelColor;
            //panel.AutoSize = true;

            Label l1 = new Label();
            l1.Text = snInfo.result;
            //l1.Font = new Font("微软雅黑", 12, FontStyle.Bold);
            l1.Font = new Font("微软雅黑",l1.Font.Size,FontStyle.Bold);
            Label l2 = new Label();        
            //绝对位置change动态位置
            l2.Location = new Point(0, 50);
            l2.Text = Regedit.Trajectory[AllInfo.SNlist.Count-1];


            Label l3 = new Label();
            l3.BringToFront();
            //l3.AutoSize = true;
            l3.Width = (Main.TlpLayout_Width / Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["col"])) - 10;
            l3.Height = 24;//可以显示2行字
            l3.Location = new Point(0, 20);
            l3.Text = snInfo.detail;
            panel.Controls.Add(l3);
            l1.ForeColor = l2.ForeColor = l3.ForeColor = strColor;

            panel.Controls.Add(l1);
            panel.Controls.Add(l2);
            
            return panel;
        }
    }
}
