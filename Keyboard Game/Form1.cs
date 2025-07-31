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
using language_dictionary;
using letter_tile;
using user32;
using System.Collections.Generic;

namespace Keyboard_Game
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
        private Letter_Bag bag;

        //number of tiles to draw in a game
        private byte tiles_to_draw = 20;

        //sets form to respond in game mode
        private bool game_on = false;

        //timer variables
        public ushort MAX_SECONDS = 120;
        private ushort seconds;

        //objext that holds all of the plaers' information
        private player[] players;
        
        //maps a keyboard handle to a player object
        private Dictionary<IntPtr, player> keyboard_map =
            new Dictionary<IntPtr, player>();

        //----------------------------------------------------
        //form functions

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            barTimer.Maximum = MAX_SECONDS;
            players = new player[4];
            players[0] = new player(new player_ui_elements(
                lblP1Score, lblP1CurrentWord, gridP1, btnP1Assign, btnP1Release,
                btnP1Heartbeat, btnP1InDict, btnP1WorthPoints), 1);
            players[1] = new player(new player_ui_elements(
                lblP2Score, lblP2CurrentWord, gridP2, btnP2Assign, btnP2Release,
                btnP2Heartbeat, btnP2InDict, btnP2WorthPoints), 2);
            players[2] = new player(new player_ui_elements(
                lblP3Score, lblP3CurrentWord, gridP3, btnP3Assign, btnP3Release,
                btnP3Heartbeat, btnP3InDict, btnP3WorthPoints), 3);
            players[3] = new player(new player_ui_elements(
                lblP4Score, lblP4CurrentWord, gridP4, btnP4Assign, btnP4Release,
                btnP4Heartbeat, btnP4InDict, btnP4WorthPoints), 4);
            foreach (player p in players) p.Reset();
            lblLetterPool.Text = "";
            player_ui_elements.BACKUP_COLOR = btnP1Release.BackColor;
        }

        //called once per second
        private void timer_Tick(object sender, EventArgs e)
        {
            //tick down timer
            if (game_on)
            {
                seconds--;

                //update timer label
                lblTimer.Text = String.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);

                //set progress color based on amount of time remaining
                Color c = Color.DarkGreen;
                if (seconds < (MAX_SECONDS / 2)) c = Color.Gold;
                if (seconds < (MAX_SECONDS / 4)) c = Color.Maroon;
                barTimer.ForeColor = c;

                //update progress
                barTimer.Value = seconds;

                //check for game over
                if (seconds == 0)
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

        //process windows messages
        protected override void WndProc(ref Message m)
        {
            //filter for input messages
            if (Winuser.is_wm_type(m, Winuser.WM_INPUT))
            {
                //memory safe read message
                Nullable<Winuser.RAWINPUT> null_or_raw = Winuser.get_input_header(m);
                if (null_or_raw.Equals(null)) return;
                Winuser.RAWINPUT raw = null_or_raw.Value;
                
                //filter for keyboard type input messages
                if (Winuser.is_rim_type(raw, Winuser.RIM_TYPEKEYBOARD))
                {
                    //pressed key
                    ushort vkey = raw.data.keyboard.VKey;

                    //handle for device
                    IntPtr deviceHandle = raw.header.hDevice;

                    player p;
                    //try to pull a player from the keyboard mappings
                    if (keyboard_map.TryGetValue(deviceHandle,out p))
                    {
                        //key up event
                        if (Convert.ToBoolean(raw.data.keyboard.Flags & Winuser.RIK_KEY_UP_FLAG))
                        {
                            p.is_pressed[vkey] = false;
                            p.ui.heartbeat.BackColor = player_ui_elements.BACKUP_COLOR;
                        }

                        //key down event filtered for debounce
                        else if (!p.is_pressed[vkey])
                        {
                            //debounce makes sure key goes up before it can be pressed again
                            p.is_pressed[vkey] = true;
                            p.ui.heartbeat.BackColor = Color.Gold;
                            keypress_handler(p, (Winuser.ANSI)vkey);
                        }
                    }
                    //look for player flagged for assignment
                    else foreach(player pl in players)
                            if (pl.assign_flag) confirm_assign(deviceHandle, pl);

                }
            }
            //perform normal windows message processing for noninput messages
            else base.WndProc(ref m);

        }

        //handles keypresses for known players
        private void keypress_handler(player p, Winuser.ANSI key)
        {
            //game actions
            if (game_on)
            {
                //get the word the player is currently typing
                string s = p.ui.current_word.Text;

                //letters (shift to lower case)
                if (key >= Winuser.ANSI.VK_A && key <= Winuser.ANSI.VK_Z)
                    p.ui.current_word.Text += Convert.ToChar((key + 0x20));

                //backspace
                else if (key == Winuser.ANSI.VK_BACKSPACE)
                {
                    if (!string.IsNullOrEmpty(s))
                        p.ui.current_word.Text = s.Remove(s.Length - 1);
                }

                //delete
                else if (key == Winuser.ANSI.VK_DELETE)
                    p.ui.current_word.Text = "";

                //spacebar or enter to check word and add to score
                else if (key == Winuser.ANSI.VK_SPACE ||
                    key == Winuser.ANSI.VK_RETURN)
                {
                    p.ui.worth_points.BackColor =
                        player_ui_elements.BACKUP_COLOR;
                    
                    //make sure word is in the dictionary
                    if (English_Dictionary.In_Dictionary(s))
                    {
                        p.ui.in_dict.BackColor = Color.DarkGreen;
                        p.Add_word(s, bag.Score_Word(s));
                    }
                    else p.ui.in_dict.BackColor = Color.DarkRed;
                    p.ui.current_word.Text = "";
                }

            }


            //universal actions

            //up arrow toggles typed in word visibility
            if (key == Winuser.ANSI.VK_UP)
                p.ui.current_word.Visible = !p.ui.current_word.Visible;

            //down arrow toggles score word list visibility
            else if (key == Winuser.ANSI.VK_DOWN)
                p.ui.score_list.Visible = !p.ui.score_list.Visible;

            //right arrow toggles between ascending and descending score mode
            else if (key == Winuser.ANSI.VK_RIGHT)
                p.ui.Set_Sort_Mode(
                    p.ui.Get_Sort_Mode() == 
                    player_ui_elements.SortModes.ScoreDesc ?
                    player_ui_elements.SortModes.ScoreAsc :
                    player_ui_elements.SortModes.ScoreDesc);

            //left arrow toggles between ascending and descending score mode
            else if (key == Winuser.ANSI.VK_LEFT)
                p.ui.Set_Sort_Mode(
                    p.ui.Get_Sort_Mode() ==
                    player_ui_elements.SortModes.WordAlpha ?
                    player_ui_elements.SortModes.WordLength :
                    player_ui_elements.SortModes.WordAlpha);
        }

        //called from windows message handler to assign player keyboard handle
        private void confirm_assign(IntPtr h, player p)
        {
            keyboard_map.Add(h, p);
            p.assign_flag = false;

            //update and reactivate relevant ui elements
            p.ui.assign.Text = String.Format(BTNMSG_READY, p.player_index);
            foreach (player pl in players)
            {
                if (keyboard_map.ContainsValue(pl)) pl.ui.release.Enabled = true;
                else pl.ui.assign.Enabled = true;
            }
            menuStrip1.Enabled = true;
            miStartGame.Enabled =
                miLoadDictionary.Enabled == false;
        }


        //---------------------------------------------------
        //common button methods

        //sets binary flag for player assignment and locks down the ui
        private void assign(player p)
        {
            menuStrip1.Enabled = false;
            foreach (player pl in players)
            {
                pl.ui.assign.Enabled = false;
                pl.ui.release.Enabled = false;
            }
            p.assign_flag = true;
            p.ui.assign.Text = BTNMSG_WAIT;
        }


        //release keyboard handle from a player
        private void release(player p)
        {
            //search for and remove handle
            IntPtr key = IntPtr.Zero;
            foreach (var kh in keyboard_map) if (kh.Value == p) key = kh.Key;
            keyboard_map.Remove(key);
            
            //reset ui elements
            p.Reset();
            p.ui.assign.Text = String.Format(BTNMSG_ASSIGN, p.player_index);
            p.ui.assign.Enabled = true;
            p.ui.release.Enabled = false;
            miStartGame.Enabled =
                keyboard_map.Count > 0 &&
                miLoadDictionary.Enabled == false;
        }


        //---------------------------------------------------
        //form button events

        private void btnP1Assign_Click(object sender, EventArgs e)
        { assign(players[0]); }

        private void btnP1Release_Click(object sender, EventArgs e)
        { release(players[0]); }

        private void btnP2Assign_Click(object sender, EventArgs e)
        { assign(players[1]); }

        private void btnP2Release_Click(object sender, EventArgs e)
        { release(players[1]); }

        private void btnP3Assign_Click(object sender, EventArgs e)
        { assign(players[2]); }

        private void btnP3Release_Click(object sender, EventArgs e)
        { release(players[2]); }

        private void btnP4Assign_Click(object sender, EventArgs e)
        { assign(players[3]); }

        private void btnP4Release_Click(object sender, EventArgs e)
        { release(players[3]); }


        //----------------------------------------
        //menu options

        //start / stop game
        private void startGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string next_text;

            //game stop actions
            if (game_on)
            {
                next_text = MNUMSG_START;
                foreach(player p in players)
                {
                    p.ui.current_word.Visible = true;
                    p.ui.score_list.Visible = true;
                    if (keyboard_map.ContainsValue(p))
                        p.ui.release.Enabled = true;
                    else p.ui.assign.Enabled = true;
                }
            }

            //game start actions
            else
            {
                next_text = MNUMSG_STOP;
                seconds = MAX_SECONDS;
                foreach (player p in players)
                {
                    p.ui.assign.Enabled = false;
                    p.ui.release.Enabled = false;
                    p.Reset();
                }
                bag.Reset();
                lblLetterPool.Text = bag.Draw(tiles_to_draw);
            }

            miStartGame.Text = next_text;
            game_on = !game_on;
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
                    English_Dictionary.Load_From_Txt(
                        openFileDialog.OpenFile(),
                        openFileDialog.FileName
                        );
                    bag = new Letter_Bag(English_Dictionary.Max_Letter_Count(), 'a');
                    miLoadDictionary.Enabled = false;
                    miStartGame.Enabled = keyboard_map.Count > 0;
                }
                
            }

        }

        //-----------------------------------------------------
        //classes to organize game data

        private class player_ui_elements
        {
            public readonly Label score, current_word;
            public readonly DataGridView score_list;
            public readonly Button assign, release,//player keyboard actions
                heartbeat, in_dict, worth_points;//feedback on keyboard inout
            private byte sort_index;
            private readonly Action[] sort_modes = new Action[4];
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
            private LengthSort length_compare = new LengthSort();
            public enum SortModes { ScoreDesc, ScoreAsc, WordAlpha, WordLength }
            private void Sort_Score_Dec()
            { score_list.Sort(score_list.Columns[1],ListSortDirection.Descending); }
            private void Sort_Score_Asc()
            { score_list.Sort(score_list.Columns[1], ListSortDirection.Ascending); }
            private void Sort_Alpha()
            { score_list.Sort(score_list.Columns[0], ListSortDirection.Ascending); }
            private void Sort_Length()
            { score_list.Sort(length_compare); }
            public void Set_Sort_Mode(SortModes s) { sort_index = (byte)s; Sort(); }
            public SortModes Get_Sort_Mode() { return (SortModes)sort_index; }
            public void Sort() { sort_modes[sort_index](); }


            //makes row header a row count
            private void dgv_sorted(object sender, EventArgs e)
            {
                foreach (DataGridViewRow r in score_list.Rows)
                    r.HeaderCell.Value = (r.Index + 1).ToString();
            }

            //constructor
            public player_ui_elements(Label s, Label cw, DataGridView sl, Button an,
                Button rl, Button hb, Button id, Button wp)
            {
                score = s;
                current_word = cw;
                score_list = sl;
                assign = an;
                release = rl;
                heartbeat = hb;
                in_dict = id;
                worth_points = wp;
                
                //setup sorting
                sort_modes[(byte)SortModes.ScoreDesc] = Sort_Score_Dec;
                sort_modes[(byte)SortModes.ScoreAsc] = Sort_Score_Asc;
                sort_modes[(byte)SortModes.WordAlpha] = Sort_Alpha;
                sort_modes[(byte)SortModes.WordLength] = Sort_Length;
                sort_index = (byte)SortModes.ScoreDesc;

                //manual setup of datagridview
                score_list.ColumnCount = 2;
                score_list.Columns[0].HeaderText = DGVCOL_WORD;
                score_list.Columns[1].HeaderText = DGVCOL_POINTS;
                score_list.Columns[0].ValueType = typeof(String);
                score_list.Columns[1].ValueType = typeof(UInt16);
                score_list.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                score_list.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                score_list.Sorted += dgv_sorted;
            }

            public void Reset()
            {
                score.Text = "0";
                current_word.Text = "";
                score_list.Rows.Clear();
                heartbeat.BackColor = BACKUP_COLOR;
                in_dict.BackColor = BACKUP_COLOR;
                worth_points.BackColor = BACKUP_COLOR;
            }
        }

        private class player
        {
            private uint score;

            public readonly player_ui_elements ui;
            public readonly byte player_index;

            //public IntPtr keyboard_handle;
            public bool assign_flag;
            public bool[] is_pressed;


            public void Reset()
            {
                score = 0;
                is_pressed = new bool[Winuser.MAX_ANSI];
                ui.Reset();
            }

            public void Add_word(String word, UInt16 points)
            {
                //make sure word is not already in the list
                foreach (DataGridViewRow row in ui.score_list.Rows)
                    if (word.Equals(row.Cells[0].Value)) return;

                //update the ui
                score += points;
                ui.worth_points.BackColor = points == 0 ? Color.DarkRed : Color.DarkGreen;
                ui.score.Text = score.ToString();
                ui.score_list.Rows.Add(word, points);
                ui.Sort();
            }

            public player(player_ui_elements u, byte i)
            {
                ui = u;
                player_index = i;
                score = 0;
                assign_flag = false;
                is_pressed = new bool[Winuser.MAX_ANSI];
            }
        }

    }

}
