using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace microcontrollerSide
{
    public partial class UserStatus : UserControl
    {
        private static string RemoteEndPoint = "";
        public UserStatus(bool status, string Text)
        {
            InitializeComponent();
            DateTime localDate = DateTime.Now;
            string israelDate = localDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            // Format the time in 24-hour format (HH:mm:ss)
            string israelTime = localDate.ToString("HH:mm:ss", CultureInfo.InvariantCulture);

            TimeLabel.Text = $"{israelDate} {israelTime}";
            UserConectivityLabel.Text = $"{Text}";
        
            if (status)
                UserConnectedStatus.BackColor = Color.LightGreen;   
            else
                UserConnectedStatus.BackColor = Color.Red;
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
