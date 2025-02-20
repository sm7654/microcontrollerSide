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
        private bool CLientOnline;

        private void CommunicaionForm_Load(object sender, EventArgs e)
        {

        }


        public CommunicaionForm()
        {
            InitializeComponent();

        }
        

        private void formClosing(object sender, EventArgs e)
        {
            ClosingController.btnExit_Click();            
        }
        
        private void SendToServerButton_Click(object sender, EventArgs e)
        {
            ExperimentController.MicroChipCommunication("shai");

            //     InitionlisePipe

            return;
            return;
            byte[] excelBytes = FileHandler.GetFileBytes(@"C:\Users\shai\Downloads\Book1.xlsx");
            if (excelBytes != null)
            {
                MicroController.SendToClient(excelBytes);
            }
        }
        
        public bool IsClientOnline()
        {
            return this.CLientOnline;
        }

        private void DialogPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CommunicaionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MicroController.DisconnectFromServer();
            Thread.Sleep(500);
            ClosingController.btnExit_Click();
        }
    }
}
