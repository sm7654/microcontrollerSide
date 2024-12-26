namespace microcontrollerSide
{
    partial class UserStatus
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.UserConnectedStatus = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.UserConectivityLabel = new System.Windows.Forms.Label();
            this.ClientEndPointLael = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(835, 49);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.796407F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 98.20359F));
            this.tableLayoutPanel1.Controls.Add(this.UserConnectedStatus, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(835, 49);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // UserConnectedStatus
            // 
            this.UserConnectedStatus.AutoSize = true;
            this.UserConnectedStatus.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.UserConnectedStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserConnectedStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.UserConnectedStatus.Location = new System.Drawing.Point(0, 0);
            this.UserConnectedStatus.Margin = new System.Windows.Forms.Padding(0);
            this.UserConnectedStatus.Name = "UserConnectedStatus";
            this.UserConnectedStatus.Size = new System.Drawing.Size(15, 49);
            this.UserConnectedStatus.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel2.Controls.Add(this.TimeLabel);
            this.panel2.Controls.Add(this.ClientEndPointLael);
            this.panel2.Controls.Add(this.UserConectivityLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(15, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(820, 49);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // UserConectivityLabel
            // 
            this.UserConectivityLabel.AutoSize = true;
            this.UserConectivityLabel.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserConectivityLabel.Location = new System.Drawing.Point(3, 1);
            this.UserConectivityLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.UserConectivityLabel.Name = "UserConectivityLabel";
            this.UserConectivityLabel.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.UserConectivityLabel.Size = new System.Drawing.Size(135, 22);
            this.UserConectivityLabel.TabIndex = 0;
            this.UserConectivityLabel.Text = "Client Connected";
            this.UserConectivityLabel.Click += new System.EventHandler(this.UserConectivityLabel_Click);
            // 
            // ClientEndPointLael
            // 
            this.ClientEndPointLael.AutoSize = true;
            this.ClientEndPointLael.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientEndPointLael.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientEndPointLael.Location = new System.Drawing.Point(3, 33);
            this.ClientEndPointLael.Margin = new System.Windows.Forms.Padding(3, 0, 2, 0);
            this.ClientEndPointLael.Name = "ClientEndPointLael";
            this.ClientEndPointLael.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.ClientEndPointLael.Size = new System.Drawing.Size(87, 12);
            this.ClientEndPointLael.TabIndex = 1;
            this.ClientEndPointLael.Text = "10.0.0.13:59847";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.TimeLabel.Font = new System.Drawing.Font("Myanmar Text", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TimeLabel.Location = new System.Drawing.Point(733, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Padding = new System.Windows.Forms.Padding(0, 28, 0, 0);
            this.TimeLabel.Size = new System.Drawing.Size(87, 48);
            this.TimeLabel.TabIndex = 2;
            this.TimeLabel.Text = "10.2.2024 10:14";
            // 
            // UserStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UserStatus";
            this.Size = new System.Drawing.Size(835, 49);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label UserConnectedStatus;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label UserConectivityLabel;
        private System.Windows.Forms.Label ClientEndPointLael;
        private System.Windows.Forms.Label TimeLabel;
    }
}
