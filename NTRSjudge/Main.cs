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
using NTRSjudge.Check;

namespace NTRSjudge
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
            光子云 = 1,
            大研 = 2,
            旧机器=3,
            单片机 = 11,
            ONS = 12
        }

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
            this.Text += " " + Application.ProductVersion.ToString();
            lblLine.Text += Pqm.line;
            lblLine.Visible = NTRSjudge.Layout.lineSwitch;
            //新建文件夹（log、pqm、sum）
            Document.CreateDocument();
            //载入统计信息
            try { TxtTotal.Text = Sum.ReadTotal(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"\r\n请检查下面统计文件格式或删除文件清零\r\n"+ Document.pathList[2] + "total_yield.txt"
                    ,"载入统计文件",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            //创建注册表文件
            try { Registry.LocalMachine.CreateSubKey(@"software\NTRS"); }
            catch (Exception ex){ MessageBox.Show(ex.Message); Environment.Exit(0); }            
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
            TlpLayout.RowCount = NTRSjudge.Layout.row;
            TlpLayout.ColumnCount =  NTRSjudge.Layout.col;

            for (int i = 0; i < NTRSjudge.Layout.row; i++)
            {
                TlpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            }
            for (int i = 0; i < NTRSjudge.Layout.col; i++)
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
            //定时执行
            SetTaskAtFixedTime();
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
                if (TxtSN.Text.Length<17) { MessageBox.Show("马达号不能少于17位，请重新输入");return; }
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
            //string[] SN = indata.Split(',');
            string[] SN = indata.Split(new string[] { Port.identifier }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in SN)
            {
                if (str == "") continue;
                if (str != "ERROR"&& !SN.Contains("+") && str.Length != 17)
                {
                    Log.WriteError(str);
                }
                //action(str);
                this.Invoke(new Action(() =>
                {
                    //就是textbox输入
                    Action(str);
                }));
            }
        }

        void Action(string SN)
        {
            Application.DoEvents();
            //填满之后，清空
            object sender=new object { };
            if (sequence == NTRSjudge.Layout.sum) { BtnClear_Click(sender, new EventArgs()); }

            //if (mode == Mode.旧机器 && !SN.Contains("ERROR"))
            //    SN = SN.Substring(0, 17);
            if (!SN.Contains("ERROR"))
                SN = SN.Substring(0, 17);

            Judge.judge(SN);
            //Judge.judge("ERROR");
            TxtSN.Text = string.Empty;

            #region 生产模式(不同方式串口发送信息)            
            //接收(光子云拍照)：一整盘由","隔开的SN
            //ERROR,GH945678901LKRQ01,......GH945678901LKRQ64,
            //发送：一整盘ok发"ok,"，存在非"PASS"发送"ng,"
            //"ok,"     "ng,"            
            //MessageBox.Show("away API,mode="+mode.ToString());
            if (mode == Mode.光子云)//光子云
            {
                if (AllInfo.SNlist.Count == NTRSjudge.Layout.sum)
                {
                    string sendStr = "ok,";
                    foreach (var var in AllInfo.SNlist)
                    {
                        if (var.result != "PASS")
                        {
                            sendStr = "ng,";
                            break;
                        }
                    }
                    SptReceiveOrSend.Write(sendStr);
                    MessageShow(ChkTest_ShowMessage.Checked, "mode="+mode+"，串口1发出" + sendStr.Length + "字节Byte:\r\n" + sendStr);
                }
            }
            //接收（扫描器）：一个接一个的SN
            //ERROR     GH945678901LKRQ01
            //发送：一整盘ok发"0"，存在非"PASS"发送坐标
            //"0"       "X0 = 2,Y0 = 3,X1 = 4,Y1 = 5,"
            //(第2行第3列，第4行第5列材料NG，需要捡出)
            else if (mode == Mode.大研)//大研
            {
                if (AllInfo.SNlist.Count == NTRSjudge.Layout.sum)
                {
                    //string sendStr =type + snInfo.firstTime.ToString("yyyyMMddHHmmss");
                    string sendStr = Pqm.type + Pqm.line.Remove(0, 1) + Pqm.process.Substring(0, 1) + AllInfo.SNlist[AllInfo.SNlist.Count - 1].firstTime.ToString("yyMMddHHmmss").Remove(0,1); 
                    for (int i = 1; i <= AllInfo.SNlist.Count; i++)
                    {
                        if (AllInfo.SNlist[i - 1].result != "PASS")
                        {
                            //如果"01.02.05."去掉"."的话，要是串口数据丢失一个，后面全部都会识别错误
                            //不去掉"."的话，只会识别丢失的位置
                            if (i < 10) { sendStr += "0" + i.ToString(); }
                            else { sendStr += i.ToString(); }
                        }
                    }
                    SptReceiveOrSend.Write(sendStr + "\r\n");
                    MessageShow(ChkTest_ShowMessage.Checked, "mode="+mode+"，串口1发出" + (sendStr.Length + 1).ToString() + "字节Byte:\r\n" + sendStr + "\r\n(回车)");
                }
            }
            //每次接收分析后发一个信号给串口
            //接收（扫描器）：一个接一个的SN+","
            //GH945678901LKRQ01,
            //发送：根据结果"MISS"、"PASS"、"FAIL"、"SKIP"分别发送他们的头一个字母
            //"M"   "P"     "F"     "S"
            else if (mode == Mode.单片机)//单片机
            {
                switch (AllInfo.SNlist[AllInfo.SNlist.Count - 1].result)
                {
                    case "MISS":
                        SptSend.Write("M");
                        MessageShow(ChkTest_ShowMessage.Checked, "mode="+mode+"，串口2发出1字节Byte:\r\nM");
                        break;
                    case "PASS":
                        SptSend.Write("P");
                        MessageShow(ChkTest_ShowMessage.Checked, "mode=" + mode + "，串口2发出1字节Byte:\r\nP");
                        break;
                    case "FAIL":
                        SptSend.Write("F");
                        MessageShow(ChkTest_ShowMessage.Checked, "mode=" + mode + "，串口2发出1字节Byte:\r\nF");
                        break;
                    case "HOLD":
                        SptSend.Write("F");
                        MessageShow(ChkTest_ShowMessage.Checked, "mode=" + mode + "，串口2发出1字节Byte:\r\nF");
                        break;
                    case "SKIP":
                        SptSend.Write("S");
                        MessageShow(ChkTest_ShowMessage.Checked, "mode=" + mode + "，串口2发出1字节Byte:\r\nS");
                        break;
                    default:
                        SptSend.Write("F");
                        MessageShow(ChkTest_ShowMessage.Checked, "mode=" + mode + "，串口2发出1字节Byte:\r\nF");
                        break;
                }
            }
            //发送错误坐标或ok
            //接收(光子云拍照)：一整盘由","隔开的SN
            //ERROR,GH945678901LKRQ01,......GH945678901LKRQ64,
            //发送：一整盘ok发"ok"，存在非"PASS"发送坐标加"."，最后结尾都加回车
            //"ok\r\n"      "1,1.1,2\r\n"
            else if (mode == Mode.ONS)//供应商
            {
                if (AllInfo.SNlist.Count == NTRSjudge.Layout.sum)
                {
                    string sendStr = "";
                    for (int i = 0; i < AllInfo.SNlist.Count; i++)
                    {
                        if (AllInfo.SNlist[i].result != "PASS")
                        {
                            //固定布局（竖排或横排）
                            //int x = i / APP_Config.row;
                            //int y = i % APP_Config.row;
                            //sendStr += (x + 1).ToString() + "," + (y + 1).ToString() + ".";                            
                            sendStr += Regedit.Trajectory[i] + ".";
                        }
                    }
                    if (sendStr == "") { sendStr = "ok"; }
                    //SptSend.WriteLine(sendStr);//
                    SptSend.Write(sendStr+"\r\n");
                    MessageShow(ChkTest_ShowMessage.Checked, "mode=" + mode + "，串口2发出" + (sendStr.Length+1).ToString() + "字节Byte:\r\n" + sendStr+"\r\n(回车)");
                }
            }
            #endregion
            //方块布局显示
            int[] location = Array.ConvertAll(Regedit.Trajectory[sequence].Split(','), int.Parse);
            
            try
            {
                TlpLayout.Controls.Add(NTRSjudge.Layout.paint(AllInfo.SNlist[AllInfo.SNlist.Count - 1]),
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
            //非"MISS"（"PASS"、"FAIL"、"SKIP"），写sum和csv
            if (LblJudge.Text != "MISS")
            {
                switch (LblJudge.Text)
                {
                    case "PASS":
                        Sum.passQty++;
                        break;
                    case "FAIL":
                        Sum.failQty++;
                        break;
                    case "SKIP":
                        Sum.skipQty++;
                        break;
                }
                Sum.WriteTotal();
                TxtTotal.Text = Sum.ReadTotal();
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
            if (Convert.ToInt16(mode)<10)//单串口个数，双串口十位数
            {
                if (ChkTest_Writeline.Checked)
                {
                    //SptReceiveOrSend.WriteLine(TxtTest_SendStr.Text);
                    SptReceiveOrSend.Write(TxtTest_SendStr.Text+"\r\n");
                    MessageShow(ChkTest_ShowMessage.Checked, "串口1发出" + TxtTest_SendStr.Text.Length + 1 + "字节Byte:\r\n" + TxtTest_SendStr.Text + "\r\n(回车)");
                }
                else
                {
                    SptReceiveOrSend.Write(TxtTest_SendStr.Text);
                    MessageShow(ChkTest_ShowMessage.Checked, "串口1发出" + TxtTest_SendStr.Text.Length+ "字节Byte:\r\n" + TxtTest_SendStr.Text);
                }
            }
            else
            {
                if (ChkTest_Writeline.Checked)
                {
                    //SptSend.WriteLine(TxtTest_SendStr.Text);
                    SptSend.Write(TxtTest_SendStr.Text+"\r\n");
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
            //#region 手动更新
            ////后面加入用户名与密码访问路径
            //string fileName = @"\\172.27.36.32\kk07\13 SFC\叶银旭\2_Release\NTRSjudge\20190403\NTRSjudgeRelease\NTRSjudge.exe";
            //System.Diagnostics.FileVersionInfo fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(fileName);

            //Version vServer = new Version(fv.FileVersion);//服务器版本
            //Version vLocal = new Version(Application.ProductVersion);//当前版本
            
            //if (vServer > vLocal)
            //{
            //    MessageBox.Show(vServer.ToString());//获取服务器版本
            //    //下载文件
            //    //更换名字
            //    //启动新版程序
            //    //关闭旧程序
            //}
            //else
            //{
            //    MessageBox.Show(vLocal.ToString());//启动自己
            //}
            //#endregion
            //return;
            DateTime start = DateTime.Now;
            if (isOne)
            {
                for (int i = 0; i < NTRSjudge.Layout.sum; i++)
                {
                    //Application.DoEvents(); //接收图形界面会增加大量时间处理
                    Action("01234567890123456");
                }
                isOne = false;
            }
            else
            {
                for (int i = 0; i < NTRSjudge.Layout.sum; i++)
                {
                    //Application.DoEvents();
                    string str = (i+1).ToString();
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
        

        void SetTaskAtFixedTime()
        {
            DateTime now = DateTime.Now;
            DateTime oneOClock = DateTime.Today.AddHours(8.0); //早上8：00
            //DateTime oneOClock = DateTime.Today.AddHours(12).AddMinutes(4).AddSeconds(20);
            if (now > oneOClock)
            {
                oneOClock = oneOClock.AddDays(1.0);
            }
            int msUntilFour = (int)((oneOClock - now).TotalMilliseconds);

            var t = new System.Threading.Timer(DoAt8AM);
            t.Change(msUntilFour, System.Threading.Timeout.Infinite);
        }

        //要执行的任务
        void DoAt8AM(object state)
        {
            //执行功能：每天8点钟
            if (!Sum.ChangeFileName())
            {
                Environment.Exit(0);
            }
            //System.InvalidOperationException:“线程间操作无效: 从不是创建控件“TxtTotal
            Invoke(new Action(() =>
            {
                TxtTotal.Text = Sum.ReadTotal();
            }));
            //再次设定下一次定时
            SetTaskAtFixedTime();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BtnClear_Click(sender, new EventArgs());
            }
        }
    }
}