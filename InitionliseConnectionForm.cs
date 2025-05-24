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
    public partial class InitionliseConnectionForm : Form
    {
        private Socket ConnToServer;
        public InitionliseConnectionForm()
        {
            RsaEncryption.GenerateKeys();
            InitializeComponent();
            /*if (!PipeStream.InitionlisePipe())
            {
                ConnectionErrorLabel.Text = "Could not connect to a pipe.....";
                return;
            }*/
            new Thread(() =>
            {
                (bool status, Socket Conn) = CreateConn();
                ConnToServer = Conn;
                byte[] aesKey = new byte[128];
                byte[] aesIv = new byte[128];

                
                if (status)
                {

                    byte[] recognitionBytes = RsaEncryption.EncryptToServer(Encoding.UTF8.GetBytes("Micro"));
                    // generate keys and returns the public key and send it do server


                    ConnToServer.Send(recognitionBytes);

                    ConnToServer.Receive(aesKey);
                    ConnToServer.Receive(aesIv);

                    AesEncryption.AddkeysForServer(aesKey, aesIv);

                   




                }
            }).Start();
        }

        

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            /*if (!PipeStream.IsPipeConnected())
            {
                ConnectionErrorLabel.Text = "Could not connect to a pipe.....";
                //return;
            }*/
            if (ControllerName.Text == "" ||  ControllerName.Text == SessionNameLabel.Text)
                return;
            ConnectionErrorLabel.Text = "Trying to connect the server.....";
            
            new Thread(() =>
            {
                byte[] SessionAndMicroName = new byte[10];
                try
                {
                    SessionAndMicroName = AesEncryption.EncryptedDataForServer(Encoding.UTF8.GetBytes($"{ControllerName.Text}&{SessionNameLabel.Text}"));
                    
                    ConnToServer.Send(Encoding.UTF8.GetBytes(SessionAndMicroName.Length.ToString()));
                    Thread.Sleep(200);
                    ConnToServer.Send(SessionAndMicroName);







                    byte[] roomCode = new byte[1024];
                    int byterec = ConnToServer.Receive(roomCode);
                    int bufferSize = int.Parse(Encoding.UTF8.GetString(roomCode, 0, byterec));
                    roomCode = new byte[bufferSize];
                    ConnToServer.Receive(roomCode);
                    string Code = AesEncryption.DecryptToServerToString(roomCode);
                    if (bufferSize <= 0 || Code == "500")
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            ErrorLabel.Text = "Something Went Wrong. \nPlease choose different session name or identifier or try again later.";
                            ConnectionErrorLabel.Text = "";
                        }));
                        return;
                    }

                    MicroController.SetMicroController(ConnToServer);
                    CommunicaionForm communicaionForm = new CommunicaionForm(ControllerName.Text, Code);
                    communicaionForm.Text = SessionNameLabel.Text;
                    MicroController.SetUI(communicaionForm);


                    this.BeginInvoke(new Action(() =>
                    {
                        this.Hide();
                        communicaionForm.Show();
                    }));
                }catch (Exception t)
                {
                    if (ConnToServer == null)
                        MessageBox.Show($"Null");
                    else if(SessionAndMicroName == null)
                        MessageBox.Show("you are fucked");
                }
            }).Start();
            
        }

        private (bool, Socket) CreateConn()
        {
            try
            {
                string name = ControllerName.Text;

                Socket Conn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
                IPEndPoint address = new IPEndPoint(IPAddress.Parse("10.0.0.6"), 65000);

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

        private void InitionliseConnectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClosingController.btnExit_Click();
        }

    }
}
