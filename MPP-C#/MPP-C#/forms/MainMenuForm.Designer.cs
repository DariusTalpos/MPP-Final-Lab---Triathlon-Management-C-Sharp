namespace MPP_C_.forms
{
    partial class MainMenuForm
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
            logoutButton = new Button();
            loadDataButton = new Button();
            addDataButton = new Button();
            userLabel = new Label();
            label2 = new Label();
            participantsGridView = new DataGridView();
            roundsGridView = new DataGridView();
            scoresGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)participantsGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)roundsGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)scoresGridView).BeginInit();
            SuspendLayout();
            // 
            // logoutButton
            // 
            logoutButton.Location = new Point(774, 12);
            logoutButton.Name = "logoutButton";
            logoutButton.Size = new Size(294, 65);
            logoutButton.TabIndex = 0;
            logoutButton.Text = "Log Out";
            logoutButton.UseVisualStyleBackColor = true;
            logoutButton.Click += logoutButton_Click;
            // 
            // loadDataButton
            // 
            loadDataButton.Location = new Point(24, 655);
            loadDataButton.Name = "loadDataButton";
            loadDataButton.Size = new Size(496, 29);
            loadDataButton.TabIndex = 1;
            loadDataButton.Text = "Load Data";
            loadDataButton.UseVisualStyleBackColor = true;
            loadDataButton.Click += loadDataButton_Click;
            // 
            // addDataButton
            // 
            addDataButton.Location = new Point(608, 655);
            addDataButton.Name = "addDataButton";
            addDataButton.Size = new Size(460, 29);
            addDataButton.TabIndex = 2;
            addDataButton.Text = "Add New Round/Score Data";
            addDataButton.UseVisualStyleBackColor = true;
            addDataButton.Click += addDataButton_Click;
            // 
            // userLabel
            // 
            userLabel.AutoSize = true;
            userLabel.Location = new Point(24, 12);
            userLabel.Name = "userLabel";
            userLabel.Size = new Size(50, 20);
            userLabel.TabIndex = 3;
            userLabel.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 304);
            label2.Name = "label2";
            label2.Size = new Size(61, 20);
            label2.TabIndex = 4;
            label2.Text = "Rounds:";
            // 
            // participantsGridView
            // 
            participantsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            participantsGridView.Location = new Point(24, 35);
            participantsGridView.Name = "participantsGridView";
            participantsGridView.RowHeadersWidth = 51;
            participantsGridView.RowTemplate.Height = 29;
            participantsGridView.Size = new Size(585, 266);
            participantsGridView.TabIndex = 5;
            // 
            // roundsGridView
            // 
            roundsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            roundsGridView.Location = new Point(24, 327);
            roundsGridView.Name = "roundsGridView";
            roundsGridView.RowHeadersWidth = 51;
            roundsGridView.RowTemplate.Height = 29;
            roundsGridView.Size = new Size(496, 304);
            roundsGridView.TabIndex = 6;
            // 
            // scoresGridView
            // 
            scoresGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            scoresGridView.Location = new Point(608, 327);
            scoresGridView.Name = "scoresGridView";
            scoresGridView.RowHeadersWidth = 51;
            scoresGridView.RowTemplate.Height = 29;
            scoresGridView.Size = new Size(460, 304);
            scoresGridView.TabIndex = 7;
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 696);
            Controls.Add(scoresGridView);
            Controls.Add(roundsGridView);
            Controls.Add(participantsGridView);
            Controls.Add(label2);
            Controls.Add(userLabel);
            Controls.Add(addDataButton);
            Controls.Add(loadDataButton);
            Controls.Add(logoutButton);
            Name = "MainMenuForm";
            Text = "Main Menu";
            ((System.ComponentModel.ISupportInitialize)participantsGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)roundsGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)scoresGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button logoutButton;
        private Button loadDataButton;
        private Button addDataButton;
        private Label label1;
        private Label label2;
        private DataGridView participantsGridView;
        private DataGridView roundsGridView;
        private DataGridView scoresGridView;
        private Label userLabel;
    }
}