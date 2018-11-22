using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            PlaceWord(w, x, y);
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
        
        // Excpects a word that can be placed
        private void PlaceWord(Word word, int row, int col)
        {
            // Local
            IBlock PlaceChar(int x, int y, int i)
            {
                if (blocks[x, y] == null)
                {
                    blocks[x, y] = new CharacterBlock(word.GetCorrectWord()[i]);
                }
                word.SetSharedBlock(blocks[x, y] as CharacterBlock, i);
                return blocks[x, y];
            }

            if (word.GetDirection() == WordDirection.Horizontal)
            {
                for (int i = 0; i < word.GetLength(); i++)
                {
                    PlaceChar(row + i, col, i).SetHorizontalWord(word);
                }
            }
            else
            {
                for (int i = 0; i < word.GetLength(); i++)
                {
                    PlaceChar(row, col + i, i).SetVerticalWord(word);                    
                }
            }
        }

        private bool CanWordBePlaced(Word word, int row, int col)
        {
            if (row < 0 || col < 0)
            {
                return false;
            }

            // Check if out of bounds 
            if (word.GetDirection() == WordDirection.Horizontal)
            {
                if (row + word.GetLength() >= blocks.GetLength(0))
                {
                    return false;
                }
            }
            else
            {
                if (col + word.GetLength() >= blocks.GetLength(1))
                {
                    return false;
                }
            }

            // Local function to avoid double code for vertical / horizontal
            bool CanPlace(int x, int y, int i)
            {
                if (blocks[x, y] == null)
                {
                    return true;
                }
                return blocks[x, y].GetAnswer() != word.GetCorrectWord()[i];
            }

            if (word.GetDirection() == WordDirection.Horizontal)
            {
                for (int i = 0; i < word.GetLength(); i++)
                {
                    if (!CanPlace(row + i, col, i))
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < word.GetLength(); i++)
                {
                    if (!CanPlace(row, col + i, i))
                    {
                        return false;
                    }
                }

            }

            return true;
        }
    }
}
