using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ADBPushApkTool
{
    public static class Commands
    {
        private static ApkPush_Tool mainForm;

        static Commands()
        {
            mainForm = ApkPush_Tool.GetFormInfo();
        }
        public static MatchCollection GetPackagesNameList()
        {
            var deviceName = Regex.Replace(mainForm.Devices.SelectedItem.ToString(), @"\(.*\)", "");
            var log = CmdCommandCenter.DoSimpleCommand("cmd.exe",$"adb -s {deviceName} shell","pm list packages -3",3000,false);
            var packNames = Regex.Matches(log, @"com\.sunborn.*(?=\r)");
            return packNames;
        }
        public static void Connect2Simulator()
        {
            string error = string.Empty;
            var simulator = CmdCommandCenter.DoSimpleCommand("adb.exe", "connect 127.0.0.1:7555", 5000, false);
            if (simulator.Contains("already"))
            {
                error = CmdCommandCenter.DoSimpleCommand("adb.exe","devices",5000,false);
            }
            if (error.Contains("offline"))
            {
                mainForm.CmdInfoWin.Text = "MuMu模拟器已离线";
                return;
            }
            mainForm.CmdInfoWin.Text = simulator;
        }
    }
}
