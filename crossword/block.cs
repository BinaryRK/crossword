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
        Confirmed ,
        Unconfirmed,
        Wrong
    };

    class Block
    {
        private TextBox control;
        private BlockState state;
        private char character;
        private char answer;

        public Block(BlockState state, char character, char answer) //constructor
        {
            this.state = state;
            this.character = char.ToUpper(character);
            this.answer = char.ToUpper(answer);
        }

        bool CanPlaceCharacer(char charact)
        {
            return state == BlockState.Unconfirmed
                || state == BlockState.Wrong;
        }

        void PlaceCharacter(char c)
        {
            if (CanPlaceCharacer(c))
            {
                character = c;
                if (character == answer)
                {
                    control.BackColor = System.Drawing.Color.MediumSeaGreen;
                }   
            }
        }

        void ConfirmCharacter()
        {
            state = BlockState.Confirmed;
        }

        public BlockState GetBlockState() //getter
        {
            return state;
        }

        public TextBox GenerateControl() //Controler
        {
            switch (state) {
                case BlockState.Black:
                    control = GenerateTextBoxBlack();
                    break;
                case BlockState.Confirmed:
                    control = GenerateTextBoxConfirmed();
                    break;
                case BlockState.Unconfirmed:
                    control = GenerateTextBoxUnconfirmed();
                    break;
                case BlockState.Wrong:
                    control = GenerateTextBoxWrong();
                    break;
                default:
                    throw new Exception("Unhandled enum case for BlockState.");
            } 

            return control;
        }

        // TODO: Refactor generate text to format the member TextBox
        private TextBox GenerateText()
        {
            TextBox textBox1 = new TextBox();
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new System.Drawing.Point(4, 4);
            textBox1.Size = new System.Drawing.Size(20, 19);
            textBox1.TabIndex = 0;
            textBox1.CharacterCasing = CharacterCasing.Upper;
            textBox1.MaxLength = 1;
            textBox1.Margin = new Padding(0);
            textBox1.TextAlign = HorizontalAlignment.Center;

            // TODO: Use key presses and disable TextEditing completely
            textBox1.TextChanged += new EventHandler(delegate (Object sender, EventArgs a)
            {
                if (textBox1.TextLength >= 1 && control != null)
                {
                    PlaceCharacter(textBox1.Text[0]);
                }
            });

            return textBox1;

        }
        
        private TextBox GenerateTextBoxBlack()
        {
            TextBox Text = GenerateText();
            Text.BackColor = System.Drawing.Color.Black;
            Text.ReadOnly = true;

            return Text;
        }
        
        private TextBox GenerateTextBoxConfirmed()
        {
            TextBox Text = GenerateText();
            Text.BackColor = System.Drawing.Color.MediumSeaGreen;
            Text.ReadOnly = true;
            Text.Text = answer.ToString();
            return Text;
        }

        private TextBox GenerateTextBoxUnconfirmed()
        {
            TextBox Text = GenerateText();
            Text.BackColor = System.Drawing.Color.LightGray;
            
            return Text;
        }
         
        private TextBox GenerateTextBoxWrong()
        {
            TextBox Text = GenerateText();
            Text.BackColor = System.Drawing.Color.FromArgb(255, 144, 85);

            return Text;
        }
    }
}
