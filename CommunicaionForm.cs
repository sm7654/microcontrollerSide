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

        private void formClosing(object sender, EventArgs e)
        {

            Application.Exit();
        }
        
        private void SendToServerButton_Click(object sender, EventArgs e)
        {
            byte[] excelBytes = FileHandler.GetFileBytes(@"C:\Users\user\Desktop\MicroData.xlsx");
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
    }
}
