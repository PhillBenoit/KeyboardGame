# Keyboard Game
This is a 4 player spelling game for Windows that uses keyboards as separate inputs.  It's a proof of concept written in C# so that its components can be exported to Unreal or Unity.

# Features
- English and Spanish.  All letter accents are treated as modifiers except for enye (ñ).  Letters are reverted to their basic form and enye can be typed using semicolon (;) or tilde (~).  User interface changes based on the loaded dictionary.
- Create your own dictionary.  A tool is encluded to encode text files as binary dictionaries.  The tool also includes an exclude function for removing words from a larger list.
- Stress tested simultanious input.  4 keyboards are treated as unique by Windows.  (typicall behavor treats them as the same keyboard)
- Instantanius by letter spell checking.  Possible words found in less than a second. (Trie structure)
- Binary searching of known words.  Underlying data for output is also used to track which players have credit for enetering a word.
- Two methods of selecting random letters.  Bag mode puts the maximum known occurancaces of each letter as possibilities into the bag.  (Eg. if "D" appears in one word 6 times and no word has 7 or more, 6 "D" tiles are placed in the bag.)  Dictonary mode selects words from the dictionary to fill the letters in the pool.

# Known Problems
- "moonbeam" scoring is a little buggy.  You can still get points for words that you have previously gotten points for.
- The dictionaries are not what they need to be.  English has too many words and spanish has too few.
- enye (ñ) is not in the correct alphabetical order and appears at the end.
- Some keys on some keyboards will generate an out of range error (above the base length FF).
- Keys are logged even when the application is not in focus.
- Sort of word list does not prioritize words the players have guessed when showing all words at game over.


# Closing Thoughts
No one should be judged by the quantity of their submissions to github.  Over my yers of unemployment, I have been criticised for not having massive output on github.  I let the voices of doubt get to me.  I made this to prove I could make something.  I no longer feel the need to make something just to prove I can.  I know what I'm doing.  I am not going to engage in slop just to show numbers.

If you have any questions about me or the code, feel free to reach out.

# Dictionary Sources
https://github.com/wordnik/wordlist/tree/main
https://github.com/zacanger/profane-words
https://launchpad.net/ubuntu/+source/wspanish/1.0.26