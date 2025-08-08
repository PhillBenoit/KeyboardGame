/*
using System;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using EncodingEnums;
*/
using System.Text;

namespace LanguageDictionary
{
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

            /*
            
            unused formula based on occrance rate of letters in the dictionary
            
            double[] occurance_rate = new double[ENGLISH_LETTERS];
            double dmin = Double.MaxValue;
            double dmax = Double.MinValue;
            ushort[] tile_count = new ushort[ENGLISH_LETTERS];
            ushort tile_reduction;
            ushort points_offset;
            ushort total_tiles = 0;

            for (ushort x = 0; x < ENGLISH_LETTERS; x++) {
                occurance_rate[x] = (letter_count[x] / (double)total_letters) * 100;
                if (occurance_rate[x] < dmin) dmin = occurance_rate[x];
                if (occurance_rate[x] > dmax) dmax = occurance_rate[x];
            }
            for (ushort x = 0; x < ENGLISH_LETTERS; x++)
                occurance_rate[x] = Occurance_Scaler(occurance_rate[x], dmin, dmax);
            tile_reduction = (ushort)(Occurance_Scaler(dmin, dmin, dmax) - 1);
            points_offset = (ushort)(Occurance_Scaler(dmax, dmin, dmax) - tile_reduction + 1);
            for (ushort x = 0; x < ENGLISH_LETTERS; x++) {
                tile_count[x] = (ushort)(Convert.ToInt16(occurance_rate[x]) - tile_reduction);
                total_tiles += tile_count[x];
            }
            for (ushort x = 0; x < ENGLISH_LETTERS; x++)
                Console.WriteLine(Convert.ToChar((x + 'a')) + " " + (
                    //4*Math.Log((letter_count[x] / (double)character_counter) * 10000, 7.1)
                    tile_count[x]
                    ).ToString());
            */
        }

        /*
          
         unused formula for scaling letter occurances

        private static ushort Occurance_Scaler(double x, double min, double max)
        {
            return Convert.ToUInt16((min + max) / 3 * Math.Log(x * 100, (min + 1) * (min + max) / 2));
        }
        

        private static int GetWordHash(string s)
        {
            return BitConverter.ToUInt32(Crc32.Hash(
                System.Text.Encoding.ASCII.GetBytes(s.ToCharArray())), 0);
        }
        */
    }
}
