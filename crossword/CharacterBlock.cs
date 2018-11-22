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
        Confirmed,
        Unconfirmed,
        Wrong
    }
    class CharacterBlock : IBlock
    {
        private BlockState state;
        private char answer;
        private char character;
        private Word vertical;
        private Word horizontal;
        TextBox text = new TextBox();

        public CharacterBlock(char answer)
        {
            CreateBox();
            this.answer = answer;
            state = BlockState.Unconfirmed;
        }

        public void CreateBox()
        {
            text.BorderStyle = BorderStyle.None;
            text.Location = new System.Drawing.Point(4, 4);
            text.Size = new System.Drawing.Size(20, 19);
            text.TabIndex = 0;
            text.CharacterCasing = CharacterCasing.Upper;
            text.MaxLength = 1;
            text.Margin = new Padding(0);
            text.TextAlign = HorizontalAlignment.Center;
            text.BackColor = System.Drawing.Color.LightGray;
            text.TextChanged += new EventHandler(delegate (Object sender, EventArgs a)
            {
                if (IsPartOfHorizontalWord())
                {
                    GetHorizontalWord().OnBlockUpdated(this);
                }

                if (IsPartOfVerticalWord())
                {
                    GetVerticalWord().OnBlockUpdated(this);
                }
            });
        }

        public char GetAnswer()
        {
            return answer;
        }

        public void Highlight()
        {
            void textBox1_MouseEnter(object sender, EventArgs e)
            {
                text.BackColor = System.Drawing.Color.LightYellow;
            }

            void textBox1_MouseLeave(object sender, EventArgs e)
            {
                text.BackColor = System.Drawing.Color.White;
            }
        }

        public void UpdateState(BlockState state)
        {
            this.state = state;
            if (state == BlockState.Confirmed)
            {
                text.BackColor = System.Drawing.Color.MediumSeaGreen;
                text.ReadOnly = true;
                text.Text = answer.ToString();
            }
            else if (state == BlockState.Wrong)
            {
                text.BackColor = System.Drawing.Color.FromArgb(255, 144, 85);
            }
            else
            {
                text.BackColor = System.Drawing.Color.LightGray;
            }
        }



        public bool IsCorrectAnswer()
        {
            if (text.Text.Length > 0 && text.Text[0] == answer)
            {
                return true;
            }
            return false;
        }

        public bool IsPartOfVerticalWord()
        {
            if (vertical == null)
            {
                return false;
            }
            return true;
        }

        public Word GetVerticalWord()
        {
            if (IsPartOfVerticalWord())
            {
                return vertical;
            }
            return null;
        }

        public bool IsPartOfHorizontalWord()
        {
            if (horizontal == null)
            {
                return false;
            }
            return true;
        }

        public Word GetHorizontalWord()
        {
            if (IsPartOfHorizontalWord())
            {
                return horizontal;
            }
            return null;
        }

        public Control GetVisualControl()
        {
            return text;
        }

        public void SetCharacter(char c)
        {
            text.Text = char.ToUpper(c).ToString();
        }

        public void SetConfirmed()
        {
            UpdateState(BlockState.Confirmed);
        }

        public void SetWrong()
        {
            UpdateState(BlockState.Wrong);
        }

        public void SetVerticalWord(Word word)
        {
            vertical = word;
        }

        public void SetHorizontalWord(Word word)
        {
            horizontal = word;
        }

        public bool IsSet()
        {
            if (text.Text.Length > 0 && !text.Text.Contains(' '))
            {
                return true;
            }
            return false;
        }
    }
}
