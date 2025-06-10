using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace microcontrollerSide
{
    static class ConnectionToElectronics
    {


        private static Socket GetConnectionFromRS;

        private static Socket receiveSocket;
        private static Socket sendSocket;
        private static bool running = false;

        private static string ExperName = "";

        private static IPEndPoint sendEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5010);
        private static IPEndPoint receiveEndPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 5020);


        public static bool Connect()
        {
            try
            {
                GetConnectionFromRS = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                GetConnectionFromRS.Bind(receiveEndPoint);
                GetConnectionFromRS.Listen(1);

                Console.WriteLine("tring to connect");
                sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sendSocket.Connect(sendEndPoint);

                Console.WriteLine("Connected");


    
                receiveSocket = GetConnectionFromRS.Accept();


                Console.WriteLine("Connected");

                running = true;
                MessageBox.Show($"starting listening\n{receiveSocket == null}");
                new Thread(ReceiveData).Start();

                return true;
            }
            catch (Exception ex) {
                MessageBox.Show($"error\n{ex.Message}"); }
            return false;
        }

        private static void ReceiveData()
        {
            byte[] buffer = new byte[1024];

            while (running)
            {
                try
                {
                    int received = receiveSocket.Receive(buffer);
                    if (received > 0)
                    {
                        string data = Encoding.UTF8.GetString(buffer, 0, received);
                        Console.WriteLine($"Received: {data}");
                        MessageSelector(data);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error\n" + ex.Message);
                }
            }
        }

        public static void SendData(string data)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                sendSocket.Send(buffer);
                Console.WriteLine($"Sent: {data}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send error: {ex.Message}");
            }
        }
        public static void setExperName(string name)
        {
            ExperName = name;
        }
        private static void MessageSelector(string message)
        {
            try
            {
                MessageBox.Show($"the messege is: {message}\nthe name is {ExperName}");
                if (message.Split(';')[0] == "EXPERIMENT_RESULTS")
                {
                    
                    message = message.Replace("{name}", ExperName);
                    ExperName = "";
                    MicroController.SendToClient(message); // delete and send diractly to SendToClient
                } 
                else if (message.Split(';')[0] == "GotExper")
                {
                    MicroController.SendToClient($"GotExper;{message.Split(';')[1]}");
                }

            } catch (Exception e) {
            
            }
        }
        public static bool WeConnectedToRoyAndNiv()
        {
            return running;
        }
        

    }
}




