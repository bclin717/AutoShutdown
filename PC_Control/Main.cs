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
        private Shutdown shutdownProcessWhenTimeIsUp = new Shutdown(0);
        private const int usingPCTime = 60*60;
        private const int pcUsedTimeStartAt = 8;
        private const int pcUsedTimeEndAt = 23;

        private void Main_Load(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        public Main() {
            runWhenStart();
            InitializeComponent();
            checkAndRunProgram();
        }

        private void checkAndRunProgram() {
            if (!checkIfKeyExist()) {
                initToCheckEveryThing();
            } else {
                MessageBox.Show("KEY IS FOUND!");
            }
        }

        private Boolean checkIfKeyExist() {
            return File.Exists("E:\\pas.pcc");
        }

        private void initToCheckEveryThing() {
            if (checkIfTimeIsUp()) {
                shutdownProcessWhenTimeIsUp.start();
            } else if (checkIfNotFirstTimeUsingPCToday()) {
                shutdownProcessWhenTimeIsUp.start();
            } else {
                setShutdownSchedule(returnTheShorterTime());
            }
        }

        private void setShutdownSchedule(int lastTime) {
            shutdownProcessWhenTimeIsUp.setTimeAndRestart(lastTime);
        }

        private int returnTheShorterTime() {
            return howManySecondsLastFrom22() < usingPCTime ? howManySecondsLastFrom22() : usingPCTime;
        }

        private int howManySecondsLastFrom22() {
            DateTime targetTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 0, 0);
            DateTime nowTime = DateTime.Now;
            TimeSpan lastTime = targetTime - nowTime;
            return (int) lastTime.TotalSeconds;
        }

        private Boolean checkIfNotFirstTimeUsingPCToday() {
            checkAndCreateDatelog();
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

        private void checkAndCreateDatelog() {
            if (!File.Exists("dateLog.pcc")) {
                createAndWriteDatelog();
            }
        }

        private void createAndWriteDatelog() {
            File.Create("dateLog.pcc").Close();
            File.WriteAllText("dateLog.pcc", "00");
        }

        private Boolean checkIfTimeIsUp() {
            if (DateTime.Now.Hour >= pcUsedTimeEndAt || DateTime.Now.Hour < pcUsedTimeStartAt) {
                return true;
            }
            return false;
        }
    }
}
