namespace KeyboardGameV2
{
    partial class frmGame
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
            mnuLetterPoolFormat = new ToolStripMenuItem();
            optSorted = new ToolStripMenuItem();
            optPoints = new ToolStripMenuItem();
            optSpaces = new ToolStripMenuItem();
            mnuLetterMode = new ToolStripMenuItem();
            optDictionarySelect = new ToolStripMenuItem();
            optBagSelect = new ToolStripMenuItem();
            mnuTime = new ToolStripMenuItem();
            optTime = new ToolStripTextBox();
            mnuPoolLetterCount = new ToolStripMenuItem();
            optPoolLetterCount = new ToolStripTextBox();
            mnuShowWords = new ToolStripMenuItem();
            optShowWords = new ToolStripTextBox();
            mnuPlayers = new ToolStripMenuItem();
            optP1 = new ToolStripMenuItem();
            optP2 = new ToolStripMenuItem();
            optP3 = new ToolStripMenuItem();
            optP4 = new ToolStripMenuItem();
            mnuDictionaryTools = new ToolStripMenuItem();
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
            mnuStrip.Items.AddRange(new ToolStripItem[] { mnuLoad, mnuStart, mnuOptions, mnuPlayers, mnuDictionaryTools });
            mnuStrip.Location = new Point(0, 0);
            mnuStrip.Name = "mnuStrip";
            mnuStrip.Padding = new Padding(18, 7, 0, 7);
            mnuStrip.Size = new Size(2285, 71);
            mnuStrip.TabIndex = 0;
            mnuStrip.Text = "menuStrip1";
            // 
            // mnuLoad
            // 
            mnuLoad.Name = "mnuLoad";
            mnuLoad.Size = new Size(296, 57);
            mnuLoad.Text = "Load Dictionary";
            mnuLoad.Click += Click_mnuLoad;
            // 
            // mnuStart
            // 
            mnuStart.Enabled = false;
            mnuStart.Name = "mnuStart";
            mnuStart.Size = new Size(224, 57);
            mnuStart.Text = "Start Game";
            mnuStart.Click += Click_mnuStart;
            // 
            // mnuOptions
            // 
            mnuOptions.DropDownItems.AddRange(new ToolStripItem[] { mnuLetterPoolFormat, mnuLetterMode, mnuTime, mnuPoolLetterCount, mnuShowWords });
            mnuOptions.Name = "mnuOptions";
            mnuOptions.Size = new Size(173, 57);
            mnuOptions.Text = "Options";
            // 
            // mnuLetterPoolFormat
            // 
            mnuLetterPoolFormat.DropDownItems.AddRange(new ToolStripItem[] { optSorted, optPoints, optSpaces });
            mnuLetterPoolFormat.Name = "mnuLetterPoolFormat";
            mnuLetterPoolFormat.Size = new Size(923, 66);
            mnuLetterPoolFormat.Text = "Letter Pool Format";
            // 
            // optSorted
            // 
            optSorted.Checked = true;
            optSorted.CheckOnClick = true;
            optSorted.CheckState = CheckState.Checked;
            optSorted.Name = "optSorted";
            optSorted.Size = new Size(327, 66);
            optSorted.Text = "Sorted";
            // 
            // optPoints
            // 
            optPoints.Checked = true;
            optPoints.CheckOnClick = true;
            optPoints.CheckState = CheckState.Checked;
            optPoints.Name = "optPoints";
            optPoints.Size = new Size(327, 66);
            optPoints.Text = "Points";
            // 
            // optSpaces
            // 
            optSpaces.Checked = true;
            optSpaces.CheckOnClick = true;
            optSpaces.CheckState = CheckState.Checked;
            optSpaces.Name = "optSpaces";
            optSpaces.Size = new Size(327, 66);
            optSpaces.Text = "Spaces";
            // 
            // mnuLetterMode
            // 
            mnuLetterMode.DropDownItems.AddRange(new ToolStripItem[] { optDictionarySelect, optBagSelect });
            mnuLetterMode.Name = "mnuLetterMode";
            mnuLetterMode.Size = new Size(923, 66);
            mnuLetterMode.Text = "Letter Mode";
            // 
            // optDictionarySelect
            // 
            optDictionarySelect.Checked = true;
            optDictionarySelect.CheckState = CheckState.Checked;
            optDictionarySelect.Name = "optDictionarySelect";
            optDictionarySelect.Size = new Size(379, 66);
            optDictionarySelect.Text = "Dictionary";
            optDictionarySelect.Click += Click_LetterMode;
            // 
            // optBagSelect
            // 
            optBagSelect.Name = "optBagSelect";
            optBagSelect.Size = new Size(379, 66);
            optBagSelect.Text = "Bag";
            optBagSelect.Click += Click_LetterMode;
            // 
            // mnuTime
            // 
            mnuTime.DropDownItems.AddRange(new ToolStripItem[] { optTime });
            mnuTime.Name = "mnuTime";
            mnuTime.Size = new Size(923, 66);
            mnuTime.Text = "Game Timer (seconds)";
            // 
            // optTime
            // 
            optTime.AutoSize = false;
            optTime.Name = "optTime";
            optTime.Size = new Size(150, 55);
            optTime.Text = "120";
            optTime.TextBoxTextAlign = HorizontalAlignment.Center;
            optTime.TextChanged += TextChanged_optTime;
            // 
            // mnuPoolLetterCount
            // 
            mnuPoolLetterCount.DropDownItems.AddRange(new ToolStripItem[] { optPoolLetterCount });
            mnuPoolLetterCount.Enabled = false;
            mnuPoolLetterCount.Name = "mnuPoolLetterCount";
            mnuPoolLetterCount.Size = new Size(923, 66);
            mnuPoolLetterCount.Text = "Pool Letter Count";
            // 
            // optPoolLetterCount
            // 
            optPoolLetterCount.AutoSize = false;
            optPoolLetterCount.Name = "optPoolLetterCount";
            optPoolLetterCount.Size = new Size(150, 55);
            optPoolLetterCount.Text = "20";
            optPoolLetterCount.TextBoxTextAlign = HorizontalAlignment.Center;
            optPoolLetterCount.TextChanged += TextChanged_optPoolLetterCount;
            // 
            // mnuShowWords
            // 
            mnuShowWords.Checked = true;
            mnuShowWords.CheckOnClick = true;
            mnuShowWords.CheckState = CheckState.Checked;
            mnuShowWords.DropDownItems.AddRange(new ToolStripItem[] { optShowWords });
            mnuShowWords.Enabled = false;
            mnuShowWords.Name = "mnuShowWords";
            mnuShowWords.Size = new Size(923, 66);
            mnuShowWords.Text = "Show Words Length X or Longer After Game";
            // 
            // optShowWords
            // 
            optShowWords.AutoSize = false;
            optShowWords.Name = "optShowWords";
            optShowWords.Size = new Size(150, 55);
            optShowWords.Text = "5";
            optShowWords.TextBoxTextAlign = HorizontalAlignment.Center;
            optShowWords.TextChanged += TextChanged_optShowWords;
            // 
            // mnuPlayers
            // 
            mnuPlayers.DropDownItems.AddRange(new ToolStripItem[] { optP1, optP2, optP3, optP4 });
            mnuPlayers.Name = "mnuPlayers";
            mnuPlayers.Size = new Size(159, 57);
            mnuPlayers.Text = "Players";
            // 
            // optP1
            // 
            optP1.CheckOnClick = true;
            optP1.Name = "optP1";
            optP1.Size = new Size(343, 66);
            optP1.Text = "Player 1";
            optP1.Click += Click_optP1;
            // 
            // optP2
            // 
            optP2.CheckOnClick = true;
            optP2.Name = "optP2";
            optP2.Size = new Size(343, 66);
            optP2.Text = "Player 2";
            optP2.Click += Click_optP2;
            // 
            // optP3
            // 
            optP3.CheckOnClick = true;
            optP3.Name = "optP3";
            optP3.Size = new Size(343, 66);
            optP3.Text = "Player 3";
            optP3.Click += Click_optP3;
            // 
            // optP4
            // 
            optP4.CheckOnClick = true;
            optP4.Name = "optP4";
            optP4.Size = new Size(343, 66);
            optP4.Text = "Player 4";
            optP4.Click += Click_optP4;
            // 
            // mnuDictionaryTools
            // 
            mnuDictionaryTools.Alignment = ToolStripItemAlignment.Right;
            mnuDictionaryTools.Name = "mnuDictionaryTools";
            mnuDictionaryTools.Size = new Size(300, 57);
            mnuDictionaryTools.Text = "Dictionary Tools";
            mnuDictionaryTools.Click += this.Click_mnuDictionaryTools;
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
            lblTimer.Location = new Point(35, 77);
            lblTimer.Margin = new Padding(8, 0, 8, 0);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(212, 96);
            lblTimer.TabIndex = 1;
            lblTimer.Text = "00:00";
            // 
            // barTimer
            // 
            barTimer.Location = new Point(255, 77);
            barTimer.Margin = new Padding(8, 10, 8, 10);
            barTimer.Name = "barTimer";
            barTimer.Size = new Size(1998, 103);
            barTimer.TabIndex = 2;
            // 
            // lblLetterPool
            // 
            lblLetterPool.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLetterPool.Location = new Point(35, 190);
            lblLetterPool.Margin = new Padding(8, 0, 8, 0);
            lblLetterPool.Name = "lblLetterPool";
            lblLetterPool.Size = new Size(2218, 223);
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
            boxP1.Enabled = false;
            boxP1.Location = new Point(35, 422);
            boxP1.Margin = new Padding(8, 10, 8, 10);
            boxP1.Name = "boxP1";
            boxP1.Padding = new Padding(8, 10, 8, 10);
            boxP1.Size = new Size(1192, 307);
            boxP1.TabIndex = 4;
            boxP1.TabStop = false;
            boxP1.Text = "Player 1";
            // 
            // lblP1Score
            // 
            lblP1Score.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP1Score.Location = new Point(378, 173);
            lblP1Score.Margin = new Padding(8, 0, 8, 0);
            lblP1Score.Name = "lblP1Score";
            lblP1Score.Size = new Size(798, 103);
            lblP1Score.TabIndex = 6;
            lblP1Score.Text = "12345678901234567890";
            lblP1Score.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnP1WorthPoints
            // 
            btnP1WorthPoints.Enabled = false;
            btnP1WorthPoints.Location = new Point(258, 173);
            btnP1WorthPoints.Margin = new Padding(8, 10, 8, 10);
            btnP1WorthPoints.Name = "btnP1WorthPoints";
            btnP1WorthPoints.Size = new Size(102, 103);
            btnP1WorthPoints.TabIndex = 7;
            btnP1WorthPoints.UseVisualStyleBackColor = true;
            // 
            // btnP1InDictionary
            // 
            btnP1InDictionary.Enabled = false;
            btnP1InDictionary.Location = new Point(138, 173);
            btnP1InDictionary.Margin = new Padding(8, 10, 8, 10);
            btnP1InDictionary.Name = "btnP1InDictionary";
            btnP1InDictionary.Size = new Size(102, 103);
            btnP1InDictionary.TabIndex = 6;
            btnP1InDictionary.UseVisualStyleBackColor = true;
            // 
            // lblP1CurrentWord
            // 
            lblP1CurrentWord.BackColor = SystemColors.Window;
            lblP1CurrentWord.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP1CurrentWord.Location = new Point(18, 60);
            lblP1CurrentWord.Margin = new Padding(8, 0, 8, 0);
            lblP1CurrentWord.Name = "lblP1CurrentWord";
            lblP1CurrentWord.Size = new Size(1168, 103);
            lblP1CurrentWord.TabIndex = 5;
            lblP1CurrentWord.Text = "123456789012345678901234567890";
            lblP1CurrentWord.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnP1Heartbeat
            // 
            btnP1Heartbeat.Enabled = false;
            btnP1Heartbeat.Location = new Point(18, 173);
            btnP1Heartbeat.Margin = new Padding(8, 10, 8, 10);
            btnP1Heartbeat.Name = "btnP1Heartbeat";
            btnP1Heartbeat.Size = new Size(102, 103);
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
            boxP2.Enabled = false;
            boxP2.Location = new Point(35, 749);
            boxP2.Margin = new Padding(8, 10, 8, 10);
            boxP2.Name = "boxP2";
            boxP2.Padding = new Padding(8, 10, 8, 10);
            boxP2.Size = new Size(1192, 307);
            boxP2.TabIndex = 5;
            boxP2.TabStop = false;
            boxP2.Text = "Player 2";
            // 
            // lblP2Score
            // 
            lblP2Score.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP2Score.Location = new Point(378, 173);
            lblP2Score.Margin = new Padding(8, 0, 8, 0);
            lblP2Score.Name = "lblP2Score";
            lblP2Score.Size = new Size(798, 103);
            lblP2Score.TabIndex = 6;
            lblP2Score.Text = "12345678901234567890";
            lblP2Score.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnP2WorthPoints
            // 
            btnP2WorthPoints.Enabled = false;
            btnP2WorthPoints.Location = new Point(258, 173);
            btnP2WorthPoints.Margin = new Padding(8, 10, 8, 10);
            btnP2WorthPoints.Name = "btnP2WorthPoints";
            btnP2WorthPoints.Size = new Size(102, 103);
            btnP2WorthPoints.TabIndex = 7;
            btnP2WorthPoints.UseVisualStyleBackColor = true;
            // 
            // btnP2InDictionary
            // 
            btnP2InDictionary.Enabled = false;
            btnP2InDictionary.Location = new Point(138, 173);
            btnP2InDictionary.Margin = new Padding(8, 10, 8, 10);
            btnP2InDictionary.Name = "btnP2InDictionary";
            btnP2InDictionary.Size = new Size(102, 103);
            btnP2InDictionary.TabIndex = 6;
            btnP2InDictionary.UseVisualStyleBackColor = true;
            // 
            // lblP2CurrentWord
            // 
            lblP2CurrentWord.BackColor = SystemColors.Window;
            lblP2CurrentWord.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP2CurrentWord.Location = new Point(18, 60);
            lblP2CurrentWord.Margin = new Padding(8, 0, 8, 0);
            lblP2CurrentWord.Name = "lblP2CurrentWord";
            lblP2CurrentWord.Size = new Size(1168, 103);
            lblP2CurrentWord.TabIndex = 5;
            lblP2CurrentWord.Text = "123456789012345678901234567890";
            lblP2CurrentWord.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnP2Heartbeat
            // 
            btnP2Heartbeat.Enabled = false;
            btnP2Heartbeat.Location = new Point(18, 173);
            btnP2Heartbeat.Margin = new Padding(8, 10, 8, 10);
            btnP2Heartbeat.Name = "btnP2Heartbeat";
            btnP2Heartbeat.Size = new Size(102, 103);
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
            boxP3.Enabled = false;
            boxP3.Location = new Point(35, 1075);
            boxP3.Margin = new Padding(8, 10, 8, 10);
            boxP3.Name = "boxP3";
            boxP3.Padding = new Padding(8, 10, 8, 10);
            boxP3.Size = new Size(1192, 307);
            boxP3.TabIndex = 8;
            boxP3.TabStop = false;
            boxP3.Text = "Player 3";
            // 
            // lblP3Score
            // 
            lblP3Score.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP3Score.Location = new Point(378, 173);
            lblP3Score.Margin = new Padding(8, 0, 8, 0);
            lblP3Score.Name = "lblP3Score";
            lblP3Score.Size = new Size(798, 103);
            lblP3Score.TabIndex = 6;
            lblP3Score.Text = "12345678901234567890";
            lblP3Score.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnP3WorthPoints
            // 
            btnP3WorthPoints.Enabled = false;
            btnP3WorthPoints.Location = new Point(258, 173);
            btnP3WorthPoints.Margin = new Padding(8, 10, 8, 10);
            btnP3WorthPoints.Name = "btnP3WorthPoints";
            btnP3WorthPoints.Size = new Size(102, 103);
            btnP3WorthPoints.TabIndex = 7;
            btnP3WorthPoints.UseVisualStyleBackColor = true;
            // 
            // btnP3InDictionary
            // 
            btnP3InDictionary.Enabled = false;
            btnP3InDictionary.Location = new Point(138, 173);
            btnP3InDictionary.Margin = new Padding(8, 10, 8, 10);
            btnP3InDictionary.Name = "btnP3InDictionary";
            btnP3InDictionary.Size = new Size(102, 103);
            btnP3InDictionary.TabIndex = 6;
            btnP3InDictionary.UseVisualStyleBackColor = true;
            // 
            // lblP3CurrentWord
            // 
            lblP3CurrentWord.BackColor = SystemColors.Window;
            lblP3CurrentWord.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP3CurrentWord.Location = new Point(18, 60);
            lblP3CurrentWord.Margin = new Padding(8, 0, 8, 0);
            lblP3CurrentWord.Name = "lblP3CurrentWord";
            lblP3CurrentWord.Size = new Size(1168, 103);
            lblP3CurrentWord.TabIndex = 5;
            lblP3CurrentWord.Text = "123456789012345678901234567890";
            lblP3CurrentWord.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnP3Heartbeat
            // 
            btnP3Heartbeat.Enabled = false;
            btnP3Heartbeat.Location = new Point(18, 173);
            btnP3Heartbeat.Margin = new Padding(8, 10, 8, 10);
            btnP3Heartbeat.Name = "btnP3Heartbeat";
            btnP3Heartbeat.Size = new Size(102, 103);
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
            boxP4.Enabled = false;
            boxP4.Location = new Point(35, 1402);
            boxP4.Margin = new Padding(8, 10, 8, 10);
            boxP4.Name = "boxP4";
            boxP4.Padding = new Padding(8, 10, 8, 10);
            boxP4.Size = new Size(1192, 307);
            boxP4.TabIndex = 9;
            boxP4.TabStop = false;
            boxP4.Text = "Player 4";
            // 
            // lblP4Score
            // 
            lblP4Score.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP4Score.Location = new Point(378, 173);
            lblP4Score.Margin = new Padding(8, 0, 8, 0);
            lblP4Score.Name = "lblP4Score";
            lblP4Score.Size = new Size(798, 103);
            lblP4Score.TabIndex = 6;
            lblP4Score.Text = "12345678901234567890";
            lblP4Score.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnP4WorthPoints
            // 
            btnP4WorthPoints.Enabled = false;
            btnP4WorthPoints.Location = new Point(258, 173);
            btnP4WorthPoints.Margin = new Padding(8, 10, 8, 10);
            btnP4WorthPoints.Name = "btnP4WorthPoints";
            btnP4WorthPoints.Size = new Size(102, 103);
            btnP4WorthPoints.TabIndex = 7;
            btnP4WorthPoints.UseVisualStyleBackColor = true;
            // 
            // btnP4InDictionary
            // 
            btnP4InDictionary.Enabled = false;
            btnP4InDictionary.Location = new Point(138, 173);
            btnP4InDictionary.Margin = new Padding(8, 10, 8, 10);
            btnP4InDictionary.Name = "btnP4InDictionary";
            btnP4InDictionary.Size = new Size(102, 103);
            btnP4InDictionary.TabIndex = 6;
            btnP4InDictionary.UseVisualStyleBackColor = true;
            // 
            // lblP4CurrentWord
            // 
            lblP4CurrentWord.BackColor = SystemColors.Window;
            lblP4CurrentWord.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblP4CurrentWord.Location = new Point(18, 60);
            lblP4CurrentWord.Margin = new Padding(8, 0, 8, 0);
            lblP4CurrentWord.Name = "lblP4CurrentWord";
            lblP4CurrentWord.Size = new Size(1168, 103);
            lblP4CurrentWord.TabIndex = 5;
            lblP4CurrentWord.Text = "123456789012345678901234567890";
            lblP4CurrentWord.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnP4Heartbeat
            // 
            btnP4Heartbeat.Enabled = false;
            btnP4Heartbeat.Location = new Point(18, 173);
            btnP4Heartbeat.Margin = new Padding(8, 10, 8, 10);
            btnP4Heartbeat.Name = "btnP4Heartbeat";
            btnP4Heartbeat.Size = new Size(102, 103);
            btnP4Heartbeat.TabIndex = 5;
            btnP4Heartbeat.UseVisualStyleBackColor = true;
            // 
            // dgvScoreboard
            // 
            dgvScoreboard.AllowUserToAddRows = false;
            dgvScoreboard.AllowUserToDeleteRows = false;
            dgvScoreboard.AllowUserToResizeColumns = false;
            dgvScoreboard.AllowUserToResizeRows = false;
            dgvScoreboard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvScoreboard.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvScoreboard.ColumnHeadersVisible = false;
            dgvScoreboard.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvScoreboard.Location = new Point(1242, 422);
            dgvScoreboard.Margin = new Padding(8, 10, 8, 10);
            dgvScoreboard.MultiSelect = false;
            dgvScoreboard.Name = "dgvScoreboard";
            dgvScoreboard.ReadOnly = true;
            dgvScoreboard.RowHeadersVisible = false;
            dgvScoreboard.RowHeadersWidth = 51;
            dgvScoreboard.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvScoreboard.ScrollBars = ScrollBars.Vertical;
            dgvScoreboard.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvScoreboard.ShowCellErrors = false;
            dgvScoreboard.ShowCellToolTips = false;
            dgvScoreboard.ShowEditingIcon = false;
            dgvScoreboard.ShowRowErrors = false;
            dgvScoreboard.Size = new Size(1008, 1286);
            dgvScoreboard.TabIndex = 99;
            dgvScoreboard.TabStop = false;
            // 
            // frmGame
            // 
            AutoScaleDimensions = new SizeF(20F, 48F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2285, 1898);
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
            Margin = new Padding(8, 10, 8, 10);
            Name = "frmGame";
            StartPosition = FormStartPosition.CenterScreen;
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
        private ToolStripMenuItem mnuLetterPoolFormat;
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
        private ToolStripMenuItem mnuLetterMode;
        private ToolStripMenuItem optDictionarySelect;
        private ToolStripMenuItem optBagSelect;
        private ToolStripMenuItem mnuTime;
        private ToolStripTextBox optTime;
        private ToolStripMenuItem mnuPoolLetterCount;
        private ToolStripTextBox optPoolLetterCount;
        private ToolStripMenuItem mnuDictionaryTools;
        private ToolStripMenuItem mnuShowWords;
        private ToolStripTextBox optShowWords;
    }
}
