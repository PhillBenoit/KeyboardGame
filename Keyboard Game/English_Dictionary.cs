using System;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Text;

namespace language_dictionary
{
    static public class English_Dictionary
    {
        public const byte ENGLISH_LETTERS = 26;
        public const byte MAX_POSSIBLE_WORD_LENGTH = byte.MaxValue;


        //count of words
        static private uint word_count = 0;
        static public uint Word_Count() { return word_count; }

        //max length of a word in the dictionary
        static private byte MAX_WORD_LENGTH = 0;
        static public byte Max_Word_Length() { return MAX_WORD_LENGTH; }

        //count of word lengths
        private static uint[] word_length_count;
        public static uint[] Word_Length_Count()
        {
            uint[] x = new uint[MAX_POSSIBLE_WORD_LENGTH];
            Array.Copy(word_length_count, x, MAX_POSSIBLE_WORD_LENGTH);
            return x;
        }

        //total letters in dictionary
        private static ulong total_letters = 0;
        public static ulong Total_Letters() { return total_letters; }

        //counts of letters in the dictionary
        static private uint[] letter_count;
        static public uint[] Letter_Count()
        {
            uint[] x = new uint[ENGLISH_LETTERS];
            Array.Copy(letter_count, x, ENGLISH_LETTERS);
            return x;
        }

        //max times a letter has appeared in a word
        static private byte[] max_letter_count;
        static public byte[] Max_Letter_Count()
        {
            byte[] x = new byte[ENGLISH_LETTERS];
            Array.Copy(max_letter_count, x, ENGLISH_LETTERS);
            return x;
        }


        //searchable and sortable object containing words and hash sums
        private class Dictionary_Entry : IComparable<Dictionary_Entry>
        {
            public readonly ulong hash;
            public readonly string word;

            public Dictionary_Entry(ulong hash, string word)
            {
                this.hash = hash;
                this.word = word;
            }

            int IComparable<Dictionary_Entry>.CompareTo(Dictionary_Entry other)
            { return (int)(this.hash - other.hash); }
        }
        static private Dictionary_Entry[] dictionary;
        
        //validation
        static public bool In_Dictionary(string s)
        {
            //find the hash
            int index = Array.BinarySearch(
                dictionary,
                new Dictionary_Entry(Get_Word_Hash(s), s)
                );
            
            //make sure the hash and the word are the same
            return index >= 0 && dictionary[index].word.Equals(s);
        }



        // https://github.com/wordnik/wordlist/tree/main
        // with quation marks removed
        // one word per line
        // lower case
        public static void Load_From_Txt(Stream s, string path)
        {
            //dictionary sized based on nummber of words
            word_count = (uint)File.ReadLines(path).Count();
            dictionary = new Dictionary_Entry[word_count];
            
            //metadata about the loaded words
            letter_count = new uint[ENGLISH_LETTERS];
            max_letter_count = new byte[ENGLISH_LETTERS];
            word_length_count = new uint[MAX_POSSIBLE_WORD_LENGTH];
            total_letters = 0;

            //setup for stream reading
            ulong dictionary_cursor = 0;
            StreamReader sr = new StreamReader(s);
            string word_from_file = sr.ReadLine();
            
            //read stream
            while (word_from_file != null)
            {
                byte[] word_letter_count = new byte[ENGLISH_LETTERS];
                
                //metadata count
                word_length_count[word_from_file.Length]++;
                foreach (char letter in word_from_file)
                {
                    total_letters++;
                    letter_count[(letter - 'a')]++;
                    word_letter_count[(letter - 'a')]++;
                }

                //check for max letter occurances
                for (byte x = 0; x < ENGLISH_LETTERS; x++)
                    if (word_letter_count[x] > max_letter_count[x])
                        max_letter_count[x] = word_letter_count[x];

                //store hash
                dictionary[dictionary_cursor++] =
                    new Dictionary_Entry(
                        Get_Word_Hash(word_from_file),
                        word_from_file);
                
                //continue reading the stream
                word_from_file = sr.ReadLine();
            }

            //sort dictionary
            Array.Sort(dictionary);


            //set max word length
            for (MAX_WORD_LENGTH = MAX_POSSIBLE_WORD_LENGTH - 1;
                word_length_count[MAX_WORD_LENGTH] == 0;
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
        */

        private static ulong Get_Word_Hash(string s)
        {
            return BitConverter.ToUInt64(
                Crc64.Hash(
                    Encoding.ASCII.GetBytes(
                        s.ToCharArray()
                        )), 0);
        }

    }
}
