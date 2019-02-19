namespace ERP
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbSplash = new System.Windows.Forms.PictureBox();
            this.lblSys = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.picSplash = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lnkLabel = new System.Windows.Forms.LinkLabel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pbKeys = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrLoading = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSplash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSplash)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbKeys)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbSplash);
            this.panel1.Controls.Add(this.lblSys);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.picSplash);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 199);
            this.panel1.TabIndex = 0;
            // 
            // pbSplash
            // 
            this.pbSplash.Location = new System.Drawing.Point(3, 3);
            this.pbSplash.Name = "pbSplash";
            this.pbSplash.Size = new System.Drawing.Size(390, 155);
            this.pbSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSplash.TabIndex = 3;
            this.pbSplash.TabStop = false;
            // 
            // lblSys
            // 
            this.lblSys.AutoSize = true;
            this.lblSys.Location = new System.Drawing.Point(3, 161);
            this.lblSys.Name = "lblSys";
            this.lblSys.Size = new System.Drawing.Size(47, 13);
            this.lblSys.TabIndex = 2;
            this.lblSys.Text = "Ready...";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 177);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(387, 19);
            this.progressBar1.TabIndex = 1;
            // 
            // picSplash
            // 
            this.picSplash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSplash.Location = new System.Drawing.Point(0, 0);
            this.picSplash.Name = "picSplash";
            this.picSplash.Size = new System.Drawing.Size(396, 199);
            this.picSplash.TabIndex = 0;
            this.picSplash.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel2.Controls.Add(this.lnkLabel);
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.btnLogin);
            this.panel2.Controls.Add(this.tbPassword);
            this.panel2.Controls.Add(this.tbUsername);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(396, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(258, 199);
            this.panel2.TabIndex = 1;
            // 
            // lnkLabel
            // 
            this.lnkLabel.AutoSize = true;
            this.lnkLabel.Location = new System.Drawing.Point(10, 177);
            this.lnkLabel.Name = "lnkLabel";
            this.lnkLabel.Size = new System.Drawing.Size(110, 13);
            this.lnkLabel.TabIndex = 7;
            this.lnkLabel.TabStop = true;
            this.lnkLabel.Text = "Configuration Settings";
            this.lnkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLabel_LinkClicked);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(152, 117);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(71, 117);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(71, 90);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(175, 20);
            this.tbPassword.TabIndex = 4;
            this.tbPassword.Enter += new System.EventHandler(this.tbPassword_Enter);
            this.tbPassword.Leave += new System.EventHandler(this.tbPassword_Leave);
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(71, 67);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(175, 20);
            this.tbUsername.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Username:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.pbKeys);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(258, 34);
            this.panel3.TabIndex = 0;
            // 
            // pbKeys
            // 
            this.pbKeys.Location = new System.Drawing.Point(6, 3);
            this.pbKeys.Name = "pbKeys";
            this.pbKeys.Size = new System.Drawing.Size(31, 26);
            this.pbKeys.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbKeys.TabIndex = 1;
            this.pbKeys.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login to System";
            // 
            // tmrLoading
            // 
            this.tmrLoading.Enabled = true;
            this.tmrLoading.Interval = 500;
            this.tmrLoading.Tick += new System.EventHandler(this.tmrLoading_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 199);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERP ver 1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSplash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSplash)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbKeys)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picSplash;
        private System.Windows.Forms.LinkLabel lnkLabel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbKeys;
        private System.Windows.Forms.Label lblSys;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pbSplash;
        private System.Windows.Forms.Timer tmrLoading;
    }
}