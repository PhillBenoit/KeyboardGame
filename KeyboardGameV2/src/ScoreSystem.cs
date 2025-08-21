using System.Text;

namespace KeyboardGameV2.src
{
    public class ScoreSystem(DataGridView data)
    {

        //holds the count of letters in the ltter pool
        private byte[] _drawCount = new byte[1];

        //point value for letters
        private byte[] POINTS_MAP = new byte[1];

        private string draw = "";

        //encoder for unicode used to give pool letters a subscript score
        private readonly UnicodeEncoding UNICODE = new();

        private ushort ScoreWord(string w)
        {
            byte[] wordLetterCount = new byte[_drawCount.Length];
            int score = 0;

            //count letters in the word and compile score
            foreach (char letter in w)
            {
                byte offsetLetter = (byte)(letter - (char)CharEncoding.ASCII.LETTER_a);
                wordLetterCount[offsetLetter]++;
                score += POINTS_MAP[offsetLetter];
            }
            score *= w.Length;

            //reject word if not enough letters are in the draw
            for (byte x = 0; x < _drawCount.Length; x++)
                if (_drawCount[x] - wordLetterCount[x] < 0)
                    return 0;

            //return score if enough letters found in the draw
            return (ushort)score;
        }

        public void SetDraw(string s, byte[] draw_count, byte[] point_map)
        {
            draw = s;
            _drawCount = draw_count;
            POINTS_MAP = point_map;
        }

        public string GetDraw() { return draw; }

        public string FormatDraw(bool sort, bool score, bool spaces)
        {
            List<byte> draw_unicode = [];
            char[] draw_ascii = new char[draw.Length];
            Array.Copy(draw.ToCharArray(), draw_ascii, draw.Length);
            //alphabetize the list
            if (sort) Array.Sort(draw_ascii);

            //load unicode formatted return string
            foreach (char l in draw_ascii)
            {
                //letter
                draw_unicode.Add((byte)l);
                draw_unicode.Add(0x00);

                if (score)
                    //substring for score
                    foreach (char digit in POINTS_MAP[(int)(l - CharEncoding.ASCII.LETTER_a)].ToString())
                    {
                        draw_unicode.Add((byte)(digit + 0x50));
                        draw_unicode.Add(0x20);
                    }

                if (spaces)
                {
                    //space
                    draw_unicode.Add(0x20);
                    draw_unicode.Add(0x00);
                }
            }

            if (spaces)
            {
                //remove last space
                draw_unicode.RemoveAt(draw_unicode.LastIndexOf(0x20));
                draw_unicode.RemoveAt(draw_unicode.LastIndexOf(0x00));
            }

            return UNICODE.GetString([.. draw_unicode]);
        }
        //----------------------------------------------------------------
        private readonly DataGridView data = data;
        private readonly ScoreBoardSort compareObject = new();

        private const char empty = '☐';
        private const char full = '☒';

        public void Clear() { data.Rows.Clear(); }

        public void Add(string word)
        {
            DataGridViewRow newRow = new();
            newRow.CreateCells(data);
            newRow.Cells[0].Value = word;
            newRow.Cells[1].Value = Mask(word);
            newRow.Cells[2].Value = ScoreWord(word);
            newRow.Cells[3].Value = empty;
            newRow.Cells[4].Value = empty;
            newRow.Cells[5].Value = empty;
            newRow.Cells[6].Value = empty;
            newRow.Visible = false;
            data.Rows.Add(newRow);
        }

        public void ShowWords(byte length)
        {
            for(int x = 0; x < data.RowCount; x++)
                if (data.Rows[x].Cells[0].Value is string s &&
                    s.Length >= length)
                    data.Rows[x].Visible = true;
        }

        //adds a word to scoreboard or gives a player credit for it
        public int Add(string word, byte playerIndex)
        {
            ushort score = ScoreWord(word);
            
            //setup as a row to use comparison object for searching
            DataGridViewRow newRow = new();
            newRow.CreateCells(data);
            newRow.Cells[0].Value = word;
            newRow.Cells[2].Value = score;

            //look for the word
            int rowIndex = Search(newRow);
            if (rowIndex > -1)
            {
                //return false if player already has credit for it
#pragma warning disable CS8605 // Unboxing a possibly null value.
                if ((char)data.Rows[rowIndex].Cells[playerIndex + 2].Value == full) return -1;
#pragma warning restore CS8605 // Unboxing a possibly null value.
                
                //give player credit for it if it exists
                data.Rows[rowIndex].Cells[playerIndex + 2].Value = full;
                data.Rows[rowIndex].Visible = true;
            }
            else
            {
                //fill out row
                newRow.Cells[1].Value = Mask(word);
                newRow.Cells[3].Value = empty;
                newRow.Cells[4].Value = empty;
                newRow.Cells[5].Value = empty;
                newRow.Cells[6].Value = empty;
                newRow.Cells[playerIndex + 2].Value = full;

                //add it and sort afterwards
                data.Rows.Add(newRow);
                Sort();
            }
            return score;
        }

        public void Sort() { data.Sort(compareObject); }

        //hide words in scoreboard by storing masked data
        private static string Mask(string word)
        {
            char[] mask = new char[word.Length];
            Array.Fill(mask, '*');
            return new string(mask);
        }

        //binary search for existing rows
        private int Search(DataGridViewRow x)
        {
            int low = 0;
            int high = data.Rows.Count - 1;
            while (low <= high)
            {
                int mid = low + ((high - low)/2);
                int cmp = compareObject.Compare(x, data.Rows[mid]);

                if (cmp == 0) return mid;
                if (cmp < 0) high = mid - 1;
                else low = mid + 1;
            }
            return -1;
        }

        private class ScoreBoardSort : System.Collections.IComparer
        {
            public int Compare(object? x, object? y)
            {
                //format data to compare
                ArgumentNullException.ThrowIfNull(x);
                ArgumentNullException.ThrowIfNull(y);
                DataGridViewRow row_x = (DataGridViewRow)x;
                DataGridViewRow row_y = (DataGridViewRow)y;
                string? word_x = row_x.Cells[0].Value as string;
                string? word_y = row_y.Cells[0].Value as string;
                ArgumentNullException.ThrowIfNull(word_x);
                ArgumentNullException.ThrowIfNull(word_y);
                ushort? score_x = row_x.Cells[2].Value as ushort?;
                ushort? score_y = row_y.Cells[2].Value as ushort?;
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1871 // Do not pass a nullable struct to 'ArgumentNullException.ThrowIfNull'
                ArgumentNullException.ThrowIfNull(score_x);
                ArgumentNullException.ThrowIfNull(score_y);
#pragma warning restore CA1871 // Do not pass a nullable struct to 'ArgumentNullException.ThrowIfNull'
#pragma warning restore IDE0079 // Remove unnecessary suppression

                //sort first by score
                if (score_x.Value != score_y.Value) return score_y.Value - score_x.Value;
                
                //then by word length
                if (word_x.Length != word_y.Length) return word_y.Length - word_x.Length;
                
                //finally alphabetically
                return word_x.CompareTo(word_y);
            }
        }
    }
}
