using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerSide;

namespace microcontrollerSide
{
    public partial class CommunicaionForm : Form
    {


        public CommunicaionForm(string ConntrollerName, string RoomCode)
        {
            InitializeComponent();
            RoomCodeTxBox.Text = RoomCode;
            Idendifeir.Text = ConntrollerName;
        }
        

        
        
        private void CommunicaionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MicroController.DisconnectFromServer();
            Thread.Sleep(500);
            ClosingController.btnExit_Click();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MicroController.MicroChipCommunication("shai");
        }

        private void DialogPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectionToElectronics.SendData("NEWEXPER;30;10");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConnectionToElectronics.SendData("StopExper;");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConnectionToElectronics.Connect();
        }
    }
}
