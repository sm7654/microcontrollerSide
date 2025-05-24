using microcontrollerSide;
using System;

namespace ServerSide
{
    static class ClosingController
    {
        public static void btnExit_Click()
        {
            PipeStream.WriteToPipe("shut;");
            Environment.Exit(0);
            
        }
    }
}
