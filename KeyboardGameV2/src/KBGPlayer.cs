//using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
//using System.ComponentModel.Design;

namespace KeyboardGameV2.src
{
    internal class KBGPlayer(byte PlayerIndex, Label CurrentWord, Label Score, Button Heartbeat,
        Button InDictionary, Button WorthPoints, ToolStripMenuItem Assign)
    {
        public class UIControlls(Label CurrentWord, Label Score, Button Heartbeat,
            Button InDictionary, Button WorthPoints, ToolStripMenuItem Assign)
        {
            private readonly Label CurrentWord = CurrentWord, Score = Score;
            private readonly Button Heartbeat = Heartbeat, InDictionary = InDictionary, WorthPoints = WorthPoints;
            private readonly ToolStripMenuItem Assign = Assign;

            public void SetWord(string word) { CurrentWord.Text = word; }
            public string GetWord() { return CurrentWord.Text; }
            public void SetScore(uint score) { Score.Text = score.ToString(); }
            public void HeartbeatOn() { Heartbeat.BackColor = Color.Gold; }
            public void HeartbeatOff() { Heartbeat.BackColor = Control.DefaultBackColor; }
            public void InDictionaryYes() { InDictionary.BackColor = Color.DarkBlue; }
            public void InDictionaryNo() { InDictionary.BackColor = Color.DarkRed; }
            public void WorthPointsYes() { InDictionary.BackColor = Color.DarkGreen; }
            public void WorthPointsNo() { InDictionary.BackColor = Color.DarkRed; }
            public void ToggleWordVisibility() { CurrentWord.Visible = !CurrentWord.Visible; }
            public void SetAssignText(string s) { Assign.Text = s; }
            public bool IsAssigned() { return Assign.Checked; }

            public void ClearLights() { 
                Heartbeat.BackColor = Control.DefaultBackColor;
                InDictionary.BackColor = Control.DefaultBackColor;
                WorthPoints.BackColor = Control.DefaultBackColor;
            }
            public void Reset()
            {
                CurrentWord.Text = "";
                Score.Text = "0";
                ClearLights();
            }
        }
        public readonly UIControlls UI = new(CurrentWord, Score, Heartbeat, InDictionary, WorthPoints, Assign);
        private uint _score = 0;

        public readonly byte PLAYER_INDEX = PlayerIndex;

        public bool assignFlag = false;
        public bool[] isPressed = new bool[CharEncoding.MAX_ANSI];

        public void AddPoints(uint points) { _score += points; UI.SetScore(_score); }

        public void Reset()
        {
            _score = 0;
            assignFlag = false;
            isPressed = new bool[CharEncoding.MAX_ANSI];
            UI.Reset();
        }
    }
}
