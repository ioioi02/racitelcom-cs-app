namespace RACITELCOM_CS_APP
{
    partial class frmExistingAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExistingAccount));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtAccountNum = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.btnChangePlan = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReconnection = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnInfoUpdate = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 47);
            this.panel1.TabIndex = 26;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = global::RACITELCOM_CS_APP.Properties.Resources.cancel_logo1;
            this.btnCancel.Location = new System.Drawing.Point(381, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(40, 40);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.panel6);
            this.panel14.Controls.Add(this.panel5);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Location = new System.Drawing.Point(0, 47);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(424, 95);
            this.panel14.TabIndex = 39;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtAccountNum);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 40);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(424, 44);
            this.panel6.TabIndex = 41;
            // 
            // txtAccountNum
            // 
            this.txtAccountNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountNum.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtAccountNum.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(7)))), ((int)(((byte)(100)))));
            this.txtAccountNum.Location = new System.Drawing.Point(0, 0);
            this.txtAccountNum.Name = "txtAccountNum";
            this.txtAccountNum.Size = new System.Drawing.Size(424, 34);
            this.txtAccountNum.TabIndex = 7;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(424, 40);
            this.panel5.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(7)))), ((int)(((byte)(100)))));
            this.label6.Location = new System.Drawing.Point(89, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 28);
            this.label6.TabIndex = 42;
            this.label6.Text = "Enter Account Number";
            // 
            // btnChangePlan
            // 
            this.btnChangePlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(7)))), ((int)(((byte)(100)))));
            this.btnChangePlan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePlan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnChangePlan.FlatAppearance.BorderSize = 0;
            this.btnChangePlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePlan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePlan.ForeColor = System.Drawing.Color.White;
            this.btnChangePlan.Location = new System.Drawing.Point(0, 142);
            this.btnChangePlan.Name = "btnChangePlan";
            this.btnChangePlan.Size = new System.Drawing.Size(424, 44);
            this.btnChangePlan.TabIndex = 42;
            this.btnChangePlan.Text = "CHANGE PLAN";
            this.btnChangePlan.UseVisualStyleBackColor = false;
            this.btnChangePlan.Click += new System.EventHandler(this.btnChangePlan_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 186);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(424, 5);
            this.panel2.TabIndex = 45;
            // 
            // btnReconnection
            // 
            this.btnReconnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(7)))), ((int)(((byte)(100)))));
            this.btnReconnection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReconnection.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReconnection.FlatAppearance.BorderSize = 0;
            this.btnReconnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReconnection.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReconnection.ForeColor = System.Drawing.Color.White;
            this.btnReconnection.Location = new System.Drawing.Point(0, 191);
            this.btnReconnection.Name = "btnReconnection";
            this.btnReconnection.Size = new System.Drawing.Size(424, 44);
            this.btnReconnection.TabIndex = 46;
            this.btnReconnection.Text = "RECONNECTION";
            this.btnReconnection.UseVisualStyleBackColor = false;
            this.btnReconnection.Click += new System.EventHandler(this.btnReconnection_Click);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 235);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(424, 5);
            this.panel3.TabIndex = 47;
            // 
            // btnInfoUpdate
            // 
            this.btnInfoUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(7)))), ((int)(((byte)(100)))));
            this.btnInfoUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInfoUpdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInfoUpdate.FlatAppearance.BorderSize = 0;
            this.btnInfoUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfoUpdate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInfoUpdate.ForeColor = System.Drawing.Color.White;
            this.btnInfoUpdate.Location = new System.Drawing.Point(0, 240);
            this.btnInfoUpdate.Name = "btnInfoUpdate";
            this.btnInfoUpdate.Size = new System.Drawing.Size(424, 44);
            this.btnInfoUpdate.TabIndex = 48;
            this.btnInfoUpdate.Text = "INFORMATION UPDATE";
            this.btnInfoUpdate.UseVisualStyleBackColor = false;
            this.btnInfoUpdate.Click += new System.EventHandler(this.btnInfoUpdate_Click);
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 284);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(424, 46);
            this.panel4.TabIndex = 49;
            // 
            // frmExistingAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(424, 332);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btnInfoUpdate);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnReconnection);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnChangePlan);
            this.Controls.Add(this.panel14);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExistingAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmExistingAccount";
            this.panel1.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Button btnChangePlan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnReconnection;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnInfoUpdate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txtAccountNum;
        private System.Windows.Forms.Label label6;
    }
}