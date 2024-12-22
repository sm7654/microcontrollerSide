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
            (bool status, Socket Conn) = CreateConn();


            if (status)
            {

                // generate keys and returns the public key and send it do server

                byte[] recognitionBytes = Encoding.UTF8.GetBytes("Esp");
                Conn.Send(Encoding.UTF8.GetBytes(recognitionBytes.Length.ToString()));
                Conn.Send(recognitionBytes);


                byte[] roomCode = new byte[1024];
                int length = Conn.Receive(roomCode);
                string gg = Encoding.UTF8.GetString(roomCode, 0, length);
                int incomingmassegeLength = int.Parse(gg);
                roomCode = new byte[incomingmassegeLength];
                Conn.Receive(roomCode);

                string Code = "";
                if (length > 0)
                {
                    Code = RsaEncryption.Decrypt(roomCode).Split(' ')[1];
                }


                MicroController temp = new MicroController(Conn, Code);
                CommunicaionForm communicaionForm = new CommunicaionForm(temp);
                temp.setUI(communicaionForm);


                this.Hide();
                communicaionForm.Show();




            }
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



                
                // recive code

                return (true, Conn);
            }
            catch (SocketException error)
            {
                return (false, null);
            }
        }
    }
}
