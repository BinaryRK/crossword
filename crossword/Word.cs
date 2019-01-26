using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crossword
{
    public enum Direction
    {
        Horizontal
        , Vertical
    }

    public class Word
    {
        Direction direction;
        string correctWord;
        string description;
        CharacterBlock[] blocks;
        bool finished = false;

        public delegate void OnFinish(Word sender);
        public OnFinish onFinish;

        public Word(string correctWord, string description, Direction direction = Direction.Horizontal)
        {
            this.correctWord = correctWord.ToUpper();
            this.direction = direction;
            this.description = description;
            this.blocks = new CharacterBlock[correctWord.Length];
        }

        public void OnDescriptionClicked()
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBox.Show(correctWord, description, buttons);
        }

        public void OnBlockUpdated(CharacterBlock block)
        {
            if (finished)
            {
                return;
            }

            if (IsFilled())
            {
                finished = TryConfirm();
                if (finished)
                {
                    onFinish.Invoke(this);
                }
            }
            else if (this == MainWindow.selectedWord)
            {
                int currentIndex = Array.FindIndex(blocks, p => p == block) + 1;
                
                if (blocks.Length > currentIndex)
                {
                    blocks[currentIndex].Focus();
                }
            }
        }

        public void SetSharedBlock(CharacterBlock block, int position)
        {
            switch (direction)
            {
                case Direction.Horizontal:
                    block.SetHorizontalWord(this);
                    break;
                case Direction.Vertical:
                    block.SetVerticalWord(this);
                    break;
                default:
                    throw new Exception("Unhandled case for enum WordDirection.");
            }
            blocks.SetValue(block, position);
        }

        // This will only generate the block if not already set.
        public CharacterBlock GenerateBlock(int position)
        {
            var block = blocks[position];
            if (block == null)
            {
                block = new CharacterBlock(correctWord[position]);
                blocks[position] = block;
            }
            return block;
        }

        // Generate the remaining unset blocks. (or all blocks if none are already generated/set)
        public void GenerateRemainingBlocks()
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                GenerateBlock(i);
            }
        }

        internal void BackspaceBefore(CharacterBlock characterBlock)
        {
            if (finished)
            {
                return;
            }

            int currentIndex = Array.FindIndex(blocks, p => p == characterBlock) - 1;
            if (currentIndex >= 0)
            {
                blocks[currentIndex].Backspace();
            }
        }

        public int GetLength()
        {
            return correctWord.Length;
        }

        public void Select()
        {
            blocks[0].Focus();
            foreach (var block in blocks)
            {
                block.Highlight();
            }
        }

        public void DeSelect()
        {
            foreach (var block in blocks)
            {
                block.DeSelect();
            }
        }

        public Direction GetDirection()
        {
            return direction;
        }

        public CharacterBlock GetBlockAt(int position)
        {
            return blocks[position];
        }

        public string GetCorrectWord()
        {
            return correctWord;
        }

        public string GetDescription()
        {
            return description;
        }

        // returns true when the all blocks contain correct answers
        public bool IsCorrect()
        {
            foreach (var block in blocks)
            {
                if (!block.IsCorrectAnswer())
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsFilled()
        {
            foreach (var block in blocks)
            {
                if (!block.IsSet())
                {
                    return false;
                }
            }
            return true;
        }

        // returns true when the all blocks are confirmed
        public bool IsFinished()
        {
            return finished;
        }

        public bool TryConfirm()
        {
            bool correct = IsCorrect();

            foreach (var block in blocks)
            {
                if (correct)
                {
                    block.SetConfirmed();
                }
                else
                {
                    block.SetWrong();
                }
            }
            return correct;
        }
        override public string ToString() 
        {
            return description;
        }
    }


}
