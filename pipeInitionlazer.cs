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
    public partial class pipeInitionlazer : Form
    {
        public pipeInitionlazer()
        {
            InitializeComponent();
        }

        private void ConnectToPipeButton_Click(object sender, EventArgs e)
        {
            bool connection;
            if (PipePath.Text == "")
                connection = PipeStream.InitionlisePipe();
            else 
                connection = PipeStream.InitionlisePipe(PipePath.Text);
            
            if (connection)
            {
                Form1 f = new Form1();
                f.Show();
                this.Hide();
            } else
            {
                errorPipeLabel.Text = "Could not create pipe connection at the specified path...";
            }
        }
    }
}
