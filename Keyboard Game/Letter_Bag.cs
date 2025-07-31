using System;
using System.Collections.Generic;
using System.Text;

namespace letter_tile
{
    public class Letter_Bag
    {

        private class Letter_Tile
        {
            public readonly char letter;
            public bool in_bag;
            
            public Letter_Tile(char c)
            {
                letter = c;
                in_bag = true;
            }

        }

        private Letter_Tile[] bag;
        private Random rng;
        private readonly byte[] points_map;
        private byte[] draw_count;
        private readonly ushort TILE_COUNT;
        private ushort tiles_remaining;
        private UnicodeEncoding unicode;

        //constructor
        public Letter_Bag(byte[] letters, char first_letter)
        {
            //metadata for counting
            TILE_COUNT = 0;
            ushort tile_cursor = 0;
            byte max = byte.MinValue;
            char letter_cursor = first_letter;
            
            //establish the other class memebers
            rng = new Random();
            points_map = new byte[letters.Length];
            draw_count = new byte[points_map.Length];
            unicode = new UnicodeEncoding();

            
            //count the number of tiles that need to be created
            for (byte x = 0; x < letters.Length; x++)
            {
                TILE_COUNT += letters[x];
                
                //find the highest number count to establish 1 point letters
                if (letters[x] > max) max = letters[x];
            }
            //creates a point scale by making the most comman letters worth 1 point
            //max(+1) letter count - each individual letter's count = a letter's score
            max++;
            
            //create the new bag
            bag = new Letter_Tile[TILE_COUNT];
            tiles_remaining = TILE_COUNT;
            for (byte x = 0; x < letters.Length; x++)
            {
                //store point value
                points_map[x] = (byte)(max - letters[x]);
                
                //create letter tiles
                for (byte y = 0; y < letters[x]; y++)
                    bag[tile_cursor++] = new Letter_Tile(letter_cursor);
                
                letter_cursor++;
            }
        }

        public ushort Score_Word(string w)
        {
            byte[] word_letter_count = new byte[draw_count.Length];
            int score = 0;

            //count letters in the word and compile score
            foreach (char letter in w)
            {
                byte offset_letter = (byte)(letter - bag[0].letter);
                word_letter_count[offset_letter]++;
                score += points_map[offset_letter];
            }
            score *= w.Length;

            //reject word if not enough letters are in the draw
            for (byte x = 0; x < draw_count.Length; x++)
                if ((draw_count[x] - word_letter_count[x]) < 0)
                    return 0;

            //return score if enough letters found in the draw
            return (ushort)score;
        }

        public void Reset()
        {
            foreach (Letter_Tile t in bag)
                t.in_bag = true;
            tiles_remaining = TILE_COUNT;
            draw_count = new byte[points_map.Length];
        }

        //returns a formatted string for display and loads validation metadata
        public String Draw(ushort number_of_letters)
        {
            //reject request if not enough tiles
            if (number_of_letters > tiles_remaining) return null;
            
            char[] letters = new char[number_of_letters];
            int random_pull;
            draw_count = new byte[points_map.Length];
            List<byte> letters_with_score = new List<byte>();

            for (byte x = 0; x < number_of_letters; x++)
            {
                //draw random tiles until one that has not been pulled is drawn
                do random_pull = rng.Next(bag.Length);
                while (!bag[random_pull].in_bag);

                //flag the tile as drawn
                bag[random_pull].in_bag = false;

                //assign it to output
                letters[x] = bag[random_pull].letter;

                //increase count of letters used to determine a valid score
                draw_count[letters[x]-bag[0].letter]++;

                //decrement the count so the bag can never be overdrawn
                tiles_remaining--;
            }

            //alphabetize the list
            Array.Sort(letters);

            
            //load unicode formatted return string
            foreach(char l in letters)
            {
                
                //letter
                letters_with_score.Add((byte)l);
                letters_with_score.Add(0x00);
                
                //substring for score
                foreach (char digit in points_map[l - bag[0].letter].ToString())
                {
                    letters_with_score.Add((byte)(digit + 0x50));
                    letters_with_score.Add(0x20);
                }
                
                //space
                letters_with_score.Add(0x20);
                letters_with_score.Add(0x00);
            }

            //remove last space
            letters_with_score.RemoveAt(letters_with_score.LastIndexOf(0x20));
            letters_with_score.RemoveAt(letters_with_score.LastIndexOf(0x00));


            return unicode.GetString(letters_with_score.ToArray());
        }
    }
}
