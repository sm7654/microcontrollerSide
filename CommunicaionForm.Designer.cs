﻿namespace microcontrollerSide
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
            this.LiftLabel = new System.Windows.Forms.Label();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.SpeedInput = new System.Windows.Forms.TextBox();
            this.LiftInput = new System.Windows.Forms.TextBox();
            this.SendToServerButton = new System.Windows.Forms.Button();
            this.RoomCodeLabel = new System.Windows.Forms.Label();
            this.IsCLientConnectedLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LiftLabel
            // 
            this.LiftLabel.AutoSize = true;
            this.LiftLabel.Location = new System.Drawing.Point(324, 175);
            this.LiftLabel.Name = "LiftLabel";
            this.LiftLabel.Size = new System.Drawing.Size(24, 13);
            this.LiftLabel.TabIndex = 0;
            this.LiftLabel.Text = "Lift:";
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.AutoSize = true;
            this.SpeedLabel.Location = new System.Drawing.Point(324, 127);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(41, 13);
            this.SpeedLabel.TabIndex = 1;
            this.SpeedLabel.Text = "Speed:";
            this.SpeedLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // SpeedInput
            // 
            this.SpeedInput.BackColor = System.Drawing.Color.White;
            this.SpeedInput.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.SpeedInput.Location = new System.Drawing.Point(327, 143);
            this.SpeedInput.Name = "SpeedInput";
            this.SpeedInput.Size = new System.Drawing.Size(129, 20);
            this.SpeedInput.TabIndex = 2;
            // 
            // LiftInput
            // 
            this.LiftInput.Location = new System.Drawing.Point(327, 191);
            this.LiftInput.Name = "LiftInput";
            this.LiftInput.Size = new System.Drawing.Size(129, 20);
            this.LiftInput.TabIndex = 3;
            // 
            // SendToServerButton
            // 
            this.SendToServerButton.Location = new System.Drawing.Point(354, 287);
            this.SendToServerButton.Name = "SendToServerButton";
            this.SendToServerButton.Size = new System.Drawing.Size(75, 23);
            this.SendToServerButton.TabIndex = 4;
            this.SendToServerButton.Text = "Send server";
            this.SendToServerButton.UseVisualStyleBackColor = true;
            this.SendToServerButton.Click += new System.EventHandler(this.SendToServerButton_Click);
            // 
            // RoomCodeLabel
            // 
            this.RoomCodeLabel.AutoSize = true;
            this.RoomCodeLabel.Location = new System.Drawing.Point(358, 50);
            this.RoomCodeLabel.Name = "RoomCodeLabel";
            this.RoomCodeLabel.Size = new System.Drawing.Size(63, 13);
            this.RoomCodeLabel.TabIndex = 5;
            this.RoomCodeLabel.Text = "room code: ";
            // 
            // IsCLientConnectedLabel
            // 
            this.IsCLientConnectedLabel.AutoSize = true;
            this.IsCLientConnectedLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsCLientConnectedLabel.Location = new System.Drawing.Point(324, 348);
            this.IsCLientConnectedLabel.Name = "IsCLientConnectedLabel";
            this.IsCLientConnectedLabel.Size = new System.Drawing.Size(161, 16);
            this.IsCLientConnectedLabel.TabIndex = 7;
            this.IsCLientConnectedLabel.Text = "waiting for client to connect";
            this.IsCLientConnectedLabel.Click += new System.EventHandler(this.IsCLientConnectedLabel_Click);
            // 
            // CommunicaionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.IsCLientConnectedLabel);
            this.Controls.Add(this.RoomCodeLabel);
            this.Controls.Add(this.SendToServerButton);
            this.Controls.Add(this.LiftInput);
            this.Controls.Add(this.SpeedInput);
            this.Controls.Add(this.SpeedLabel);
            this.Controls.Add(this.LiftLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CommunicaionForm";
            this.Text = "CommunicaionForm";
            this.Load += new System.EventHandler(this.CommunicaionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LiftLabel;
        private System.Windows.Forms.Label SpeedLabel;
        private System.Windows.Forms.TextBox SpeedInput;
        private System.Windows.Forms.TextBox LiftInput;
        private System.Windows.Forms.Button SendToServerButton;
        private System.Windows.Forms.Label RoomCodeLabel;
        private System.Windows.Forms.Label IsCLientConnectedLabel;
        public System.Windows.Forms.Label GetLabel()
        {
            return this.RoomCodeLabel;
        }

        

        public System.Windows.Forms.Label GetCLientStatusLabel()
        {
            return this.IsCLientConnectedLabel;
        }
        public void CLientIsOnline()
        {
            this.CLientOnline = true;
            this.IsCLientConnectedLabel.Text = "Client connected!";
        }
    }
}