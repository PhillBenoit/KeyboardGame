//bag of letters selected at random for the letter pool
using System.Text;

namespace KeyboardGameV2.src
{
    public class LetterBag
    {
        private class LetterTile(char c)
        {
            public readonly char LETTER = c;
            public bool inBag = true;
        }

        //full set of letters
        private readonly LetterTile[] BAG;
        
        //rng for selection
        private readonly Random RNG;
        
        //point value for letters
        private readonly byte[] POINTS_MAP;
        
        //total number of letters in the bag
        private readonly ushort TILE_COUNT;
        
        //encoder for unicode used to give pool letters a subscript score
        private readonly UnicodeEncoding UNICODE;
        
        //number of letters left in the bag
        public ushort tilesRemaining;
        
        //holds the count of letters in the ltter pool
        private byte[] _drawCount;

        public string draw_string = "";

        //generic constructor for basic declaration
        public LetterBag()
        {
            BAG = new LetterTile[1];
            RNG = new Random();
            POINTS_MAP = new byte[1];
            UNICODE = new UnicodeEncoding();
            _drawCount = new byte[1];
        }

        //real constructor
        public LetterBag(byte[] letters, char firstLetter)
        {
            //metadata for counting
            TILE_COUNT = 0;
            ushort tileCursor = 0;
            byte max = byte.MinValue;
            char letterCursor = firstLetter;
            
            //establish the other class memebers
            RNG = new Random();
            POINTS_MAP = new byte[letters.Length];
            _drawCount = new byte[POINTS_MAP.Length];
            UNICODE = new UnicodeEncoding();

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
            BAG = new LetterTile[TILE_COUNT];
            tilesRemaining = TILE_COUNT;
            for (byte x = 0; x < letters.Length; x++)
            {
                //store point value
                POINTS_MAP[x] = (byte)(max - letters[x]);
                
                //create letter tiles
                for (byte y = 0; y < letters[x]; y++)
                    BAG[tileCursor++] = new LetterTile(letterCursor);
                
                letterCursor++;
            }
        }

        public ushort ScoreWord(string w)
        {
            byte[] wordLetterCount = new byte[_drawCount.Length];
            int score = 0;

            //count letters in the word and compile score
            foreach (char letter in w)
            {
                byte offsetLetter = (byte)(letter - BAG[0].LETTER);
                wordLetterCount[offsetLetter]++;
                score += POINTS_MAP[offsetLetter];
            }
            score *= w.Length;

            //reject word if not enough letters are in the draw
            for (byte x = 0; x < _drawCount.Length; x++)
                if (_drawCount[x] - wordLetterCount[x] < 0)
                    return 0;

            //return score if enough letters found in the draw
            return (ushort)score;
        }

        public void Reset()
        {
            foreach (LetterTile t in BAG)
                t.inBag = true;
            tilesRemaining = TILE_COUNT;
            _drawCount = new byte[POINTS_MAP.Length];
        }

        //returns a formatted string for display and loads validation metadata
        //null if not enough letters remain in the bag
        public string? Draw(ushort numberOfLetters, bool sort, bool score, bool spaces)
        {
            //reject request if not enough tiles
            if (numberOfLetters > tilesRemaining) return null;
            
            char[] letters = new char[numberOfLetters];
            int randomPull;
            _drawCount = new byte[POINTS_MAP.Length];
            List<byte> lettersWithScore = [];

            for (byte x = 0; x < numberOfLetters; x++)
            {
                //draw random tiles until one that has not been pulled is drawn
                do randomPull = RNG.Next(BAG.Length);
                while (!BAG[randomPull].inBag);

                //flag the tile as drawn
                BAG[randomPull].inBag = false;

                //assign it to output
                letters[x] = BAG[randomPull].LETTER;

                //increase count of letters used to determine a valid score
                _drawCount[letters[x]-BAG[0].LETTER]++;

                //decrement the count so the bag can never be overdrawn
                tilesRemaining--;
            }

            //alphabetize the list
            if (sort) Array.Sort(letters);
            draw_string = new string(letters);

            //load unicode formatted return string
            foreach(char l in letters)
            {
                //letter
                lettersWithScore.Add((byte)l);
                lettersWithScore.Add(0x00);
                
                if (score)
                    //substring for score
                    foreach (char digit in POINTS_MAP[l - BAG[0].LETTER].ToString())
                    {
                        lettersWithScore.Add((byte)(digit + 0x50));
                        lettersWithScore.Add(0x20);
                    }
                
                if (spaces)
                {
                    //space
                    lettersWithScore.Add(0x20);
                    lettersWithScore.Add(0x00);
                }
            }

            if (spaces)
            {
                //remove last space
                lettersWithScore.RemoveAt(lettersWithScore.LastIndexOf(0x20));
                lettersWithScore.RemoveAt(lettersWithScore.LastIndexOf(0x00));
            }

            return UNICODE.GetString([..lettersWithScore]);
        }
    }
}
