namespace ClientManagementSystem.UI
{
    partial class MainUIForUser
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.userButton = new System.Windows.Forms.Button();
            this.buttonSalesClient = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.feedbackButton = new System.Windows.Forms.Button();
            this.emailBankButton = new System.Windows.Forms.Button();
            this.logOutButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.userButton);
            this.groupBox1.Controls.Add(this.buttonSalesClient);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.feedbackButton);
            this.groupBox1.Controls.Add(this.emailBankButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 538);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // userButton
            // 
            this.userButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userButton.Location = new System.Drawing.Point(21, 338);
            this.userButton.Name = "userButton";
            this.userButton.Size = new System.Drawing.Size(127, 61);
            this.userButton.TabIndex = 15;
            this.userButton.Text = "User Management";
            this.userButton.UseVisualStyleBackColor = true;
            this.userButton.Click += new System.EventHandler(this.userButton_Click);
            // 
            // buttonSalesClient
            // 
            this.buttonSalesClient.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSalesClient.Location = new System.Drawing.Point(21, 102);
            this.buttonSalesClient.Name = "buttonSalesClient";
            this.buttonSalesClient.Size = new System.Drawing.Size(127, 56);
            this.buttonSalesClient.TabIndex = 13;
            this.buttonSalesClient.Text = "Sales Client";
            this.buttonSalesClient.UseVisualStyleBackColor = true;
            this.buttonSalesClient.Click += new System.EventHandler(this.buttonSalesClient_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(21, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 63);
            this.button1.TabIndex = 12;
            this.button1.Text = "Inquiry Client";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // feedbackButton
            // 
            this.feedbackButton.BackColor = System.Drawing.Color.White;
            this.feedbackButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedbackButton.Location = new System.Drawing.Point(21, 173);
            this.feedbackButton.Name = "feedbackButton";
            this.feedbackButton.Size = new System.Drawing.Size(127, 53);
            this.feedbackButton.TabIndex = 11;
            this.feedbackButton.Text = "Feed Back";
            this.feedbackButton.UseVisualStyleBackColor = false;
            this.feedbackButton.Click += new System.EventHandler(this.feedbackButton_Click);
            // 
            // emailBankButton
            // 
            this.emailBankButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailBankButton.Location = new System.Drawing.Point(21, 249);
            this.emailBankButton.Name = "emailBankButton";
            this.emailBankButton.Size = new System.Drawing.Size(127, 58);
            this.emailBankButton.TabIndex = 10;
            this.emailBankButton.Text = "E-mail Bank";
            this.emailBankButton.UseVisualStyleBackColor = true;
            this.emailBankButton.Click += new System.EventHandler(this.emailBankButton_Click);
            // 
            // logOutButton
            // 
            this.logOutButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logOutButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logOutButton.Location = new System.Drawing.Point(948, 2);
            this.logOutButton.Name = "logOutButton";
            this.logOutButton.Size = new System.Drawing.Size(73, 57);
            this.logOutButton.TabIndex = 16;
            this.logOutButton.Text = "LogOut";
            this.logOutButton.UseVisualStyleBackColor = false;
            this.logOutButton.Click += new System.EventHandler(this.logOutButton_Click);
            // 
            // MainUIForUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ClientManagementSystem.Properties.Resources.Clint_managment_System_4055;
            this.ClientSize = new System.Drawing.Size(1022, 681);
            this.ControlBox = false;
            this.Controls.Add(this.logOutButton);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "MainUIForUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainUIForUser";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button emailBankButton;
        private System.Windows.Forms.Button feedbackButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSalesClient;
        private System.Windows.Forms.Button logOutButton;
        private System.Windows.Forms.Button userButton;
    }
}