using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crossword
{
    class BlackBlock : IBlock
    {
        TextBox text = new TextBox();

        public BlackBlock()
        {
            CreateBox();
        }

        private void CreateBox()
        {
            text.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            text.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            text.Location = new System.Drawing.Point(1, 1);
            text.Margin = new System.Windows.Forms.Padding(0);
            text.MaxLength = 1;
            text.ReadOnly = true;
            text.Size = new System.Drawing.Size(38, 19);
            text.TabIndex = 0;
            text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            text.Enabled = false;
        }

        public char GetAnswer()
        {
            return '#';
        }

        public void Highlight() { }

        public Control GetVisualControl()
        {
            return text;
        }

        public bool IsCorrectAnswer()
        {
            return true;
        }

        public bool IsPartOfVerticalWord()
        {
            return false;
        }
        public Word GetVerticalWord()
        {
            return null;
        }
        public bool IsPartOfHorizontalWord()
        {
            return false;
        }
        public Word GetHorizontalWord()
        {
            return null;
        }
        public void SetCharacter(char c) { }

        public bool Validate()
        {
            return true;
        }

        public void SetConfirmed() { }

        public void SetWrong() { }

        public void SetVerticalWord(Word word) { }

        public void SetHorizontalWord(Word word) { }

        public bool IsSet()
        {
            return true;
        }
    }


}
