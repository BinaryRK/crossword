using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crossword
{
    public enum GameDifficulty
    {
        Easy
        , Medium
        , Hard
    }


    class Crossword
    {
        private IBlock[,] blocks { get; set; }
        //private Word[] words = new Word[10];

        public Word[] GetWords()
        {
            List<Word> a = new List<Word>();

            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));

            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Cat", "Not a cat.", WordDirection.Vertical));
            a.Add(new Word("Dog", "Not a dog", WordDirection.Vertical));
            a.Add(new Word("Dog", "lorem ip", WordDirection.Vertical));
            a.Add(new Word("Dog", "Not a dog", WordDirection.Vertical));
            a.Add(new Word("Dog", "Not a dog", WordDirection.Vertical));
            a.Add(new Word("Lorem", "Lorem ipsum dolor sit amet, viris eruditi cum at. Te sea minim omittam, imperdiet reprehendunt cum ut. Eum ea summo mollis eleifend. Illum aeque instructior cum et. Pri velit dignissim in, cu qui eros epicurei platonem."));
            a.Add(new Word("Kappa", "kappa", WordDirection.Horizontal));
            a.Add(new Word("Klol", "lol", WordDirection.Vertical));
            a.Add(new Word("Kappa", "kappa", WordDirection.Horizontal));
            a.Add(new Word("Kappa", "kappa", WordDirection.Horizontal));
            a.Add(new Word("Kappa", "kappa", WordDirection.Horizontal));
            a.Add(new Word("Kappa", "kappa", WordDirection.Horizontal));
            a.Add(new Word("Kappa", "kappa", WordDirection.Horizontal));
            a.Add(new Word("Kappa", "kappa", WordDirection.Horizontal));
            return a.ToArray();
        }

        static public IBlock CreateBlock(char c)
        {
            if (c == '#')
            {
                return new BlackBlock();
            }
            return new CharacterBlock(c);
        }

        public void GenerateNewCrossword(GameDifficulty difficulty) {
            string[] crw =
                new string[] {
                      "#character##"
                    , "#a##########"
                    , "#t#F?##f####"
                    , "#a#a###o####"
                    , "#l#k###r####"
                    , "#overwatch##"
                    , "#g#####n####"
                    , "##drastikon#"
                    , "#######t####"
                    , "#kafeeeeee##"
                };

            blocks = new IBlock[crw.Length, crw[0].Length];
            for (int row = 0; row < crw.Length; row++)
            {
                for (int col = 0; col < crw[0].Length; col++)
                {
                    blocks[row, col] = CreateBlock(crw[row][col]);
                }
            } 
        }

        public bool IsSolved()
        {
            foreach (var block in blocks)
            {
                if (!block.IsCorrectAnswer())
                {
                    return false;
                }
            }
            return true;
        }

        public IBlock[,] GetBlocks()
        {
            return blocks;
        }
    }
}
