
namespace ADBPushApkTool
{
    partial class ApkPush_Tool
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApkPush_Tool));
            this.selectApk = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ChooseFileButton = new System.Windows.Forms.Button();
            this.Devices = new System.Windows.Forms.ComboBox();
            this.TarCommandAndPush = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pushButton = new System.Windows.Forms.Button();
            this.selectRAR = new System.Windows.Forms.OpenFileDialog();
            this.CheckDevices = new System.Windows.Forms.Timer(this.components);
            this.ReadOnlyPathPushButton = new System.Windows.Forms.Button();
            this.CmdInfoWin = new System.Windows.Forms.TextBox();
            this.BundleNames = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Connect2Simulator = new System.Windows.Forms.Button();
            this.SimulatorsCheckPoint = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectApk
            // 
            this.selectApk.FileName = "apkFileSelect";
            this.selectApk.Filter = "|*.apk";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 333);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "没检测到设备";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "目前所选的设备";
            // 
            // ChooseFileButton
            // 
            this.ChooseFileButton.Location = new System.Drawing.Point(12, 119);
            this.ChooseFileButton.Name = "ChooseFileButton";
            this.ChooseFileButton.Size = new System.Drawing.Size(208, 27);
            this.ChooseFileButton.TabIndex = 5;
            this.ChooseFileButton.Text = "选择Apk并Install";
            this.ChooseFileButton.UseVisualStyleBackColor = true;
            this.ChooseFileButton.Click += new System.EventHandler(this.ChooseFileButton_Click);
            // 
            // Devices
            // 
            this.Devices.DisplayMember = "未检测到头为sunborn的包体";
            this.Devices.FormattingEnabled = true;
            this.Devices.Items.AddRange(new object[] {
            "未检测到设备"});
            this.Devices.Location = new System.Drawing.Point(12, 35);
            this.Devices.Name = "Devices";
            this.Devices.Size = new System.Drawing.Size(208, 25);
            this.Devices.TabIndex = 6;
            this.Devices.ValueMember = "未检测到头为sunborn的包体";
            this.Devices.SelectedIndexChanged += new System.EventHandler(this.Devices_SelectedIndexChanged);
            // 
            // TarCommandAndPush
            // 
            this.TarCommandAndPush.Location = new System.Drawing.Point(12, 152);
            this.TarCommandAndPush.Name = "TarCommandAndPush";
            this.TarCommandAndPush.Size = new System.Drawing.Size(208, 27);
            this.TarCommandAndPush.TabIndex = 7;
            this.TarCommandAndPush.Text = "选择分包解压";
            this.TarCommandAndPush.UseVisualStyleBackColor = true;
            this.TarCommandAndPush.Click += new System.EventHandler(this.TarCommandAndPush_Click);
            // 
            // pushButton
            // 
            this.pushButton.Location = new System.Drawing.Point(12, 185);
            this.pushButton.Name = "pushButton";
            this.pushButton.Size = new System.Drawing.Size(208, 26);
            this.pushButton.TabIndex = 8;
            this.pushButton.Text = "Push";
            this.pushButton.UseVisualStyleBackColor = true;
            this.pushButton.Click += new System.EventHandler(this.pushButton_Click);
            // 
            // selectRAR
            // 
            this.selectRAR.FileName = "selectRAR";
            this.selectRAR.Filter = "*.rar,*.zip|";
            // 
            // CheckDevices
            // 
            this.CheckDevices.Interval = 1000;
            // 
            // ReadOnlyPathPushButton
            // 
            this.ReadOnlyPathPushButton.Location = new System.Drawing.Point(12, 217);
            this.ReadOnlyPathPushButton.Name = "ReadOnlyPathPushButton";
            this.ReadOnlyPathPushButton.Size = new System.Drawing.Size(208, 27);
            this.ReadOnlyPathPushButton.TabIndex = 9;
            this.ReadOnlyPathPushButton.Text = "分包解压+Push";
            this.ReadOnlyPathPushButton.UseVisualStyleBackColor = true;
            this.ReadOnlyPathPushButton.Click += new System.EventHandler(this.ReadOnlyPathPushButton_Click);
            // 
            // CmdInfoWin
            // 
            this.CmdInfoWin.AllowDrop = true;
            this.CmdInfoWin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmdInfoWin.Location = new System.Drawing.Point(226, 11);
            this.CmdInfoWin.Multiline = true;
            this.CmdInfoWin.Name = "CmdInfoWin";
            this.CmdInfoWin.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CmdInfoWin.Size = new System.Drawing.Size(462, 317);
            this.CmdInfoWin.TabIndex = 10;
            this.CmdInfoWin.TextChanged += new System.EventHandler(this.CmdInfoWin_TextChanged);
            // 
            // BundleNames
            // 
            this.BundleNames.DisplayMember = "未检测到头为sunborn的包体";
            this.BundleNames.FormattingEnabled = true;
            this.BundleNames.Items.AddRange(new object[] {
            "未检测到头为sunborn的包体"});
            this.BundleNames.Location = new System.Drawing.Point(12, 83);
            this.BundleNames.Name = "BundleNames";
            this.BundleNames.Size = new System.Drawing.Size(208, 25);
            this.BundleNames.TabIndex = 11;
            this.BundleNames.ValueMember = "未检测到头为sunborn的包体";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "设备中所含包体";
            // 
            // Connect2Simulator
            // 
            this.Connect2Simulator.Location = new System.Drawing.Point(12, 301);
            this.Connect2Simulator.Name = "Connect2Simulator";
            this.Connect2Simulator.Size = new System.Drawing.Size(208, 27);
            this.Connect2Simulator.TabIndex = 13;
            this.Connect2Simulator.Text = "连接至指定模拟器";
            this.Connect2Simulator.UseVisualStyleBackColor = true;
            this.Connect2Simulator.Click += new System.EventHandler(this.Connect2Simulator_Click_1);
            // 
            // SimulatorsCheckPoint
            // 
            this.SimulatorsCheckPoint.DisplayMember = "未检测到头为sunborn的包体";
            this.SimulatorsCheckPoint.FormattingEnabled = true;
            this.SimulatorsCheckPoint.Items.AddRange(new object[] {
            "7555(MuMu)",
            "62001(夜神)",
            "26944(海马)",
            "21503(逍遥)",
            "6555(天天)",
            "5555(雷电)"});
            this.SimulatorsCheckPoint.Location = new System.Drawing.Point(12, 270);
            this.SimulatorsCheckPoint.Name = "SimulatorsCheckPoint";
            this.SimulatorsCheckPoint.Size = new System.Drawing.Size(208, 25);
            this.SimulatorsCheckPoint.TabIndex = 14;
            this.SimulatorsCheckPoint.Text = "7555(MuMu)";
            this.SimulatorsCheckPoint.ValueMember = "未检测到头为sunborn的包体";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 250);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "选择要连接的模拟器";
            // 
            // ApkPush_Tool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 359);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SimulatorsCheckPoint);
            this.Controls.Add(this.Connect2Simulator);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BundleNames);
            this.Controls.Add(this.CmdInfoWin);
            this.Controls.Add(this.ReadOnlyPathPushButton);
            this.Controls.Add(this.pushButton);
            this.Controls.Add(this.TarCommandAndPush);
            this.Controls.Add(this.Devices);
            this.Controls.Add(this.ChooseFileButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(732, 318);
            this.Name = "ApkPush_Tool";
            this.Text = "ApkPush_Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.OpenFileDialog selectApk;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button ChooseFileButton;
        public System.Windows.Forms.Button TarCommandAndPush;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button pushButton;
        private System.Windows.Forms.OpenFileDialog selectRAR;
        private System.Windows.Forms.Timer CheckDevices;
        public System.Windows.Forms.Button ReadOnlyPathPushButton;
        public System.Windows.Forms.TextBox CmdInfoWin;
        private System.Windows.Forms.ComboBox BundleNames;
        public System.Windows.Forms.ComboBox Devices;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button Connect2Simulator;
        public System.Windows.Forms.ComboBox SimulatorsCheckPoint;
        public System.Windows.Forms.Label label5;
    }
}

