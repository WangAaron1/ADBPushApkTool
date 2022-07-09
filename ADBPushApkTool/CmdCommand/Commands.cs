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
            var log = CmdCommandCenter.DoSimpleCommand("cmd.exe",$"adb -s {mainForm.Devices.SelectedItem} shell","pm list packages -3",2000,false);
            var packNames = Regex.Matches(log, @"com\.sunborn.*(?=\r)");
            return packNames;
        }
    }
}
