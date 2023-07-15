namespace CompetitionClient.forms
{
    partial class AddDataForm
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
            participantGridView = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            roundBox = new TextBox();
            pointsBox = new TextBox();
            saveDataButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)participantGridView).BeginInit();
            SuspendLayout();
            // 
            // participantGridView
            // 
            participantGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            participantGridView.Location = new Point(40, 75);
            participantGridView.Name = "participantGridView";
            participantGridView.RowHeadersWidth = 51;
            participantGridView.RowTemplate.Height = 29;
            participantGridView.Size = new Size(505, 341);
            participantGridView.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 1;
            label1.Text = "Round:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 453);
            label2.Name = "label2";
            label2.Size = new Size(51, 20);
            label2.TabIndex = 2;
            label2.Text = "Points:";
            // 
            // roundBox
            // 
            roundBox.Location = new Point(73, 15);
            roundBox.Name = "roundBox";
            roundBox.Size = new Size(472, 27);
            roundBox.TabIndex = 3;
            // 
            // pointsBox
            // 
            pointsBox.Location = new Point(73, 446);
            pointsBox.Name = "pointsBox";
            pointsBox.Size = new Size(472, 27);
            pointsBox.TabIndex = 4;
            // 
            // saveDataButton
            // 
            saveDataButton.Location = new Point(12, 590);
            saveDataButton.Name = "saveDataButton";
            saveDataButton.Size = new Size(237, 29);
            saveDataButton.TabIndex = 5;
            saveDataButton.Text = "Save Data";
            saveDataButton.UseVisualStyleBackColor = true;
            saveDataButton.Click += saveDataButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(304, 590);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(241, 29);
            cancelButton.TabIndex = 6;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // AddDataForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(564, 642);
            Controls.Add(cancelButton);
            Controls.Add(saveDataButton);
            Controls.Add(pointsBox);
            Controls.Add(roundBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(participantGridView);
            Name = "AddDataForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)participantGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView participantGridView;
        private Label label1;
        private Label label2;
        private TextBox roundBox;
        private TextBox pointsBox;
        private Button saveDataButton;
        private Button cancelButton;
    }
}