using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_Control {
    public partial class Main : Form {
        private Process shutdownProcess;
        private const int usingPCTime = 60*60*1000;

        private void Main_Load(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        public Main() {
            runWhenStart();
            InitializeComponent();
            startProgram();
        }

        private void startProgram() {
            if (!checkIfKeyExist()) {
                initialization();
            } else {
                MessageBox.Show("KEY IS FOUND!");
            }
        }

        private Boolean checkIfKeyExist() {
            return File.Exists("E:\\pas.pcc");
        }

        private void initialization() {
            shutdownSettingPrepare();
            checkEveryThing();
        }

        private void checkEveryThing() {
            if (checkIfTimeIsUp()) {
                shutdown();
            } else if (checkIfNotFirstTimeUsingPCToday()) {
                shutdown();
            } else {
                setAllTimer();
            }
        }

        private int howManyMinisecondsLastFrom22() {
            DateTime targetTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0);
            DateTime nowTime = DateTime.Now;
            TimeSpan lastTime = targetTime - nowTime;
            return (int) lastTime.TotalSeconds * 1000;
        }

        private Boolean checkIfNotFirstTimeUsingPCToday() {
            checkAndCreateDateLog();
            return chekcIfPCFileUsedTodayFromDatelog();
        }

        private Boolean chekcIfPCFileUsedTodayFromDatelog() {
            string lastBootingDate = File.ReadAllText("dateLog.pcc");
            if (lastBootingDate.Equals(DateTime.Now.Day.ToString())) {
                return true;
            } else {
                File.WriteAllText("dateLog.pcc", DateTime.Now.Day.ToString());
                return false;
            }
        }

        private void checkAndCreateDateLog() {
            if (!File.Exists("dateLog.pcc")) {
                File.Create("dateLog.pcc").Close();
                File.WriteAllText("dateLog.pcc", "00");
            }
        }

        private Boolean checkIfTimeIsUp() {
            if (DateTime.Now.Hour >= 23 || DateTime.Now.Hour <= 7) {
                return true;
            }
            return false;
        }

        private void shutdownSettingPrepare() {
            shutdownProcess = new Process();
            shutdownProcess.StartInfo.FileName = "shutdown.exe";
            shutdownProcess.StartInfo.Arguments = "-s -f -t 0";
        }

        private void shutdown() {
            //shutdownProcess.Start();
            MessageBox.Show("SHUT");
        }

        private void setAllTimer() {
            Timer.Interval = howManyMinisecondsLastFrom22();
            Timer.Enabled = true;

            Timer_TwoHour.Interval = usingPCTime;
            Timer_TwoHour.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e) {
            shutdown();
        }

        private void Timer_TwoHour_Tick(object sender, EventArgs e) {
            shutdown();
        }
    }
}
