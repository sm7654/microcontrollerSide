using ServerSide;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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
        private static CommunicaionForm UI;
        private static byte[] ServerRole = Encoding.UTF8.GetBytes("%%ServerRelatedMessage%%");
        private static bool clientConnected = false;
        private static object communicationLock = new object();

       
        public static void SetMicroController(Socket controllerSock)
        {
            controller = controllerSock;
            new Thread(ListenTo200Code).Start();  
            new Thread(KeepConnectionAlive).Start();
        }

        public static void SetUI(CommunicaionForm form)
        {
            UI = form;
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
                    byte[] EncryptedData = AesEncryption.EncryptedDataForClient(data);
                    controller.Send(Encoding.UTF8.GetBytes(EncryptedData.Length.ToString()));
                    Thread.Sleep(200);
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
            try
            {
                lock (communicationLock)
                {
                    byte[] data = Encoding.UTF8.GetBytes($"{dataStr}");
                    data = AesEncryption.EncryptedDataForServer(ServerRole.Concat(data).ToArray());
                    controller.Send(Encoding.UTF8.GetBytes(data.Length.ToString()));
                    Thread.Sleep(200);
                    controller.Send(data);
                }
            }
            catch (SocketException e)
            {
                controller.Close();
                UI.BeginInvoke(new Action(() =>
                {
                    UserStatus Control = new UserStatus(false, "Lost Connection With Server;");
                    Control.SetRemoteEndPoint("");

                    UI.GetDialogPanel().Controls.Add(Control);

                }));
            }
            catch (Exception e) { MessageBox.Show("\n\n\n\n\n\n\nhello"); }
        }
       
        // Communication Flow Methods
        private static void GetAESkeysAndStartCommunication(string remoteEndPoint)
        {
            try
            {
                lock (communicationLock)
                {
                    byte[] AESKey = new byte[128];

                    byte[] AESIv = new byte[128];


                    int bytesread = controller.Receive(AESKey);

                    bytesread = controller.Receive(AESIv);

                    AesEncryption.AddkeysForClient(AESKey, AESIv);
                }
                clientConnected = true;
                UI.BeginInvoke(new Action(() =>
                {
                    UserStatus Control = new UserStatus(true, "Client Connected!");
                    Control.SetRemoteEndPoint(remoteEndPoint);

                    UI.GetDialogPanel().Controls.Add(Control);

                }));
            }
            catch (Exception e) {  }

            new Thread(StartClientCommunication_recv).Start();
        }

        private static void ListenTo200Code()
        {

            while (true)
            {
                try
                {

                    controller.ReceiveTimeout = 5000;
                    byte[] bytes = new byte[1024];
                    int bytesRead = controller.Receive(bytes);
                    byte[] bytes1 = new byte[int.Parse(Encoding.UTF8.GetString(bytes, 0, bytesRead))];
                    controller.Receive(bytes1);
                    string[] Status = AesEncryption.DecryptToServerToString(bytes1).Split('&');
                    string returnCode = Status[1];

                    if (returnCode == "200")
                    {
                        GetAESkeysAndStartCommunication(Status[2]);
                        return;
                    }
                    else if (returnCode == "Shut")
                    {
                        controller.Close();
                        UI.BeginInvoke(new Action(() => { UI.Close(); }));
                        ClosingController.btnExit_Click();
                        break;
                    }
                    else { }
                }
                catch (SocketException ex) {
;
                    UI.BeginInvoke(new Action(() =>
                    {
                        UserStatus Control = new UserStatus(false, "Lost Connection With Server;");
                        Control.SetRemoteEndPoint("");

                        UI.GetDialogPanel().Controls.Add(Control);

                    }));
                    break;
                }
                catch (Exception ex) {  }
            }
            
        }

        private static void StartClientCommunication_recv()
        {
            try
            {
                while (controller.Connected && clientConnected)
                {
                    try
                    {
                        byte[] buffer = new byte[1024];
                        controller.ReceiveTimeout = 5000;
                        int byteRec = controller.Receive(buffer);
                        buffer = new byte[int.Parse(Encoding.UTF8.GetString(buffer, 0, byteRec))];
                        controller.Receive(buffer);

                        new Thread(() =>
                        {
                            if (buffer.Length >= ServerRole.Length)
                            {
                                try
                                {
                                    byte[] data = AesEncryption.DecryptDataForServer(buffer);
                                    if (data.Take(ServerRole.Length).SequenceEqual(ServerRole))
                                    {
                                        
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
                        }).Start();
                    }
                    catch (Exception e)
                    {
                        
                    }
                }
            }
            catch (Exception e) { }
        }

        

        private static void ServerRelatedMessages(byte[] data)
        {
            try
            {
                string[] bytes = Encoding.UTF8.GetString(data).Split('&');
                
                if (bytes[1] == "302")
                {
                    clientConnected = false;
                    UserStatus userStatus = new UserStatus(false, $"Client disconnected;New Code - {bytes[2]}");
                    UI.BeginInvoke(new Action(() => { 
                        UI.GetDialogPanel().Controls.Add(userStatus);
                        UI.SetNewCode(bytes[2]);
                    }));

                    new Thread(ListenTo200Code).Start();
                }
                else if (bytes[1] == "CHANGEKEYIV")
                {
                    AesEncryption.SetNewKeyAndIvForServer(bytes[2], bytes[3]);
                }
                else if (bytes[1] == "200")
                {
                    // Additional processing for 200 code if necessary
                }
                else if (bytes[1] == "Shut")
                {
                    controller.Close();
                    //PipeStream.DisconnetFromPipe();
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
                byte[] bytes = AesEncryption.DecryptDataForClient(data);
                string[] message = Encoding.UTF8.GetString(bytes).Split(';');

                switch (message[0])
                {
                    case "NEWXPERIMENT":

                        SendToPipeNewExperiment(message);
                        UserStatus Control = new UserStatus("New Expreriment");
                        Control.SetDetails($"Name: {message[1]}      Frequency: {message[2]}      Duration: {message[3]}");


                        UI.BeginInvoke(new Action(() => { UI.GetDialogPanel().Controls.Add(Control); }));
                        

                        break;
                    case "GotEnKeys":

                        AesEncryption.KeysChangesSuccessfuly();
                        break;
                    case "StopExper":
                        Control = new UserStatus("Experiment Stoped");
                        UI.BeginInvoke(new Action(() => { UI.GetDialogPanel().Controls.Add(Control); }));


                        PipeStream.WriteToPipe(message[0]);
                        break;
                    default: break;
                } 
            }
            catch (Exception e) 
            {
                MessageBox.Show($"error \n {e.Message}");
            }
        }

        public static void SendToPipeNewExperiment(string[] experimentString)
        {

            if (!(experimentString.Length > 0))
                return;
            try
            {
                string experName = experimentString[1]; // Fxperiment name
                string Frequency = experimentString[2]; // Frequency of engine
                string Duration = experimentString[3];
                Console.WriteLine($"{experName}{Frequency}");
                //form.GetCLientStatusLabel().Text = $"{experName}: {Frequency}";

                PipeStream.WriteToPipe($"NEWXPERIMENT;{experName};{Frequency};{Duration}");
                

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); ; return; }
        }
        public static void MicroChipCommunication(string name)
        {
            DateTime curentDate = DateTime.Now;
            DateTime dateOnly = curentDate.Date;
            Random random = new Random();
            string curentTime = dateOnly.ToString("yyyy-MM-dd");
            string deltaSpeed = random.Next(1, 100).ToString();
            string temp = random.Next(-30, 50).ToString();
            string cameraSpeed = random.Next(1, 100).ToString();
            string innerPressure = random.Next(10, 100).ToString();
            string humidity = random.Next(0, 100).ToString();
            string time = random.Next(1, 60).ToString();

            string[] firstNames = { "Aria", "Liam", "Olivia", "Noah", "Ava", "Ethan", "Sophia", "Mason", "Isabella", "Logan" };
            string[] lastNames = { "Silverwood", "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez" };

            string randomFirstName = firstNames[random.Next(firstNames.Length)];
            string randomLastName = lastNames[random.Next(lastNames.Length)];


            randomFirstName = name;



            string ExperResults = $"EXPERIMENT_RESULTS;Name:{randomFirstName}|;DeltaSpeed:{deltaSpeed}|m/s;Temperature:{temp}|°C;Camera Speed:{cameraSpeed}|fps;Pressure:{innerPressure}|kPa;Humidity:{humidity}|%;Duration:{time}|sec;Date:{curentTime}|";
            Console.WriteLine(ExperResults);
            SendToClient(ExperResults);

        }

        public static void DisconnectFromServer()
        {
            SendToServer("&303");
            controller.Close();
        }
        
        public static void KeepConnectionAlive()
        {
            Thread.Sleep(2000);
            try
            {
                while (controller.Connected)
                {
                    SendToServer("&Ping;");
                    Thread.Sleep(500);
                }
            }
            catch (Exception e) {  }
        }
        
        public static bool IsClientConnected()
        {
            return clientConnected;
        }


    }
}
