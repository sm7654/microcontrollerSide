using ServerSide;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
namespace microcontrollerSide
{
    partial class CommunicaionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RoomCodeTxBox = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DialogPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.Header = new System.Windows.Forms.Panel();
            this.Idendifeir = new System.Windows.Forms.Label();
            this.Background = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // RoomCodeTxBox
            // 
            this.RoomCodeTxBox.AutoSize = true;
            this.RoomCodeTxBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.RoomCodeTxBox.ForeColor = System.Drawing.Color.White;
            this.RoomCodeTxBox.Location = new System.Drawing.Point(1100, 15);
            this.RoomCodeTxBox.Name = "RoomCodeTxBox";
            this.RoomCodeTxBox.Size = new System.Drawing.Size(125, 28);
            this.RoomCodeTxBox.TabIndex = 5;
            this.RoomCodeTxBox.Text = "Room Code:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.DialogPanel);
            this.panel1.Location = new System.Drawing.Point(56, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1185, 500);
            this.panel1.TabIndex = 8;
            // 
            // DialogPanel
            // 
            this.DialogPanel.AutoScroll = true;
            this.DialogPanel.BackColor = System.Drawing.Color.White;
            this.DialogPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DialogPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DialogPanel.Location = new System.Drawing.Point(0, 0);
            this.DialogPanel.Name = "DialogPanel";
            this.DialogPanel.Padding = new System.Windows.Forms.Padding(25, 8, 8, 8);
            this.DialogPanel.Size = new System.Drawing.Size(1185, 500);
            this.DialogPanel.TabIndex = 0;
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.Transparent;
            this.Header.Controls.Add(this.Idendifeir);
            this.Header.Controls.Add(this.RoomCodeTxBox);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(1280, 50);
            this.Header.TabIndex = 9;
            this.Header.Paint += new System.Windows.Forms.PaintEventHandler(this.set_background);
            // 
            // Idendifeir
            // 
            this.Idendifeir.AutoSize = true;
            this.Idendifeir.BackColor = System.Drawing.Color.Transparent;
            this.Idendifeir.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.Idendifeir.ForeColor = System.Drawing.Color.White;
            this.Idendifeir.Location = new System.Drawing.Point(50, 11);
            this.Idendifeir.Name = "Idendifeir";
            this.Idendifeir.Size = new System.Drawing.Size(150, 32);
            this.Idendifeir.TabIndex = 0;
            this.Idendifeir.Text = "CellRoom #9";
            // 
            // Background
            // 
            this.Background.BackColor = System.Drawing.Color.White;
            this.Background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Background.Location = new System.Drawing.Point(0, 0);
            this.Background.Name = "Background";
            this.Background.Size = new System.Drawing.Size(1280, 649);
            this.Background.TabIndex = 10;
            // 
            // CommunicaionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 649);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.Background);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CommunicaionForm";
            this.Text = "CommunicaionForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommunicaionForm_FormClosing);
            this.Load += new System.EventHandler(this.CommunicaionForm_Load);
            this.panel1.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Header.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label RoomCodeTxBox;
       
        
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel DialogPanel;

        public System.Windows.Forms.FlowLayoutPanel GetDialogPanel()
        {
            return this.DialogPanel;
        }

        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Label Idendifeir;
        private System.Windows.Forms.Panel Background;



        private void set_background(object sender, PaintEventArgs e)
        {
            // Get the graphics object
            Graphics graphics = e.Graphics;

            // Create a rectangle that fills the entire form
            Rectangle gradientRectangle = new Rectangle(0, 0, Width, Height);

            // Create the gradient brush: from dark red to orange-red (professional colors for blocking or warning)
            Brush brush = new LinearGradientBrush(gradientRectangle, Color.FromArgb(20, 40, 60), Color.FromArgb(100, 180, 220), 45f);


            // Fill the rectangle with the gradient
            graphics.FillRectangle(brush, gradientRectangle);
        }
    }
}