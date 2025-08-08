namespace KeyboardGameV2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mnuStrip = new MenuStrip();
            mnuLoad = new ToolStripMenuItem();
            mnuStart = new ToolStripMenuItem();
            mnuOptions = new ToolStripMenuItem();
            mnuLetterPool = new ToolStripMenuItem();
            optSorted = new ToolStripMenuItem();
            optPoints = new ToolStripMenuItem();
            optSpaces = new ToolStripMenuItem();
            mnuPlayers = new ToolStripMenuItem();
            optP1 = new ToolStripMenuItem();
            optP2 = new ToolStripMenuItem();
            optP3 = new ToolStripMenuItem();
            optP4 = new ToolStripMenuItem();
            Timer = new System.Windows.Forms.Timer(components);
            lblTimer = new Label();
            barTimer = new ProgressBar();
            lblLetterPool = new Label();
            boxP1 = new GroupBox();
            lblP1Score = new Label();
            btnP1WorthPoints = new Button();
            btnP1InDictionary = new Button();
            lblP1CurrentWord = new Label();
            btnP1Heartbeat = new Button();
            boxP2 = new GroupBox();
            lblP2Score = new Label();
            btnP2WorthPoints = new Button();
            btnP2InDictionary = new Button();
            lblP2CurrentWord = new Label();
            btnP2Heartbeat = new Button();
            boxP3 = new GroupBox();
            lblP3Score = new Label();
            btnP3WorthPoints = new Button();
            btnP3InDictionary = new Button();
            lblP3CurrentWord = new Label();
            btnP3Heartbeat = new Button();
            boxP4 = new GroupBox();
            lblP4Score = new Label();
            btnP4WorthPoints = new Button();
            btnP4InDictionary = new Button();
            lblP4CurrentWord = new Label();
            btnP4Heartbeat = new Button();
            dgvScoreboard = new DataGridView();
            mnuStrip.SuspendLayout();
            boxP1.SuspendLayout();
            boxP2.SuspendLayout();
            boxP3.SuspendLayout();
            boxP4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvScoreboard).BeginInit();
            SuspendLayout();
            // 
            // mnuStrip
            // 
            mnuStrip.ImageScalingSize = new Size(20, 20);
            mnuStrip.Items.AddRange(new ToolStripItem[] { mnuLoad, mnuStart, mnuOptions, mnuPlayers });
            mnuStrip.Location = new Point(0, 0);
            mnuStrip.Name = "mnuStrip";
            mnuStrip.Padding = new Padding(7, 3, 0, 3);
            mnuStrip.Size = new Size(914, 30);
            mnuStrip.TabIndex = 0;
            mnuStrip.Text = "menuStrip1";
            // 
            // mnuLoad
            // 
            mnuLoad.Name = "mnuLoad";
            mnuLoad.Size = new Size(128, 24);
            mnuLoad.Text = "Load Dictionary";
            mnuLoad.Click += Click_mnuLoad;
            // 
            // mnuStart
            // 
            mnuStart.Enabled = false;
            mnuStart.Name = "mnuStart";
            mnuStart.Size = new Size(97, 24);
            mnuStart.Text = "Start Game";
            mnuStart.Click += Click_mnuStart;
            // 
            // mnuOptions
            // 
            mnuOptions.DropDownItems.AddRange(new ToolStripItem[] { mnuLetterPool });
            mnuOptions.Name = "mnuOptions";
            mnuOptions.Size = new Size(75, 24);
            mnuOptions.Text = "Options";
            // 
            // mnuLetterPool
            // 
            mnuLetterPool.DropDownItems.AddRange(new ToolStripItem[] { optSorted, optPoints, optSpaces });
            mnuLetterPool.Name = "mnuLetterPool";
            mnuLetterPool.Size = new Size(224, 26);
            mnuLetterPool.Text = "Letter Pool";
            // 
            // optSorted
            // 
            optSorted.Checked = true;
            optSorted.CheckOnClick = true;
            optSorted.CheckState = CheckState.Checked;
            optSorted.Name = "optSorted";
            optSorted.Size = new Size(224, 26);
            optSorted.Text = "Sorted";
            // 
            // optPoints
            // 
            optPoints.Checked = true;
            optPoints.CheckOnClick = true;
            optPoints.CheckState = CheckState.Checked;
            optPoints.Name = "optPoints";
            optPoints.Size = new Size(224, 26);
            optPoints.Text = "Points";
            // 
            // optSpaces
            // 
            optSpaces.Checked = true;
            optSpaces.CheckOnClick = true;
            optSpaces.CheckState = CheckState.Checked;
            optSpaces.Name = "optSpaces";
            optSpaces.Size = new Size(224, 26);
            optSpaces.Text = "Spaces";
            // 
            // mnuPlayers
            // 
            mnuPlayers.DropDownItems.AddRange(new ToolStripItem[] { optP1, optP2, optP3, optP4 });
            mnuPlayers.Name = "mnuPlayers";
            mnuPlayers.Size = new Size(69, 24);
            mnuPlayers.Text = "Players";
            // 
            // optP1
            // 
            optP1.CheckOnClick = true;
            optP1.Name = "optP1";
            optP1.Size = new Size(224, 26);
            optP1.Text = "Player 1";
            optP1.Click += Click_optP1;
            // 
            // optP2
            // 
            optP2.CheckOnClick = true;
            optP2.Name = "optP2";
            optP2.Size = new Size(224, 26);
            optP2.Text = "Player 2";
            optP2.Click += Click_optP2;
            // 
            // optP3
            // 
            optP3.CheckOnClick = true;
            optP3.Name = "optP3";
            optP3.Size = new Size(224, 26);
            optP3.Text = "Player 3";
            optP3.Click += Click_optP3;
            // 
            // optP4
            // 
            optP4.CheckOnClick = true;
            optP4.Name = "optP4";
            optP4.Size = new Size(224, 26);
            optP4.Text = "Player 4";
            optP4.Click += Click_optP4;
            // 
            // Timer
            // 
            Timer.Interval = 1000;
            Timer.Tick += Timer_Tick;
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTimer.Location = new Point(14, 32);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(89, 41);
            lblTimer.TabIndex = 1;
            lblTimer.Text = "00:00";
            // 
            // barTimer
            // 
            barTimer.Location = new Point(102, 32);
            barTimer.Margin = new Padding(3, 4, 3, 4);
            barTimer.Name = "barTimer";
            barTimer.Size = new Size(799, 43);
            barTimer.TabIndex = 2;
            // 
            // lblLetterPool
            // 
            lblLetterPool.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLetterPool.Location = new Point(14, 79);
            lblLetterPool.Name = "lblLetterPool";
            lblLetterPool.Size = new Size(887, 93);
            lblLetterPool.TabIndex = 3;
            lblLetterPool.Text = "1234567890123456789012345678901234567890";
            lblLetterPool.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // boxP1
            // 
            boxP1.Controls.Add(lblP1Score);
            boxP1.Controls.Add(btnP1WorthPoints);
            boxP1.Controls.Add(btnP1InDictionary);
            boxP1.Controls.Add(lblP1CurrentWord);
            boxP1.Controls.Add(btnP1Heartbeat);
            boxP1.Location = new Point(14, 176);
            boxP1.Margin = new Padding(3, 4, 3, 4);
            boxP1.Name = "boxP1";
            boxP1.Padding = new Padding(3, 4, 3, 4);
            boxP1.Size = new Size(477, 128);
            boxP1.TabIndex = 4;
            boxP1.TabStop = false;
            boxP1.Text = "Player 1";
            // 
            // lblP1Score
            // 
            lblP1Score.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP1Score.Location = new Point(151, 72);
            lblP1Score.Name = "lblP1Score";
            lblP1Score.Size = new Size(319, 43);
            lblP1Score.TabIndex = 6;
            lblP1Score.Text = "12345678901234567890";
            lblP1Score.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnP1WorthPoints
            // 
            btnP1WorthPoints.Enabled = false;
            btnP1WorthPoints.Location = new Point(103, 72);
            btnP1WorthPoints.Margin = new Padding(3, 4, 3, 4);
            btnP1WorthPoints.Name = "btnP1WorthPoints";
            btnP1WorthPoints.Size = new Size(41, 43);
            btnP1WorthPoints.TabIndex = 7;
            btnP1WorthPoints.UseVisualStyleBackColor = true;
            // 
            // btnP1InDictionary
            // 
            btnP1InDictionary.Enabled = false;
            btnP1InDictionary.Location = new Point(55, 72);
            btnP1InDictionary.Margin = new Padding(3, 4, 3, 4);
            btnP1InDictionary.Name = "btnP1InDictionary";
            btnP1InDictionary.Size = new Size(41, 43);
            btnP1InDictionary.TabIndex = 6;
            btnP1InDictionary.UseVisualStyleBackColor = true;
            // 
            // lblP1CurrentWord
            // 
            lblP1CurrentWord.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP1CurrentWord.Location = new Point(7, 25);
            lblP1CurrentWord.Name = "lblP1CurrentWord";
            lblP1CurrentWord.Size = new Size(467, 43);
            lblP1CurrentWord.TabIndex = 5;
            lblP1CurrentWord.Text = "123456789012345678901234567890";
            lblP1CurrentWord.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnP1Heartbeat
            // 
            btnP1Heartbeat.Enabled = false;
            btnP1Heartbeat.Location = new Point(7, 72);
            btnP1Heartbeat.Margin = new Padding(3, 4, 3, 4);
            btnP1Heartbeat.Name = "btnP1Heartbeat";
            btnP1Heartbeat.Size = new Size(41, 43);
            btnP1Heartbeat.TabIndex = 5;
            btnP1Heartbeat.UseVisualStyleBackColor = true;
            // 
            // boxP2
            // 
            boxP2.Controls.Add(lblP2Score);
            boxP2.Controls.Add(btnP2WorthPoints);
            boxP2.Controls.Add(btnP2InDictionary);
            boxP2.Controls.Add(lblP2CurrentWord);
            boxP2.Controls.Add(btnP2Heartbeat);
            boxP2.Location = new Point(14, 312);
            boxP2.Margin = new Padding(3, 4, 3, 4);
            boxP2.Name = "boxP2";
            boxP2.Padding = new Padding(3, 4, 3, 4);
            boxP2.Size = new Size(477, 128);
            boxP2.TabIndex = 5;
            boxP2.TabStop = false;
            boxP2.Text = "Player 2";
            // 
            // lblP2Score
            // 
            lblP2Score.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP2Score.Location = new Point(151, 72);
            lblP2Score.Name = "lblP2Score";
            lblP2Score.Size = new Size(319, 43);
            lblP2Score.TabIndex = 6;
            lblP2Score.Text = "12345678901234567890";
            lblP2Score.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnP2WorthPoints
            // 
            btnP2WorthPoints.Enabled = false;
            btnP2WorthPoints.Location = new Point(103, 72);
            btnP2WorthPoints.Margin = new Padding(3, 4, 3, 4);
            btnP2WorthPoints.Name = "btnP2WorthPoints";
            btnP2WorthPoints.Size = new Size(41, 43);
            btnP2WorthPoints.TabIndex = 7;
            btnP2WorthPoints.UseVisualStyleBackColor = true;
            // 
            // btnP2InDictionary
            // 
            btnP2InDictionary.Enabled = false;
            btnP2InDictionary.Location = new Point(55, 72);
            btnP2InDictionary.Margin = new Padding(3, 4, 3, 4);
            btnP2InDictionary.Name = "btnP2InDictionary";
            btnP2InDictionary.Size = new Size(41, 43);
            btnP2InDictionary.TabIndex = 6;
            btnP2InDictionary.UseVisualStyleBackColor = true;
            // 
            // lblP2CurrentWord
            // 
            lblP2CurrentWord.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP2CurrentWord.Location = new Point(7, 25);
            lblP2CurrentWord.Name = "lblP2CurrentWord";
            lblP2CurrentWord.Size = new Size(467, 43);
            lblP2CurrentWord.TabIndex = 5;
            lblP2CurrentWord.Text = "123456789012345678901234567890";
            lblP2CurrentWord.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnP2Heartbeat
            // 
            btnP2Heartbeat.Enabled = false;
            btnP2Heartbeat.Location = new Point(7, 72);
            btnP2Heartbeat.Margin = new Padding(3, 4, 3, 4);
            btnP2Heartbeat.Name = "btnP2Heartbeat";
            btnP2Heartbeat.Size = new Size(41, 43);
            btnP2Heartbeat.TabIndex = 5;
            btnP2Heartbeat.UseVisualStyleBackColor = true;
            // 
            // boxP3
            // 
            boxP3.Controls.Add(lblP3Score);
            boxP3.Controls.Add(btnP3WorthPoints);
            boxP3.Controls.Add(btnP3InDictionary);
            boxP3.Controls.Add(lblP3CurrentWord);
            boxP3.Controls.Add(btnP3Heartbeat);
            boxP3.Location = new Point(14, 448);
            boxP3.Margin = new Padding(3, 4, 3, 4);
            boxP3.Name = "boxP3";
            boxP3.Padding = new Padding(3, 4, 3, 4);
            boxP3.Size = new Size(477, 128);
            boxP3.TabIndex = 8;
            boxP3.TabStop = false;
            boxP3.Text = "Player 3";
            // 
            // lblP3Score
            // 
            lblP3Score.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP3Score.Location = new Point(151, 72);
            lblP3Score.Name = "lblP3Score";
            lblP3Score.Size = new Size(319, 43);
            lblP3Score.TabIndex = 6;
            lblP3Score.Text = "12345678901234567890";
            lblP3Score.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnP3WorthPoints
            // 
            btnP3WorthPoints.Enabled = false;
            btnP3WorthPoints.Location = new Point(103, 72);
            btnP3WorthPoints.Margin = new Padding(3, 4, 3, 4);
            btnP3WorthPoints.Name = "btnP3WorthPoints";
            btnP3WorthPoints.Size = new Size(41, 43);
            btnP3WorthPoints.TabIndex = 7;
            btnP3WorthPoints.UseVisualStyleBackColor = true;
            // 
            // btnP3InDictionary
            // 
            btnP3InDictionary.Enabled = false;
            btnP3InDictionary.Location = new Point(55, 72);
            btnP3InDictionary.Margin = new Padding(3, 4, 3, 4);
            btnP3InDictionary.Name = "btnP3InDictionary";
            btnP3InDictionary.Size = new Size(41, 43);
            btnP3InDictionary.TabIndex = 6;
            btnP3InDictionary.UseVisualStyleBackColor = true;
            // 
            // lblP3CurrentWord
            // 
            lblP3CurrentWord.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP3CurrentWord.Location = new Point(7, 25);
            lblP3CurrentWord.Name = "lblP3CurrentWord";
            lblP3CurrentWord.Size = new Size(467, 43);
            lblP3CurrentWord.TabIndex = 5;
            lblP3CurrentWord.Text = "123456789012345678901234567890";
            lblP3CurrentWord.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnP3Heartbeat
            // 
            btnP3Heartbeat.Enabled = false;
            btnP3Heartbeat.Location = new Point(7, 72);
            btnP3Heartbeat.Margin = new Padding(3, 4, 3, 4);
            btnP3Heartbeat.Name = "btnP3Heartbeat";
            btnP3Heartbeat.Size = new Size(41, 43);
            btnP3Heartbeat.TabIndex = 5;
            btnP3Heartbeat.UseVisualStyleBackColor = true;
            // 
            // boxP4
            // 
            boxP4.Controls.Add(lblP4Score);
            boxP4.Controls.Add(btnP4WorthPoints);
            boxP4.Controls.Add(btnP4InDictionary);
            boxP4.Controls.Add(lblP4CurrentWord);
            boxP4.Controls.Add(btnP4Heartbeat);
            boxP4.Location = new Point(14, 584);
            boxP4.Margin = new Padding(3, 4, 3, 4);
            boxP4.Name = "boxP4";
            boxP4.Padding = new Padding(3, 4, 3, 4);
            boxP4.Size = new Size(477, 128);
            boxP4.TabIndex = 9;
            boxP4.TabStop = false;
            boxP4.Text = "Player 4";
            // 
            // lblP4Score
            // 
            lblP4Score.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP4Score.Location = new Point(151, 72);
            lblP4Score.Name = "lblP4Score";
            lblP4Score.Size = new Size(319, 43);
            lblP4Score.TabIndex = 6;
            lblP4Score.Text = "12345678901234567890";
            lblP4Score.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnP4WorthPoints
            // 
            btnP4WorthPoints.Enabled = false;
            btnP4WorthPoints.Location = new Point(103, 72);
            btnP4WorthPoints.Margin = new Padding(3, 4, 3, 4);
            btnP4WorthPoints.Name = "btnP4WorthPoints";
            btnP4WorthPoints.Size = new Size(41, 43);
            btnP4WorthPoints.TabIndex = 7;
            btnP4WorthPoints.UseVisualStyleBackColor = true;
            // 
            // btnP4InDictionary
            // 
            btnP4InDictionary.Enabled = false;
            btnP4InDictionary.Location = new Point(55, 72);
            btnP4InDictionary.Margin = new Padding(3, 4, 3, 4);
            btnP4InDictionary.Name = "btnP4InDictionary";
            btnP4InDictionary.Size = new Size(41, 43);
            btnP4InDictionary.TabIndex = 6;
            btnP4InDictionary.UseVisualStyleBackColor = true;
            // 
            // lblP4CurrentWord
            // 
            lblP4CurrentWord.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP4CurrentWord.Location = new Point(7, 25);
            lblP4CurrentWord.Name = "lblP4CurrentWord";
            lblP4CurrentWord.Size = new Size(467, 43);
            lblP4CurrentWord.TabIndex = 5;
            lblP4CurrentWord.Text = "123456789012345678901234567890";
            lblP4CurrentWord.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnP4Heartbeat
            // 
            btnP4Heartbeat.Enabled = false;
            btnP4Heartbeat.Location = new Point(7, 72);
            btnP4Heartbeat.Margin = new Padding(3, 4, 3, 4);
            btnP4Heartbeat.Name = "btnP4Heartbeat";
            btnP4Heartbeat.Size = new Size(41, 43);
            btnP4Heartbeat.TabIndex = 5;
            btnP4Heartbeat.UseVisualStyleBackColor = true;
            // 
            // dgvScoreboard
            // 
            dgvScoreboard.AllowUserToAddRows = false;
            dgvScoreboard.AllowUserToDeleteRows = false;
            dgvScoreboard.AllowUserToOrderColumns = true;
            dgvScoreboard.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvScoreboard.Location = new Point(497, 176);
            dgvScoreboard.Margin = new Padding(3, 4, 3, 4);
            dgvScoreboard.Name = "dgvScoreboard";
            dgvScoreboard.ReadOnly = true;
            dgvScoreboard.RowHeadersWidth = 51;
            dgvScoreboard.Size = new Size(403, 536);
            dgvScoreboard.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 791);
            Controls.Add(dgvScoreboard);
            Controls.Add(boxP4);
            Controls.Add(boxP3);
            Controls.Add(boxP2);
            Controls.Add(boxP1);
            Controls.Add(lblLetterPool);
            Controls.Add(barTimer);
            Controls.Add(lblTimer);
            Controls.Add(mnuStrip);
            MainMenuStrip = mnuStrip;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Not Scrabble or Boggle";
            mnuStrip.ResumeLayout(false);
            mnuStrip.PerformLayout();
            boxP1.ResumeLayout(false);
            boxP2.ResumeLayout(false);
            boxP3.ResumeLayout(false);
            boxP4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvScoreboard).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mnuStrip;
        private ToolStripMenuItem mnuLoad;
        private ToolStripMenuItem mnuStart;
        private ToolStripMenuItem mnuOptions;
        private ToolStripMenuItem mnuLetterPool;
        private ToolStripMenuItem optSorted;
        private ToolStripMenuItem optPoints;
        private ToolStripMenuItem optSpaces;
        private ToolStripMenuItem mnuPlayers;
        private ToolStripMenuItem optP1;
        private ToolStripMenuItem optP2;
        private ToolStripMenuItem optP3;
        private ToolStripMenuItem optP4;
        private System.Windows.Forms.Timer Timer;
        private Label lblTimer;
        private ProgressBar barTimer;
        private Label lblLetterPool;
        private GroupBox boxP1;
        private Label lblP1CurrentWord;
        private Button btnP1Heartbeat;
        private Label lblP1Score;
        private Button btnP1WorthPoints;
        private Button btnP1InDictionary;
        private GroupBox boxP2;
        private Label lblP2Score;
        private Button btnP2WorthPoints;
        private Button btnP2InDictionary;
        private Label lblP2CurrentWord;
        private Button btnP2Heartbeat;
        private GroupBox boxP3;
        private Label lblP3Score;
        private Button btnP3WorthPoints;
        private Button btnP3InDictionary;
        private Label lblP3CurrentWord;
        private Button btnP3Heartbeat;
        private GroupBox boxP4;
        private Label lblP4Score;
        private Button btnP4WorthPoints;
        private Button btnP4InDictionary;
        private Label lblP4CurrentWord;
        private Button btnP4Heartbeat;
        private DataGridView dgvScoreboard;
    }
}
