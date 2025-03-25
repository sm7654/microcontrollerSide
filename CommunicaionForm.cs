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

        private void CommunicaionForm_Load(object sender, EventArgs e)
        {

        }


        public CommunicaionForm(string ConntrollerName, string RoomCode)
        {
            InitializeComponent();
            RoomCodeTxBox.Text = RoomCode;
            Idendifeir.Text = ConntrollerName;

        }
        

        private void formClosing(object sender, EventArgs e)
        {
            ClosingController.btnExit_Click();            
        }
        
        private void CommunicaionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MicroController.DisconnectFromServer();
            Thread.Sleep(500);
            ClosingController.btnExit_Click();
        }


        
    }
}
