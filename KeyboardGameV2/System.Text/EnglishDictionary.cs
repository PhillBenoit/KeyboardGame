#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Text;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public class EnglishDictionary
{
    public const byte ENGLISH_LETTERS =
        CharEncoding.ASCII.LETTER_z - CharEncoding.ASCII.LETTER_a + 1;
    public const byte MAX_POSSIBLE_WORD_LENGTH = byte.MaxValue;

    //count of words
    public readonly uint WORD_COUNT;

    //max length of a word in the dictionary
    public readonly byte MAX_WORD_LENGTH;

    //count of word lengths
    public readonly uint[] WORD_LENGTH_COUNT;

    //total letters in dictionary
    public readonly ulong TOTAL_LETTERS;

    //counts of letters in the dictionary
    public readonly uint[] LETTER_COUNT;

    //max times a letter has appeared in a word
    public readonly byte[] MAX_LETTER_COUNT;

    public List<string> found_words = [];
    int calls;

    public void StartSearch(string letters)
    {
        found_words.Clear();
        calls = 0;
        Search(letters, "", 0);
        found_words.Sort();
        System.Diagnostics.Debug.WriteLine("---------------------------------------------");
        System.Diagnostics.Debug.WriteLine("Dictionary count: " + dictionary.Count);
        System.Diagnostics.Debug.WriteLine("Dictionary capacity: " + dictionary.Capacity);
        System.Diagnostics.Debug.WriteLine("found words: " + found_words.Count);
        System.Diagnostics.Debug.WriteLine("total calls: " + calls);
        int length_count = 0;
        foreach (string word in found_words)
            if (word.Length > 5)
                length_count++;
        System.Diagnostics.Debug.WriteLine("words longer than 5: " + length_count);
    }

    private void TryWord(string word, int index)
    { calls++;  if (dictionary[index].endWord) found_words.Add(word); }

    private void Search(string pool, string root, int index)
    {
        TryWord(root, index);
        if (pool.Length == 2)
        {
            int index1 = dictionary[index].children[pool[0] - (int)CharEncoding.ASCII.LETTER_a];
            int index2 = dictionary[index].children[pool[1] - (int)CharEncoding.ASCII.LETTER_a];
            int index3 = dictionary[index1].children[pool[1] - (int)CharEncoding.ASCII.LETTER_a];
            int index4 = dictionary[index2].children[pool[0] - (int)CharEncoding.ASCII.LETTER_a];

            TryWord(root + pool[0], index1);
            TryWord(root + pool[1], index2);
            TryWord(root + pool, index3);
            TryWord(root + pool.Reverse(), index4);
        }
        else
        {
            for (byte x = 1; x < pool.Length - 2; x++)
            {
                int nextIndex = dictionary[index].children[pool[x] - (int)CharEncoding.ASCII.LETTER_a];
                if (nextIndex > 0)
                {
                    string right = pool[(x + 1)..];
                    string left = pool[..(x - 1)];
                    Search(left + right, root + pool[x], nextIndex);
                }
                while (x < pool.Length - 2 && pool[x] == pool[x + 1]) x++;
            }
        }
    }

    //validation
    public bool InDictionary(string s)
    {
        int index = 0;
        foreach(char c in s)
        {
            index = dictionary[index].children[c - (byte)CharEncoding.ASCII.LETTER_a];
            if (index == 0) return false;
        }
        return dictionary[index].endWord;
    }

    public EnglishDictionary()
    {
        WORD_LENGTH_COUNT = new uint[1];
        LETTER_COUNT = new uint[1];
        MAX_LETTER_COUNT = new byte[1];
    }

    // https://github.com/wordnik/wordlist/tree/main
    // with quation marks removed
    // one word per line
    // lower case
    public EnglishDictionary(Stream s, string path)
    {
        WORD_COUNT = (uint)File.ReadLines(path).Count();
        dictionary.Add(new Trie());

        //metadata about the loaded words
        LETTER_COUNT = new uint[ENGLISH_LETTERS];
        MAX_LETTER_COUNT = new byte[ENGLISH_LETTERS];
        WORD_LENGTH_COUNT = new uint[MAX_POSSIBLE_WORD_LENGTH];
        TOTAL_LETTERS = 0;

        //setup for stream reading
        StreamReader sr = new(s);
        string? wordFromFile = sr.ReadLine();
        
        //read stream
        while (wordFromFile != null)
        {
            byte[] wordLetterCount = new byte[ENGLISH_LETTERS];
            int dictionaryIndex = 0;
            
            //metadata count
            WORD_LENGTH_COUNT[wordFromFile.Length]++;
            foreach (char letter in wordFromFile)
            {
                int letterIndex = letter - (byte)CharEncoding.ASCII.LETTER_a;
                
                //dictionary node navigation and addition
                if (dictionary[dictionaryIndex].children[letterIndex] == 0)
                {
                    Trie t = new();
                    dictionary.Add(t);
                    dictionary[dictionaryIndex].children[letterIndex] = dictionary.Count - 1;
                    dictionaryIndex = dictionary[dictionaryIndex].children[letterIndex];
                }
                else dictionaryIndex = dictionary[dictionaryIndex].children[letterIndex];
                
                //metadata count
                TOTAL_LETTERS++;
                LETTER_COUNT[letterIndex]++;
                wordLetterCount[letterIndex]++;
            }
            dictionary[dictionaryIndex].endWord = true;

            //check for max letter occurances
            for (byte x = 0; x < ENGLISH_LETTERS; x++)
                if (wordLetterCount[x] > MAX_LETTER_COUNT[x])
                    MAX_LETTER_COUNT[x] = wordLetterCount[x];
            
            //continue reading the stream
            wordFromFile = sr.ReadLine();
        }
        
        //set max word length
        for (MAX_WORD_LENGTH = MAX_POSSIBLE_WORD_LENGTH - 1;
            WORD_LENGTH_COUNT[MAX_WORD_LENGTH] == 0;
            MAX_WORD_LENGTH--) ;
    }

    private readonly List<Trie> dictionary = [];

    private class Trie
    {
        public int[] children = new int[ENGLISH_LETTERS];
        public bool endWord = false;
        public Trie()
        {
            for (int x = 0; x < children.Length; x++) children[x] = 0;
        }
    }
}
