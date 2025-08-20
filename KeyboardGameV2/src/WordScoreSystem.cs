using System.Text;

namespace KeyboardGameV2.src
{
    internal class WordScoreSystem(byte[] POINTS_MAP)
    {
        //holds the count of letters in the ltter pool
        private readonly byte[] _drawCount = new byte[POINTS_MAP.Length];

        //point value for letters
        private readonly byte[] POINTS_MAP = POINTS_MAP;
        
        //encoder for unicode used to give pool letters a subscript score
        private readonly UnicodeEncoding UNICODE = new();

        private string draw = "";

        public ushort ScoreWord(string w)
        {
            byte[] wordLetterCount = new byte[_drawCount.Length];
            int score = 0;

            //count letters in the word and compile score
            foreach (char letter in w)
            {
                byte offsetLetter = (byte)(letter - (char)CharEncoding.ASCII.LETTER_a);
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

        public void SetDraw(string s, byte[] draw_count)
        {
            draw = s;
            Array.Copy(draw_count, _drawCount, draw_count.Length);
        }

        public string FormatDraw(bool sort, bool score, bool spaces)
        {
            List<byte> draw_unicode = [];
            char[] draw_ascii = new char[draw.Length];
            Array.Copy(draw.ToCharArray(), draw_ascii, draw.Length);
            //alphabetize the list
            if (sort) Array.Sort(draw_ascii);

            //load unicode formatted return string
            foreach (char l in draw_ascii)
            {
                //letter
                draw_unicode.Add((byte)l);
                draw_unicode.Add(0x00);

                if (score)
                    //substring for score
                    foreach (char digit in POINTS_MAP[(int)(l - CharEncoding.ASCII.LETTER_a)].ToString())
                    {
                        draw_unicode.Add((byte)(digit + 0x50));
                        draw_unicode.Add(0x20);
                    }

                if (spaces)
                {
                    //space
                    draw_unicode.Add(0x20);
                    draw_unicode.Add(0x00);
                }
            }

            if (spaces)
            {
                //remove last space
                draw_unicode.RemoveAt(draw_unicode.LastIndexOf(0x20));
                draw_unicode.RemoveAt(draw_unicode.LastIndexOf(0x00));
            }

            return UNICODE.GetString([.. draw_unicode]);
        }
    }
}
