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
        Unconfirmed,
        Wrong
    };

    

    class Block
    {
        private TextBox control;
        private BlockState state;
        private char character;
        private char charanswer;
        
        

        public Block(int i) //remove
        {
            state = BlockState.Unconfirmed;
            character = 'a';
        }



        public Block(BlockState state,char character,char answer) //constructor
        {
            this.state = state;
            this.character = character;
            this.character = answer;
        }

        public BlockState GetBlockState() //getter
        {
            return state;
        }
        
        public TextBox GenerateControl() //Controler
        {
           
            if (state == BlockState.Black)
            {
                control = GenerateTextBoxBlack();
            }else if(state == BlockState.Confirmed)
            {
                control = GenerateTextBoxConfirmed();
            }else if(state == BlockState.Empty)
            {
                control = GenerateTextBoxUnconfirmed();
            }else if(state == BlockState.Unconfirmed)
            {
                control = GenerateTextBoxUnconfirmed();
            }else
            {
                control = GenerateTextBoxWrong();
            }
            return control;
            
        }
        

       

        bool CanPlaceCharacer(char charact)
        {
            return Convert.ToBoolean( BlockState.Empty ) || Convert.ToBoolean(BlockState.Unconfirmed);
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


        private TextBox GenerateText()
        {
            TextBox textBox1 = new TextBox();
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1.Location = new System.Drawing.Point(4, 4);
            textBox1.Size = new System.Drawing.Size(20, 19);
            textBox1.TabIndex = 0;
            textBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            textBox1.MaxLength = 1;
            textBox1.Margin = new System.Windows.Forms.Padding(0);
            textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            textBox1.TextChanged += new EventHandler(delegate (Object sender, EventArgs a)
            {
                if (textBox1.TextLength > 1)
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
            Text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(144)))), ((int)(((byte)(85)))));

            return Text;
        }

        
    }
}
