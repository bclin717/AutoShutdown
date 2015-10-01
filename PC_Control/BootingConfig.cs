using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace PC_Control {
    public partial class Main {
        private void runWhenStart() {
            try {
                string app_name = "PC_CONTROL";
                string R_startPath = Application.ExecutablePath;
                RegistryKey aimdir = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (aimdir.GetValue(app_name) != null) {
                    aimdir.DeleteValue(app_name, false);
                }
                aimdir.SetValue(app_name, R_startPath);
                aimdir.Close();
            } catch (Exception ex) {
                Console.WriteLine("登錄檔寫入失敗:" + ex.Message);
            }
        }
    }
}
