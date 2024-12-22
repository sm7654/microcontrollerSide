using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    static class ClosingController
    {
        public static void btnExit_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("microontrollerSide");
            foreach (Process process in processes)
            {
                try { process.Kill(); } catch { }
            }
        }
    }
}
