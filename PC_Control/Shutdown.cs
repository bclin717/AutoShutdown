using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_Control {
    class Shutdown {
        private string shutdownMode = "-s";
        private Process shutdownProcess = new Process();
        private int untilShutdownTime = 0;

        public Shutdown(int untilShutdownTime) {
            this.untilShutdownTime = untilShutdownTime;
            shutdownSettingPrepare();
        }

        public void start() {
            shutdownProcess.Start();
        }

        private void shutdownSettingPrepare() {
            shutdownProcess.StartInfo.FileName = "shutdown.exe";
            shutdownProcess.StartInfo.Arguments = shutdownMode + " -f -t " + untilShutdownTime;
        }

        public void setTimeAndRestart(int untilShutdownTime) {
            this.untilShutdownTime = untilShutdownTime;
            prepareAndStart();
        }

        public void resetModeAndRestart(string mode) {
            recongnizeModeAndSetIt(mode);
            prepareAndStart();
        }

        private void prepareAndStart() {
            shutdownSettingPrepare();
            start();
        }

        private void recongnizeModeAndSetIt(string mode) {
            if (mode.Equals("s") || mode.Equals("S")) {
                this.shutdownMode = "-s";
            } else if (mode.Equals("r") || mode.Equals("R")) {
                this.shutdownMode = "-r";
            } else if (mode.Equals("a") || mode.Equals("A")) {
                this.shutdownMode = "-a";
            }
        }
    }
}
