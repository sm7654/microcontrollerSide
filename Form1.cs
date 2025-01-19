using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerSide;

namespace microcontrollerSide
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            RsaEncryption.GenerateKeys();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (ControllerName.Text == "")
                return;
            ConnectionErrorLabel.Text = "Trying to connect the server.....";
            new Thread(() =>
            {
                (bool status, Socket Conn) = CreateConn();


                if (status)
                {

                    // generate keys and returns the public key and send it do server

                    byte[] recognitionBytes = RsaEncryption.EncryptToServer(Encoding.UTF8.GetBytes($"Esp;{ControllerName.Text}"));

                    Conn.Send(Encoding.UTF8.GetBytes(recognitionBytes.Length.ToString()));
                    Thread.Sleep(200);
                    Conn.Send(recognitionBytes);


                    byte[] roomCode = new byte[128];
                    int length = Conn.Receive(roomCode);


                    string Code = RsaEncryption.Decrypt(roomCode);
                    if (length <= 0 || Code == "500")
                        return;
                    


                    MicroController temp = new MicroController(Conn, Code);
                    CommunicaionForm communicaionForm = new CommunicaionForm(temp);
                    temp.setUI(communicaionForm);

                    
                    this.BeginInvoke(new Action(() => {
                        this.Hide();
                        communicaionForm.Show();

                    }));
                    



                }
            }).Start();

            
        }


        


        private (bool, Socket) CreateConn()
        {
            try
            {
                string name = ControllerName.Text;

                Socket Conn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint address = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 65000);

                Conn.Connect(address);

                
                Conn.Send(RsaEncryption.GenerateKeys());


                byte[] ServerpublicKey = new byte[1024];
                int bytesRec = Conn.Receive(ServerpublicKey);


                RsaEncryption.SetServerPublicKey(Encoding.UTF8.GetString(ServerpublicKey, 0, bytesRec));


                // recive code

                return (true, Conn);
            }
            catch (SocketException error)
            {
                ConnectionErrorLabel.Text = "No connection could be made. Try again later...";
                return (false, null);
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void ControlletNameLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
