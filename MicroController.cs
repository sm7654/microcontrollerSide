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

        public static void setUI(CommunicaionForm form)//
        {
            UI = form;
            UI.GetLabel().Text = code;
            UserStatus Control = new UserStatus(true, "Connected To Server");
            Control.SetRemoteEndPoint(controller.RemoteEndPoint.ToString());
            UI.GetDialogPanel().Controls.Add(Control);

        }





        public static void VideoCasting()
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







        public static void ListenTo200Code()
        {
            try
            {
                byte[] bytes = new byte[1024];
                int bytesRead = controller.Receive(bytes);
                byte[] bytes1 = new byte[int.Parse(Encoding.UTF8.GetString(bytes, 0, bytesRead))];
                controller.Receive(bytes1);
                string[] Status = RsaEncryption.Decrypt(bytes1).Split(';');


                string returnCode = Status[1];


                if (returnCode == "200")
                {
                    UI.BeginInvoke(new Action(() =>
                    {
                        UI.CLientIsOnline();
                        UserStatus Control = new UserStatus(true, "Client Connected!");
                        Control.SetRemoteEndPoint(Status[2]);
                        UI.GetDialogPanel().Controls.Add(Control);

                        byte[] AESKey = new byte[128];
                        int bytesread = controller.Receive(AESKey);


                        byte[] AESIv = new byte[128];
                        bytesread = controller.Receive(AESIv);

                        AesEncryption.Addkeys(AESKey, AESIv);

                        clientConnected = true;
                        new Thread(() => StartClientCommunication_recv()).Start();


                    }));

                    return;
                } else if (returnCode == "Shut")
                {


                    controller.Close();
                    UI.BeginInvoke(new Action(() =>
                    {
                        UI.Close();
                        
                    }));
                    ClosingController.btnExit_Click();
                }
            }
            catch (Exception e) { controller.Close(); }
        }





	/* 
		this line was writen using linux os!!!!
	*/



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
                    controller.ReceiveTimeout = 10000;
                    controller.Receive(buffer);


                    PipeMessageRec("got the message");
                    if (buffer.Length >= ServerRole.Length)
                    {
                        try
                        {
                            byte[] data = RsaEncryption.DecryptToByte(buffer);
                            PipeMessageRec("encrypted rsa");
                            if (data.Take(ServerRole.Length).SequenceEqual(ServerRole))
                            {
                                if (Is200Mesgae(data))
                                {
                                    return;
                                }
                                PipeMessageRec("Server message");
                                new Thread(() => ServerRelatedMessages(data)).Start();
                            }
                            else
                            {
                                PipeMessageRec("Client message");
                                new Thread(() => ClientRelatedMessages(data)).Start();
                            }
                        }
                        catch (Exception e) { PipeMessageRec("Client message"); new Thread(() => ClientRelatedMessages(buffer)).Start(); }
                    }
                    else
                    {
                        PipeMessageRec("cleint message");

                        new Thread(() => ClientRelatedMessages(buffer)).Start();
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    
        public static void SendToClient(string data)
        {
            SendToClient(Encoding.UTF8.GetBytes(data));
        }
        public static void SendToClient(byte[] data)
        {
            if (!clientConnected)
                return;
            
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

        private static bool Is200Mesgae(byte[] data)
        {
            string[] bytes = Encoding.UTF8.GetString(data).Split(';');
            if (bytes[1] == "200")
            {
                ServerRelatedMessages(data);
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
                    UI.BeginInvoke(new Action(() =>
                    {
                        UI.GetDialogPanel().Controls.Add(userStatus);
                    }));

                }
                else if (bytes[1] == "200")
                {
                    UI.BeginInvoke(new Action(() =>
                    {
                        UI.CLientIsOnline();
                        UserStatus Control = new UserStatus(true, "Client Connected!");
                        Control.SetRemoteEndPoint(bytes[2]);
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
                else if (bytes[1] == "Shut")
                {
                    controller.Close();
                    UI.BeginInvoke(new Action(() =>
                    {
                        UI.Close();

                    }));
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
                
                if (message[0] == "ERROR")
                {
                    switch (message[1])
                    {
                        



                        default: break;
                    }

                }
                else if (message[0] == "SUCCESS")
                {
                    switch (message[1])
                    {

                        
                        default: break;
                    }
                }
                else
                {
                    switch (message[0])
                    {
                        case "NEWXPERIMENT":
                            PipeMessageRec($"Got {message[0]} {message[1]}");
                            ExperimentController.NewExperiment(message);
                            SendToClient("EXPERREACHED");

                            break;

                        default: break;
                    }
                }
                UI.BeginInvoke(new Action(() =>
                {
                    UI.EnableSendAction();
                }));
            }
            catch (Exception e)
            { }
        }


        public static void PipeMessageRec(string g)
        {
            
            UI.BeginInvoke(new Action(() =>
            {
                UI.GG(g);
            }));
        }

        public static void SendToServer(byte[] data)
        {
            data = RsaEncryption.EncryptToServer(ServerRole.Concat(data).ToArray());

            controller.Send(Encoding.UTF8.GetBytes(data.Length.ToString()));

            Thread.Sleep(400);
            controller.Send(data);
        }


        public static void DisconnectFromServer()
        {


            byte[] disMessage = Encoding.UTF8.GetBytes("&303");

            





            //303 --> disconnected code
            //controller.Send("303");
        }


    }
}
