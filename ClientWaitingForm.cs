using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace microcontrollerSide
{
    public partial class ClientWaitingForm : Form
    {
        public ClientWaitingForm()
        {
            InitializeComponent();
        }
        public ClientWaitingForm(string roomcode)
        {
            InitializeComponent();
            sessionCodeLabel.Text = roomcode;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ClientWaitingForm_Load(object sender, EventArgs e)
        {

        }
    }
}
