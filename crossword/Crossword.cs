using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crossword
{
    class Crossword
    {
        public Block[][] GetBlockStates() {
            return new Block[][] {
                new Block[] { new Block(BlockState.Black,' ',' '), new Block(BlockState.Wrong, 'b', 'a'), new Block(BlockState.Empty, ' ', ' '), new Block (BlockState.Confirmed,' ',' ') }
                , new Block[] { new Block(0), new Block(1), new Block(1), new Block (0) }
                , new Block[] { new Block(0), new Block(1), new Block(1), new Block (0) }
                , new Block[] { new Block(0), new Block(1), new Block(1), new Block (0) }
                , new Block[] { new Block(0), new Block(1), new Block(1), new Block (0) }
                , new Block[] { new Block(0), new Block(1), new Block(1), new Block (0) }
                , new Block[] { new Block(0), new Block(1), new Block(1), new Block (0) }
                , new Block[] { new Block(0), new Block(1), new Block(1), new Block (0) }

            };
        }

    }
}
