#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Text;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public class SpellingDictionary
{
    public readonly CharEncoding.Language language;

    //data about word lengths
    public readonly double average_word_length;
    public readonly double word_length_stdev;
    public readonly byte word_stdev_min;
    public readonly byte word_stdev_max;
    public readonly byte word_2stdev_min;
    public readonly byte word_2stdev_max;

    //count of words
    public readonly uint WORD_COUNT;

    //max length of a word in the dictionary
    public readonly byte MAX_WORD_LENGTH;
    public readonly byte MIN_WORD_LENGTH;

    //total letters in dictionary
    public readonly ulong TOTAL_LETTERS;

    //count of word lengths
    public readonly uint[] WORD_LENGTH_COUNT;

    //counts of letters in the dictionary
    public readonly uint[] LETTER_COUNT;

    //max times a letter has appeared in a word
    public readonly byte[] MAX_LETTER_COUNT;

    //percnt usage for letters
    public readonly double[] OCCURANCE_RATE;

    //point system scaled by occurance rate
    public readonly byte[] OCCURANCE_RATE_POINT_MAP;

    private readonly List<TrieNode> dictionary_list = [];
    private readonly TrieNode[] dictionary;

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

    //check to see if indexed letter is the end of a word
    private void TryWord(string word, int index)
    { if (dictionary[index].endWord) found_words.Add(word); }

    private void Search(string pool, string root, int index)
    {
        //check the current index for a valid word
        TryWord(root, index);
        
        //final outcome of pools that start greater than 2
        if (pool.Length == 2)
        {
            //permute the remaining letters
            int index1 = dictionary[index].children[language.indexer(pool[0])];
            int index2 = dictionary[index].children[language.indexer(pool[1])];
            int index3 = dictionary[index1].children[language.indexer(pool[1])];
            int index4 = dictionary[index2].children[language.indexer(pool[0])];

            //first trie node should never be the end of a word
            TryWord(root + pool[0], index1);
            TryWord(root + pool[1], index2);
            
            //make sure first letter was valid before checking second
            if (index1 > 0) TryWord(root + pool, index3);
            if (index2 > 0) TryWord(root + pool.Reverse(), index4);
        }
        else
        {
            //go from second letter too the second from the last
            for (byte x = 1; x < pool.Length - 2; x++)
            {
                //get index of the letter
                int nextIndex = dictionary[index].children[language.indexer(pool[x])];
                
                //check to make sure it's a valid path
                if (nextIndex > 0)
                {
                    //remove the letter from the pool and call the next search
                    string right = pool[(x + 1)..];
                    string left = pool[..(x - 1)];
                    Search(left + right, root + pool[x], nextIndex);
                }
                
                //makes sure repeated letters aren't checked twice
                while (x < pool.Length - 2 && pool[x] == pool[x + 1]) x++;
            }
        }
    }

    //rng for drawing letters
    //------------------------------------------------
    private readonly Random RNG;
    public string draw = "";
    public byte[] drawLetterCount;
    private bool keep_drawing(ref byte pool) { return pool > draw.Length; }
    public string Draw(byte pool)
    {
        byte drawCount;
        byte min = word_2stdev_min;
        byte max = word_2stdev_max;
        while (WORD_LENGTH_COUNT[min] == 0 && min < max) min++;
        while (WORD_LENGTH_COUNT[max] == 0 && max > min) max--;
        if (WORD_LENGTH_COUNT[min] == 0 || WORD_LENGTH_COUNT[max] == 0)
            throw new ArgumentException("words missing from dictionary");
        drawLetterCount = new byte[language.letterCount];
        
        do
        {
            byte word_length;
            //find a random word length within 2 standerd deviations
            do word_length = (byte)RNG.Next(min,max);
            while (WORD_LENGTH_COUNT[word_length] == 0);
            
            //find a word and count the letters
            byte[] wordLetterCount = new byte[language.letterCount];
            draw = "";
            DrawRecurse(ref word_length, word_length, 0, "");
            foreach(char letter in draw)
                wordLetterCount[language.indexer(letter)]++;
            
            //add letters to the draw that are not already in it
            drawCount = 0;
            for(int x = 0; x < language.letterCount; x++)
            {
                drawLetterCount[x] = wordLetterCount[x] < drawLetterCount[x] ?
                    drawLetterCount[x] :
                    wordLetterCount[x];
                drawCount += drawLetterCount[x];
            }
        //check to see if the pool is filled
        } while (drawCount < pool);
        
        //remove excess letters by selecting the largest
        while (drawCount > pool)
        {
            byte maxIndex = 0;
            for (byte x = 1; x < language.letterCount; x++)
                if (drawLetterCount[x] > drawLetterCount[maxIndex])
                    maxIndex = x;
            drawLetterCount[maxIndex]--;
            drawCount--;
        }

        //encode the pool as a char array
        char[] newPool = new char[pool];
        byte poolCursor = 0;
        for (byte x = 0; x < language.letterCount; x++)
            if (drawLetterCount[x] > 0)
                for (byte y = 0; y < drawLetterCount[x]; y++)
                    newPool[poolCursor++] = language.deindexer(x);
        
        //randomize to match output from bag draws
        RNG.Shuffle(newPool);

        //form and return the string
        draw = new string(newPool);
        return draw;
    }

    //Traverse Tire to find a word
    private void DrawRecurse(ref byte pool, byte distance_remaining, int index, string root)
    {
        if (distance_remaining > 0)
        {
            bool[] letters = new bool[language.letterCount];
            
            //unrefrenced counter for letters
            for (byte x = 0; x < language.letterCount; x++)
            {
                //check for base case
                if (keep_drawing(ref pool))
                {
                    byte next;
                    
                    //find and flag a letter from the avalible pool
                    do next = (byte)RNG.Next(language.letterCount);
                    while (letters[next]);
                    letters[next] = true;
                    
                    //go to next letter if it's valid
                    if (dictionary[index].children[next] > 0)
                        DrawRecurse(ref pool,
                            (byte)(distance_remaining - 1),
                            dictionary[index].children[next],
                            root + language.deindexer(next));
                }
                else return;
            }
        }
        //check to see if indexed at the end of a word
        else if (dictionary[index].endWord) draw += root;
    }

    //------------------------------------------------
    //validation
    
    //returns all words from a player entry that can be found in the substrings of the entry
    public List<string> InDictionary(string s)
    {
        List<string> list = [];
        int index = 0;
        
        //navigates the trie for the player entry
        for (byte letter = 0; letter < s.Length; letter++)
        {
            index = dictionary[index].children[language.indexer(s[letter])];
            
            //at no point can the trie path return to the origin
            if (index == 0)
            {
                list.Clear();
                return list;
            }
            
            //add words as the trie is navigated
            if (dictionary[index].endWord) list.Add(s[..(letter+1)]);
        }
        
        //make sure the entered word is valid
        if (!dictionary[index].endWord)
        {
            list.Clear();
            return list;
        }
        
        //iterate through substrings of the entered word
        for (byte startLetter = 1; startLetter < s.Length; startLetter++)
            //find words in substrings
            foreach (string word in FindChildren(s[startLetter..]))
                list.Add(word);
        
        return list;
    }

    //finds words to score that are substrings of the entered word
    private List<string> FindChildren(string s)
    {
        List<string> words = [];
        int index = 0;
        for (byte letter = 0; letter < s.Length; letter++)
        {
            index = dictionary[index].children[language.indexer(s[letter])];
            if (index == 0) return words;
            if (dictionary[index].endWord) words.Add(s[..letter]);
        }
        return words;
    }

    //dummy constructor
#pragma warning disable CS8618 //*********************************************************************
    public SpellingDictionary()
    {
    }
#pragma warning restore CS8618 //*********************************************************************

    // 190k list
    // https://github.com/wordnik/wordlist/tree/main

    // bad words
    // https://github.com/zacanger/profane-words

    // spanish
    // https://github.com/words/an-array-of-spanish-words
    // https://github.com/keepassxreboot/keepassxc/files/12651434/Diccionario.Espanol.136k.palabras.txt
    // https://launchpad.net/ubuntu/+source/wspanish/1.0.26

    // with quation marks removed
    // one word per line
    // lower case

    //constructor for text file conversion
    public SpellingDictionary(List<string> words, CharEncoding.Language language)
    {
        this.language = language;
        dictionary = new TrieNode[1];
        drawLetterCount = new byte[1];
        WORD_COUNT = (uint)words.Count;
        dictionary_list.Add(new TrieNode(language.letterCount));
        RNG = new Random();

        //metadata about the loaded words
        LETTER_COUNT = new uint[language.letterCount];
        MAX_LETTER_COUNT = new byte[language.letterCount];
        WORD_LENGTH_COUNT = new uint[byte.MaxValue];
        TOTAL_LETTERS = 0;

        //read stream
        foreach(string word in words)
        {
            byte[] wordLetterCount = new byte[language.letterCount];
            int dictionaryIndex = 0;

            //metadata count
            WORD_LENGTH_COUNT[word.Length]++;
            foreach (char letter in word)
            {
                int letterIndex = language.indexer(letter);

                //dictionary node navigation and addition
                if (dictionary_list[dictionaryIndex].children[letterIndex] == 0)
                {
                    TrieNode t = new(language.letterCount);
                    dictionary_list.Add(t);
                    dictionary_list[dictionaryIndex].children[letterIndex] = dictionary_list.Count - 1;
                    dictionaryIndex = dictionary_list[dictionaryIndex].children[letterIndex];
                }
                else dictionaryIndex = dictionary_list[dictionaryIndex].children[letterIndex];

                //metadata count
                TOTAL_LETTERS++;
                LETTER_COUNT[letterIndex]++;
                wordLetterCount[letterIndex]++;
            }
            dictionary_list[dictionaryIndex].endWord = true;

            //check for max letter occurances
            for (byte x = 0; x < language.letterCount; x++)
                if (wordLetterCount[x] > MAX_LETTER_COUNT[x])
                    MAX_LETTER_COUNT[x] = wordLetterCount[x];
        }

        //set max word length
        for (MAX_WORD_LENGTH = byte.MaxValue - 1;
            WORD_LENGTH_COUNT[MAX_WORD_LENGTH] == 0;
            MAX_WORD_LENGTH--) ;

        //set min word length
        for (MIN_WORD_LENGTH = 1;
            WORD_LENGTH_COUNT[MIN_WORD_LENGTH] == 0;
            MIN_WORD_LENGTH++) ;

        //setup for occurance rate calculations
        OCCURANCE_RATE = new double[language.letterCount];
        OCCURANCE_RATE_POINT_MAP = new byte[language.letterCount];
        double dmin = Double.MaxValue;
        double dmax = Double.MinValue;
        byte tile_reduction;
        byte points_offset;

        //percent occurance rate for each letter
        for (byte x = 0; x < language.letterCount; x++)
        {
            OCCURANCE_RATE[x] = (LETTER_COUNT[x] / (double)TOTAL_LETTERS) * 100;
            if (OCCURANCE_RATE[x] < dmin) dmin = OCCURANCE_RATE[x];
            if (OCCURANCE_RATE[x] > dmax) dmax = OCCURANCE_RATE[x];
        }

        //point scaling
        tile_reduction = (byte)(Occurance_Scaler(dmin, dmin, dmax) - 1);
        points_offset = (byte)(Occurance_Scaler(dmax, dmin, dmax) - tile_reduction + 1);
        for (byte x = 0; x < language.letterCount; x++)
            OCCURANCE_RATE_POINT_MAP[x] = (byte)(points_offset -
                (Occurance_Scaler(OCCURANCE_RATE[x], dmin, dmax) - tile_reduction));

        //Word Length Standard Deviation
        ulong total_word_lengths = 0;
        for (byte x = MIN_WORD_LENGTH; x <= MAX_WORD_LENGTH; x++)
            total_word_lengths += WORD_LENGTH_COUNT[x] * x;
        average_word_length = (double)total_word_lengths / (double)WORD_COUNT;
        word_length_stdev = 0.0;
        for (byte x = MIN_WORD_LENGTH; x <= MAX_WORD_LENGTH; x++)
            word_length_stdev += WORD_LENGTH_COUNT[x] * Math.Pow(x - average_word_length, 2);
        word_length_stdev = Math.Sqrt(word_length_stdev / (WORD_COUNT - 1));
        word_stdev_min = (byte)Math.Round(average_word_length - word_length_stdev);
        word_stdev_max = (byte)Math.Round(average_word_length + word_length_stdev);
        word_2stdev_min = (byte)Math.Round(average_word_length - (word_length_stdev * 2));
        word_2stdev_max = (byte)Math.Round(average_word_length + (word_length_stdev * 2));
    }

    //logarithmic point scaler to keep letter scores in an acceptable range
    private static byte Occurance_Scaler(double x, double min, double max)
    {
        if (x < 0.01) return 0;
        return Convert.ToByte((min + max) / 3 * Math.Log(x * 100, (min + 1) * (min + max) / 2));
    }

    //data structure for dictionary
    private class TrieNode(byte number_of_letters)
    {
        public int[] children = new int[number_of_letters];
        public bool endWord = false;
    }

    //constructor for regular game use
    public SpellingDictionary(BinaryReader reader)
    {
        string fileLanguage = reader.ReadString();

        if (fileLanguage.Equals("en")) language = CharEncoding.Languages.EN;
        else if (fileLanguage.Equals("es")) language = CharEncoding.Languages.ES;
        else throw new NotImplementedException(fileLanguage);

        word_stdev_min = reader.ReadByte();
        word_stdev_max = reader.ReadByte();
        word_2stdev_min = reader.ReadByte();
        word_2stdev_max = reader.ReadByte();
        MAX_WORD_LENGTH = reader.ReadByte();
        MIN_WORD_LENGTH = reader.ReadByte();

        WORD_COUNT = reader.ReadUInt32();

        TOTAL_LETTERS = reader.ReadUInt64();

        average_word_length = reader.ReadDouble();
        word_length_stdev = reader.ReadDouble();

        WORD_LENGTH_COUNT = new uint[MAX_WORD_LENGTH+1];
        for (int x = 0; x < WORD_LENGTH_COUNT.Length; x++)
            WORD_LENGTH_COUNT[x] = reader.ReadUInt32();
        MAX_LETTER_COUNT = new byte[language.letterCount];
        for (int letter = 0; letter < language.letterCount; letter++)
            MAX_LETTER_COUNT[letter] = reader.ReadByte();
        OCCURANCE_RATE_POINT_MAP = new byte[language.letterCount];
        for (int letter = 0; letter < language.letterCount; letter++)
            OCCURANCE_RATE_POINT_MAP[letter] = reader.ReadByte();
        LETTER_COUNT = new uint[language.letterCount];
        for (int letter = 0; letter < language.letterCount; letter++)
            LETTER_COUNT[letter] = reader.ReadUInt32();
        OCCURANCE_RATE = new double[language.letterCount];
        for (int letter = 0; letter < language.letterCount; letter++)
            OCCURANCE_RATE[letter] = reader.ReadDouble();

        dictionary = new TrieNode[reader.ReadInt32()];
        for (int x = 0; x < dictionary.Length; x++)
        {
            dictionary[x] = new TrieNode(language.letterCount);
            for (int letter = 0; letter < language.letterCount; letter++)
                dictionary[x].children[letter] = reader.ReadInt32();
            dictionary[x].endWord = reader.ReadBoolean();
        }

        RNG = new();
        drawLetterCount = new byte[language.letterCount];
    }

    public void Write(BinaryWriter writer)
    {
        if (language == CharEncoding.Languages.EN) writer.Write("en");
        else if (language == CharEncoding.Languages.ES) writer.Write("es");
        else throw new NotImplementedException("language not recognized");

        //byte
        writer.Write(word_stdev_min);
        writer.Write(word_stdev_max);
        writer.Write(word_2stdev_min);
        writer.Write(word_2stdev_max);
        writer.Write(MAX_WORD_LENGTH);
        writer.Write(MIN_WORD_LENGTH);
        
        //uint
        writer.Write(WORD_COUNT);
        
        //ulong
        writer.Write(TOTAL_LETTERS);
        
        //double
        writer.Write(average_word_length);
        writer.Write(word_length_stdev);
        
        for (int x = 0; x <= MAX_WORD_LENGTH; x++)
            writer.Write(WORD_LENGTH_COUNT[x]);
        foreach (byte letter in MAX_LETTER_COUNT)
            writer.Write(letter);
        foreach (byte letter in OCCURANCE_RATE_POINT_MAP)
            writer.Write(letter);
        foreach (uint letter in LETTER_COUNT)
            writer.Write(letter);
        foreach (double letter in OCCURANCE_RATE)
            writer.Write(letter);
        
        writer.Write(dictionary_list.Count);
        foreach (TrieNode node in dictionary_list)
        {
            foreach (int child in node.children)
                writer.Write(child);
            writer.Write(node.endWord);
        }
    }
}
