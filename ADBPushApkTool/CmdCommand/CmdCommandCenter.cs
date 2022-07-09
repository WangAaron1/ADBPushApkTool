using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ADBPushApkTool
{
    public static class CmdCommandCenter
    {
        /// <summary>
        /// 后台指令台，生命周期非单个方法
        /// </summary>
        public static Process pro;
        public static List<string> devices = new List<string>();
        public static bool needSimulator = false;
        private static EventWaitHandle outputWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
        private static EventWaitHandle errorWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
        public static void InitProcess()
        {
             pro = new Process()
             {
                StartInfo = {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
             };
        }
        /// <summary>
        /// 全局Process开启
        /// </summary>
        public static void StartProcess()
        {
            pro.Start();
            pro.StandardInput.AutoFlush = true;
        }
        /// <summary>
        /// 全局Process关闭
        /// </summary>
        public static void CloseProcess()
        {
            pro.Close();
        }
        /// <summary>
        /// 输入指令
        /// </summary>
        public static void Cmd_WriteLine(string command)
        {
            pro.StandardInput.WriteLine(command);
        }
        public static string GetEndText()
        {
            return pro.StandardOutput.ReadToEnd();
        }
        public static void SetArgument(string command)
        {
            pro.StartInfo.Arguments = command;
        }
        public static void WaitForExit()
        {
            pro.WaitForExit();
        }


        /// <summary>
        /// 快速执行Cmd一段命令，生命周期很短
        /// </summary>
        public static string DoSimpleCommand(string exe, string command,int exitTime,bool isPush)
        {
            StringBuilder processOutputBuilder = new StringBuilder();
            Process adb = new Process()
            {
                StartInfo = {
                    FileName = exe,
                    Arguments = command,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };
            adb.EnableRaisingEvents = true;
            adb.OutputDataReceived += (sender, eventArgs) =>
            {
                if (eventArgs.Data != null)
                {
                    processOutputBuilder.AppendLine(eventArgs.Data); 
                    if (isPush)
                    {
                        ApkPush_Tool.apkPush_Tool.CmdInfoWin.Text = processOutputBuilder.ToString();
                    }
                }
                else
                {
                    outputWaitHandle.Set();
                }
            };

            adb.ErrorDataReceived += (sender, eventArgs) =>
            {
                if (eventArgs.Data != null)
                {
                    processOutputBuilder.AppendLine(eventArgs.Data); 
                    
                }
                else
                {
                    errorWaitHandle.Set();
                }
            };

            adb.Start();
            adb.StandardInput.AutoFlush = true;
            adb.StandardInput.WriteLine(command);
            adb.StandardInput.Close();
            adb.BeginOutputReadLine();
            adb.BeginErrorReadLine();
            if (isPush)
            {
                adb.WaitForExit();
                adb.CancelOutputRead();
                adb.CancelErrorRead();
                adb.Kill();
                adb.Close();
            }
            else if (adb.WaitForExit(exitTime) && outputWaitHandle.WaitOne(exitTime) && errorWaitHandle.WaitOne(exitTime))
            {
                adb.CancelOutputRead();
                adb.CancelErrorRead();
                adb.Kill();
                adb.Close();
            }
            return processOutputBuilder.ToString();
        }
        /// <summary>
        /// 执行Cmd2段命令 快速执行
        /// </summary>
        public static string DoSimpleCommand(string exe, string command, string command2, int exitTime, bool isPush)
        {
            StringBuilder processOutputBuilder = new StringBuilder();
            Process adb = new Process()
            {
                StartInfo = {
                    FileName = exe,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,

                    CreateNoWindow = true
                }
            };
            adb.EnableRaisingEvents = true;
            adb.OutputDataReceived += (sender, eventArgs) =>
            {
                if (eventArgs.Data != null)
                {
                    processOutputBuilder.AppendLine(eventArgs.Data);
                    if (isPush)
                    {
                        ApkPush_Tool.apkPush_Tool.CmdInfoWin.Text = processOutputBuilder.ToString();
                    }
                }
                else
                {
                    outputWaitHandle.Set();
                }
            };

            adb.ErrorDataReceived += (sender, eventArgs) =>
            {
                if (eventArgs.Data != null)
                {
                    processOutputBuilder.AppendLine(eventArgs.Data);
                }
                else
                {
                    errorWaitHandle.Set();
                }
            };
            adb.Start();
            adb.StandardInput.AutoFlush = true;

            if (exe == "adb.exe")
            {
                adb.StartInfo.Arguments = command;
                adb.StartInfo.Arguments = command2;
            }
            else
            {
                adb.StandardInput.WriteLine(command);
                adb.StandardInput.WriteLine(command2);
            }
            adb.StandardInput.Close();

            adb.BeginOutputReadLine();
            adb.BeginErrorReadLine();
            if (isPush)
            {
                adb.WaitForExit();
                adb.CancelOutputRead();
                adb.CancelErrorRead();
                adb.Kill();
                adb.Close();
            }
            else if (adb.WaitForExit(exitTime) && outputWaitHandle.WaitOne(exitTime) && errorWaitHandle.WaitOne(exitTime))
            {
                adb.CancelOutputRead();
                adb.CancelErrorRead();
                adb.Kill();
                adb.Close();
            }

            return processOutputBuilder.ToString();
        }

        /// <summary>
        /// 确认下设备信息
        /// </summary>
        public static void CheckDeviceItemInfo()
        {
            if (devices.Count != 0 ) devices.Clear();
            var state = DoSimpleCommand("adb.exe", "devices",3000,false);
            if (needSimulator)
            {
                var simulator = DoSimpleCommand("adb.exe", "connect 127.0.0.1:7555", 3000,false);
                if (!simulator.Contains("already") && !simulator.Contains("failed to connect") && !ApkPush_Tool.apkPush_Tool.Devices.Items.Contains("127.0.0.1:7555"))
                {
                    devices.Add("127.0.0.1:7555");
                }
            }
            var se = Regex.Matches(state, @".*(?=(\tdevice\r\n))?");
            for (int i = 1; i < se.Count; i++)
            {
                if (se[i].Captures[0].Length != 0 && se[i].Captures[0].Length != 1 && !se[i].Captures[0].Value.Contains("offline"))
                {
                    devices.Add(Regex.Replace(se[i].Value, @"\t*device\r", ""));
                }
            }
        }
        /// <summary>
        /// 获取设备列表
        /// </summary>
        public static List<string> GetDevices()
        {
            return devices;
        }

    }
}
