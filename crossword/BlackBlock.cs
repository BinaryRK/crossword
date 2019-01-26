using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace crossword
{
    enum BlockOverwrite
    {
        Any,
        VerticalOnly,
        HorizontalOnly,
        None
    }

    class BlackBlock : IBlock
    {
        TextBox text = new TextBox();
        BlockOverwrite overwriteCondition = BlockOverwrite.Any;

        public BlackBlock()
        {
            CreateBox();
        }

        public BlackBlock(BlockOverwrite overwriteCond) : this()
        {
            this.overwriteCondition = overwriteCond;
        }

        private void CreateBox()
        {
            text.BackColor = Color.FromArgb(43, 42, 39);
            text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            text.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            text.Location = new System.Drawing.Point(-1, -1);
            text.Margin = new System.Windows.Forms.Padding(-1);
            text.MaxLength = 1;
            text.ReadOnly = true;
            text.Size = new System.Drawing.Size(MainWindow.blockSizePx, MainWindow.blockSizePx);
            text.TabIndex = 0;
            text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            text.Enabled = false;
        }

        public bool CanOverwriteVertical()
        {
            return overwriteCondition == BlockOverwrite.VerticalOnly
                || overwriteCondition == BlockOverwrite.Any;
        }

        public bool CanOverwriteHoriznotal()
        {
            return overwriteCondition == BlockOverwrite.HorizontalOnly
                || overwriteCondition == BlockOverwrite.Any;
        }

        public bool CanOverwrite(Direction direction)
        {
            switch (direction)
            {
                case Direction.Horizontal:
                    return overwriteCondition == BlockOverwrite.HorizontalOnly
                        || overwriteCondition == BlockOverwrite.Any;
                case Direction.Vertical:
                    return overwriteCondition == BlockOverwrite.VerticalOnly
                        || overwriteCondition == BlockOverwrite.Any;
            }
            return overwriteCondition != BlockOverwrite.None;
        }

        public void RemoveOverwritePossibility(Direction direction)
        {
            if (!CanOverwrite(direction))
            {
                return;
            }

            if (overwriteCondition == BlockOverwrite.Any)
            {
                switch (direction) {
                    case Direction.Vertical:
                        overwriteCondition = BlockOverwrite.HorizontalOnly;
                        return;
                    case Direction.Horizontal:
                        overwriteCondition = BlockOverwrite.VerticalOnly;
                        return;
                }
            }
            else
            {
                // we can reach here only if we can overwrite ONLY this direction in this block.
                overwriteCondition = BlockOverwrite.None;
            }
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
