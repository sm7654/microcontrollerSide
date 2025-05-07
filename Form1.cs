using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ServerSide;

namespace microcontrollerSide
{
    public partial class Form1 : Form
    {
        private Socket ConnToServer;
        public Form1()
        {
            RsaEncryption.GenerateKeys();
            InitializeComponent();
            if(!PipeStream.InitionlisePipe())
                ConnectionErrorLabel.Text = "Could not connect to a pipe.....";
            new Thread(() =>
            {
                (bool status, Socket Conn) = CreateConn();
                ConnToServer = Conn;
                byte[] aesKey = new byte[128];
                byte[] aesIv = new byte[128];

                
                if (status)
                {

                    byte[] recognitionBytes = RsaEncryption.EncryptToServer(Encoding.UTF8.GetBytes($"Esp"));
                    // generate keys and returns the public key and send it do server


                    ConnToServer.Send(recognitionBytes);

                    ConnToServer.Receive(aesKey);
                    ConnToServer.Receive(aesIv);

                    AesEncryption.AddkeysForServer(aesKey, aesIv);

                   




                }
            }).Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (!PipeStream.IsPipeConnected())
            {
                ConnectionErrorLabel.Text = "Could not connect to a pipe.....";
                return;
            }
            if (ControllerName.Text == "" ||  ControllerName.Text == SessionNameLabel.Text)
                return;
            ConnectionErrorLabel.Text = "Trying to connect the server.....";

            new Thread(() =>
            {
                byte[] SessionAndMicroName = AesEncryption.EncryptedDataForServer(Encoding.UTF8.GetBytes($"{SessionNameLabel.Text}&{ControllerName.Text}"));
                ConnToServer.Send(Encoding.UTF8.GetBytes(SessionAndMicroName.Length.ToString()));
                Thread.Sleep(100);
                ConnToServer.Send(SessionAndMicroName);

                
                
                
                
                
                
                byte[] roomCode = new byte[1024];
                int byterec = ConnToServer.Receive(roomCode);
                int bufferSize = int.Parse(Encoding.UTF8.GetString(roomCode, 0, byterec));
                roomCode = new byte[bufferSize];
                ConnToServer.Receive(roomCode);
                string Code = AesEncryption.DecryptToServerToString(roomCode);
                if (bufferSize <= 0 || Code == "500")
                    return;


                //MessageBox.Show();
                MicroController.SetMicroController(ConnToServer);
                CommunicaionForm communicaionForm = new CommunicaionForm(ControllerName.Text, Code);
                communicaionForm.Text = SessionNameLabel.Text;
                MicroController.SetUI(communicaionForm);
                PipeStream.InitionlisePipe();


                this.BeginInvoke(new Action(() => {
                    this.Hide();
                    communicaionForm.Show();
                }));
            }).Start();
            
        }


        


        private (bool, Socket) CreateConn()
        {
            try
            {
                string name = ControllerName.Text;

                Socket Conn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
                IPEndPoint address = new IPEndPoint(IPAddress.Parse("149.106.149.75"), 65000);

                Conn.Connect(address);

                Conn.Send(RsaEncryption.GenerateKeys());

                byte[] ServerpublicKey = new byte[1024];
                int bytesRec = Conn.Receive(ServerpublicKey);

                RsaEncryption.SetServerPublicKey(Encoding.UTF8.GetString(ServerpublicKey, 0, bytesRec));




                //Conn.Receive(ServerpublicKey);
                //Conn.Receive(ServerpublicKey);
                // recive code

                return (true, Conn);
            }
            catch (SocketException error)
            {
                ConnectionErrorLabel.Text = "No connection could be made. Try again later...";
                return (false, null);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClosingController.btnExit_Click();
        }

        

        private void ConnectionErrorLabel_Click(object sender, EventArgs e)
        {

        }

        private void ControlletNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
