using System.Text;

namespace KeyboardGameV2.src
{
    public class Scoreboard(DataGridView data)
    {
        private readonly DataGridView data = data;
        private readonly ScoreBoardSort compareObject = new();

        private const char empty = '☐';
        private const char full = '☒';

        public void Clear() { data.Rows.Clear(); }

        //adds a word to scoreboard or gives a player credit for it
        public bool Add(string word, ushort points, byte playerIndex)
        {
            //setup as a row to use comparison object for searching
            DataGridViewRow newRow = new();
            newRow.CreateCells(data);
            newRow.Cells[0].Value = word;
            newRow.Cells[2].Value = points;

            //look for the word
            int rowIndex = Search(newRow);
            if (rowIndex > -1)
            {
                //return false if player already has credit for it
#pragma warning disable CS8605 // Unboxing a possibly null value.
                if ((char)data.Rows[rowIndex].Cells[playerIndex + 2].Value == full) return false;
#pragma warning restore CS8605 // Unboxing a possibly null value.
                
                //give player credit for it if it exists
                else data.Rows[rowIndex].Cells[playerIndex + 2].Value = full;
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
            return true;
        }

        private void Sort() { data.Sort(compareObject); }

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
#pragma warning disable CA1871 // Do not pass a nullable struct to 'ArgumentNullException.ThrowIfNull'
                ArgumentNullException.ThrowIfNull(score_x);
                ArgumentNullException.ThrowIfNull(score_y);
#pragma warning restore CA1871 // Do not pass a nullable struct to 'ArgumentNullException.ThrowIfNull'

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
