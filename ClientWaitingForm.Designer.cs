namespace microcontrollerSide
{
    partial class ClientWaitingForm
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
            this.StatusFormLabel = new System.Windows.Forms.Label();
            this.sessionCodeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StatusFormLabel
            // 
            this.StatusFormLabel.AutoSize = true;
            this.StatusFormLabel.Location = new System.Drawing.Point(97, 48);
            this.StatusFormLabel.Name = "StatusFormLabel";
            this.StatusFormLabel.Size = new System.Drawing.Size(153, 13);
            this.StatusFormLabel.TabIndex = 0;
            this.StatusFormLabel.Text = "Wating for client to connect.....";
            this.StatusFormLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // sessionCodeLabel
            // 
            this.sessionCodeLabel.AutoSize = true;
            this.sessionCodeLabel.Location = new System.Drawing.Point(108, 114);
            this.sessionCodeLabel.Name = "sessionCodeLabel";
            this.sessionCodeLabel.Size = new System.Drawing.Size(75, 13);
            this.sessionCodeLabel.TabIndex = 1;
            this.sessionCodeLabel.Text = "session code: ";
            // 
            // ClientWaitingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 163);
            this.Controls.Add(this.sessionCodeLabel);
            this.Controls.Add(this.StatusFormLabel);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Name = "ClientWaitingForm";
            this.Text = "Wating for client to connect";
            this.Load += new System.EventHandler(this.ClientWaitingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StatusFormLabel;
        private System.Windows.Forms.Label sessionCodeLabel;
    }
}