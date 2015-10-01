using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_Control {
    public partial class Main : Form {
        private DateTime nowDateTime;
        private int CurrentHour;
        private Process process;

        public Main() {
            InitializeComponent();
            shutdownSettingPrepare();

            if (checkIfTimeIsUp()) {
                shutdown();
            }

            nowDateTime = new DateTime();

        }

        private void Main_Load(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private Boolean checkIfTimeIsUp() {
            if (DateTime.Now.Hour >= 22 || DateTime.Now.Hour <= 7) {
                return true;
            }
            return false;
        }

        private void shutdownSettingPrepare() {
            process = new Process();
            process.StartInfo.FileName = "shutdown.exe";
            process.StartInfo.Arguments = "-s -t 0";
        }

        private void shutdown() {
            process.Start();
            //MessageBox.Show("SHUTDOWN!");
        }

    }
}
