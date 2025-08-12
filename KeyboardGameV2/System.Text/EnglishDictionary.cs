#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Text;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public class EnglishDictionary
{
    public const byte ENGLISH_LETTERS = 26;
    public const byte MAX_POSSIBLE_WORD_LENGTH = byte.MaxValue;

    //count of words
    public readonly uint WORD_COUNT;

    //max length of a word in the dictionary
    public readonly byte MAX_WORD_LENGTH ;

    //count of word lengths
    public readonly uint[] WORD_LENGTH_COUNT;

    //total letters in dictionary
    public readonly ulong TOTAL_LETTERS;

    //counts of letters in the dictionary
    public readonly uint[] LETTER_COUNT;

    //max times a letter has appeared in a word
    public readonly byte[] MAX_LETTER_COUNT;

    //searchable and sortable object containing words and hash sums
    private class DictionaryEntry(int hash, string word) : IComparable<DictionaryEntry>
    {
        public readonly int HASH = hash;
        public readonly string WORD = word;

        int IComparable<DictionaryEntry>.CompareTo(DictionaryEntry? other)
        {
            if (other == null) return 1;
            else return this.HASH - other.HASH;
        }
    }
    private readonly DictionaryEntry[] _dictionary;
    
    
    //validation
    public bool InDictionary(string s)
    {
        //find the hash
        int index = Array.BinarySearch(
            _dictionary,
            new DictionaryEntry(s.GetHashCode(), s)
            );
        
        //make sure the hash and the word are the same
        return index >= 0 && _dictionary[index].WORD.Equals(s);
    }

    public EnglishDictionary()
    {
        WORD_LENGTH_COUNT = new uint[1];
        LETTER_COUNT = new uint[1];
        MAX_LETTER_COUNT = new byte[1];
        _dictionary = new DictionaryEntry[1];
    }

    // https://github.com/wordnik/wordlist/tree/main
    // with quation marks removed
    // one word per line
    // lower case
    public EnglishDictionary(Stream s, string path)
    {
        //dictionary sized based on nummber of words
        WORD_COUNT = (uint)File.ReadLines(path).Count();
        _dictionary = new DictionaryEntry[WORD_COUNT];
        
        //metadata about the loaded words
        LETTER_COUNT = new uint[ENGLISH_LETTERS];
        MAX_LETTER_COUNT = new byte[ENGLISH_LETTERS];
        WORD_LENGTH_COUNT = new uint[MAX_POSSIBLE_WORD_LENGTH];
        TOTAL_LETTERS = 0;

        //setup for stream reading
        ulong dictionaryCursor = 0;
        StreamReader sr = new(s);
        string? wordFromFile = sr.ReadLine();
        
        //read stream
        while (wordFromFile != null)
        {
            byte[] wordLetterCount = new byte[ENGLISH_LETTERS];
            
            //metadata count
            WORD_LENGTH_COUNT[wordFromFile.Length]++;
            foreach (char letter in wordFromFile)
            {
                TOTAL_LETTERS++;
                LETTER_COUNT[(letter - (byte)CharEncoding.ASCII.LETTER_a)]++;
                wordLetterCount[(letter - (byte)CharEncoding.ASCII.LETTER_a)]++;
            }

            //check for max letter occurances
            for (byte x = 0; x < ENGLISH_LETTERS; x++)
                if (wordLetterCount[x] > MAX_LETTER_COUNT[x])
                    MAX_LETTER_COUNT[x] = wordLetterCount[x];
            
            //store hash
            _dictionary[dictionaryCursor++] =
                new DictionaryEntry(
                    wordFromFile.GetHashCode(),
                    wordFromFile);
            
            //continue reading the stream
            wordFromFile = sr.ReadLine();
        }

        //sort dictionary
        Array.Sort(_dictionary);

        //set max word length
        for (MAX_WORD_LENGTH = MAX_POSSIBLE_WORD_LENGTH - 1;
            WORD_LENGTH_COUNT[MAX_WORD_LENGTH] == 0;
            MAX_WORD_LENGTH--) ;
    }
}
