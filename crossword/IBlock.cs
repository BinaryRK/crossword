using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crossword
{
    public interface IBlock
    {
        bool IsCorrectAnswer();

        bool IsPartOfVerticalWord();
        void SetVerticalWord(Word word);
        Word GetVerticalWord();

        bool IsPartOfHorizontalWord();
        void SetHorizontalWord(Word word);
        Word GetHorizontalWord();

        Control GetVisualControl();

        void SetCharacter(char c);

        char GetAnswer();

        void Highlight();

        void SetConfirmed();
        void SetWrong();
        bool IsSet();

        bool CanOverwrite(Direction direction);
        void RemoveOverwritePossibility(Direction direction);
    }
}
