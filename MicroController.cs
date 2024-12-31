using ServerSide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace microcontrollerSide
{
    public class MicroController
    {
        private static Socket controller;
        private static string code;
        private static CommunicaionForm UI;
        public MicroController(Socket controllerSock, string Roomcode)
        {
            controller = controllerSock;
            code = Roomcode;
            new Thread(() => ListenTo200Code()).Start();
            //this.form.GetroomCodeLabel().Text = "RoomCode: " + code;
        }
        public MicroController(Socket controllerSock)
        {
            controller = controllerSock;
        }

        public void setUI(CommunicaionForm form)//
        {
            UI = form;
            UI.GetLabel().Text = code;
        }







        public void ListenTo200Code()
        {
            try
            {
                byte[] bytes = new byte[1024];
                int bytesRead = controller.Receive(bytes);
                byte[] bytes1 = new byte[int.Parse(Encoding.UTF8.GetString(bytes,0,bytesRead))];
                controller.Receive(bytes1);
                string[] Status = RsaEncryption.Decrypt(bytes1).Split(';');


                string returnCode = Status[0];


                if (returnCode == "200")
                {
                    UI.BeginInvoke(new Action(() =>
                    {
                        UI.CLientIsOnline();
                        UserStatus Control = new UserStatus(true);
                        Control.SetRemoteEndPoint(Status[1]);
                        UI.GetDialogPanel().Controls.Add(Control);

                    }));

                    return;
                } else if (returnCode == "Shut")
                {


                    controller.Close();
                    UI.BeginInvoke(new Action(() =>
                    {
                        UI.Close();
                        ClosingController.btnExit_Click();
                    }));
                }
            }
            catch (Exception e) { controller.Close(); }
        }

    }
}
