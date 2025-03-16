namespace microcontrollerSide
{
    partial class pipeInitionlazer
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
            this.label1 = new System.Windows.Forms.Label();
            this.PipePath = new System.Windows.Forms.TextBox();
            this.ConnectToPipeButton = new System.Windows.Forms.Button();
            this.errorPipeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter The Path Of The Pipe";
            // 
            // PipePath
            // 
            this.PipePath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.PipePath.Location = new System.Drawing.Point(25, 46);
            this.PipePath.Name = "PipePath";
            this.PipePath.Size = new System.Drawing.Size(240, 30);
            this.PipePath.TabIndex = 1;
            // 
            // ConnectToPipeButton
            // 
            this.ConnectToPipeButton.BackColor = System.Drawing.Color.Black;
            this.ConnectToPipeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectToPipeButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ConnectToPipeButton.ForeColor = System.Drawing.Color.White;
            this.ConnectToPipeButton.Location = new System.Drawing.Point(285, 46);
            this.ConnectToPipeButton.Name = "ConnectToPipeButton";
            this.ConnectToPipeButton.Size = new System.Drawing.Size(83, 30);
            this.ConnectToPipeButton.TabIndex = 2;
            this.ConnectToPipeButton.Text = "Connect";
            this.ConnectToPipeButton.UseVisualStyleBackColor = false;
            this.ConnectToPipeButton.Click += new System.EventHandler(this.ConnectToPipeButton_Click);
            // 
            // errorPipeLabel
            // 
            this.errorPipeLabel.AutoSize = true;
            this.errorPipeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.errorPipeLabel.ForeColor = System.Drawing.Color.Red;
            this.errorPipeLabel.Location = new System.Drawing.Point(24, 87);
            this.errorPipeLabel.Name = "errorPipeLabel";
            this.errorPipeLabel.Size = new System.Drawing.Size(28, 15);
            this.errorPipeLabel.TabIndex = 3;
            this.errorPipeLabel.Text = "___";
            // 
            // pipeInitionlazer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 120);
            this.Controls.Add(this.errorPipeLabel);
            this.Controls.Add(this.ConnectToPipeButton);
            this.Controls.Add(this.PipePath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "pipeInitionlazer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pipe Initializer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PipePath;
        private System.Windows.Forms.Button ConnectToPipeButton;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label errorPipeLabel;
    }
}