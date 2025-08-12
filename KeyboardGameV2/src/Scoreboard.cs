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
            /*
            if (value is null) return -1;
            ScoreEntry entry = (ScoreEntry)value;
            for (int x = 0; x < _data.Count; x++)
                if (_data[x].Word.Equals(entry.Word))
                    return x;
            return -1;
            */
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

            //sorted first by score then by word size
            public int CompareTo(object? obj)
            {
                ArgumentNullException.ThrowIfNull(obj);
                ScoreEntry other = (ScoreEntry)obj;
                if (this.Points == other.Points) return other.Word.Length - this.Word.Length;
                else return (int)(this.Points - other.Points);
            }
        }
    }
}
