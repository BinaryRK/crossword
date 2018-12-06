using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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
        private List<Word> words = new List<Word>();

        public Word[] GetWords()
        {
            List<Word> a = new List<Word>();

            a.Add(new Word("Cat", "Not a cat."));
            a.Add(new Word("Dog", "Not a dog"));
            a.Add(new Word("Dog", "lorem ip"));
            a.Add(new Word("Dog", "Not a dog"));
            a.Add(new Word("Dog", "Not a dog"));
            a.Add(new Word("Lorem", "Lorem ipsum dolor sit amet, viris eruditi cum at. Te sea minim omittam, imperdiet reprehendunt cum ut. Eum ea summo mollis eleifend. Illum aeque instructior cum et. Pri velit dignissim in, cu qui eros epicurei platonem."));

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

        public IBlock[,] GetBlocks()
        {
            return blocks;
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


        // TODO: Refactor
        private void PlaceHelper(string word, int vertical, int x, int y)
        {
            WordDirection dir = WordDirection.Horizontal;

            if (vertical == 1)
            {
                dir = WordDirection.Vertical;
            }

            Word w = new Word(word, "placeholder word", dir);

            if (!CanWordBePlaced(w, x, y))
            {
                MessageBox.Show("Cannot place word: " + word);
                //throw new Exception("Cannot place word: " + word);
            }
            else
            {
                PlaceWord(w, x, y);
            }
        }

        public void GenerateNewCrossword(GameDifficulty difficulty)
        {
            blocks = new IBlock[15, 20];

            // TODO: generate
            PlaceHelper("temporary", 0, 1, 3);
            PlaceHelper("elegant", 0, 2, 5);
            PlaceHelper("imminent", 0, 2, 7);

            PlaceHelper("enemies", 1, 2, 3);
            PlaceHelper("long", 1, 5, 2);
            PlaceHelper("retina", 1, 8, 3);


            // Fill empty blocks
            for (int row = 0; row < blocks.GetLength(0); row++)
            {
                for (int col = 0; col < blocks.GetLength(1); col++)
                {
                    if (blocks[row, col] == null)
                    {
                        blocks[row, col] = new BlackBlock();
                    }
                }
            }
        }

        // Helper function to resolve points cleaner. 
        // This will also work for negative indexes
        private static Point GetWordCoord(Word word, Point startPoint, int index)
        {
            int x = startPoint.X + (word.GetDirection() == WordDirection.Horizontal ? index : 0);
            int y = startPoint.Y + (word.GetDirection() == WordDirection.Vertical   ? index : 0);
            return new Point(x, y);
        }

        // Excpects a word that can be placed
        private void PlaceWord(Word word, int row, int col)
        {
            Point start = new Point(row, col);

            Point before = GetWordCoord(word, start, -1);
            Point after = GetWordCoord(word, start, word.GetLength());

            // Its ok to overwrite the old block here since black blocks do not get stored anywhere else than the block[,] array
            blocks[before.X, before.Y] = new BlackBlock();
            blocks[after.X, after.Y] = new BlackBlock();

            for (int i = 0; i < word.GetLength(); i++)
            {
                Point p = GetWordCoord(word, start, i);
                if (blocks[p.X, p.Y] == null)
                {
                    blocks[p.X, p.Y] = new CharacterBlock(word.GetCorrectWord()[i]);
                }
                word.SetSharedBlock(blocks[p.X, p.Y] as CharacterBlock, i);
            }
        }

        private bool CanWordBePlaced(Word word, int row, int col)
        {
            Point start = new Point(row, col);

            // Check lower bound
            if (row <= 0 || col <= 0)
            {
                return false;
            }

            // Check clearence before (previous must be black point)
            Point before = GetWordCoord(word, start, -1);
            if (blocks[before.X, before.Y] != null && blocks[before.X, before.Y].GetAnswer() != '#')
            {
                return false;
            }

            Point after = GetWordCoord(word, start, word.GetLength());

            // Check upper bound
            if (after.X >= blocks.GetLength(0) || after.Y >= blocks.GetLength(1))
            {
                return false;
            }

            // Check clearence after.
            if (blocks[after.X, after.Y] != null && blocks[after.X, after.Y].GetAnswer() != '#')
            {
                return false;
            }


            for (int i = 0; i < word.GetLength(); ++i)
            {
                Point p = GetWordCoord(word, start, i);

                if (blocks[p.X, p.Y] != null && blocks[p.X, p.Y].GetAnswer() != word.GetCorrectWord()[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
