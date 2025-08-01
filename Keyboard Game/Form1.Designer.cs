namespace KeyboardGame
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnP1Assign = new System.Windows.Forms.Button();
            this.lblP1CurrentWord = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miLoadDictionary = new System.Windows.Forms.ToolStripMenuItem();
            this.miStartGame = new System.Windows.Forms.ToolStripMenuItem();
            this.barTimer = new System.Windows.Forms.ProgressBar();
            this.lblTimer = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnP1Release = new System.Windows.Forms.Button();
            this.lblLetterPool = new System.Windows.Forms.Label();
            this.lblP1Score = new System.Windows.Forms.Label();
            this.gridP1 = new System.Windows.Forms.DataGridView();
            this.btnP1InDict = new System.Windows.Forms.Button();
            this.btnP1WorthPoints = new System.Windows.Forms.Button();
            this.btnP1Heartbeat = new System.Windows.Forms.Button();
            this.btnP2WorthPoints = new System.Windows.Forms.Button();
            this.btnP2Heartbeat = new System.Windows.Forms.Button();
            this.btnP2InDict = new System.Windows.Forms.Button();
            this.gridP2 = new System.Windows.Forms.DataGridView();
            this.lblP2Score = new System.Windows.Forms.Label();
            this.btnP2Release = new System.Windows.Forms.Button();
            this.lblP2CurrentWord = new System.Windows.Forms.Label();
            this.btnP2Assign = new System.Windows.Forms.Button();
            this.btnP4WorthPoints = new System.Windows.Forms.Button();
            this.btnP4Heartbeat = new System.Windows.Forms.Button();
            this.btnP4InDict = new System.Windows.Forms.Button();
            this.gridP4 = new System.Windows.Forms.DataGridView();
            this.lblP4Score = new System.Windows.Forms.Label();
            this.btnP4Release = new System.Windows.Forms.Button();
            this.lblP4CurrentWord = new System.Windows.Forms.Label();
            this.btnP4Assign = new System.Windows.Forms.Button();
            this.btnP3WorthPoints = new System.Windows.Forms.Button();
            this.btnP3Heartbeat = new System.Windows.Forms.Button();
            this.btnP3InDict = new System.Windows.Forms.Button();
            this.gridP3 = new System.Windows.Forms.DataGridView();
            this.lblP3Score = new System.Windows.Forms.Label();
            this.btnP3Release = new System.Windows.Forms.Button();
            this.lblP3CurrentWord = new System.Windows.Forms.Label();
            this.btnP3Assign = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridP2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridP4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridP3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnP1Assign
            // 
            this.btnP1Assign.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP1Assign.Location = new System.Drawing.Point(56, 30);
            this.btnP1Assign.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP1Assign.Name = "btnP1Assign";
            this.btnP1Assign.Size = new System.Drawing.Size(156, 60);
            this.btnP1Assign.TabIndex = 0;
            this.btnP1Assign.Text = "Assign Player 1";
            this.btnP1Assign.UseVisualStyleBackColor = true;
            this.btnP1Assign.Click += new System.EventHandler(this.btnP1Assign_Click);
            // 
            // lblP1CurrentWord
            // 
            this.lblP1CurrentWord.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblP1CurrentWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1CurrentWord.Location = new System.Drawing.Point(12, 92);
            this.lblP1CurrentWord.Name = "lblP1CurrentWord";
            this.lblP1CurrentWord.Size = new System.Drawing.Size(563, 32);
            this.lblP1CurrentWord.TabIndex = 2;
            this.lblP1CurrentWord.Text = "123456789012345678901234567890";
            this.lblP1CurrentWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLoadDictionary,
            this.miStartGame});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1233, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miLoadDictionary
            // 
            this.miLoadDictionary.Name = "miLoadDictionary";
            this.miLoadDictionary.Size = new System.Drawing.Size(128, 24);
            this.miLoadDictionary.Text = "Load Dictionary";
            this.miLoadDictionary.Click += new System.EventHandler(this.loadDictionaryToolStripMenuItem_Click);
            // 
            // miStartGame
            // 
            this.miStartGame.Enabled = false;
            this.miStartGame.Name = "miStartGame";
            this.miStartGame.Size = new System.Drawing.Size(97, 24);
            this.miStartGame.Text = "Start Game";
            this.miStartGame.Click += new System.EventHandler(this.startGameToolStripMenuItem_Click);
            // 
            // barTimer
            // 
            this.barTimer.Location = new System.Drawing.Point(118, 397);
            this.barTimer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barTimer.Name = "barTimer";
            this.barTimer.Size = new System.Drawing.Size(1115, 32);
            this.barTimer.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.barTimer.TabIndex = 9;
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F);
            this.lblTimer.Location = new System.Drawing.Point(12, 397);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(86, 32);
            this.lblTimer.TabIndex = 10;
            this.lblTimer.Text = "00:00";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnP1Release
            // 
            this.btnP1Release.Enabled = false;
            this.btnP1Release.Image = global::KeyboardGame.Properties.Resources.Offline_16xLG;
            this.btnP1Release.Location = new System.Drawing.Point(218, 30);
            this.btnP1Release.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP1Release.Name = "btnP1Release";
            this.btnP1Release.Size = new System.Drawing.Size(25, 25);
            this.btnP1Release.TabIndex = 14;
            this.btnP1Release.UseVisualStyleBackColor = true;
            this.btnP1Release.Click += new System.EventHandler(this.btnP1Release_Click);
            // 
            // lblLetterPool
            // 
            this.lblLetterPool.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblLetterPool.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLetterPool.Location = new System.Drawing.Point(0, 347);
            this.lblLetterPool.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLetterPool.Name = "lblLetterPool";
            this.lblLetterPool.Size = new System.Drawing.Size(1233, 48);
            this.lblLetterPool.TabIndex = 15;
            this.lblLetterPool.Text = "1234567890123456789012345678901234567890";
            this.lblLetterPool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblP1Score
            // 
            this.lblP1Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1Score.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblP1Score.Location = new System.Drawing.Point(280, 30);
            this.lblP1Score.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblP1Score.Name = "lblP1Score";
            this.lblP1Score.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblP1Score.Size = new System.Drawing.Size(261, 60);
            this.lblP1Score.TabIndex = 17;
            this.lblP1Score.Text = "99999999999";
            this.lblP1Score.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridP1
            // 
            this.gridP1.AllowUserToAddRows = false;
            this.gridP1.AllowUserToDeleteRows = false;
            this.gridP1.AllowUserToResizeColumns = false;
            this.gridP1.AllowUserToResizeRows = false;
            this.gridP1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridP1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridP1.CausesValidation = false;
            this.gridP1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridP1.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridP1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridP1.Location = new System.Drawing.Point(12, 128);
            this.gridP1.Margin = new System.Windows.Forms.Padding(4);
            this.gridP1.Name = "gridP1";
            this.gridP1.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridP1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridP1.RowHeadersWidth = 51;
            this.gridP1.Size = new System.Drawing.Size(563, 198);
            this.gridP1.TabIndex = 18;
            this.gridP1.TabStop = false;
            // 
            // btnP1InDict
            // 
            this.btnP1InDict.Enabled = false;
            this.btnP1InDict.Location = new System.Drawing.Point(218, 66);
            this.btnP1InDict.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP1InDict.Name = "btnP1InDict";
            this.btnP1InDict.Size = new System.Drawing.Size(25, 25);
            this.btnP1InDict.TabIndex = 19;
            this.btnP1InDict.UseVisualStyleBackColor = true;
            // 
            // btnP1WorthPoints
            // 
            this.btnP1WorthPoints.Enabled = false;
            this.btnP1WorthPoints.Location = new System.Drawing.Point(248, 66);
            this.btnP1WorthPoints.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP1WorthPoints.Name = "btnP1WorthPoints";
            this.btnP1WorthPoints.Size = new System.Drawing.Size(25, 25);
            this.btnP1WorthPoints.TabIndex = 21;
            this.btnP1WorthPoints.UseVisualStyleBackColor = true;
            // 
            // btnP1Heartbeat
            // 
            this.btnP1Heartbeat.Enabled = false;
            this.btnP1Heartbeat.Location = new System.Drawing.Point(248, 30);
            this.btnP1Heartbeat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP1Heartbeat.Name = "btnP1Heartbeat";
            this.btnP1Heartbeat.Size = new System.Drawing.Size(25, 25);
            this.btnP1Heartbeat.TabIndex = 20;
            this.btnP1Heartbeat.UseVisualStyleBackColor = true;
            // 
            // btnP2WorthPoints
            // 
            this.btnP2WorthPoints.Enabled = false;
            this.btnP2WorthPoints.Location = new System.Drawing.Point(887, 66);
            this.btnP2WorthPoints.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP2WorthPoints.Name = "btnP2WorthPoints";
            this.btnP2WorthPoints.Size = new System.Drawing.Size(25, 25);
            this.btnP2WorthPoints.TabIndex = 29;
            this.btnP2WorthPoints.UseVisualStyleBackColor = true;
            // 
            // btnP2Heartbeat
            // 
            this.btnP2Heartbeat.Enabled = false;
            this.btnP2Heartbeat.Location = new System.Drawing.Point(887, 30);
            this.btnP2Heartbeat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP2Heartbeat.Name = "btnP2Heartbeat";
            this.btnP2Heartbeat.Size = new System.Drawing.Size(25, 25);
            this.btnP2Heartbeat.TabIndex = 28;
            this.btnP2Heartbeat.UseVisualStyleBackColor = true;
            // 
            // btnP2InDict
            // 
            this.btnP2InDict.Enabled = false;
            this.btnP2InDict.Location = new System.Drawing.Point(857, 66);
            this.btnP2InDict.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP2InDict.Name = "btnP2InDict";
            this.btnP2InDict.Size = new System.Drawing.Size(25, 25);
            this.btnP2InDict.TabIndex = 27;
            this.btnP2InDict.UseVisualStyleBackColor = true;
            // 
            // gridP2
            // 
            this.gridP2.AllowUserToAddRows = false;
            this.gridP2.AllowUserToDeleteRows = false;
            this.gridP2.AllowUserToResizeColumns = false;
            this.gridP2.AllowUserToResizeRows = false;
            this.gridP2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridP2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridP2.CausesValidation = false;
            this.gridP2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridP2.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridP2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridP2.Location = new System.Drawing.Point(657, 128);
            this.gridP2.Margin = new System.Windows.Forms.Padding(4);
            this.gridP2.Name = "gridP2";
            this.gridP2.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridP2.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridP2.RowHeadersWidth = 51;
            this.gridP2.Size = new System.Drawing.Size(563, 198);
            this.gridP2.TabIndex = 26;
            this.gridP2.TabStop = false;
            // 
            // lblP2Score
            // 
            this.lblP2Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2Score.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblP2Score.Location = new System.Drawing.Point(919, 30);
            this.lblP2Score.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblP2Score.Name = "lblP2Score";
            this.lblP2Score.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblP2Score.Size = new System.Drawing.Size(261, 60);
            this.lblP2Score.TabIndex = 25;
            this.lblP2Score.Text = "99999999999";
            this.lblP2Score.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnP2Release
            // 
            this.btnP2Release.Enabled = false;
            this.btnP2Release.Image = global::KeyboardGame.Properties.Resources.Offline_16xLG;
            this.btnP2Release.Location = new System.Drawing.Point(857, 30);
            this.btnP2Release.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP2Release.Name = "btnP2Release";
            this.btnP2Release.Size = new System.Drawing.Size(25, 25);
            this.btnP2Release.TabIndex = 24;
            this.btnP2Release.UseVisualStyleBackColor = true;
            this.btnP2Release.Click += new System.EventHandler(this.btnP2Release_Click);
            // 
            // lblP2CurrentWord
            // 
            this.lblP2CurrentWord.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblP2CurrentWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2CurrentWord.Location = new System.Drawing.Point(657, 92);
            this.lblP2CurrentWord.Name = "lblP2CurrentWord";
            this.lblP2CurrentWord.Size = new System.Drawing.Size(563, 32);
            this.lblP2CurrentWord.TabIndex = 23;
            this.lblP2CurrentWord.Text = "123456789012345678901234567890";
            this.lblP2CurrentWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnP2Assign
            // 
            this.btnP2Assign.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP2Assign.Location = new System.Drawing.Point(695, 30);
            this.btnP2Assign.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP2Assign.Name = "btnP2Assign";
            this.btnP2Assign.Size = new System.Drawing.Size(156, 60);
            this.btnP2Assign.TabIndex = 22;
            this.btnP2Assign.Text = "Assign Player 2";
            this.btnP2Assign.UseVisualStyleBackColor = true;
            this.btnP2Assign.Click += new System.EventHandler(this.btnP2Assign_Click);
            // 
            // btnP4WorthPoints
            // 
            this.btnP4WorthPoints.Enabled = false;
            this.btnP4WorthPoints.Location = new System.Drawing.Point(887, 480);
            this.btnP4WorthPoints.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP4WorthPoints.Name = "btnP4WorthPoints";
            this.btnP4WorthPoints.Size = new System.Drawing.Size(25, 25);
            this.btnP4WorthPoints.TabIndex = 45;
            this.btnP4WorthPoints.UseVisualStyleBackColor = true;
            // 
            // btnP4Heartbeat
            // 
            this.btnP4Heartbeat.Enabled = false;
            this.btnP4Heartbeat.Location = new System.Drawing.Point(887, 444);
            this.btnP4Heartbeat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP4Heartbeat.Name = "btnP4Heartbeat";
            this.btnP4Heartbeat.Size = new System.Drawing.Size(25, 25);
            this.btnP4Heartbeat.TabIndex = 44;
            this.btnP4Heartbeat.UseVisualStyleBackColor = true;
            // 
            // btnP4InDict
            // 
            this.btnP4InDict.Enabled = false;
            this.btnP4InDict.Location = new System.Drawing.Point(857, 480);
            this.btnP4InDict.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP4InDict.Name = "btnP4InDict";
            this.btnP4InDict.Size = new System.Drawing.Size(25, 25);
            this.btnP4InDict.TabIndex = 43;
            this.btnP4InDict.UseVisualStyleBackColor = true;
            // 
            // gridP4
            // 
            this.gridP4.AllowUserToAddRows = false;
            this.gridP4.AllowUserToDeleteRows = false;
            this.gridP4.AllowUserToResizeColumns = false;
            this.gridP4.AllowUserToResizeRows = false;
            this.gridP4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridP4.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridP4.CausesValidation = false;
            this.gridP4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridP4.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridP4.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridP4.Location = new System.Drawing.Point(657, 542);
            this.gridP4.Margin = new System.Windows.Forms.Padding(4);
            this.gridP4.Name = "gridP4";
            this.gridP4.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridP4.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridP4.RowHeadersWidth = 51;
            this.gridP4.Size = new System.Drawing.Size(563, 198);
            this.gridP4.TabIndex = 42;
            this.gridP4.TabStop = false;
            // 
            // lblP4Score
            // 
            this.lblP4Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP4Score.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblP4Score.Location = new System.Drawing.Point(919, 444);
            this.lblP4Score.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblP4Score.Name = "lblP4Score";
            this.lblP4Score.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblP4Score.Size = new System.Drawing.Size(261, 60);
            this.lblP4Score.TabIndex = 41;
            this.lblP4Score.Text = "99999999999";
            this.lblP4Score.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnP4Release
            // 
            this.btnP4Release.Enabled = false;
            this.btnP4Release.Image = global::KeyboardGame.Properties.Resources.Offline_16xLG;
            this.btnP4Release.Location = new System.Drawing.Point(857, 444);
            this.btnP4Release.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP4Release.Name = "btnP4Release";
            this.btnP4Release.Size = new System.Drawing.Size(25, 25);
            this.btnP4Release.TabIndex = 40;
            this.btnP4Release.UseVisualStyleBackColor = true;
            this.btnP4Release.Click += new System.EventHandler(this.btnP4Release_Click);
            // 
            // lblP4CurrentWord
            // 
            this.lblP4CurrentWord.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblP4CurrentWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP4CurrentWord.Location = new System.Drawing.Point(657, 506);
            this.lblP4CurrentWord.Name = "lblP4CurrentWord";
            this.lblP4CurrentWord.Size = new System.Drawing.Size(563, 32);
            this.lblP4CurrentWord.TabIndex = 39;
            this.lblP4CurrentWord.Text = "123456789012345678901234567890";
            this.lblP4CurrentWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnP4Assign
            // 
            this.btnP4Assign.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP4Assign.Location = new System.Drawing.Point(695, 444);
            this.btnP4Assign.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP4Assign.Name = "btnP4Assign";
            this.btnP4Assign.Size = new System.Drawing.Size(156, 60);
            this.btnP4Assign.TabIndex = 38;
            this.btnP4Assign.Text = "Assign Player 4";
            this.btnP4Assign.UseVisualStyleBackColor = true;
            this.btnP4Assign.Click += new System.EventHandler(this.btnP4Assign_Click);
            // 
            // btnP3WorthPoints
            // 
            this.btnP3WorthPoints.Enabled = false;
            this.btnP3WorthPoints.Location = new System.Drawing.Point(248, 480);
            this.btnP3WorthPoints.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP3WorthPoints.Name = "btnP3WorthPoints";
            this.btnP3WorthPoints.Size = new System.Drawing.Size(25, 25);
            this.btnP3WorthPoints.TabIndex = 37;
            this.btnP3WorthPoints.UseVisualStyleBackColor = true;
            // 
            // btnP3Heartbeat
            // 
            this.btnP3Heartbeat.Enabled = false;
            this.btnP3Heartbeat.Location = new System.Drawing.Point(248, 444);
            this.btnP3Heartbeat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP3Heartbeat.Name = "btnP3Heartbeat";
            this.btnP3Heartbeat.Size = new System.Drawing.Size(25, 25);
            this.btnP3Heartbeat.TabIndex = 36;
            this.btnP3Heartbeat.UseVisualStyleBackColor = true;
            // 
            // btnP3InDict
            // 
            this.btnP3InDict.Enabled = false;
            this.btnP3InDict.Location = new System.Drawing.Point(218, 480);
            this.btnP3InDict.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP3InDict.Name = "btnP3InDict";
            this.btnP3InDict.Size = new System.Drawing.Size(25, 25);
            this.btnP3InDict.TabIndex = 35;
            this.btnP3InDict.UseVisualStyleBackColor = true;
            // 
            // gridP3
            // 
            this.gridP3.AllowUserToAddRows = false;
            this.gridP3.AllowUserToDeleteRows = false;
            this.gridP3.AllowUserToResizeColumns = false;
            this.gridP3.AllowUserToResizeRows = false;
            this.gridP3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridP3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridP3.CausesValidation = false;
            this.gridP3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridP3.DefaultCellStyle = dataGridViewCellStyle7;
            this.gridP3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridP3.Location = new System.Drawing.Point(12, 542);
            this.gridP3.Margin = new System.Windows.Forms.Padding(4);
            this.gridP3.Name = "gridP3";
            this.gridP3.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.NullValue = null;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridP3.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gridP3.RowHeadersWidth = 51;
            this.gridP3.Size = new System.Drawing.Size(563, 198);
            this.gridP3.TabIndex = 34;
            this.gridP3.TabStop = false;
            // 
            // lblP3Score
            // 
            this.lblP3Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP3Score.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblP3Score.Location = new System.Drawing.Point(280, 444);
            this.lblP3Score.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblP3Score.Name = "lblP3Score";
            this.lblP3Score.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblP3Score.Size = new System.Drawing.Size(261, 60);
            this.lblP3Score.TabIndex = 33;
            this.lblP3Score.Text = "99999999999";
            this.lblP3Score.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnP3Release
            // 
            this.btnP3Release.Enabled = false;
            this.btnP3Release.Image = global::KeyboardGame.Properties.Resources.Offline_16xLG;
            this.btnP3Release.Location = new System.Drawing.Point(218, 444);
            this.btnP3Release.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP3Release.Name = "btnP3Release";
            this.btnP3Release.Size = new System.Drawing.Size(25, 25);
            this.btnP3Release.TabIndex = 32;
            this.btnP3Release.UseVisualStyleBackColor = true;
            this.btnP3Release.Click += new System.EventHandler(this.btnP3Release_Click);
            // 
            // lblP3CurrentWord
            // 
            this.lblP3CurrentWord.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblP3CurrentWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP3CurrentWord.Location = new System.Drawing.Point(12, 506);
            this.lblP3CurrentWord.Name = "lblP3CurrentWord";
            this.lblP3CurrentWord.Size = new System.Drawing.Size(563, 32);
            this.lblP3CurrentWord.TabIndex = 31;
            this.lblP3CurrentWord.Text = "123456789012345678901234567890";
            this.lblP3CurrentWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnP3Assign
            // 
            this.btnP3Assign.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP3Assign.Location = new System.Drawing.Point(56, 444);
            this.btnP3Assign.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnP3Assign.Name = "btnP3Assign";
            this.btnP3Assign.Size = new System.Drawing.Size(156, 60);
            this.btnP3Assign.TabIndex = 30;
            this.btnP3Assign.Text = "Assign Player 3";
            this.btnP3Assign.UseVisualStyleBackColor = true;
            this.btnP3Assign.Click += new System.EventHandler(this.btnP3Assign_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 753);
            this.Controls.Add(this.btnP4WorthPoints);
            this.Controls.Add(this.btnP4Heartbeat);
            this.Controls.Add(this.btnP4InDict);
            this.Controls.Add(this.gridP4);
            this.Controls.Add(this.lblP4Score);
            this.Controls.Add(this.btnP4Release);
            this.Controls.Add(this.lblP4CurrentWord);
            this.Controls.Add(this.btnP4Assign);
            this.Controls.Add(this.btnP3WorthPoints);
            this.Controls.Add(this.btnP3Heartbeat);
            this.Controls.Add(this.btnP3InDict);
            this.Controls.Add(this.gridP3);
            this.Controls.Add(this.lblP3Score);
            this.Controls.Add(this.btnP3Release);
            this.Controls.Add(this.lblP3CurrentWord);
            this.Controls.Add(this.btnP3Assign);
            this.Controls.Add(this.btnP2WorthPoints);
            this.Controls.Add(this.btnP2Heartbeat);
            this.Controls.Add(this.btnP2InDict);
            this.Controls.Add(this.gridP2);
            this.Controls.Add(this.lblP2Score);
            this.Controls.Add(this.btnP2Release);
            this.Controls.Add(this.lblP2CurrentWord);
            this.Controls.Add(this.btnP2Assign);
            this.Controls.Add(this.btnP1WorthPoints);
            this.Controls.Add(this.btnP1Heartbeat);
            this.Controls.Add(this.btnP1InDict);
            this.Controls.Add(this.gridP1);
            this.Controls.Add(this.lblP1Score);
            this.Controls.Add(this.lblLetterPool);
            this.Controls.Add(this.btnP1Release);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.barTimer);
            this.Controls.Add(this.lblP1CurrentWord);
            this.Controls.Add(this.btnP1Assign);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Not Scrabble or Boggle";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridP2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridP4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridP3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnP1Assign;
        private System.Windows.Forms.Label lblP1CurrentWord;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ProgressBar barTimer;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.ToolStripMenuItem miStartGame;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button btnP1Release;
        private System.Windows.Forms.Label lblLetterPool;
        private System.Windows.Forms.ToolStripMenuItem miLoadDictionary;
        private System.Windows.Forms.Label lblP1Score;
        private System.Windows.Forms.DataGridView gridP1;
        private System.Windows.Forms.Button btnP1InDict;
        private System.Windows.Forms.Button btnP1WorthPoints;
        private System.Windows.Forms.Button btnP1Heartbeat;
        private System.Windows.Forms.Button btnP2WorthPoints;
        private System.Windows.Forms.Button btnP2Heartbeat;
        private System.Windows.Forms.Button btnP2InDict;
        private System.Windows.Forms.DataGridView gridP2;
        private System.Windows.Forms.Label lblP2Score;
        private System.Windows.Forms.Button btnP2Release;
        private System.Windows.Forms.Label lblP2CurrentWord;
        private System.Windows.Forms.Button btnP2Assign;
        private System.Windows.Forms.Button btnP4WorthPoints;
        private System.Windows.Forms.Button btnP4Heartbeat;
        private System.Windows.Forms.Button btnP4InDict;
        private System.Windows.Forms.DataGridView gridP4;
        private System.Windows.Forms.Label lblP4Score;
        private System.Windows.Forms.Button btnP4Release;
        private System.Windows.Forms.Label lblP4CurrentWord;
        private System.Windows.Forms.Button btnP4Assign;
        private System.Windows.Forms.Button btnP3WorthPoints;
        private System.Windows.Forms.Button btnP3Heartbeat;
        private System.Windows.Forms.Button btnP3InDict;
        private System.Windows.Forms.DataGridView gridP3;
        private System.Windows.Forms.Label lblP3Score;
        private System.Windows.Forms.Button btnP3Release;
        private System.Windows.Forms.Label lblP3CurrentWord;
        private System.Windows.Forms.Button btnP3Assign;
    }
}

