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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.Header.SuspendLayout();
            this.Background.SuspendLayout();
            this.SuspendLayout();
            // 
            // RoomCodeTxBox
            // 
            this.RoomCodeTxBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RoomCodeTxBox.AutoSize = true;
            this.RoomCodeTxBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.RoomCodeTxBox.ForeColor = System.Drawing.Color.White;
            this.RoomCodeTxBox.Location = new System.Drawing.Point(1099, 19);
            this.RoomCodeTxBox.Name = "RoomCodeTxBox";
            this.RoomCodeTxBox.Size = new System.Drawing.Size(125, 28);
            this.RoomCodeTxBox.TabIndex = 5;
            this.RoomCodeTxBox.Text = "Room Code:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.DialogPanel);
            this.panel1.Location = new System.Drawing.Point(47, 84);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1185, 448);
            this.panel1.TabIndex = 8;
            // 
            // DialogPanel
            // 
            this.DialogPanel.AutoScroll = true;
            this.DialogPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.DialogPanel.Location = new System.Drawing.Point(3, 2);
            this.DialogPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DialogPanel.Name = "DialogPanel";
            this.DialogPanel.Padding = new System.Windows.Forms.Padding(25, 7, 8, 7);
            this.DialogPanel.Size = new System.Drawing.Size(1185, 446);
            this.DialogPanel.TabIndex = 0;
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Header.Controls.Add(this.Idendifeir);
            this.Header.Controls.Add(this.RoomCodeTxBox);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(1280, 66);
            this.Header.TabIndex = 9;
            // 
            // Idendifeir
            // 
            this.Idendifeir.AutoSize = true;
            this.Idendifeir.BackColor = System.Drawing.Color.Transparent;
            this.Idendifeir.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.Idendifeir.ForeColor = System.Drawing.Color.White;
            this.Idendifeir.Location = new System.Drawing.Point(41, 15);
            this.Idendifeir.Name = "Idendifeir";
            this.Idendifeir.Size = new System.Drawing.Size(150, 32);
            this.Idendifeir.TabIndex = 0;
            this.Idendifeir.Text = "CellRoom #9";
            // 
            // Background
            // 
            this.Background.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Background.Controls.Add(this.panel1);
            this.Background.Controls.Add(this.button1);
            this.Background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Background.Location = new System.Drawing.Point(0, 0);
            this.Background.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Background.Name = "Background";
            this.Background.Size = new System.Drawing.Size(1280, 649);
            this.Background.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(900, 614);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CommunicaionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 649);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.Background);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CommunicaionForm";
            this.Text = "CommunicaionForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommunicaionForm_FormClosing);
            
            this.panel1.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Header.PerformLayout();
            this.Background.ResumeLayout(false);
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



        

        private Button button1;

        public void SetNewCode(string code)
        {
            RoomCodeTxBox.Text = code;
        }
    }
}