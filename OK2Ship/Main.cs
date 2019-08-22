using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;

namespace OK2Ship
{
    public partial class Main : Form
    {
        //解决tableLayoutPanel闪烁
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
                return cp;
            }
        }

        public enum Mode
        {
            无串口 = 0,

            //光子云 = 1,
            //大研 = 2,
            //旧机器=3,
            旧机器API2 = 4,
            //单片机 = 11,
            //ONS = 12
        }

        //字符串分行参数
        public static int TlpLayout_Width;
        //生成模式
        public static Mode mode;
        //静态类，操作串口属性
        public static Main main;
        //当前已扫描二维码个数
        int sequence = 0;
        public Main()
        {
            //CheckForIllegalCrossThreadCalls = false;
            main = this;
            InitializeComponent();
            //显示版本号
            this.Text += "_" + Application.ProductVersion.ToString();
            //新建文件夹（log、pqm、sum）
            Document.CreateDocument();
            //创建注册表文件
            try { Registry.LocalMachine.CreateSubKey(@"software\NTRS"); }
            catch (Exception ex) { MessageBox.Show(ex.Message); Environment.Exit(0); }
            //验证（串口）
            if (!Regedit.verifyPort())
            {
            checkPort:
                DialogResult dr = MessageBox.Show("串口验证失败，\n按确定键重新设置串口属性或关闭程序。", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == DialogResult.No)
                {
                    Environment.Exit(0);
                }
                Port port = new Port();
                port.ShowDialog();
                if (!Regedit.verifyPort()) { goto checkPort; }
            }
            //验证（运动轨迹）
            if (!Regedit.verifyTrajectory())
            {
            checkTrajectory:
                DialogResult dr = MessageBox.Show("运动轨迹验证失败，\n按确定键重新设置运动轨迹或关闭程序。", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == DialogResult.No)
                {
                    Environment.Exit(0);
                }
                Trajectory trajectory = new Trajectory();
                trajectory.ShowDialog();
                if (!Regedit.verifyTrajectory()) { goto checkTrajectory; }
            }
            #region 布局
            TlpLayout.RowCount = OK2Ship.Layout.row;
            TlpLayout.ColumnCount = OK2Ship.Layout.col;

            for (int i = 0; i < OK2Ship.Layout.row; i++)
            {
                TlpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            }
            for (int i = 0; i < OK2Ship.Layout.col; i++)
            {
                TlpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            }
            /*
            for (int i = 0; i < NTRSjudge.Layout.row; i++)
            {
                for (int j = 0; j < NTRSjudge.Layout.col; j++)
                {
                    Label label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.BackColor = Color.Red;
                    label.Text = "\r\n\r\n\r\n(" + (i + 1).ToString() + "," + (j + 1).ToString() + ")";
                    tableLayoutPanel1.Controls.Add(label, j, i);
                    //下面这句要是在行和列不够的情况下，会建到100行1000列
                    //tableLayoutPanel1.Controls.Add(label1, 100, 1000);
                }
            }
            */
            #endregion
            TlpLayout_Width = TlpLayout.Width;
        }

        private void 运动轨迹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trajectory trajectory = new Trajectory();
            trajectory.ShowDialog();
        }

        private void 串口通信ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Port serialPort = new Port();
            serialPort.ShowDialog();
        }
        private void TxtSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (TxtSN.Text.Length < 17) { MessageBox.Show("马达号不能少于17位，请重新输入"); return; }
                string testPassword = new string(DateTime.Now.ToString("yyyyMMddHHmm").ToCharArray().Reverse().ToArray());
                if (TxtSN.Text == "TEST1" + testPassword)
                { 设置ToolStripMenuItem.Visible = GrpTest.Visible = true; return; }
                else if (TxtSN.Text == "TEST0" + testPassword)
                { 设置ToolStripMenuItem.Visible = GrpTest.Visible = false; return; }
                Action(TxtSN.Text);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            sequence = 0;
            TlpLayout.Controls.Clear();
            AllInfo.SNlist = new List<AllInfo.SNinfo>();
            //数据显示
            SNlab.Text = LblJudge.Text = TxtDetail.Text = string.Empty;
        }

        string saveLast;
        private void SptReceiveOrSend_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //MessageBox.Show("串口1已触发DataReceived");
            //byte[] byteRead = new byte[SptReceiveOrSend.BytesToRead];
            //SptReceiveOrSend.Read(byteRead, 0, byteRead.Length);
            //string dataRe = Encoding.Default.GetString(byteRead);

            byte[] readBuffer = new byte[SptReceiveOrSend.BytesToRead];
            SptReceiveOrSend.Read(readBuffer, 0, readBuffer.Length);
            string indata = Encoding.Default.GetString(readBuffer);
            MessageShow(ChkTest_ShowMessage.Checked, "串口1接收到" + readBuffer.Length + "字节Byte:\r\n" + indata);
            //SerialPort sp = (SerialPort)sender;
            #region 改成readBuffer
            //int count = sp.BytesToRead;
            //byte[] readBuffer = new byte[count];
            //sp.Read(readBuffer, 0, count);
            //string indata = Encoding.Default.GetString(readBuffer);
            #endregion
            //string indata = sp.ReadExisting();
            //MessageBox.Show("串口1接收到:\r\n" + indata);

            //MessageBox.Show("从串口接收到:\r\n"+indata);
            //要是不能接收到完整的记录就存起来(最后一位是终止符",")
            //if (indata.Substring(indata.Length - 1, 1) != ",")

            if (indata.Substring(indata.Length - 1, 1) != Port.identifier)
            {
                saveLast += indata;
                return;
            }
            //MessageBox.Show("action");
            indata = saveLast + indata;
            saveLast = "";
            string[] SN = indata.Split(new string[] { Port.identifier }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in SN)
            {
                string sn = str.Replace("\r", "").Replace("\n", "");
                if (sn == "") continue;
                if (sn != "ERROR" && !SN.Contains("+") && sn.Length != 17)
                {
                    //MessageBox.Show(str);
                    Log.WriteError(sn);
                }
                //action(sn);
                this.Invoke(new Action(() =>
                {
                    //就是textbox输入
                    Action(sn);
                }));
            }
        }

        void Action(string SN)
        {
            Application.DoEvents();
            //填满之后，清空
            object sender = new object { };
            if (sequence == OK2Ship.Layout.sum) { BtnClear_Click(sender, new EventArgs()); }
            if (!SN.Contains("ERROR"))
                SN = SN.Substring(0, 17);

            Judge.judge(SN);
            TxtSN.Text = string.Empty;

            //方块布局显示
            int[] location = Array.ConvertAll(Regedit.Trajectory[sequence].Split(','), int.Parse);

            try
            {
                TlpLayout.Controls.Add(OK2Ship.Layout.paint(AllInfo.SNlist[AllInfo.SNlist.Count - 1]),
                    location[0] - 1, location[1] - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //数据显示
            SNlab.Text = AllInfo.SNlist[AllInfo.SNlist.Count - 1].SN;
            TxtDetail.Text = AllInfo.SNlist[AllInfo.SNlist.Count - 1].detail;
            LblJudge.Text = AllInfo.SNlist[AllInfo.SNlist.Count - 1].result;
            //写log
            Log.WriteLog(SNlab.Text, TxtDetail.Text, LblJudge.Text);
            //非"MISS"写csv
            if (LblJudge.Text != "MISS")
            {
                Pqm.WriteCSV(AllInfo.SNlist[AllInfo.SNlist.Count - 1]);
            }
            sequence++;
        }

        //循环次数少，不需要
        #region windows消息(按了关闭键)(重写void接收到信息后触发，要是关闭信息就变成最小化，其他信息就运行base原void)
        //protected override void WndProc(ref Message m)
        //{
        //    //关闭消息
        //    const int WM_SYSCOMMAND = 0x0112;
        //    const int SC_CLOSE = 0xF060;

        //    if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
        //    {
        //        // 屏蔽传入的消息事件 
        //        //MessageBox.Show("触发");
        //        //this.WindowState = FormWindowState.Minimized;
        //        //简单关闭之后foreach或for要是还在运行会占用串口，要么完全关闭程序要么等待结束再打开
        //        Environment.Exit(0);
        //        //base.WndProc(ref m);
        //        return;
        //    }
        //    base.WndProc(ref m);
        //}
        #endregion

        #region 按键ESC
        //protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        //{
        //    int WM_KEYDOWN = 256;
        //    int WM_SYSKEYDOWN = 260;

        //    if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
        //    {
        //        switch (keyData)
        //        {
        //            case Keys.Escape:
        //                this.Close();
        //                break;
        //        }
        //    }
        //    return false;
        //}
        #endregion
        #region 测试功能
        private void BtnTest_Send_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(mode) < 10)//单串口个数，双串口十位数
            {
                if (ChkTest_Writeline.Checked)
                {
                    //SptReceiveOrSend.WriteLine(TxtTest_SendStr.Text);
                    SptReceiveOrSend.Write(TxtTest_SendStr.Text + "\r\n");
                    MessageShow(ChkTest_ShowMessage.Checked, "串口1发出" + TxtTest_SendStr.Text.Length + 1 + "字节Byte:\r\n" + TxtTest_SendStr.Text + "\r\n(回车)");
                }
                else
                {
                    SptReceiveOrSend.Write(TxtTest_SendStr.Text);
                    MessageShow(ChkTest_ShowMessage.Checked, "串口1发出" + TxtTest_SendStr.Text.Length + "字节Byte:\r\n" + TxtTest_SendStr.Text);
                }
            }
            else
            {
                if (ChkTest_Writeline.Checked)
                {
                    //SptSend.WriteLine(TxtTest_SendStr.Text);
                    SptSend.Write(TxtTest_SendStr.Text + "\r\n");
                    MessageShow(ChkTest_ShowMessage.Checked, "串口2发出" + TxtTest_SendStr.Text.Length + 1 + "字节Byte:\r\n" + TxtTest_SendStr.Text + "\r\n(回车)");
                }
                else
                {
                    SptSend.Write(TxtTest_SendStr.Text);
                    MessageShow(ChkTest_ShowMessage.Checked, "串口2发出" + TxtTest_SendStr.Text.Length + "字节Byte:\r\n" + TxtTest_SendStr.Text);
                }
            }
        }
        bool isOne = true;
        private void BtnTest_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            if (isOne)
            {
                for (int i = 0; i < OK2Ship.Layout.sum; i++)
                {
                    //Application.DoEvents(); //接收图形界面会增加大量时间处理
                    Action("01234567890123456");
                }
                isOne = false;
            }
            else
            {
                for (int i = 0; i < OK2Ship.Layout.sum; i++)
                {
                    //Application.DoEvents();
                    string str = (i + 1).ToString();
                    if (str.Length < 2)
                    {
                        str = "0" + str;
                    }
                    str = "123456789012345" + str;
                    Action(str);
                }
                isOne = true;
            }
            DateTime end = DateTime.Now;
            TimeSpan time = end - start;
            MessageBox.Show(time.ToString());
        }

        private void GrpTest_VisibleChanged(object sender, EventArgs e)
        {
            if (!GrpTest.Visible) { ChkTest_ShowMessage.Checked = false; }
        }

        void MessageShow(bool isShow, string message)
        {
            if (isShow) { MessageBox.Show(message); }
        }
        #endregion

        private void BtnPath_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
            //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}