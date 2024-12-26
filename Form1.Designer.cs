using ServerSide;

namespace microcontrollerSide
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.ControllerName = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ControlletNameLabel = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ControllerName
            // 
            this.ControllerName.Location = new System.Drawing.Point(333, 110);
            this.ControllerName.Name = "ControllerName";
            this.ControllerName.Size = new System.Drawing.Size(162, 20);
            this.ControllerName.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // ControlletNameLabel
            // 
            this.ControlletNameLabel.AutoSize = true;
            this.ControlletNameLabel.Location = new System.Drawing.Point(330, 94);
            this.ControlletNameLabel.Name = "ControlletNameLabel";
            this.ControlletNameLabel.Size = new System.Drawing.Size(50, 13);
            this.ControlletNameLabel.TabIndex = 2;
            this.ControlletNameLabel.Text = "Iderntifier";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(375, 343);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 3;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 450);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ControlletNameLabel);
            this.Controls.Add(this.ControllerName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ControllerName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label ControlletNameLabel;
        private System.Windows.Forms.Button ConnectButton;
    }
}

