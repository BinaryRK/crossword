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
        private Block[][] blocks { get; set; }

        public Block[][] GetBlocks()
        {
            return blocks;
        }

        public void GenerateNewCrossword(GameDifficulty difficulty) {
            string[] crw =
                new string[] {
                      "#character##"
                    , "#a##########"
                    , "#t#f###f####"
                    , "#a#a###o####"
                    , "#l#k###r####"
                    , "#overwatch##"
                    , "#g#####n####"
                    , "##drastikon#"
                    , "#######t####"
                    , "#kafeeeeee##"
                };

            blocks = new Block[crw.Length][];
            Random r = new Random();
            for (int row=0; row<crw.Length; row++)
            {
                blocks[row] = new Block[crw[0].Length];
                for (int col=0; col<crw[0].Length; col++)
                {
                    char c = crw[row][col];

                    if (c == '#')
                    {
                        blocks[row][col] = new Block(BlockState.Black, ' ', ' ');
                    }
                    else
                    {

                        if (r.Next() % 100 <= 10)
                        {
                            blocks[row][col] = new Block(BlockState.Confirmed, c, c);
                        }
                        else
                        {
                            blocks[row][col] = new Block(BlockState.Unconfirmed, ' ', c);
                        }
                    }

                }
            }
        }
    }
}
