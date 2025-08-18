#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Text;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public class EnglishDictionary
{
    public const byte ENGLISH_LETTERS =
        CharEncoding.ASCII.LETTER_z - CharEncoding.ASCII.LETTER_a + 1;

    //count of words
    public readonly uint WORD_COUNT;

    //max length of a word in the dictionary
    public readonly byte MAX_WORD_LENGTH;
    public readonly byte MIN_WORD_LENGTH;

    //count of word lengths
    public readonly uint[] WORD_LENGTH_COUNT;

    //total letters in dictionary
    public readonly ulong TOTAL_LETTERS;

    //counts of letters in the dictionary
    public readonly uint[] LETTER_COUNT;

    //max times a letter has appeared in a word
    public readonly byte[] MAX_LETTER_COUNT;

    //percnt usage for letters
    public readonly double[] OCCURANCE_RATE;

    //point system scaled by occurance rate
    public readonly byte[] OCCURANCE_RATE_POINT_MAP;

    private readonly List<TrieNode> dictionary = [];

    //bute force search of sorted string of letters
    //------------------------------------------------

    //loop independant storage for found words
    public List<string> found_words = [];

    public void StartSearch(string letters)
    {
        found_words.Clear();
        Search(letters, "", 0);
        found_words.Sort();
    }

    private void TryWord(string word, int index)
    { if (dictionary[index].endWord) found_words.Add(word); }

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
            if (index1 > 0) TryWord(root + pool, index3);
            if (index2 > 0) TryWord(root + pool.Reverse(), index4);
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
    //------------------------------------------------
    //rng for drawing letters
    private readonly Random RNG;
    private string draw = "";
    public byte[] drawLetterCount;
    private bool keep_drawing(ref byte pool) { return pool > draw.Length; }
    public string Draw(byte pool)
    {
        byte drawCount = 0;
        drawLetterCount = new byte[ENGLISH_LETTERS];
        found_words.Clear();

        do
        {
            //byte word_length = (byte)RNG.Next(1,(pool+2)-drawCount );
            byte word_length;
            do word_length = (byte)RNG.Next(
                word_stdev_min,
                Math.Min(pool, word_stdev_max));
            while (WORD_LENGTH_COUNT[word_length] == 0);
            byte[] wordLetterCount = new byte[ENGLISH_LETTERS];
            draw = "";
            DrawRecurse(ref word_length, word_length, 0, "");
            foreach(char letter in draw)
                wordLetterCount[(int)(letter - CharEncoding.ASCII.LETTER_a)]++;
            drawCount = 0;
            for(int x = 0; x < ENGLISH_LETTERS; x++)
            {
                drawLetterCount[x] = wordLetterCount[x] < drawLetterCount[x] ?
                    drawLetterCount[x] :
                    wordLetterCount[x];
                drawCount += drawLetterCount[x];
            }
        } while (drawCount < pool);
        
        while (drawCount > pool)
        {
            byte maxIndex = 0;
            for (byte x = 1; x < ENGLISH_LETTERS; x++)
                if (drawLetterCount[x] > drawLetterCount[maxIndex])
                    maxIndex = x;
            drawLetterCount[maxIndex]--;
            drawCount--;
        }

        char[] newPool = new char[pool];
        byte poolCursor = 0;
        for (byte x = 0; x < ENGLISH_LETTERS; x++)
            if (drawLetterCount[x] > 0)
                for (byte y = 0; y < drawLetterCount[x]; y++)
                    newPool[poolCursor++] = (char)(x + CharEncoding.ASCII.LETTER_a);
        RNG.Shuffle(newPool);

        draw = new string(newPool);
        return draw;
    }

    private void DrawRecurse(ref byte pool, byte distance_remaining, int index, string root)
    {
        if (distance_remaining > 0)
        {
            bool[] letters = new bool[ENGLISH_LETTERS];
            for (byte x = 0; x < ENGLISH_LETTERS; x++)
            {
                if (keep_drawing(ref pool))
                {
                    byte next;
                    do next = (byte)RNG.Next(ENGLISH_LETTERS);
                    while (letters[next]);
                    letters[next] = true;
                    if (dictionary[index].children[next] > 0)
                        DrawRecurse(ref pool,
                            (byte)(distance_remaining - 1),
                            dictionary[index].children[next],
                            root + (char)(CharEncoding.ASCII.LETTER_a + next));
                }
                else return;
            }
        }
        else if (dictionary[index].endWord) draw += root;
    }

    //------------------------------------------------
    //validation
    public bool InDictionary(string s)
    {
        int index = 0;
        foreach (char c in s)
        {
            index = dictionary[index].children[c - (byte)CharEncoding.ASCII.LETTER_a];
            if (index == 0) return false;
        }
        return dictionary[index].endWord;
    }

    //dummy constructor
    public EnglishDictionary()
    {
        WORD_LENGTH_COUNT = new uint[1];
        LETTER_COUNT = new uint[1];
        MAX_LETTER_COUNT = new byte[1];
        OCCURANCE_RATE = new double[1];
        OCCURANCE_RATE_POINT_MAP = new byte[1];
        RNG = new Random();
        drawLetterCount = new byte[1];
    }

    // https://github.com/wordnik/wordlist/tree/main
    // with quation marks removed
    // one word per line
    // lower case
    public EnglishDictionary(Stream s, string path)
    {
        drawLetterCount = new byte[1];
        WORD_COUNT = (uint)File.ReadLines(path).Count();
        dictionary.Add(new TrieNode());
        RNG = new Random();

        //metadata about the loaded words
        LETTER_COUNT = new uint[ENGLISH_LETTERS];
        MAX_LETTER_COUNT = new byte[ENGLISH_LETTERS];
        WORD_LENGTH_COUNT = new uint[byte.MaxValue];
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
                    TrieNode t = new();
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
        for (MAX_WORD_LENGTH = byte.MaxValue - 1;
            WORD_LENGTH_COUNT[MAX_WORD_LENGTH] == 0;
            MAX_WORD_LENGTH--);

        //set min word length
        for (MIN_WORD_LENGTH = 1;
            WORD_LENGTH_COUNT[MIN_WORD_LENGTH] == 0;
            MIN_WORD_LENGTH++);

        //setup for occurance rate calculations
        OCCURANCE_RATE = new double[ENGLISH_LETTERS];
        OCCURANCE_RATE_POINT_MAP = new byte[ENGLISH_LETTERS];
        double dmin = Double.MaxValue;
        double dmax = Double.MinValue;
        byte tile_reduction;
        byte points_offset;

        //percent occurance rate for each letter
        for (byte x = 0; x < ENGLISH_LETTERS; x++)
        {
            OCCURANCE_RATE[x] = (LETTER_COUNT[x] / (double)TOTAL_LETTERS) * 100;
            if (OCCURANCE_RATE[x] < dmin) dmin = OCCURANCE_RATE[x];
            if (OCCURANCE_RATE[x] > dmax) dmax = OCCURANCE_RATE[x];
        }

        //point scaling
        tile_reduction = (byte)(Occurance_Scaler(dmin, dmin, dmax) - 1);
        points_offset = (byte)(Occurance_Scaler(dmax, dmin, dmax) - tile_reduction + 1);
        for (byte x = 0; x < ENGLISH_LETTERS; x++)
            OCCURANCE_RATE_POINT_MAP[x] = (byte)(points_offset -
                (Occurance_Scaler(OCCURANCE_RATE[x], dmin, dmax) - tile_reduction));

        //Word Length Standard Deviation
        ulong total_word_lengths = 0;
        for (byte x = MIN_WORD_LENGTH; x <= MAX_WORD_LENGTH; x++)
            total_word_lengths += WORD_LENGTH_COUNT[x] * x;
        average_word_length = (double)total_word_lengths / (double)WORD_COUNT;
        word_length_stdev = 0.0;
        for (byte x = MIN_WORD_LENGTH; x <= MAX_WORD_LENGTH; x++)
            word_length_stdev += WORD_LENGTH_COUNT[x] * Math.Pow(x-average_word_length,2);
        word_length_stdev = Math.Sqrt(word_length_stdev/(WORD_COUNT - 1));
        word_stdev_min = (byte)Math.Max(MIN_WORD_LENGTH, (int)Math.Round((word_length_stdev * 2) - average_word_length));
        word_stdev_max = Math.Min(MAX_WORD_LENGTH, (byte)Math.Round((word_length_stdev * 2) + average_word_length));
    }

    public readonly double average_word_length;
    public readonly double word_length_stdev;
    public readonly byte word_stdev_min;
    public readonly byte word_stdev_max;

    private static byte Occurance_Scaler(double x, double min, double max)
    {
        return Convert.ToByte((min + max) / 3 * Math.Log(x * 100, (min + 1) * (min + max) / 2));
    }

    //data structure for dictionary
    private class TrieNode
    {
        public int[] children = new int[ENGLISH_LETTERS];
        public bool endWord = false;
        public TrieNode()
        {
            for (int x = 0; x < children.Length; x++) children[x] = 0;
        }
    }
}
