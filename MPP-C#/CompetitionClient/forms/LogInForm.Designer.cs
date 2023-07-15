namespace CompetitionClient.forms
{
    partial class LogInForm
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
            usernameBox = new TextBox();
            passwordBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            loginButton = new Button();
            SuspendLayout();
            // 
            // usernameBox
            // 
            usernameBox.Location = new Point(96, 87);
            usernameBox.Name = "usernameBox";
            usernameBox.Size = new Size(272, 27);
            usernameBox.TabIndex = 0;
            // 
            // passwordBox
            // 
            passwordBox.Location = new Point(96, 202);
            passwordBox.Name = "passwordBox";
            passwordBox.Size = new Size(272, 27);
            passwordBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 94);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 2;
            label1.Text = "Username:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 209);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 3;
            label2.Text = "Password:";
            // 
            // loginButton
            // 
            loginButton.Location = new Point(12, 385);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(356, 29);
            loginButton.TabIndex = 4;
            loginButton.Text = "Log In";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // LogInForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(399, 579);
            Controls.Add(loginButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(passwordBox);
            Controls.Add(usernameBox);
            Name = "LogInForm";
            Text = "Log In";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox usernameBox;
        private TextBox passwordBox;
        private Label label1;
        private Label label2;
        private Button loginButton;
    }
}