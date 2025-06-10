using microcontrollerSide;
using System;

namespace ServerSide
{
    static class ClosingController
    {
        public static void btnExit_Click()
        {
            ConnectionToElectronics.SendData("Kill;");
            Environment.Exit(0);
            
        }
    }
}
