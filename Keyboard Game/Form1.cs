/*
 http://github.com/phillbenoit
 
Word game that takes input from 4 separate keyboards.

This program works by:
- mapping keypresses to player objects
- including uniform ui elements to manipulate in player objects
- making actions universal amongst all players
- using a binary search of word hashes to verify spelling 
*/

using System;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LanguageDictionary;
using LetterTile;
using User32;
using System.Collections.Generic;
using AsciiChatacterMap;

namespace KeyboardGame
{

    public partial class Form1 : Form
    {
        //strings for UI controlls
        private const string BTNMSG_ASSIGN = "Assign Player {0}";
        private const string BTNMSG_WAIT = "waiting...";
        private const string BTNMSG_READY = "Player {0} Ready";
        private const string MNUMSG_START = "Start Game";
        private const string MNUMSG_STOP = "Stop Game";
        private const string POPMSG_HANDLE_ERROR = "Failed to set up handler for raw input.";
        private const string POPMSG_FILE_FILTER = "one word per line txt files (*.txt)|*.txt";
        private const string POPMSG_GAME_OVER = "Game Over!";
        private const string DGVCOL_WORD = "word";
        private const string DGVCOL_POINTS = "points";

        //game tiles
        private LetterBag _bag;

        //number of tiles to draw in a game
        private const byte TILES_TO_DRAW = 20;

        //sets form to respond in game mode
        private bool _gameOn = false;

        //timer variables
        private const ushort MAX_SECONDS = 120;
        private ushort _seconds;

        //objext that holds all of the plaers' information
        private Player[] _players;
        
        //maps a keyboard handle to a player object
        private readonly Dictionary<IntPtr, Player> _keyboardMap =
            new Dictionary<IntPtr, Player>();

        private EnglishDictionary _dictionary;

        //----------------------------------------------------
        //form functions

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            barTimer.Maximum = MAX_SECONDS;
            _players = new Player[4];
            _players[0] = new Player(new PlayerUiElements(
                lblP1Score, lblP1CurrentWord, gridP1, btnP1Assign, btnP1Release,
                btnP1Heartbeat, btnP1InDict, btnP1WorthPoints), 1);
            _players[1] = new Player(new PlayerUiElements(
                lblP2Score, lblP2CurrentWord, gridP2, btnP2Assign, btnP2Release,
                btnP2Heartbeat, btnP2InDict, btnP2WorthPoints), 2);
            _players[2] = new Player(new PlayerUiElements(
                lblP3Score, lblP3CurrentWord, gridP3, btnP3Assign, btnP3Release,
                btnP3Heartbeat, btnP3InDict, btnP3WorthPoints), 3);
            _players[3] = new Player(new PlayerUiElements(
                lblP4Score, lblP4CurrentWord, gridP4, btnP4Assign, btnP4Release,
                btnP4Heartbeat, btnP4InDict, btnP4WorthPoints), 4);
            foreach (Player p in _players) p.Reset();
            lblLetterPool.Text = "";
            PlayerUiElements.BACKUP_COLOR = btnP1Release.BackColor;
        }

        //called once per second
        private void timer_Tick(object sender, EventArgs e)
        {
            //tick down timer
            if (_gameOn)
            {
                _seconds--;

                //update timer label
                lblTimer.Text = String.Format("{0:D2}:{1:D2}", _seconds / 60, _seconds % 60);

                //set progress color based on amount of time remaining
                Color c = Color.DarkGreen;
                if (_seconds < (MAX_SECONDS / 2)) c = Color.Gold;
                if (_seconds < (MAX_SECONDS / 4)) c = Color.Maroon;
                barTimer.ForeColor = c;

                //update progress
                barTimer.Value = _seconds;

                //check for game over
                if (_seconds == 0)
                {
                    startGameToolStripMenuItem_Click(sender, e);
                    MessageBox.Show(POPMSG_GAME_OVER);
                }

            }
        }

        //----------------------------------------------------
        //input functions

        //creates raw input controller handles for input devices
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            var ric = new Winuser.RAWINPUTDEVICECLASS[1];
            ric[0].usUsagePage = Winuser.RIC_PRIMARY_PAGE;
            ric[0].usUsage = Winuser.RIC_KEYBOARD;
            ric[0].dwFlags = Winuser.RIC_INPUTSINK; 
            ric[0].hwndTarget = this.Handle;

            //throw error message box if device registration fails
            if (!Winuser.RegisterRawInputDevices(
                ric,
                (uint)ric.Length,
                (uint)Marshal.SizeOf(ric[0])
                ))
            {
                MessageBox.Show(POPMSG_HANDLE_ERROR);
            }
            
        }

        public static Nullable<Winuser.RAWINPUT> GetInputHeader(Message m)
        {
            //get size of input buffer
            uint dwSize = 0;
            Winuser.GetRawInputData(
                m.LParam,
                Winuser.RID_INPUT,
                IntPtr.Zero,
                ref dwSize,
                (uint)Marshal.SizeOf(typeof(Winuser.RAWINPUTHEADER)
                ));

            IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);
            try
            {
                //read input buffer if it is of the expected size
                if (Winuser.GetRawInputData(
                    m.LParam,
                    Winuser.RID_INPUT,
                    buffer,
                    ref dwSize,
                    (uint)Marshal.SizeOf(typeof(Winuser.RAWINPUTHEADER)))
                    == dwSize) return Marshal.PtrToStructure<Winuser.RAWINPUT>(buffer);

            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
            return null;
        }

        private static bool IsWmType(Message message, int type)
        { return message.Msg == type; }
        private static bool IsRimType(Winuser.RAWINPUT message, int type)
        { return message.header.dwType == type; }

        //process windows messages
        protected override void WndProc(ref Message m)
        {
            //filter for input messages
            if (IsWmType(m, Winuser.WM_INPUT))
            {
                //memory safe read message
                Nullable<Winuser.RAWINPUT> null_or_raw = GetInputHeader(m);
                if (null_or_raw.Equals(null)) return;
                Winuser.RAWINPUT raw = null_or_raw.Value;
                
                //filter for keyboard type input messages
                if (IsRimType(raw, Winuser.RIM_TYPEKEYBOARD))
                {
                    //pressed key
                    ushort vkey = raw.data.keyboard.VKey;

                    //handle for device
                    IntPtr deviceHandle = raw.header.hDevice;

                    Player p;
                    //try to pull a player from the keyboard mappings
                    if (_keyboardMap.TryGetValue(deviceHandle,out p))
                    {
                        //key up event
                        if (Convert.ToBoolean(raw.data.keyboard.Flags & Winuser.RIK_KEY_UP_FLAG))
                        {
                            p.isPressed[vkey] = false;
                            p.ui.heartbeat.BackColor = PlayerUiElements.BACKUP_COLOR;
                            if (p.ui.currentWord.Text.Length > 0)
                            {
                                p.ui.inDict.BackColor = PlayerUiElements.BACKUP_COLOR;
                                p.ui.worthPoints.BackColor = PlayerUiElements.BACKUP_COLOR;
                            }
                        }

                        //key down event filtered for debounce
                        else if (!p.isPressed[vkey])
                        {
                            //debounce makes sure key goes up before it can be pressed again
                            p.isPressed[vkey] = true;
                            p.ui.heartbeat.BackColor = Color.Gold;
                            KeypressHandler(p, (Winuser.ANSI)vkey);
                        }
                    }
                    //look for player flagged for assignment
                    else foreach(Player pl in _players)
                            if (pl.assignFlag) ConfirmAssign(deviceHandle, pl);

                }
            }
            //perform normal windows message processing for noninput messages
            else base.WndProc(ref m);

        }

        //handles keypresses for known players
        private void KeypressHandler(Player p, Winuser.ANSI key)
        {
            //game actions
            if (_gameOn)
            {
                //get the word the player is currently typing
                string s = p.ui.currentWord.Text;

                //letters (shift to lower case)
                if (key >= Winuser.ANSI.VK_A && key <= Winuser.ANSI.VK_Z)
                    p.ui.currentWord.Text += Convert.ToChar((key + 0x20));

                //backspace
                else if (key == Winuser.ANSI.VK_BACKSPACE)
                {
                    if (!string.IsNullOrEmpty(s))
                        p.ui.currentWord.Text = s.Remove(s.Length - 1);
                }

                //delete
                else if (key == Winuser.ANSI.VK_DELETE)
                    p.ui.currentWord.Text = "";

                //spacebar or enter to check word and add to score
                else if (key == Winuser.ANSI.VK_SPACE ||
                    key == Winuser.ANSI.VK_RETURN)
                {
                    p.ui.worthPoints.BackColor =
                        PlayerUiElements.BACKUP_COLOR;
                    
                    //make sure word is in the dictionary
                    if (_dictionary.InDictionary(s))
                    {
                        p.ui.inDict.BackColor = Color.DarkGreen;
                        p.AddWord(s, _bag.ScoreWord(s));
                    }
                    else p.ui.inDict.BackColor = Color.DarkRed;
                    p.ui.currentWord.Text = "";
                }

            }


            //universal actions

            //up arrow toggles typed in word visibility
            if (key == Winuser.ANSI.VK_UP)
                p.ui.currentWord.Visible = !p.ui.currentWord.Visible;

            //down arrow toggles score word list visibility
            else if (key == Winuser.ANSI.VK_DOWN)
                p.ui.scoreList.Visible = !p.ui.scoreList.Visible;

            //right arrow toggles between ascending and descending score mode
            else if (key == Winuser.ANSI.VK_RIGHT)
                p.ui.SetSortMode(
                    p.ui.GetSortMode() == 
                    PlayerUiElements.SortModes.ScoreDesc ?
                    PlayerUiElements.SortModes.ScoreAsc :
                    PlayerUiElements.SortModes.ScoreDesc);

            //left arrow toggles between ascending and descending score mode
            else if (key == Winuser.ANSI.VK_LEFT)
                p.ui.SetSortMode(
                    p.ui.GetSortMode() ==
                    PlayerUiElements.SortModes.WordAlpha ?
                    PlayerUiElements.SortModes.WordLength :
                    PlayerUiElements.SortModes.WordAlpha);
        }

        //called from windows message handler to assign player keyboard handle
        private void ConfirmAssign(IntPtr h, Player p)
        {
            _keyboardMap.Add(h, p);
            p.assignFlag = false;

            //update and reactivate relevant ui elements
            p.ui.assign.Text = String.Format(BTNMSG_READY, p.PLAYER_INDEX);
            foreach (Player pl in _players)
            {
                if (_keyboardMap.ContainsValue(pl)) pl.ui.release.Enabled = true;
                else pl.ui.assign.Enabled = true;
            }
            menuStrip1.Enabled = true;
            miStartGame.Enabled =
                miLoadDictionary.Enabled == false;
        }


        //---------------------------------------------------
        //common button methods

        //sets binary flag for player assignment and locks down the ui
        private void Assign(Player p)
        {
            menuStrip1.Enabled = false;
            foreach (Player pl in _players)
            {
                pl.ui.assign.Enabled = false;
                pl.ui.release.Enabled = false;
            }
            p.assignFlag = true;
            p.ui.assign.Text = BTNMSG_WAIT;
        }


        //release keyboard handle from a player
        private void Release(Player p)
        {
            //search for and remove handle
            IntPtr key = IntPtr.Zero;
            foreach (var kh in _keyboardMap) if (kh.Value == p) key = kh.Key;
            _keyboardMap.Remove(key);
            
            //reset ui elements
            p.Reset();
            p.ui.assign.Text = String.Format(BTNMSG_ASSIGN, p.PLAYER_INDEX);
            p.ui.assign.Enabled = true;
            p.ui.release.Enabled = false;
            miStartGame.Enabled =
                _keyboardMap.Count > 0 &&
                miLoadDictionary.Enabled == false;
        }


        //---------------------------------------------------
        //form button events

        private void btnP1Assign_Click(object sender, EventArgs e)
        { Assign(_players[0]); }

        private void btnP1Release_Click(object sender, EventArgs e)
        { Release(_players[0]); }

        private void btnP2Assign_Click(object sender, EventArgs e)
        { Assign(_players[1]); }

        private void btnP2Release_Click(object sender, EventArgs e)
        { Release(_players[1]); }

        private void btnP3Assign_Click(object sender, EventArgs e)
        { Assign(_players[2]); }

        private void btnP3Release_Click(object sender, EventArgs e)
        { Release(_players[2]); }

        private void btnP4Assign_Click(object sender, EventArgs e)
        { Assign(_players[3]); }

        private void btnP4Release_Click(object sender, EventArgs e)
        { Release(_players[3]); }


        //----------------------------------------
        //menu options

        //start / stop game
        private void startGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nextText;

            //game stop actions
            if (_gameOn)
            {
                nextText = MNUMSG_START;
                foreach(Player p in _players)
                {
                    p.ui.currentWord.Visible = true;
                    p.ui.scoreList.Visible = true;
                    if (_keyboardMap.ContainsValue(p))
                        p.ui.release.Enabled = true;
                    else p.ui.assign.Enabled = true;
                }
            }

            //game start actions
            else
            {
                nextText = MNUMSG_STOP;
                _seconds = MAX_SECONDS;
                foreach (Player p in _players)
                {
                    p.ui.assign.Enabled = false;
                    p.ui.release.Enabled = false;
                    p.Reset();
                }
                _bag.Reset();
                lblLetterPool.Text = _bag.Draw(TILES_TO_DRAW);
            }

            miStartGame.Text = nextText;
            _gameOn = !_gameOn;
        }

        //load dictionary and set up bag of game tiles
        private void loadDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = POPMSG_FILE_FILTER;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _dictionary = new EnglishDictionary(
                        openFileDialog.OpenFile(),
                        openFileDialog.FileName
                        );
                    _bag = new LetterBag(_dictionary.MAX_LETTER_COUNT, (char)ASCII.Value.LETTER_a);
                    miLoadDictionary.Enabled = false;
                    miStartGame.Enabled = _keyboardMap.Count > 0;
                }
                
            }

        }

        //-----------------------------------------------------
        //classes to organize game data

        private class PlayerUiElements
        {
            public readonly Label score, currentWord;
            public readonly DataGridView scoreList;
            public readonly Button assign, release,//player keyboard actions
                heartbeat, inDict, worthPoints;//feedback on keyboard inout
            private byte _sortIndex;
            private readonly Action[] sortModes = new Action[4];
            public static Color BACKUP_COLOR;
            

            //everything for sorting
            private class LengthSort : System.Collections.IComparer
            {
                public int Compare(object x, object y)
                {
                    DataGridViewRow x_dgr = (DataGridViewRow)x;
                    DataGridViewRow y_dgr = (DataGridViewRow)y;
                    return y_dgr.Cells[0].Value.ToString().Length -
                        x_dgr.Cells[0].Value.ToString().Length;
                }
            }
            private readonly LengthSort _lengthCompare = new LengthSort();
            public enum SortModes { ScoreDesc, ScoreAsc, WordAlpha, WordLength }
            private void SortScoreDec()
            { scoreList.Sort(scoreList.Columns[1],ListSortDirection.Descending); }
            private void SortScoreAsc()
            { scoreList.Sort(scoreList.Columns[1], ListSortDirection.Ascending); }
            private void SortAlpha()
            { scoreList.Sort(scoreList.Columns[0], ListSortDirection.Ascending); }
            private void SortLength()
            { scoreList.Sort(_lengthCompare); }
            public void SetSortMode(SortModes s) { _sortIndex = (byte)s; Sort(); }
            public SortModes GetSortMode() { return (SortModes)_sortIndex; }
            public void Sort() { sortModes[_sortIndex](); }


            //makes row header a row count
            private void DgvSorted(object sender, EventArgs e)
            {
                foreach (DataGridViewRow r in scoreList.Rows)
                    r.HeaderCell.Value = (r.Index + 1).ToString();
            }

            //constructor
            public PlayerUiElements(Label s, Label cw, DataGridView sl, Button an,
                Button rl, Button hb, Button id, Button wp)
            {
                score = s;
                currentWord = cw;
                scoreList = sl;
                assign = an;
                release = rl;
                heartbeat = hb;
                inDict = id;
                worthPoints = wp;
                
                //setup sorting
                sortModes[(byte)SortModes.ScoreDesc] = SortScoreDec;
                sortModes[(byte)SortModes.ScoreAsc] = SortScoreAsc;
                sortModes[(byte)SortModes.WordAlpha] = SortAlpha;
                sortModes[(byte)SortModes.WordLength] = SortLength;
                _sortIndex = (byte)SortModes.ScoreDesc;

                //manual setup of datagridview
                scoreList.ColumnCount = 2;
                scoreList.Columns[0].HeaderText = DGVCOL_WORD;
                scoreList.Columns[1].HeaderText = DGVCOL_POINTS;
                scoreList.Columns[0].ValueType = typeof(String);
                scoreList.Columns[1].ValueType = typeof(UInt16);
                scoreList.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                scoreList.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                scoreList.Sorted += DgvSorted;
            }

            public void Reset()
            {
                score.Text = "0";
                currentWord.Text = "";
                scoreList.Rows.Clear();
                heartbeat.BackColor = BACKUP_COLOR;
                inDict.BackColor = BACKUP_COLOR;
                worthPoints.BackColor = BACKUP_COLOR;
            }
        }

        private class Player
        {
            private uint _score;

            public readonly PlayerUiElements ui;
            public readonly byte PLAYER_INDEX;

            public bool assignFlag;
            public bool[] isPressed;


            public void Reset()
            {
                _score = 0;
                isPressed = new bool[Winuser.MAX_ANSI];
                ui.Reset();
            }

            public void AddWord(String word, UInt16 points)
            {
                //make sure word is not already in the list
                foreach (DataGridViewRow row in ui.scoreList.Rows)
                    if (word.Equals(row.Cells[0].Value)) return;

                //update the ui
                _score += points;
                ui.worthPoints.BackColor = points == 0 ? Color.DarkRed : Color.DarkGreen;
                ui.score.Text = _score.ToString();
                ui.scoreList.Rows.Add(word, points);
                ui.Sort();
            }

            public Player(PlayerUiElements u, byte i)
            {
                ui = u;
                PLAYER_INDEX = i;
                _score = 0;
                assignFlag = false;
                isPressed = new bool[Winuser.MAX_ANSI];
            }
        }

    }

}
