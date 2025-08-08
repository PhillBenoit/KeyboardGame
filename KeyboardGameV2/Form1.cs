/*
http://github.com/phillbenoit

Word game that takes input from 4 separate keyboards.

This program works by:
- mapping keypresses to player objects
- including uniform ui elements to manipulate in player objects
- making actions universal amongst all players
- using a binary search of word hashes to verify spelling 
*/

//using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
//using System.Windows.Forms;
using KeyboardGameV2.src;
using LanguageDictionary;
using Windows.Win32;

namespace KeyboardGameV2
{
    public partial class Form1 : Form
    {
        //strings for UI controlls
        private const string MNUMSG_ASSIGN = "Assign Player {0}";
        private const string MNUMSG_RELEASE = "Release Player {0}";
        private const string MSG_ASSIGN = "please press a letter";
        //private const string BTNMSG_READY = "Player {0} Ready";
        private const string MNUMSG_START = "Start Game";
        private const string MNUMSG_STOP = "Stop Game";
        private const string POPMSG_HANDLE_ERROR = "Failed to set up handler for raw input.";
        private const string POPMSG_FILE_FILTER = "one word per line txt files (*.txt)|*.txt";
        private const string POPMSG_GAME_OVER = "Game Over!";
        //private const string DGVCOL_WORD = "word";
        //private const string DGVCOL_POINTS = "points";

        //game tiles
        private LetterBag _bag = new();

        private readonly Scoreboard _scoreboard = [];

        //private byte _playerCount = 0;

        //number of tiles to draw in a game
        private const byte TILES_TO_DRAW = 20;

        //sets form to respond in game mode
        //private bool _gameOn = false;

        //timer variables
        private const ushort MAX_SECONDS = 120;
        private ushort _seconds = 0;

        //objext that holds all of the plaers' information
        private readonly KBGPlayer[] _players;

        //maps a keyboard handle to a player object
        private readonly Dictionary<IntPtr, KBGPlayer> _keyboardMap = [];

        private EnglishDictionary _dictionary = new();
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

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            //throw error message box if device registration fails
            if (!RegisterKBHandle((Windows.Win32.Foundation.HWND)this.Handle))
                MessageBox.Show(POPMSG_HANDLE_ERROR);
        }

        //method declared unsafe for the use of a variable's address
        static private unsafe bool RegisterKBHandle(Windows.Win32.Foundation.HWND FormHandle)
        {
            Windows.Win32.UI.Input.RAWINPUTDEVICE device = new()
            {
                usUsagePage = (ushort)Win32Enum.HID_USAGE_PAGE.GENERIC,
                usUsage = (ushort)Win32Enum.HID_USAGE_ID.KEYBOARD,
                dwFlags = Windows.Win32.UI.Input.RAWINPUTDEVICE_FLAGS.RIDEV_INPUTSINK,
                hwndTarget = FormHandle
            };
            return PInvoke.RegisterRawInputDevices(&device, 1, (uint)sizeof(Windows.Win32.UI.Input.RAWINPUTDEVICE));
        }
        unsafe private static Windows.Win32.UI.Input.RAWINPUT? ReadInputMessage(Message m)
        {
            //get size of input buffer
            uint dwSize = 0;
            PInvoke.GetRawInputData(
                (Windows.Win32.UI.Input.HRAWINPUT)m.LParam,
                Windows.Win32.UI.Input.RAW_INPUT_DATA_COMMAND_FLAGS.RID_INPUT,
                null,
                ref dwSize,
                (uint)sizeof(Windows.Win32.UI.Input.HRAWINPUT));

            /*
            Winuser.GetRawInputData(
                m.LParam,
                Winuser.RID_INPUT,
                IntPtr.Zero,
                ref dwSize,
                (uint)Marshal.SizeOf(typeof(Winuser.RAWINPUTHEADER)
                ));
            */
            IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);
            try
            {
                //read input buffer if it is of the expected size
                if (PInvoke.GetRawInputData(
                    (Windows.Win32.UI.Input.HRAWINPUT)m.LParam,
                    Windows.Win32.UI.Input.RAW_INPUT_DATA_COMMAND_FLAGS.RID_INPUT,
                    (void*)buffer,
                    ref dwSize,
                    (uint)sizeof(Windows.Win32.UI.Input.HRAWINPUT))
                    == dwSize) return Marshal.PtrToStructure<Windows.Win32.UI.Input.RAWINPUT>(buffer);

            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
            return null;
        }

        private static bool IsWmType(Message message, int type)
        { return message.Msg == type; }
        private static bool IsRimType(Windows.Win32.UI.Input.RAWINPUT message, int type)
        { return message.header.dwType == type; }

        //process windows messages
        protected override void WndProc(ref Message m)
        {
            //filter for input messages
            if (IsWmType(m, (int)Win32Enum.WINDOWS_MESSAGE_TYPE.WM_INPUT))
            {
                //memory safe read message
                Windows.Win32.UI.Input.RAWINPUT? null_or_raw = ReadInputMessage(m);
                if (null_or_raw is null) return;
                Windows.Win32.UI.Input.RAWINPUT raw = null_or_raw.Value;

                //filter for keyboard type input messages
                if (IsRimType(raw, (int)Win32Enum.RIM_TYPE.KEYBOARD))
                {
                    //pressed key
                    ushort vkey = raw.data.keyboard.VKey;

                    //handle for device
                    IntPtr deviceHandle = raw.header.hDevice;

                    //try to pull a player from the keyboard mappings
                    if (_keyboardMap.TryGetValue(deviceHandle, out KBGPlayer? p))
                    {
                        //key up event
                        if (Convert.ToBoolean(raw.data.keyboard.Flags & (ushort)Win32Enum.RAW_KB_FLAGS.UP))
                        {
                            p.isPressed[vkey] = false;
                            if (p.UI.GetWord().Length == 1) p.UI.ClearLights();
                            else p.UI.HeartbeatOff();
                        }

                        //key down event filtered for debounce
                        else if (!p.isPressed[vkey])
                        {
                            //debounce makes sure key goes up before it can be pressed again
                            p.isPressed[vkey] = true;
                            p.UI.HeartbeatOn();
                            KeypressHandler(p, (CharEncoding.ANSI)vkey);
                        }
                    }
                    //look for player flagged for assignment
                    else foreach (KBGPlayer pl in _players)
                            if (pl.assignFlag) ConfirmAssign(deviceHandle, pl);

                }
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
                    if (!string.IsNullOrEmpty(s)) p.UI.SetWord(s[..^1]);

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

        internal void AddWord(String word, UInt16 points, KBGPlayer player)
        {
            Scoreboard.ScoreEntry entry = new(word, points);
            entry.Players[player.PLAYER_INDEX - 1] = true;
            //make sure word is not already in the list
            int index = _scoreboard.IndexOf(entry);
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

        private void ConfirmAssign(IntPtr h, KBGPlayer p)
        {
            _keyboardMap.Add(h, p);
            p.assignFlag = false;

            //update and reactivate relevant ui elements
            p.UI.SetAssignText(String.Format(MNUMSG_RELEASE, p.PLAYER_INDEX));

            mnuStrip.Enabled = true;
            mnuStart.Enabled = mnuLoad.Enabled == false;
            p.UI.SetWord("");
        }

        private void PlayerAssignment(KBGPlayer p)
        {
            string nextText;

            //release
            if (p.UI.IsAssigned())
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

            //assign
            else
            {
                nextText = String.Format(MNUMSG_RELEASE, p.PLAYER_INDEX);
                mnuStrip.Enabled = false;
                p.assignFlag = true;
                p.UI.SetWord(MSG_ASSIGN);
            }

            p.UI.SetAssignText(nextText);
        }

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
                lblLetterPool.Text = _bag.Draw(TILES_TO_DRAW);
            }

            mnuStart.Text = nextText;
            Timer.Enabled = !Timer.Enabled;
        }

        private void Click_optP1(object sender, EventArgs e) { PlayerAssignment(_players[0]); }
        private void Click_optP2(object sender, EventArgs e) { PlayerAssignment(_players[1]); }
        private void Click_optP3(object sender, EventArgs e) { PlayerAssignment(_players[2]); }
        private void Click_optP4(object sender, EventArgs e) { PlayerAssignment(_players[3]); }

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
