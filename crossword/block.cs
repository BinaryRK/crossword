using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crossword
{
    public enum BlockState
    {
        Black ,
        Empty ,
        Confirmed ,
        Unconfirmed 
    };

    

    class Block
    {
        private BlockState state;
        private char character;
        
        public BlockState GetBlockState()
        {
            return state;
        }


        bool IsBlack()
        {
            if (state == BlockState.Black)
            {
                return true;
            }
            return false;
        }
        bool IsEmpty()
        {
            if (state == BlockState.Empty)
            {
                return true;
            }
            return false;
        }
        bool IsConfirmed()
        {
            if (state == BlockState.Confirmed)
            {
                return true;
            }
            return false;
        }
        bool IsUnconfirmed()
        {
            if (state == BlockState.Unconfirmed)
            {
                return true;
            }
            return false;
        }

        bool CanPlaceCharacer(char charact)
        {
            return IsEmpty() || IsUnconfirmed();
        }

        void PlaceCharacter(char charact)
        {
            if (CanPlaceCharacer(charact))
            {
                character = charact;
            }
        }
        void ConfirmCharacter()
        {
            state = BlockState.Confirmed;
        }
        
    }
}
