namespace KeyboardGameV2.src
{
    public class Scoreboard //: IList
    {
        private readonly DataGridView data;
        private readonly ScoreBoardSort compareObject = new();

        public Scoreboard(DataGridView data)
        {
            this.data = data;
        }

        public void Clear() { data.Rows.Clear(); }

        public bool Add(string word, ushort points, byte playerIndex)
        {
            DataGridViewRow newRow = new();
            newRow.CreateCells(data);
            newRow.Cells[0].Value = word;
            newRow.Cells[2].Value = points;

            int rowIndex = Search(newRow);
            if (rowIndex > -1)
            {
#pragma warning disable CS8605 // Unboxing a possibly null value.
                if ((bool)data.Rows[rowIndex].Cells[playerIndex + 2].Value) return false;
#pragma warning restore CS8605 // Unboxing a possibly null value.
                else data.Rows[rowIndex].Cells[playerIndex + 2].Value = true;
            }
            else
            {
                newRow.Cells[1].Value = Mask(word);
                newRow.Cells[3].Value = false;
                newRow.Cells[4].Value = false;
                newRow.Cells[5].Value = false;
                newRow.Cells[6].Value = false;
                newRow.Cells[playerIndex + 2].Value = true;
                data.Rows.Add(newRow);
                Sort();
            }
            return true;
        }

        private static string Mask(string word)
        {
            char[] mask = new char[word.Length];
            Array.Fill(mask, '*');
            return new string(mask);
        }

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

        private void Sort() { data.Sort(compareObject); }

        private class ScoreBoardSort : System.Collections.IComparer
        {
            public int Compare(object? x, object? y)
            {
                ArgumentNullException.ThrowIfNull(x);
                ArgumentNullException.ThrowIfNull(y);
                DataGridViewRow row_x = (DataGridViewRow)x;
                DataGridViewRow row_y = (DataGridViewRow)y;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string word_x = (string)row_x.Cells[0].Value;
                string word_y = (string)row_y.Cells[0].Value;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                ArgumentNullException.ThrowIfNull(word_x);
                ArgumentNullException.ThrowIfNull(word_y);
#pragma warning disable CS8605 // Converting null literal or possible null value to non-nullable type.
                ushort score_x = (ushort)row_x.Cells[2].Value;
                ushort score_y = (ushort)row_y.Cells[2].Value;
#pragma warning restore CS8605 // Converting null literal or possible null value to non-nullable type.
                if (score_x != score_y) return score_y - score_x;
                if (word_x.Length != word_y.Length) return word_y.Length - word_x.Length;
                return word_x.CompareTo(word_y);
            }
        }

        /*
        private readonly List<ScoreEntry> _data = [];

        public object? this[int index] { get { return _data[index]; }
            set {
                ArgumentNullException.ThrowIfNull(value);
                _data[index] = (ScoreEntry)value;
            } }

        public bool IsFixedSize => false;

        public bool IsReadOnly => false;

        public int Count => _data.Count;

        public bool IsSynchronized => false;

        public object SyncRoot => throw new NotImplementedException();

        public int Add(object? value)
        {
            if (value is null) return -1;
            _data.Add((ScoreEntry)value);
            return _data.IndexOf((ScoreEntry)value);
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool Contains(object? value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return _data.Contains((ScoreEntry)value);
        }

        public void CopyTo(Array array, int index)
        {
            _data.CopyTo((ScoreEntry[])array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return _data.GetEnumerator();
        }
        

        public int IndexOf(object? value)
        {
            ArgumentNullException.ThrowIfNull(value);
            int index = Array.BinarySearch([.. _data], (ScoreEntry)value);
            return index < 0 ? -1 : index;
        }

        public void Insert(int index, object? value)
        {
            ArgumentNullException.ThrowIfNull(value);
            _data.Insert(index, (ScoreEntry)value);
        }

        public void Remove(object? value)
        {
            ArgumentNullException.ThrowIfNull(value);
            _data.Remove((ScoreEntry)value);
        }

        public void RemoveAt(int index)
        {
            _data.RemoveAt(index);
        }

        public void Sort()
        {
            _data.Sort();
        }

        //object for individual entries in the scoreboard
        public class ScoreEntry(string Word, uint Points) : IComparable
        {
            public readonly string Word = Word;
            public readonly uint Points = Points;
            public bool[] Players = new bool[4];
            
            public int CompareTo(object? obj)
            {
                ArgumentNullException.ThrowIfNull(obj);
                ScoreEntry other = (ScoreEntry)obj;

                //sorted first by score
                int scoreSort = (int)(other.Points - Points);
                if (scoreSort != 0) return scoreSort;

                //then by word size
                int lengthSort = Word.Length - other.Word.Length;
                if (lengthSort != 0) return lengthSort;

                //finally alphabetically
                return Word.CompareTo(other.Word);
            }
        }
        */
    }
}
