using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ADBPushApkTool
{
    public partial class ApkPush_Tool : Form
    {
        protected string noBundle = "未检测到头为sunborn的包体";
        protected string noDevice = "未检测到设备";
        protected Thread back_Refresh;
        protected ThreadStart back_RefreshStart;
        protected MatchCollection names;
        protected bool needCheck = true;
        protected bool inSimulator = false;
        public static ApkPush_Tool apkPush_Tool;
        public List<string> devicesID = new List<string>();
        public Dictionary<string, string> devices = new Dictionary<string, string>();

        public ApkPush_Tool()
        {
            InitializeComponent();
            InitComponent();
            //图个方便 ，把跨线程检测关了
            Control.CheckForIllegalCrossThreadCalls = false;
            InitThread();
            apkPush_Tool = this;
            back_Refresh.Start();
            Application.ApplicationExit += (sender,e) => { KillAllProcess(); };
        }
        /// <summary>
        /// 初始化一下线程
        /// </summary>
        public void InitThread()
        {
            back_RefreshStart = new ThreadStart(() => 
            {
                while (needCheck)
                {
                    Thread.Sleep(1000);
                    RefreshDevices();
                    if (devicesID.Count != 0)
                        GetBundleNames();
                }
            });

            back_Refresh = new Thread(back_RefreshStart);
            back_Refresh.IsBackground = true;
        }
        /// <summary>
        /// 用于获取主窗实例
        /// </summary>
        public static ApkPush_Tool GetFormInfo()
        {
            return apkPush_Tool;
        }
        /// <summary>
        /// Install按钮被按下
        /// </summary>
        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            if (selectApk.ShowDialog() == DialogResult.OK)
            {
                CmdInfoWin.Text = CmdCommandCenter.DoSimpleCommand("adb.exe", $"-s {devicesID[Devices.SelectedIndex]} install {selectApk.FileName}",-1,true);
            }
        }

        /// <summary>
        /// 选择分包解压按钮
        /// </summary>
        private void TarCommandAndPush_Click(object sender, EventArgs e)
        {
            TarFileCommand();
        }

        /// <summary>
        /// PushButton被按下
        /// </summary>
        private void pushButton_Click(object sender, EventArgs e)
        {
            PushVoid();
        }
        /// <summary>
        /// 固定路径Push 写死的路径方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadOnlyPathPushButton_Click(object sender, EventArgs e)
        {
            TarFileCommand();
            var filePath = selectRAR.FileName.Replace(selectRAR.SafeFileName, "");
            var bundlePath = filePath + "bundles\\.";
            var mediaPath = filePath + "media\\.";
            CmdInfoWin.Text = CmdCommandCenter.DoSimpleCommand("adb.exe", $"-s {devicesID[Devices.SelectedIndex]} push {bundlePath} /sdcard/Android/data/{BundleNames.SelectedItem}/files/bundles",-1,true);
            CmdInfoWin.Text = CmdCommandCenter.DoSimpleCommand("adb.exe", $"-s {devicesID[Devices.SelectedIndex]} push {mediaPath} /sdcard/Android/data/{BundleNames.SelectedItem}/files/media",-1,true);
        }
        /// <summary>
        /// Push的具体执行方法
        /// </summary>
        private void PushVoid()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                CmdInfoWin.Text =  CmdCommandCenter.DoSimpleCommand("adb.exe", $"-s {devicesID[Devices.SelectedIndex]} push {folderBrowserDialog1.SelectedPath} /sdcard/Android/data/{BundleNames.SelectedItem}/files",-1,true);
            }
        }

        private void TarFileCommand()
        {
            if (selectRAR.ShowDialog() == DialogResult.OK)
            {
                if (selectRAR.SafeFileName.Contains(" "))
                {
                    MessageBox.Show("选择的压缩包中不可带有空格!");
                    return;
                }
                //解压指定文件
                TarTargetFile(selectRAR);
                folderBrowserDialog1.SelectedPath = selectRAR.FileName.Replace(selectRAR.SafeFileName, "");
            
            }
        }
        /// <summary>
        /// 解压目标的方法
        /// </summary>
        private void TarTargetFile(OpenFileDialog target)
        {
            var path = target.FileName.Replace(target.SafeFileName, "");
            var command = $"tar -zxvf {target.SafeFileName}";
            CmdInfoWin.Text = CmdCommandCenter.DoSimpleCommand("cmd.exe", $"cd /d {path}", command,3000,false);
        }
        /// <summary>
        /// 时钟时间刷新设备信息
        /// todo = > 用协程
        /// </summary>
        protected void RefreshDevices()
        {
            //label2.Text = CmdCommandCenter.CheckDeviceItemInfo();
            CmdCommandCenter.CheckDeviceItemInfo();
            AddDevicesID();
            SetSelectedDevice();
            label2.Text = devicesID.Count > 0 ? $"检测到{devicesID.Count}个设备":"没检测到设备";
        }
        /// <summary>
        /// 用于显示下拉框中目前选中的设备
        /// </summary>
        protected void SetSelectedDevice()
        {
            if (CmdCommandCenter.devices.Count != Devices.Items.Count)
            {
                Devices.Items.Clear();
            }
            if (devices.Count == 0 && Devices.Items.Count == 0)
            {
                if (!Devices.Items.Contains(noDevice))
                {
                    Devices.Items.Add(noDevice);
                }
                Devices.SelectedItem = noDevice;
                return;
            }
            foreach (var item in devices)
            {
                var comboName = item.Key + $"({item.Value})";
                if (Devices.Items.Contains(comboName))
                {
                    continue;
                }
                else
                {
                    if (Devices.Items.Contains(noDevice))
                    {
                        Devices.Items.Remove(noDevice);
                    }
                    Devices.Items.Add(comboName);
                    Devices.SelectedItem = comboName;
                }
            }
            
            //if (Devices.SelectedItem == null)
            //{
            //    if (!Devices.Items.Contains(noDevice))
            //    {
            //        Devices.Items.Add(noDevice);
            //    }
            //    Devices.SelectedItem = noDevice;
            //}
        }
        /// <summary>
        /// 用于设置BundleCombox下拉框的可选包体选项
        /// </summary>
        protected void GetBundleNames()
        {
            names = Commands.GetPackagesNameList();
            if (names == null)
                return;
            if (names.Count != BundleNames.Items.Count)
            {
                BundleNames.Items.Clear();
            }
            
            for (int i = 0; i < names.Count; i++)
            {
                if (BundleNames.Items.Contains(names[i].Value))
                {
                    continue;
                }
                else
                {
                    if (BundleNames.Items.Contains(noBundle))
                    {
                        BundleNames.Items.Remove(noBundle);
                    }
                    BundleNames.Items.Add(names[i].Value);
                    BundleNames.SelectedItem = names[i].Value;
                }
            }
            
            if (BundleNames.SelectedItem == null)
            {
                if (!BundleNames.Items.Contains(noBundle))
                {
                    BundleNames.Items.Add(noBundle);
                }
                BundleNames.SelectedItem = noBundle;
            }
            
        }

        private void Devices_SelectedIndexChanged(object sender, EventArgs e)
        {
            BundleNames.Items.Clear();
            BundleNames.Text = noBundle;
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            inSimulator = !inSimulator;
        }
        private void InitComponent()
        {
            BundleNames.SelectedItem = noBundle;
            Devices.SelectedItem = noDevice;
            SimulatorsCheckPoint.SelectedIndex = 0;
        }

        private void CmdInfoWin_TextChanged(object sender, EventArgs e)
        {
            CmdInfoWin.SelectionStart = CmdInfoWin.Text.Length;
            CmdInfoWin.ScrollToCaret();
        }

        private void AddDevicesID()
        {
            devicesID.Clear();
            devices = CmdCommandCenter.devices;
            foreach (var item in devices)
            {
                if (devicesID.Contains(item.Key))
                {
                    continue;
                }
                devicesID.Add(item.Key);
            }
        }
        public void KillAllProcess()
        {
            Process[] pro = Process.GetProcesses();//获取已开启的所有进程

            //遍历所有查找到的进程

            for (int i = 0; i < pro.Length; i++)

            {
                var proName = pro[i].ProcessName.ToString().ToLower();
                if (proName == "adb")

                {
                    pro[i].Kill();//结束进程
                }
            }
        }

        private void Connect2Simulator_Click_1(object sender, EventArgs e)
        {
            Commands.Connect2Simulator();
        }
    }

}
