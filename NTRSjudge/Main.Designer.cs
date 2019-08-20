namespace NTRSjudge
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnClear = new System.Windows.Forms.Button();
            this.PnlJudge = new System.Windows.Forms.Panel();
            this.LblJudge = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtSN = new System.Windows.Forms.TextBox();
            this.SNlab = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtTotal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtDetail = new System.Windows.Forms.TextBox();
            this.BtnTest = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运动轨迹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.串口通信ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TlpLayout = new System.Windows.Forms.TableLayoutPanel();
            this.SptReceiveOrSend = new System.IO.Ports.SerialPort(this.components);
            this.SptSend = new System.IO.Ports.SerialPort(this.components);
            this.BtnTest_Send = new System.Windows.Forms.Button();
            this.TxtTest_SendStr = new System.Windows.Forms.TextBox();
            this.ChkTest_Writeline = new System.Windows.Forms.CheckBox();
            this.GrpTest = new System.Windows.Forms.GroupBox();
            this.btnPath = new System.Windows.Forms.Button();
            this.ChkTest_ShowMessage = new System.Windows.Forms.CheckBox();
            this.PnlJudge.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.GrpTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(222, 578);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(52, 51);
            this.btnClear.TabIndex = 21;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // PnlJudge
            // 
            this.PnlJudge.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PnlJudge.Controls.Add(this.LblJudge);
            this.PnlJudge.Location = new System.Drawing.Point(4, 126);
            this.PnlJudge.Name = "PnlJudge";
            this.PnlJudge.Size = new System.Drawing.Size(270, 80);
            this.PnlJudge.TabIndex = 19;
            // 
            // LblJudge
            // 
            this.LblJudge.AutoSize = true;
            this.LblJudge.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LblJudge.Location = new System.Drawing.Point(25, -3);
            this.LblJudge.Name = "LblJudge";
            this.LblJudge.Size = new System.Drawing.Size(0, 86);
            this.LblJudge.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(4, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 25);
            this.label3.TabIndex = 23;
            this.label3.Text = "Detail:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(-1, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 25);
            this.label2.TabIndex = 22;
            this.label2.Text = "Judge:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(-1, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 25);
            this.label1.TabIndex = 18;
            this.label1.Text = "S/N:";
            // 
            // TxtSN
            // 
            this.TxtSN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtSN.Location = new System.Drawing.Point(4, 48);
            this.TxtSN.Name = "TxtSN";
            this.TxtSN.Size = new System.Drawing.Size(270, 21);
            this.TxtSN.TabIndex = 17;
            this.TxtSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSN_KeyDown);
            // 
            // SNlab
            // 
            this.SNlab.AutoSize = true;
            this.SNlab.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SNlab.ForeColor = System.Drawing.Color.Black;
            this.SNlab.Location = new System.Drawing.Point(4, 72);
            this.SNlab.Name = "SNlab";
            this.SNlab.Size = new System.Drawing.Size(0, 25);
            this.SNlab.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(1, 489);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 125);
            this.label5.TabIndex = 27;
            this.label5.Text = "Pass Qty:\r\nFail Qty:\r\nSkip Qty:\r\nYld(Fail):\r\nYld(Skip):";
            // 
            // TxtTotal
            // 
            this.TxtTotal.Enabled = false;
            this.TxtTotal.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.TxtTotal.Location = new System.Drawing.Point(98, 489);
            this.TxtTotal.Multiline = true;
            this.TxtTotal.Name = "TxtTotal";
            this.TxtTotal.Size = new System.Drawing.Size(118, 140);
            this.TxtTotal.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(93, 467);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 25);
            this.label4.TabIndex = 25;
            this.label4.Text = "Total:";
            // 
            // TxtDetail
            // 
            this.TxtDetail.Location = new System.Drawing.Point(4, 228);
            this.TxtDetail.Multiline = true;
            this.TxtDetail.Name = "TxtDetail";
            this.TxtDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtDetail.Size = new System.Drawing.Size(270, 239);
            this.TxtDetail.TabIndex = 24;
            // 
            // BtnTest
            // 
            this.BtnTest.Location = new System.Drawing.Point(117, 48);
            this.BtnTest.Name = "BtnTest";
            this.BtnTest.Size = new System.Drawing.Size(77, 22);
            this.BtnTest.TabIndex = 29;
            this.BtnTest.Text = "test";
            this.BtnTest.UseVisualStyleBackColor = true;
            this.BtnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(913, 25);
            this.menuStrip1.TabIndex = 30;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.运动轨迹ToolStripMenuItem,
            this.串口通信ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 运动轨迹ToolStripMenuItem
            // 
            this.运动轨迹ToolStripMenuItem.Name = "运动轨迹ToolStripMenuItem";
            this.运动轨迹ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.运动轨迹ToolStripMenuItem.Text = "运动轨迹";
            this.运动轨迹ToolStripMenuItem.Click += new System.EventHandler(this.运动轨迹ToolStripMenuItem_Click);
            // 
            // 串口通信ToolStripMenuItem
            // 
            this.串口通信ToolStripMenuItem.Name = "串口通信ToolStripMenuItem";
            this.串口通信ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.串口通信ToolStripMenuItem.Text = "串口通信";
            this.串口通信ToolStripMenuItem.Click += new System.EventHandler(this.串口通信ToolStripMenuItem_Click);
            // 
            // TlpLayout
            // 
            this.TlpLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TlpLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.TlpLayout.ColumnCount = 1;
            this.TlpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TlpLayout.Location = new System.Drawing.Point(280, 0);
            this.TlpLayout.Name = "TlpLayout";
            this.TlpLayout.RowCount = 1;
            this.TlpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TlpLayout.Size = new System.Drawing.Size(630, 630);
            this.TlpLayout.TabIndex = 3;
            // 
            // SptReceiveOrSend
            // 
            this.SptReceiveOrSend.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SptReceiveOrSend_DataReceived);
            // 
            // BtnTest_Send
            // 
            this.BtnTest_Send.Location = new System.Drawing.Point(117, 19);
            this.BtnTest_Send.Name = "BtnTest_Send";
            this.BtnTest_Send.Size = new System.Drawing.Size(77, 26);
            this.BtnTest_Send.TabIndex = 29;
            this.BtnTest_Send.Text = "手动发信号";
            this.BtnTest_Send.UseVisualStyleBackColor = true;
            this.BtnTest_Send.Click += new System.EventHandler(this.BtnTest_Send_Click);
            // 
            // TxtTest_SendStr
            // 
            this.TxtTest_SendStr.Location = new System.Drawing.Point(11, 64);
            this.TxtTest_SendStr.Name = "TxtTest_SendStr";
            this.TxtTest_SendStr.Size = new System.Drawing.Size(100, 21);
            this.TxtTest_SendStr.TabIndex = 0;
            this.TxtTest_SendStr.Text = "ng,";
            // 
            // ChkTest_Writeline
            // 
            this.ChkTest_Writeline.AutoSize = true;
            this.ChkTest_Writeline.Location = new System.Drawing.Point(15, 42);
            this.ChkTest_Writeline.Name = "ChkTest_Writeline";
            this.ChkTest_Writeline.Size = new System.Drawing.Size(84, 16);
            this.ChkTest_Writeline.TabIndex = 31;
            this.ChkTest_Writeline.Text = "末尾加回车";
            this.ChkTest_Writeline.UseVisualStyleBackColor = true;
            // 
            // GrpTest
            // 
            this.GrpTest.Controls.Add(this.btnPath);
            this.GrpTest.Controls.Add(this.ChkTest_ShowMessage);
            this.GrpTest.Controls.Add(this.ChkTest_Writeline);
            this.GrpTest.Controls.Add(this.BtnTest);
            this.GrpTest.Controls.Add(this.BtnTest_Send);
            this.GrpTest.Controls.Add(this.TxtTest_SendStr);
            this.GrpTest.Location = new System.Drawing.Point(55, 364);
            this.GrpTest.Name = "GrpTest";
            this.GrpTest.Size = new System.Drawing.Size(200, 100);
            this.GrpTest.TabIndex = 32;
            this.GrpTest.TabStop = false;
            this.GrpTest.Text = "Test";
            this.GrpTest.Visible = false;
            this.GrpTest.VisibleChanged += new System.EventHandler(this.GrpTest_VisibleChanged);
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(117, 73);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(77, 22);
            this.btnPath.TabIndex = 33;
            this.btnPath.Text = "打开路径";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.BtnPath_Click);
            // 
            // ChkTest_ShowMessage
            // 
            this.ChkTest_ShowMessage.AutoSize = true;
            this.ChkTest_ShowMessage.Location = new System.Drawing.Point(15, 20);
            this.ChkTest_ShowMessage.Name = "ChkTest_ShowMessage";
            this.ChkTest_ShowMessage.Size = new System.Drawing.Size(96, 16);
            this.ChkTest_ShowMessage.TabIndex = 32;
            this.ChkTest_ShowMessage.Text = "窗口收发提示";
            this.ChkTest_ShowMessage.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 631);
            this.Controls.Add(this.GrpTest);
            this.Controls.Add(this.TlpLayout);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.PnlJudge);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtSN);
            this.Controls.Add(this.SNlab);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TxtTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TxtDetail);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "NTRSjudge";
            this.PnlJudge.ResumeLayout(false);
            this.PnlJudge.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.GrpTest.ResumeLayout(false);
            this.GrpTest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel PnlJudge;
        private System.Windows.Forms.Label LblJudge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SNlab;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtDetail;
        private System.Windows.Forms.Button BtnTest;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运动轨迹ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 串口通信ToolStripMenuItem;
        private System.Windows.Forms.TextBox TxtSN;
        private System.Windows.Forms.TableLayoutPanel TlpLayout;
        internal System.IO.Ports.SerialPort SptReceiveOrSend;
        internal System.IO.Ports.SerialPort SptSend;
        private System.Windows.Forms.Button BtnTest_Send;
        private System.Windows.Forms.TextBox TxtTest_SendStr;
        private System.Windows.Forms.CheckBox ChkTest_Writeline;
        private System.Windows.Forms.GroupBox GrpTest;
        private System.Windows.Forms.CheckBox ChkTest_ShowMessage;
        private System.Windows.Forms.Button btnPath;
    }
}

