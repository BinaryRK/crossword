using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crossword
{
    interface IBlock
    {
        bool IsCorrectAnswer();

        bool IsPartOfVerticalWord();
        Word GetVerticalWord();

        bool IsPartOfHorizontalWord();
        Word GetHorizontalWord();

        Control GetVisualControl();

        void SetCharacter(char c);

        void Highlight();

        void SetConfirmed();
        void SetWrong();
    }
}
