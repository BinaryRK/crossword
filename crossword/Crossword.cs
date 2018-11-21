using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crossword
{
    class Crossword
    {
        public int[][] GetBlockStates() {
            return new int[][] {
                new int[] { 0, 1, 1, 0 }
                , new int [] { 0, 0, 1, 0 }
                , new int [] { 1, 1, 1, 0 }
                , new int [] { 1, 1, 1, 0 }
                , new int [] { 1, 1, 1, 0 }
                , new int [] { 1, 1, 1, 0 }
                , new int [] { 1, 1, 1, 0 }
                , new int [] { 1, 1, 1, 0 }
            };
        }

    }
}
