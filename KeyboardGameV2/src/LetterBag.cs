//bag of letters selected at random for the letter pool
using System.Text;

namespace KeyboardGameV2.src
{
    // one of the two structures avalible for selecting letters
    public class LetterBag
    {
        private readonly CharEncoding.Language language;

        private class LetterTile(char c)
        {
            public readonly char LETTER = c;
            public bool inBag = true;
        }

        //full set of letters
        private readonly LetterTile[] BAG;

        //rng for drawing letters
        private readonly Random RNG;

        //point value for letters
        public readonly byte[] POINTS_MAP;

        //total number of letters in the bag
        private readonly ushort TILE_COUNT;

        //number of letters left in the bag
        private ushort tilesRemaining;
        public ushort TilesRemaining() { return tilesRemaining; }

        //holds the count of letters in the ltter pool
        public byte[] _drawCount;

        public string draw_string = "";

        //generic constructor for basic declaration
#pragma warning disable CS8618 //*********************************************************************
        public LetterBag()
        {
        }
#pragma warning restore CS8618 //*********************************************************************

        //real constructor
        public LetterBag(byte[] letters, CharEncoding.Language language)
        {
            this.language = language;
            
            //metadata for counting
            TILE_COUNT = 0;
            ushort tileCursor = 0;
            byte max = byte.MinValue;

            //establish the other class memebers
            RNG = new Random();
            POINTS_MAP = new byte[language.letterCount];
            _drawCount = new byte[language.letterCount];

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
                    BAG[tileCursor++] = new LetterTile(language.deindexer(x));
            }
        }

        public void Reset()
        {
            foreach (LetterTile t in BAG)
                t.inBag = true;
            tilesRemaining = TILE_COUNT;
            _drawCount = new byte[language.letterCount];
            draw_string = "";
        }

        //returns a formatted string for display and loads validation metadata
        //null if not enough letters remain in the bag
        public string? Draw(ushort numberOfLetters)
        {
            //reject request if not enough tiles
            if (numberOfLetters > tilesRemaining) return null;

            char[] letters = new char[numberOfLetters];
            int randomPull;
            _drawCount = new byte[language.letterCount];


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
                _drawCount[language.indexer(letters[x])]++;

                //decrement the count so the bag can never be overdrawn
                tilesRemaining--;
            }

            draw_string = new string(letters);
            return draw_string;
        }
    }
}
