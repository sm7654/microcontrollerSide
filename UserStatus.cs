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
        public UserStatus(string Text)
        {

            InitializeComponent();
            UserConectivityLabel.Text = Text;
            DateTime localDate = DateTime.Now;
            string israelDate = localDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            string israelTime = localDate.ToString("HH:mm:ss", CultureInfo.InvariantCulture);

            TimeLabel.Text = $"{israelDate} {israelTime}";

            UserConnectedStatus.BackColor = Color.DodgerBlue;

            
        }

        public UserStatus(bool status, string Text)
        {
            InitializeComponent();
            DateTime localDate = DateTime.Now;
            string israelDate = localDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            string israelTime = localDate.ToString("HH:mm:ss", CultureInfo.InvariantCulture);

            TimeLabel.Text = $"{israelDate} {israelTime}";
            UserConectivityLabel.Text = $"{Text.Split(';')[0]}";

            if (status)
            {
                UserConnectedStatus.BackColor = Color.LightGreen;
                NewCode.Text = "";
            }
            else
            {
                NewCode.Text = Text.Split(';')[1];
                NewCode.ForeColor = Color.Black;
                UserConnectedStatus.BackColor = Color.Red;
            }
        }
        public void SetColor(Color c)
        {
            UserConnectedStatus.BackColor = c;
        }

        public void SetRemoteEndPoint(string EN)
        {
            ClientEndPointLael.Text = EN;
        }
        public void SetDetails(string de)
        {
            ClientEndPointLael.Text = de;
        }


    }
}
