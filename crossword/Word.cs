using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crossword
{
    enum WordDirection
    {
        Horizontal
        , Vertical
    }

    class Word
    {
        WordDirection direction;
        string correctWord;
        string description;
        CharacterBlock[] blocks;

        public Word(string correctWord, string description, WordDirection direction = WordDirection.Horizontal)
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

        public void OnBlockUpdated(IBlock block)
        {
            if (IsFilled())
            {
                TryConfirm();
            }
        }

        public void SetSharedBlock(CharacterBlock block, int position)
        {
            switch (direction)
            {
                case WordDirection.Horizontal:
                    block.SetHorizontalWord(this);
                    break;
                case WordDirection.Vertical:
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

        public int GetLength()
        {
            return correctWord.Length;
        }

        void Highlight()
        {
            foreach (var block in blocks)
            {
                block.Highlight();
            }
        }

        public WordDirection GetDirection()
        {
            return direction;
        }

        // Position is in range 0 -> Len-1
        public char GetLetterAt(int position)
        {
            return correctWord[position];
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
        public bool IsConfirmed()
        {
            foreach (var block in blocks)
            {
                if (false) // TODO:
                {
                    return false;
                }
            }
            return true;
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
    }
}
