using System;
using System.Windows.Forms;
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
        private Control control;
        


        private BlockState state;
        private char character;
        private char charanswer;
        
        public Block(int i) //remove
        {
            state = BlockState.Unconfirmed;
            character = 'a';
        }
        public Block(BlockState state,char character)
        {
            this.state = state;
            this.character = character;
        }

        public BlockState GetBlockState()
        {
            return state;
        }
        
        public Control GenerateControl()
        {
            TextBox textBox = new TextBox();
            textBox.Text = character.ToString();
            control = textBox;
            return control;
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
