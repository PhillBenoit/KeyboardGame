/*
http://github.com/phillbenoit

Word game that takes input from 4 separate keyboards.

This program works by:
- mapping keypresses to player objects
- including uniform ui elements to manipulate in player objects
- making actions universal amongst all players
- using a binary search of word hashes to verify spelling 
*/

using KeyboardGameV2.src;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Win32.UI.Input;

namespace KeyboardGameV2
{
    public partial class Form1 : Form
    {
        //Variables
        //---------------------------------

        //strings for UI controlls
        private const string MNUMSG_ASSIGN = "Assign Player {0}";
        private const string MNUMSG_RELEASE = "Release Player {0}";
        private const string MSG_ASSIGN = "please press a letter";
        private const string MNUMSG_START = "Start Game";
        private const string MNUMSG_STOP = "Stop Game";
        private const string POPMSG_HANDLE_ERROR = "Failed to set up handler for raw input.\n";
        private const string POPMSG_FILE_FILTER = "one word per line txt files (*.txt)|*.txt";
        private const string POPMSG_GAME_OVER = "Game Over!";

        //game tiles
        private LetterBag _bag = new();

        //list of words found by all players
        private readonly Scoreboard _scoreboard;

        private WordScoreSystem wss;

        //number of tiles to draw in a game
        private const byte MAX_TILES = 30;
        private const byte DEFAULT_TILES = 20;
        private byte TILES_TO_DRAW;

        //timer variables
        private const ushort MIN_SECONDS = 30;
        private const ushort MAX_SECONDS = 300;
        private const ushort DEFAULT_SECONDS = 120;
        private ushort _seconds;

        private byte showWordsLength = 5;

        //objext that holds all of the plaers' information
        private readonly KBGPlayer[] _players;

        //maps a keyboard handle to a player object
        private readonly Dictionary<IntPtr, KBGPlayer> _keyboardMap = [];

        //dictionary of correctly spelled words
        private EnglishDictionary _dictionary = new();

        //Overrides for KB input
        //---------------------------------

        //capture keyboard handle creation to register for windows messages
        protected override void OnHandleCreated(EventArgs e)
        {
            if (!KeyEvent.RegisterKBHandle(this.Handle))
                //throw error message box if device registration fails
                MessageBox.Show(POPMSG_HANDLE_ERROR +
                    Marshal.GetLastPInvokeErrorMessage());

            base.OnHandleCreated(e);
        }

        //process windows messages
        protected override void WndProc(ref Message m)
        {
            KeyEvent.KeyFrom? kf = KeyEvent.GetKeyFrom(ref m);

            //filter message
            if (kf is not null)
            {
                //try to pull a player from the keyboard mappings
                if (_keyboardMap.TryGetValue(kf.from, out KBGPlayer? p))
                {
                    //key up event
                    if (kf.isUp)
                    {
                        p.isPressed[kf.key] = false;
                        if (p.UI.GetWord().Length == 1) p.UI.ClearLights();
                        else p.UI.HeartbeatOff();
                    }

                    //key down event filtered for debounce
                    else if (!p.isPressed[kf.key])
                    {
                        //debounce makes sure key goes up before it can be pressed again
                        p.isPressed[kf.key] = true;
                        p.UI.HeartbeatOn();
                        keyEvents[kf.key](p, (CharEncoding.VKEYS)kf.key);
                    }
                }
                //look for player flagged for assignment
                else foreach (KBGPlayer pl in _players)
                        if (pl.assignFlag) ConfirmAssign(kf.from, pl);
            }
            //perform normal windows message processing for noninput messages
            else base.WndProc(ref m);
        }

        //Keypress event methods and variables
        //---------------------------------

        //delegate array used to process key presses in O = 1 time
        delegate void FormKeyEvent(KBGPlayer p, CharEncoding.VKEYS key);
        private readonly FormKeyEvent[] keyEvents = new FormKeyEvent[CharEncoding.VK_COUNT];

        private static void KEDefault(KBGPlayer p, CharEncoding.VKEYS key) { }
        private static void KELetter(KBGPlayer p, CharEncoding.VKEYS key)
        { p.UI.SetWord(p.UI.GetWord() + (char)(key + 0x20)); }
        private static void KEColon(KBGPlayer p, CharEncoding.VKEYS key)
        { p.UI.SetWord(p.UI.GetWord() + (char)CharEncoding.ASCII.LETTER_ñ); }
        private static void KEBackspace(KBGPlayer p, CharEncoding.VKEYS key)
        { string s = p.UI.GetWord(); if (!string.IsNullOrEmpty(s)) p.UI.SetWord(s[..^1]); }
        private static void KEDelete(KBGPlayer p, CharEncoding.VKEYS key)
        { p.UI.SetWord(""); }
        private static void KEUp(KBGPlayer p, CharEncoding.VKEYS key)
        { p.UI.ToggleWordVisibility(); }
        private void KESubmit(KBGPlayer p, CharEncoding.VKEYS key)
        {
            if (Timer.Enabled)
            {
                string s = p.UI.GetWord();
                //make sure word is in the dictionary
                if (_dictionary.InDictionary(s))
                {
                    p.UI.InDictionaryYes();
                    AddWord(s, wss.ScoreWord(s), p);
                }
                else p.UI.InDictionaryNo();
                p.UI.SetWord("");
            }
        }

        //Form functions
        //---------------------------------

        //form constructor
        public Form1()
        {
            InitializeComponent();
            _players = new KBGPlayer[4];
            _players[0] = new KBGPlayer(1, lblP1CurrentWord, lblP1Score, btnP1Heartbeat,
                btnP1InDictionary, btnP1WorthPoints, optP1, boxP1);
            _players[1] = new KBGPlayer(2, lblP2CurrentWord, lblP2Score, btnP2Heartbeat,
                btnP2InDictionary, btnP2WorthPoints, optP2, boxP2);
            _players[2] = new KBGPlayer(3, lblP3CurrentWord, lblP3Score, btnP3Heartbeat,
                btnP3InDictionary, btnP3WorthPoints, optP3, boxP3);
            _players[3] = new KBGPlayer(4, lblP4CurrentWord, lblP4Score, btnP4Heartbeat,
                btnP4InDictionary, btnP4WorthPoints, optP4, boxP4);
            foreach (KBGPlayer p in _players)
            {
                p.Reset();
                p.UI.SetAssignText(String.Format(MNUMSG_ASSIGN, p.PLAYER_INDEX));
            }
            wss = new WordScoreSystem(new byte[1]);
            lblLetterPool.Text = "";
            _seconds = DEFAULT_SECONDS;
            TILES_TO_DRAW = DEFAULT_TILES;

            dgvScoreboard.ColumnCount = 7;
            dgvScoreboard.Columns[0].ValueType = typeof(string);
            dgvScoreboard.Columns[1].ValueType = typeof(string);
            dgvScoreboard.Columns[2].ValueType = typeof(ushort);
            dgvScoreboard.Columns[3].ValueType = typeof(char);
            dgvScoreboard.Columns[4].ValueType = typeof(char);
            dgvScoreboard.Columns[5].ValueType = typeof(char);
            dgvScoreboard.Columns[6].ValueType = typeof(char);
            dgvScoreboard.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvScoreboard.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvScoreboard.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvScoreboard.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvScoreboard.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvScoreboard.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvScoreboard.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvScoreboard.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvScoreboard.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvScoreboard.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dgvScoreboard.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dgvScoreboard.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dgvScoreboard.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dgvScoreboard.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            _scoreboard = new Scoreboard(dgvScoreboard);

            //make keypresses do nothing by default
            for (byte x = 0; x < keyEvents.Length; x++)
                keyEvents[x] = KEDefault;

            //assign individual keypress handlers
            for (CharEncoding.VKEYS letter = CharEncoding.VKEYS.VK_A;
                letter <= CharEncoding.VKEYS.VK_Z;
                letter++)
                keyEvents[(int)letter] = KELetter;
            keyEvents[(int)CharEncoding.VKEYS.VK_BACKSPACE] = KEBackspace;
            keyEvents[(int)CharEncoding.VKEYS.VK_DELETE] = KEDelete;
            keyEvents[(int)CharEncoding.VKEYS.VK_UP] = KEUp;
            keyEvents[(int)CharEncoding.VKEYS.VK_SPACE] = KESubmit;
            keyEvents[(int)CharEncoding.VKEYS.VK_RETURN] = KESubmit;
            keyEvents[(int)CharEncoding.VKEYS.VK_SEMICOLON] = KEColon;
            keyEvents[(int)CharEncoding.VKEYS.VK_TILDE] = KEColon;
        }

        //called once per second
        private void Timer_Tick(object sender, EventArgs e)
        {
            //tick down timer
            _seconds--;

            //update timer label
            lblTimer.Text = String.Format("{0:D2}:{1:D2}", _seconds / 60, _seconds % 60);

            //update progress
            barTimer.Value = _seconds;

            //check for game over
            if (_seconds == 0)
            {
                Click_mnuStart(sender, e);
                MessageBox.Show(POPMSG_GAME_OVER);
            }
        }

        //process a correctly spelled word
        internal void AddWord(String word, UInt16 points, KBGPlayer player)
        {

            //do nothing if the player already has credit for the word
            if (!_scoreboard.Add(word, points, player.PLAYER_INDEX)) return;

            //update the ui
            player.AddPoints(points);
            if (points == 0) player.UI.WorthPointsNo();
            else player.UI.WorthPointsYes();
        }

        //called from the windows message reader to register a player's keyboard
        private void ConfirmAssign(IntPtr h, KBGPlayer p)
        {
            //add user and turn off assignment flag
            _keyboardMap.Add(h, p);
            p.assignFlag = false;

            //update and reactivate ui elements
            p.UI.SetAssignText(String.Format(MNUMSG_RELEASE, p.PLAYER_INDEX));
            mnuStrip.Enabled = true;
            mnuStart.Enabled = mnuLoad.Enabled == false;
            p.UI.SetWord("");
        }

        //event handler for the player assignment menu options
        private void PlayerAssignment(KBGPlayer p)
        {
            string nextText;

            //assign
            if (p.UI.IsAssigned())
            {
                nextText = String.Format(MNUMSG_RELEASE, p.PLAYER_INDEX);
                mnuStrip.Enabled = false;
                p.assignFlag = true;
                p.UI.SetWord(MSG_ASSIGN);
            }

            //release
            else
            {
                nextText = String.Format(MNUMSG_ASSIGN, p.PLAYER_INDEX);
                //search for and remove handle
                IntPtr key = IntPtr.Zero;
                foreach (var kh in _keyboardMap) if (kh.Value == p) key = kh.Key;
                _keyboardMap.Remove(key);

                //reset ui elements
                p.Reset();
                mnuStart.Enabled =
                    _keyboardMap.Count > 0 &&
                    mnuLoad.Enabled == false;
            }

            p.UI.SetAssignText(nextText);
        }

        //Menu click events
        //---------------------------------

        //individual calls from each player menu option
        private void Click_optP1(object sender, EventArgs e) { PlayerAssignment(_players[0]); }
        private void Click_optP2(object sender, EventArgs e) { PlayerAssignment(_players[1]); }
        private void Click_optP3(object sender, EventArgs e) { PlayerAssignment(_players[2]); }
        private void Click_optP4(object sender, EventArgs e) { PlayerAssignment(_players[3]); }

        //starts / stops the game
        private void Click_mnuStart(object sender, EventArgs e)
        {
            //always do these
            bool start = !Timer.Enabled;
            string nextText;
            mnuOptions.Enabled = Timer.Enabled;
            mnuPlayers.Enabled = Timer.Enabled;
            dgvScoreboard.Columns[0].Visible = Timer.Enabled;
            dgvScoreboard.Columns[1].Visible = start;
            mnuDictionaryTools.Enabled = Timer.Enabled;

            //game start actions
            if (start)
            {
                nextText = MNUMSG_STOP;
                _seconds = ushort.Parse(optTime.Text);
                barTimer.Maximum = _seconds;
                barTimer.Value = _seconds;
                foreach (KBGPlayer p in _players) p.Reset();
                _bag.Reset();
                _scoreboard.Clear();

                if (optDictionarySelect.Checked)
                {
                    wss = new WordScoreSystem(_dictionary.OCCURANCE_RATE_POINT_MAP);
                    _dictionary.Draw(TILES_TO_DRAW);
                    wss.SetDraw(_dictionary.draw, _dictionary.drawLetterCount);
                }
                else
                {
                    wss = new WordScoreSystem(_bag.POINTS_MAP);
                    string? s = _bag.Draw(TILES_TO_DRAW);
                    if (s is not null) wss.SetDraw(s, _bag._drawCount);
                }
                lblLetterPool.Text = wss.FormatDraw(
                    optSorted.Checked, optPoints.Checked, optSpaces.Checked);
                char[] sorted = wss.GetDraw().ToCharArray();
                Array.Sort(sorted);
                _dictionary.StartSearch(new string(sorted));
                foreach (string word in _dictionary.found_words)
                    _scoreboard.Add(word, wss.ScoreWord(word));
                _scoreboard.Sort();
            }

            //stop game actions
            else
            {
                if (mnuShowWords.Checked)
                    _scoreboard.ShowWords(byte.Parse(optShowWords.Text));
                nextText = MNUMSG_START;
            }

            //finish with these
            mnuStart.Text = nextText;
            Timer.Enabled = start;
        }

        //load dictionary and set up bag of game tiles
        private void Click_mnuLoad(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = POPMSG_FILE_FILTER;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _dictionary = new EnglishDictionary(
                    openFileDialog.OpenFile(),
                    openFileDialog.FileName
                    );
                _bag = new LetterBag(_dictionary.MAX_LETTER_COUNT, (char)CharEncoding.ASCII.LETTER_a);
                mnuLoad.Enabled = false;
                mnuPoolLetterCount.Enabled = true;
                mnuShowWords.Enabled = true;
                optShowWords.Text = _dictionary.word_stdev_min.ToString();
                mnuStart.Enabled = _keyboardMap.Count > 0;
            }
        }

        private void Click_LetterMode(object sender, EventArgs e)
        {
            optBagSelect.Checked = optDictionarySelect.Checked;
            optDictionarySelect.Checked = !optDictionarySelect.Checked;
        }

        private void TextChanged_optTime(object sender, EventArgs e)
        {
            uint wrapper = _seconds;
            SetNumericText(optTime, ref wrapper,
                MIN_SECONDS, MAX_SECONDS, DEFAULT_SECONDS);
            _seconds = (ushort)wrapper;
        }

        private void TextChanged_optPoolLetterCount(object sender, EventArgs e)
        {
            uint wrapper = TILES_TO_DRAW;
            SetNumericText(optPoolLetterCount, ref wrapper,
                _dictionary.word_2stdev_min, MAX_TILES, DEFAULT_TILES);
            TILES_TO_DRAW = (byte)wrapper;
        }

        private void TextChanged_optShowWords(object sender, EventArgs e)
        {
            uint wrapper = showWordsLength;
            SetNumericText(optShowWords,
                ref wrapper,
                _dictionary.MIN_WORD_LENGTH,
                _dictionary.MAX_WORD_LENGTH,
                _dictionary.word_stdev_min);
            showWordsLength = (byte)wrapper;
        }

        private static void SetNumericText(ToolStripTextBox box,
            ref uint value, uint min, uint max, uint deflt)
        {
            if (!UInt32.TryParse(box.Text, out value))
                value = deflt;
            else if (value < min)
                value = min;
            else if (value > max)
                value = max;
            box.Text = value.ToString();
        }
    }
}
