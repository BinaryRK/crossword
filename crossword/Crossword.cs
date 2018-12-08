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
        private bool PlaceHelper(string word, int vertical, int x, int y, int minIntersections)
        {
            Direction dir = Direction.Horizontal;

            if (vertical == 1)
            {
                dir = Direction.Vertical;
            }

            Word w = new Word(word, "placeholder word", dir);

            if (!CanWordBePlaced(w, x, y))
            {
                return false;
                //MessageBox.Show("Cannot place word: " + word);
                //throw new Exception("Cannot place word: " + word);
            }
            else
            {
                int inters = CountIntersections(w, x, y);
                if (inters < minIntersections)
                {
                    return false;
                }
                else if (inters == word.Length)
                {
                    return false;
                }
                PlaceWord(w, x, y);
                return true;
            }
        }


        public void GenerateNewCrossword(GameDifficulty difficulty)
        {
            const int SizeX = 50;
            const int SizeY = 50;
            const float GenerationComplexityFactor = 2.0f;
            blocks = new IBlock[SizeX, SizeY];



            List<String> wordlist = new List<string>() {
                "temporary"
                , "elegant"
                , "imminent"
                , "enemies"
                , "long"
                , "retina"
                , "drastikon"
                , "binary"
                , "function"
                , "electronic"
                , "rubbish"
                , "harmony"
                , "avant-garde"
                , "visible"
                , "committee"
                , "wisecrack"
                , "timber"
                , "devote"
                , "flavor"
                , "cheque"
                , "sunshine"
                , "tissue"
                , "temple"
                , "accept"
                , "restaurant"
                , "gossip"
                , "fitness"
                , "reproduction"
                , "turkey"
                , "qualify"
                , "vehicle"
                , "reveal"
                , "perform"
                , "revolutionary"
                , "sunrise"
                , "certain"
                , "pocket"
                , "liability"
                , "surgeon"
                , "pressure"
                , "bulletin"
                , "freshman"
                , "birthday"
                , "complication"
                , "deviation"
                , "cheese"
                , "height"
                , "situation"
                , "community"
                , "evolution"
                , "confrontation"
                , "transmission"
                , "intensify"
                , "subway"
                , "arrogant"
                , "constituency"
                , "qualification"
                , "insistence"
                , "costume"
                , "treaty"
                , "confront"
                , "global"
                , "husband"
                , "abolish"
                , "confidence"
                , "channel"
                , "option"
                , "implicit"
                , "ghostwriter"
                , "display"
                , "lifestyle"
                , "priority"
                , "pledge"
                , "wording"
                , "exposure"
                , "contradiction"
                , "temporary"
                , "suburb"
                , "consultation"
                , "correspond"
                , "access"
                , "labour"
                , "curtain"
                , "banana"
                , "activate"
                , "available"
                , "unrest"
                , "radiation"
                , "nursery"
                , "terminal"
                , "fabricate"
                , "detail"
                , "elegant"
                , "animal"
                , "break down"
                , "beneficiary"
                , "cutting"
                , "autonomy"
                , "present"
                , "transform"
                , "hilarious"
                , "sensitivity"
                , "district"
                , "curriculum"
                , "bloodshed"
                , "crackpot"
                , "cinema"
                , "occupy"
                , "revive"
            };


            int SizeWords = wordlist.Count;

            Random r = new Random();

            // Place first word at random
            bool Placed = false;
            while(!Placed)
            {
                String w = wordlist[r.Next(wordlist.Count)];
                int vert = r.Next(2);
                int x = r.Next((int)Math.Ceiling(SizeX * 0.30), (int)Math.Floor(SizeX * 0.70));
                int y = r.Next((int)Math.Ceiling(SizeY * 0.30), (int)Math.Floor(SizeY * 0.70));

                if (PlaceHelper(w, vert, x, y, 0))
                {
                    wordlist.Remove(w);
                    Placed = true;
                }
            }


            int LoopsWithoutProgress = 0;
            float GenerationFactor = SizeWords * GenerationComplexityFactor;

            while (wordlist.Count > 0 && LoopsWithoutProgress < GenerationFactor)
            {
                LoopsWithoutProgress++;

                String w = wordlist[r.Next(wordlist.Count)];
                int vert = r.Next(2);



                int minIntersections = 0;
                if (LoopsWithoutProgress < GenerationFactor * 0.1)
                {
                    minIntersections = 4;
                }
                else if (LoopsWithoutProgress < GenerationFactor * 0.3)
                {
                    minIntersections = 3;
                }
                else if (LoopsWithoutProgress < GenerationFactor * 0.6)
                {
                    minIntersections = 2;
                }
                else
                {
                    minIntersections = 1;
                }

                int offseti = r.Next(15);
                int offsetj = r.Next(15);

                for (int i = 0; i < SizeX - 1; i++)
                {
                    for (int j = 0; j < SizeY - 1; j++)
                    {
                        if (PlaceHelper(w, vert, (i + offseti) % (SizeX-2) + 1, (j + offsetj) % (SizeY-2) + 1, minIntersections))
                        {
                            LoopsWithoutProgress = 0;
                            wordlist.Remove(w);
                            goto nextword;
                        }
                    }
                }

                nextword:
                {
                    // nothing
                }
            }





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

        public bool IsValidPoint(Point p)
        {
            return IsValidPoint(p.X, p.Y);
        }

        public bool IsValidPoint(int x, int y)
        {
            return x >= 0 && y >= 0
                && x < blocks.GetLength(0)
                && y < blocks.GetLength(1);
        }

        // Helper function to resolve points cleaner. 
        // This will also work for negative indexes
        private static Point GetWordCoord(Word word, Point startPoint, int index, int parallelOffset = 0)
        {
            startPoint.X += word.GetDirection() == Direction.Vertical   ? parallelOffset : 0;
            startPoint.Y += word.GetDirection() == Direction.Horizontal ? parallelOffset : 0;

            int x = startPoint.X + (word.GetDirection() == Direction.Horizontal ? index : 0);
            int y = startPoint.Y + (word.GetDirection() == Direction.Vertical   ? index : 0);
            return new Point(x, y);
        }

        private void ProhibitOverwrite(Point p, Direction dir)
        {
            if (blocks[p.X, p.Y] == null)
            {
                blocks[p.X, p.Y] = new BlackBlock(dir == Direction.Horizontal ? BlockOverwrite.VerticalOnly : BlockOverwrite.HorizontalOnly);
            }
            else
            {
                blocks[p.X, p.Y].RemoveOverwritePossibility(dir);
            }
        }

        // Excpects a word that can be placed
        private void PlaceWord(Word word, int row, int col)
        {
            Point start = new Point(row, col);

            Point before = GetWordCoord(word, start, -1);
            Point after = GetWordCoord(word, start, word.GetLength());

            // Its ok to overwrite the old block here since black blocks do not get stored anywhere else than the block[,] array
            blocks[before.X, before.Y] = new BlackBlock(BlockOverwrite.None);
            blocks[after.X, after.Y] = new BlackBlock(BlockOverwrite.None);


            for (int i = 0; i < word.GetLength(); i++)
            {
                Point p = GetWordCoord(word, start, i);
                if (blocks[p.X, p.Y] == null || blocks[p.X, p.Y] is BlackBlock)
                {
                    blocks[p.X, p.Y] = new CharacterBlock(word.GetCorrectWord()[i]);
                }
                word.SetSharedBlock(blocks[p.X, p.Y] as CharacterBlock, i);

                ProhibitOverwrite(GetWordCoord(word, start, i, 1), word.GetDirection());
                ProhibitOverwrite(GetWordCoord(word, start, i, -1), word.GetDirection());
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

                if (blocks[p.X, p.Y] != null)
                {
                    if (!(blocks[p.X, p.Y].GetAnswer() == word.GetCorrectWord()[i] || blocks[p.X, p.Y].CanOverwrite(word.GetDirection())))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Expects a word that can be placed
        private int CountIntersections(Word word, int row, int col)
        {
            int intersections = 0;
            Point start = new Point(row, col);

            for (int i = 0; i < word.GetLength(); ++i)
            {
                Point p = GetWordCoord(word, start, i);

                if (blocks[p.X, p.Y] != null)
                {
                    intersections++;
                }
            }

            return intersections;
        }
    }
}
