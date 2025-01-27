using ServerSide;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
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
        private static byte[] EncryptedToServerCode;
        private static bool ClientVideoRequest;
        private static CommunicaionForm UI;
        private static byte[] ServerRole = Encoding.UTF8.GetBytes("%%ServerRelatedMessage%%");

        public MicroController(Socket controllerSock, string Roomcode)
        {
            controller = controllerSock;
            code = Roomcode;
            EncryptedToServerCode = RsaEncryption.EncryptToServer(Encoding.UTF8.GetBytes(code));
            ClientVideoRequest = false;


            new Thread(() => ListenTo200Code()).Start();
        }
        public MicroController(Socket controllerSock)
        {
            controller = controllerSock;
        }

        public void setUI(CommunicaionForm form)//
        {
            UI = form;
            UI.GetLabel().Text = code;
            UserStatus Control = new UserStatus(true, "Connected To Server");
            Control.SetRemoteEndPoint(controller.RemoteEndPoint.ToString());
            UI.GetDialogPanel().Controls.Add(Control);

        }





        public void VideoCasting()
        {
            /*
             while (ClientVideoRequest){
                var source = Camera output;
                byte[] videobyte = sourse.getVideo(2048); get 2048 chunks of video bytes
                videobyte = AES.encrypt(videobytes);
                byte[] FullyEncryptedVideo = EncryptedToServerCode + videobytes;
                UdpServer.SendTo(FullyEncryptedVideo, (serverIP, 64000) );
             }
             */
        }







        public void ListenTo200Code()
        {
            try
            {
                byte[] bytes = new byte[1024];
                int bytesRead = controller.Receive(bytes);
                byte[] bytes1 = new byte[int.Parse(Encoding.UTF8.GetString(bytes, 0, bytesRead))];
                controller.Receive(bytes1);
                string[] Status = RsaEncryption.Decrypt(bytes1).Split(';');


                string returnCode = Status[0];


                if (returnCode == "200")
                {
                    UI.BeginInvoke(new Action(() =>
                    {
                        UI.CLientIsOnline();
                        UserStatus Control = new UserStatus(true, "Client Connected!");
                        Control.SetRemoteEndPoint(Status[1]);
                        UI.GetDialogPanel().Controls.Add(Control);

                        byte[] AESKey = new byte[128];
                        int bytesread = controller.Receive(AESKey);


                        byte[] AESIv = new byte[128];
                        bytesread = controller.Receive(AESIv);

                        AesEncryption.Addkeys(AESKey, AESIv);

                        new Thread(() => StartClientCommunication_recv()).Start();


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


        private void StartClientCommunication_recv()
        {

            while (controller.Connected)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int byteRec = controller.Receive(buffer);

                    buffer = new byte[int.Parse(Encoding.UTF8.GetString(buffer, 0, byteRec))];
                    controller.Receive(buffer);

                    if (buffer.Length >= ServerRole.Length)
                    {
                        try
                        {
                            byte[] data = RsaEncryption.DecryptToByte(buffer);
                            if (data.Take(ServerRole.Length).SequenceEqual(ServerRole))
                                ServerRelatedMessages(data);
                        }
                        catch (Exception e) { }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    

        public static void SendToClient(byte[] data)
        {
            try
            {
                byte[] EncryptedData = AesEncryption.EncryptedData(data);
                controller.Send(Encoding.UTF8.GetBytes(EncryptedData.Length.ToString()));
                Thread.Sleep(200);
                controller.Send(EncryptedData);
            } catch (Exception e)
            {
                return;
            }
        }

        private void ServerRelatedMessages(byte[] data)
        {
            try
            {
                string[] bytes = Encoding.UTF8.GetString(data).Split(';');
                if (bytes[1] == "cDis") 
                {
                    UserStatus userStatus = new UserStatus(false, "Client disconnected");
                    UI.BeginInvoke(new Action(() =>
                    {
                        UI.GetDialogPanel().Controls.Add(userStatus);
                    }));
                        
                        
                }
            }
            catch (Exception e) { }
        }



        public static void DisconnectFromServer()
        {
            //303 --> disconnected code
            //controller.Send("303");
        }


    }
}
