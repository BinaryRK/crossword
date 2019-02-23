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

    public class CharacterBlock : IBlock
    {
        private BlockState state;
        private char answer;
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
            text.Location = new System.Drawing.Point(0, 0);
            text.Size = new System.Drawing.Size(MainWindow.blockSizePx - 2, MainWindow.blockSizePx - 2);
            text.TabIndex = 0;
            text.CharacterCasing = CharacterCasing.Upper;
            text.MaxLength = 1;
            text.Margin = new Padding(0);
            text.TextAlign = HorizontalAlignment.Center;
            text.BackColor = System.Drawing.Color.LightGray;

            text.GotFocus += new EventHandler(delegate (Object sender, EventArgs a)
            {
                text.SelectAll();
                if (MainWindow.selectedWord != GetHorizontalWord() && MainWindow.selectedWord != GetVerticalWord())
                {
                    if (IsPartOfVerticalWord() && !GetVerticalWord().IsFinished())
                    {
                        MainWindow.instance.SelectWord(GetVerticalWord());
                    }
                    else if (IsPartOfHorizontalWord() && !GetHorizontalWord().IsFinished())
                    {
                        MainWindow.instance.SelectWord(GetHorizontalWord());
                    }
                }
            });

            text.TextChanged += new EventHandler(delegate (Object sender, EventArgs a)
            {
                if (state == BlockState.Confirmed)
                {
                    text.Text = answer.ToString();
                }
                if (text.Text.Length > 0)
                {
                    if (IsPartOfHorizontalWord())
                    {
                        GetHorizontalWord().OnBlockUpdated(this);
                    }

                    if (IsPartOfVerticalWord())
                    {
                        GetVerticalWord().OnBlockUpdated(this);
                    }
                }
            });

            text.KeyDown += new KeyEventHandler(delegate (Object sender, KeyEventArgs a)
            {
                if (a.KeyCode == Keys.Back && text.TextLength == 0)
                {
                    MainWindow.selectedWord.BackspaceBefore(this);
                }
            });
        }

        public void Backspace()
        {
            if (state == BlockState.Confirmed)
            {
                MainWindow.selectedWord.BackspaceBefore(this);
            }
            else
            {
                text.Text = "";
                Focus();
            }
        }

        public void Focus()
        {
            text.Focus();
        }

        public char GetAnswer()
        {
            return answer;
        }

        public void Highlight()
        {
            if (state != BlockState.Confirmed)
            {
                text.BackColor = System.Drawing.Color.LightYellow;
            }
        }

        public void UpdateState(BlockState state)
        {
            if (this.state == BlockState.Confirmed)
            {
                return;
            }
            this.state = state;
            if (state == BlockState.Confirmed)
            {
                text.BackColor = System.Drawing.Color.MediumSeaGreen;
                text.TabStop = false;
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

        public void SetUnconfirmed()
        {
            UpdateState(BlockState.Unconfirmed);
        }

        public void SetVerticalWord(Word word)
        {
            vertical = word;
        }

        public void DeSelect()
        {
            UpdateState(state);
        }

        public void SetHorizontalWord(Word word)
        {
            horizontal = word;
        }

        public bool IsSet()
        {
            if (text.Text.Length > 0)
            {
                return true;
            }
            return false;
        }

        public bool CanOverwrite(Direction direction)
        {
            return false;
        }

        public void RemoveOverwritePossibility(Direction direction)
        {
        
        }
    }
}
