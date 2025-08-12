/*
http://github.com/phillbenoit

Word game that takes input from 4 separate keyboards.

This program works by:
- mapping keypresses to player objects
- including uniform ui elements to manipulate in player objects
- making actions universal amongst all players
- using a binary search of word hashes to verify spelling 
*/

using System.Runtime.InteropServices;
using System.Text;
using KeyboardGameV2.src;
using Windows.Win32.UI.Input;


namespace KeyboardGameV2
{
    public partial class Form1 : Form
    {
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
        private readonly Scoreboard _scoreboard = [];

        //number of tiles to draw in a game
        private const byte TILES_TO_DRAW = 20;

        //timer variables
        private const ushort MAX_SECONDS = 120;
        private ushort _seconds = 0;

        //objext that holds all of the plaers' information
        private readonly KBGPlayer[] _players;

        //maps a keyboard handle to a player object
        private readonly Dictionary<IntPtr, KBGPlayer> _keyboardMap = [];

        //dictionary of correctly spelled words
        private EnglishDictionary _dictionary = new();
        
        //form constructor
        public Form1()
        {
            InitializeComponent();
            _players = new KBGPlayer[4];
            _players[0] = new KBGPlayer(1, lblP1CurrentWord, lblP1Score, btnP1Heartbeat,
                btnP1InDictionary, btnP1WorthPoints, optP1);
            _players[1] = new KBGPlayer(2, lblP2CurrentWord, lblP2Score, btnP2Heartbeat,
                btnP2InDictionary, btnP2WorthPoints, optP2);
            _players[2] = new KBGPlayer(3, lblP3CurrentWord, lblP3Score, btnP3Heartbeat,
                btnP3InDictionary, btnP3WorthPoints, optP3);
            _players[3] = new KBGPlayer(4, lblP4CurrentWord, lblP4Score, btnP4Heartbeat,
                btnP4InDictionary, btnP4WorthPoints, optP4);
            foreach (KBGPlayer p in _players)
            {
                p.Reset();
                p.UI.SetAssignText(String.Format(MNUMSG_ASSIGN, p.PLAYER_INDEX));
            }
            lblLetterPool.Text = "";
            dgvScoreboard.DataSource = _scoreboard;
        }

        //called once per second
        private void Timer_Tick(object sender, EventArgs e)
        {
            //tick down timer
            _seconds--;

            //update timer label
            lblTimer.Text = String.Format("{0:D2}:{1:D2}", _seconds / 60, _seconds % 60);

            //set progress color based on amount of time remaining
            Color c = Color.DarkGreen;
            if (_seconds < (MAX_SECONDS / 2)) c = Color.Gold;
            if (_seconds < (MAX_SECONDS / 4)) c = Color.Maroon;
            barTimer.ForeColor = c;

            //update progress
            barTimer.Value = (int)_seconds;

            //check for game over
            if (_seconds == 0)
            {
                Click_mnuStart(sender, e);
                MessageBox.Show(POPMSG_GAME_OVER);
            }
        }

        //capture keyboard handle creation to register for windows messages
        protected override void OnHandleCreated(EventArgs e)
        {
            if (!Winuser.RegisterKBHandle(this.Handle))
                //throw error message box if device registration fails
                MessageBox.Show(POPMSG_HANDLE_ERROR +
                    Marshal.GetLastPInvokeErrorMessage());
            
            base.OnHandleCreated(e);
        }

        //process windows messages
        protected override void WndProc(ref Message m)
        {
            Winuser.KeyFrom? kf = Winuser.GetKeyFrom(ref m);
            
            //filter for input messages
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
                        KeypressHandler(p, (CharEncoding.ANSI)kf.key);
                    }
                }
                //look for player flagged for assignment
                else foreach (KBGPlayer pl in _players)
                        if (pl.assignFlag) ConfirmAssign(kf.from, pl);
            }
            //perform normal windows message processing for noninput messages
            else base.WndProc(ref m);
        }

        //handles keypresses for known players
        private void KeypressHandler(KBGPlayer p, CharEncoding.ANSI key)
        {
            //game actions
            if (Timer.Enabled)
            {
                //get the word the player is currently typing
                string s = p.UI.GetWord();

                //letters (shift to lower case)
                if (key >= CharEncoding.ANSI.VK_A && key <= CharEncoding.ANSI.VK_Z)
                    p.UI.SetWord(s + Convert.ToChar(key + 0x20));

                //backspace
                else if (key == CharEncoding.ANSI.VK_BACKSPACE)
                {
                    if (!string.IsNullOrEmpty(s)) p.UI.SetWord(s[..^1]);
                }

                //delete
                else if (key == CharEncoding.ANSI.VK_DELETE) p.UI.SetWord("");

                //spacebar or enter to check word and add to score
                else if (key == CharEncoding.ANSI.VK_SPACE ||
                        key == CharEncoding.ANSI.VK_RETURN)
                {
                    //make sure word is in the dictionary
                    if (_dictionary.InDictionary(s))
                    {
                        p.UI.InDictionaryYes();
                        AddWord(s, _bag.ScoreWord(s), p);
                    }
                    else p.UI.InDictionaryNo();
                    p.UI.SetWord("");
                }

            }

            //universal actions

            //up arrow toggles typed in word visibility
            if (key == CharEncoding.ANSI.VK_UP) p.UI.ToggleWordVisibility();
        }

        //process a correctly spelled word
        internal void AddWord(String word, UInt16 points, KBGPlayer player)
        {
            //format found word for the scoreboard
            Scoreboard.ScoreEntry entry = new(word, points);
            entry.Players[player.PLAYER_INDEX - 1] = true;
            
            //try to find it in the list
            int index = _scoreboard.IndexOf(entry);
            
            //if found
            if (index > -1)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                entry = (Scoreboard.ScoreEntry)_scoreboard[index];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (entry is null) throw new NullReferenceException();

                //player is already credited with the word
                if (entry.Players[player.PLAYER_INDEX - 1] == true) return;

                //credit player in existing record
                else entry.Players[player.PLAYER_INDEX - 1] = true;
            }

            //insert entry if it is not already in the list
            else
            {
                _scoreboard.Add(entry);
                _scoreboard.Sort();
            }

            //update the ui
            player.AddPoints(points);
            if (points == 0) player.UI.WorthPointsNo();
            else player.UI.WorthPointsYes();
        }

        //called from the windows message reader to register a player's keyboard
        private void ConfirmAssign(IntPtr h, KBGPlayer p)
        {
            //add user ant turn off assignment flag
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

        //individual calls from each player menu option
        private void Click_optP1(object sender, EventArgs e) { PlayerAssignment(_players[0]); }
        private void Click_optP2(object sender, EventArgs e) { PlayerAssignment(_players[1]); }
        private void Click_optP3(object sender, EventArgs e) { PlayerAssignment(_players[2]); }
        private void Click_optP4(object sender, EventArgs e) { PlayerAssignment(_players[3]); }

        //starts / stops the game
        private void Click_mnuStart(object sender, EventArgs e)
        {
            
            string nextText;

            //game stop actions
            if (Timer.Enabled) nextText = MNUMSG_START;

            //game start actions
            else
            {
                nextText = MNUMSG_STOP;
                _seconds = MAX_SECONDS;
                barTimer.Maximum = MAX_SECONDS;
                foreach (KBGPlayer p in _players) p.Reset();
                _bag.Reset();
                _scoreboard.Clear();
                lblLetterPool.Text = _bag.Draw(TILES_TO_DRAW,
                    optSorted.Checked, optPoints.Checked, optSpaces.Checked);
            }

            //always do these
            mnuStart.Text = nextText;
            Timer.Enabled = !Timer.Enabled;
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
                mnuStart.Enabled = _keyboardMap.Count > 0;
            }
        }
    }
}
