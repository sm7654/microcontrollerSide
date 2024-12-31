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
    public partial class UserStatus : UserControl
    {
        private static string RemoteEndPoint = "";
        public UserStatus(bool status)
        {
            InitializeComponent();
            DateTime localDate = DateTime.Now;
            TimeLabel.Text = localDate.ToString();
            if (status)
            {
                UserConnectedStatus.BackColor = Color.LightGreen;
            }
        }
        public void SetRemoteEndPoint(string EN)
        {
            RemoteEndPoint = EN;
            ClientEndPointLael.Text = RemoteEndPoint;
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserConectivityLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
