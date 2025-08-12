//ilist compatible object to store the words discovered

using System.Collections;

namespace KeyboardGameV2.src
{
    public class Scoreboard : IList
    {
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
    }
}
