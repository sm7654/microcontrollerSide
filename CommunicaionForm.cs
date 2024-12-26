using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerSide;

namespace microcontrollerSide
{
    public partial class CommunicaionForm : Form
    {
        private static MicroController microController;
        private bool CLientOnline;

        private void CommunicaionForm_Load(object sender, EventArgs e)
        {

        }


        public CommunicaionForm(MicroController Controller)
        {
            microController = Controller;

            InitializeComponent();
        }
        public CommunicaionForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        
        private void SendToServerButton_Click(object sender, EventArgs e)
        {
            if (!this.CLientOnline) return;

            string Speed = SpeedInput.Text;
            string Lift = LiftInput.Text;
        }
        
        public bool IsClientOnline()
        {
            return this.CLientOnline;
        }

        private void IsCLientConnectedLabel_Click(object sender, EventArgs e)
        {

        }

        private void RoomCodeLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
