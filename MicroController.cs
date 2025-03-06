using ServerSide;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace microcontrollerSide
{
    static class MicroController
    {
        private static Socket controller;
        private static string code;
        private static byte[] EncryptedToServerCode;
        private static bool ClientVideoRequest;
        private static CommunicaionForm UI;
        private static byte[] ServerRole = Encoding.UTF8.GetBytes("%%ServerRelatedMessage%%");
        private static bool clientConnected = false;
        private static object communicationLock = new object();

        public static bool IsClientConnected()
        {
            return clientConnected;
        }
        // Initialization and Setup Methods
        public static void SetMicroController(Socket controllerSock, string Roomcode)
        {
            controller = controllerSock;
            code = Roomcode;
            EncryptedToServerCode = RsaEncryption.EncryptToServer(Encoding.UTF8.GetBytes(code));
            ClientVideoRequest = false;

            new Thread(() => ListenTo200Code()).Start();
        }

        public static void SetMicroController(Socket controllerSock)
        {
            controller = controllerSock;
        }

        public static void SetUI(CommunicaionForm form)
        {
            UI = form;
            UI.GetLabel().Text = code;
            UserStatus Control = new UserStatus(true, "Connected To Server");
            Control.SetRemoteEndPoint(controller.RemoteEndPoint.ToString());
            UI.GetDialogPanel().Controls.Add(Control);
        }

        // Communication & Encryption Methods
        public static void SendToClient(string data)
        {
            SendToClient(Encoding.UTF8.GetBytes(data));
        }
        
        public static void SendToClient(byte[] data)
        {
            lock (communicationLock)
            {
                if (!clientConnected)
                    return;

                try
                {
                    byte[] EncryptedData = AesEncryption.EncryptedData(data);
                    controller.Send(Encoding.UTF8.GetBytes(EncryptedData.Length.ToString()));
                    Thread.Sleep(250);
                    controller.Send(EncryptedData);
                }
                catch (Exception e)
                {
                    return;
                }
            }
        }

        public static void SendToServer(string dataStr)
        {
            lock (communicationLock)
            {
                byte[] data = Encoding.UTF8.GetBytes($"{dataStr}");
                data = RsaEncryption.EncryptToServer(ServerRole.Concat(data).ToArray());
                controller.Send(Encoding.UTF8.GetBytes(data.Length.ToString()));

                Thread.Sleep(400);
                controller.Send(data);
            }
        }

        public static void DisconnectFromServer()
        {
            SendToServer("&303");
        }

        // Video Methods (Commented Out)
        public static void VideoCasting()
        {
            /*
            while (ClientVideoRequest){
                var source = Camera output;
                byte[] videobyte = sourse.getVideo(2048); // get 2048 chunks of video bytes
                videobyte = AES.encrypt(videobytes);
                byte[] FullyEncryptedVideo = EncryptedToServerCode + videobytes;
                UdpServer.SendTo(FullyEncryptedVideo, (serverIP, 64000));
            }
            */
        }

        // Communication Flow Methods
        private static void GetAESkeysANdStaertCommunication(string remoteEndPoint)
        {
            UI.BeginInvoke(new Action(() =>
            {
                UI.CLientIsOnline();
                UserStatus Control = new UserStatus(true, "Client Connected!");
                Control.SetRemoteEndPoint(remoteEndPoint);
                UI.GetDialogPanel().Controls.Add(Control);

                byte[] AESKey = new byte[128];
                int bytesread = controller.Receive(AESKey);

                byte[] AESIv = new byte[128];
                bytesread = controller.Receive(AESIv);

                AesEncryption.Addkeys(AESKey, AESIv);

                clientConnected = true;
                new Thread(() => StartClientCommunication_recv()).Start();
            }));
        }

        private static void ListenTo200Code()
        {
            try
            {
                byte[] bytes = new byte[1024];
                int bytesRead = controller.Receive(bytes);
                byte[] bytes1 = new byte[int.Parse(Encoding.UTF8.GetString(bytes, 0, bytesRead))];
                controller.Receive(bytes1);
                string[] Status = RsaEncryption.Decrypt(bytes1).Split('&');
                string returnCode = Status[1];

                if (returnCode == "200")
                {
                    GetAESkeysANdStaertCommunication(Status[2]);
                    return;
                }
                else if (returnCode == "Shut")
                {
                    controller.Close();
                    UI.BeginInvoke(new Action(() => { UI.Close(); }));
                    ClosingController.btnExit_Click();
                }
            }
            catch (Exception e)
            {
                controller.Close();
            }
        }

        private static void StartClientCommunication_recv()
        {
            while (controller.Connected && clientConnected)
            {
                try
                {
                    bool Server = false;
                    byte[] buffer = new byte[1024];
                    controller.ReceiveTimeout = 0;
                    int byteRec = controller.Receive(buffer);
                    buffer = new byte[int.Parse(Encoding.UTF8.GetString(buffer, 0, byteRec))];
                    controller.Receive(buffer);

                    if (buffer.Length >= ServerRole.Length)
                    {
                        try
                        {
                            byte[] data = RsaEncryption.DecryptToByte(buffer);
                            if (data.Take(ServerRole.Length).SequenceEqual(ServerRole))
                            {
                                if (Is200Mesgae(data))
                                {
                                    return;
                                }
                                new Thread(() => ServerRelatedMessages(data)).Start();
                            }
                            else
                            {
                                new Thread(() => ClientRelatedMessages(data)).Start();
                            }
                        }
                        catch (Exception e)
                        {
                            new Thread(() => ClientRelatedMessages(buffer)).Start();
                        }
                    }
                    else
                    {
                        new Thread(() => ClientRelatedMessages(buffer)).Start();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // Message Handling Methods
        private static bool Is200Mesgae(byte[] data)
        {
            string[] bytes = Encoding.UTF8.GetString(data).Split('&');
            if (bytes[1] == "200")
            {
                GetAESkeysANdStaertCommunication(bytes[2]);
                return true;
            }
            return false;
        }

        private static void ServerRelatedMessages(byte[] data)
        {
            try
            {
                string[] bytes = Encoding.UTF8.GetString(data).Split('&');

                if (bytes[1] == "302")
                {
                    clientConnected = false;
                    UserStatus userStatus = new UserStatus(false, "Client disconnected");
                    UI.BeginInvoke(new Action(() => { UI.GetDialogPanel().Controls.Add(userStatus); }));
                }
                else if (bytes[1] == "200")
                {
                    // Additional processing for 200 code if necessary
                }
                else if (bytes[1] == "Shut")
                {
                    controller.Close();
                    UI.BeginInvoke(new Action(() => { UI.Close(); }));
                    ClosingController.btnExit_Click();
                }
            }
            catch (Exception e) { }
        }

        private static void ClientRelatedMessages(byte[] data)
        {
            try
            {
                string[] message = Encoding.UTF8.GetString(AesEncryption.DecryptData(data)).Split(';');
                Console.WriteLine(Encoding.UTF8.GetString(AesEncryption.DecryptData(data)));

                switch (message[0])
                {
                    case "NEWXPERIMENT":

                        Console.WriteLine("detected new exper");
                        ExperimentController.NewExperiment(message);
                        break;

                    default: break;
                }
            }
            catch (Exception e) { }
        }

        // UI Helper Methods
        public static void PipeMessageRec(string g)
        {
            UI.BeginInvoke(new Action(() => { UI.GG(g); }));
        }
    }
}
