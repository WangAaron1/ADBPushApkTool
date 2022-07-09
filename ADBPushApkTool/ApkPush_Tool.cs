using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ADBPushApkTool
{
    public partial class ApkPush_Tool : Form
    {
        private string noBundle = "未检测到头为sunborn的包体";
        private string noDevice = "未检测到设备";
        private string mumuPath = "127.0.0.1:7555";
        public static ApkPush_Tool apkPush_Tool;
        private Thread back_Refresh;
        private ThreadStart back_RefreshStart;
        private bool needCheck = true;
        private bool inSimulator = false;
        public List<string> devices = new List<string>();
        private MatchCollection names;
        public ApkPush_Tool()
        {
            InitializeComponent();
            InitComponent();
            //图个方便 ，把跨线程检测关了
            Control.CheckForIllegalCrossThreadCalls = false;
            InitThread();
            apkPush_Tool = this;
            back_Refresh.Start();
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
                    Thread.Sleep(3000);
                    RefreshDevices();
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
                CmdInfoWin.Text = CmdCommandCenter.DoSimpleCommand("adb.exe", $"-s {Devices.SelectedItem} install {selectApk.FileName}",-1,true);
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
            CmdInfoWin.Text = CmdCommandCenter.DoSimpleCommand("adb.exe", $"-s {Devices.SelectedItem} push {selectRAR.FileName.Replace(selectRAR.SafeFileName,"")}bundles/ /sdcard/Android/data/{BundleNames.SelectedItem}/files/",-1,true);
        }
        /// <summary>
        /// Push的具体执行方法
        /// </summary>
        private void PushVoid()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                CmdInfoWin.Text =  CmdCommandCenter.DoSimpleCommand("adb.exe", $"-s {Devices.SelectedItem} push {folderBrowserDialog1.SelectedPath} /sdcard/Android/data/{BundleNames.SelectedItem}/files/",-1,true);
            
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
        private void RefreshDevices()
        {
            //label2.Text = CmdCommandCenter.CheckDeviceItemInfo();
            CmdCommandCenter.CheckDeviceItemInfo();
            SetSelectedDevice();
            label2.Text = Devices.Items.Count > 0 ? $"检测到{Devices.Items.Count}个设备":"没检测到设备";
        }
        /// <summary>
        /// 用于显示下拉框中目前选中的设备
        /// </summary>
        private void SetSelectedDevice()
        {
            if (CmdCommandCenter.GetDevices().Count != Devices.Items.Count)
            {
                Devices.Items.Clear();
            }
            devices = CmdCommandCenter.GetDevices();
            for (int i = 0; i < devices.Count; i++)
            {
                if (Devices.Items.Contains(devices[i]))
                {
                    continue;
                }
                else
                {
                    if (Devices.Items.Contains(noDevice))
                    {
                        Devices.Items.Remove(noDevice);
                    }
                    Devices.Items.Add(devices[i]);
                    Devices.SelectedItem = devices[i];
                }
            }
            
            if (Devices.SelectedItem == null)
            {
                if (!Devices.Items.Contains(noDevice))
                {
                    Devices.Items.Add(noDevice);
                }
                Devices.SelectedItem = noDevice;
            }
        }
        private void GetBundleNames()
        {
            if (Commands.GetPackagesNameList().Count != BundleNames.Items.Count)
            {
                BundleNames.Items.Clear();
            }
            names = Commands.GetPackagesNameList();
            
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
        }

        private void ConnectSimulator_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            inSimulator = !inSimulator;
            CmdCommandCenter.needSimulator = !CmdCommandCenter.needSimulator;
        }
        private void InitComponent()
        {
            BundleNames.SelectedItem = noBundle;
            Devices.SelectedItem = noDevice;
        }

        private void CmdInfoWin_TextChanged(object sender, EventArgs e)
        {
            CmdInfoWin.SelectionStart = CmdInfoWin.Text.Length;
            CmdInfoWin.ScrollToCaret();
        }
    }

}
