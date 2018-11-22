using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace crossword
{
    public partial class MainWindow : Form
    {
        Crossword activeCrossword;

        private int blockSizePx = 21;

        public MainWindow()
        {
            InitializeComponent();
            activeCrossword = new Crossword();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            activeCrossword.GenerateNewCrossword(GameDifficulty.Easy);
            RemakeTable();
            RemakeWords();
        }

        public void RemakeWords()
        {
            UI_Words.Controls.Add(new TextBox());

        }

        public void RemakeTable()
        {
            IBlock[,] blocks = activeCrossword.GetBlocks();

            int rowcount = blocks.GetLength(0);
            int columncount = blocks.GetLength(1);

            // Clear everything old first
            UI_TablePanel.Controls.Clear();
            UI_TablePanel.RowStyles.Clear();
            UI_TablePanel.ColumnStyles.Clear();

            UI_TablePanel.RowCount = rowcount;
            UI_TablePanel.ColumnCount = columncount;

            // Add rows
            for (int row = 0; row < rowcount; row++)
            {
                UI_TablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, blockSizePx));
            }

            // Add columns
            for (int col = 0; col < columncount; col++)
            {
                UI_TablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, blockSizePx));
            }
            

            for (int row = 0; row < rowcount; row++) 
            {
                for (int col = 0; col < columncount; col++)
                {
                    UI_TablePanel.Controls.Add(blocks[row,col].GetVisualControl(), col, row);
                }
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IBlock[,] blocks = activeCrossword.GetBlocks();
            Random r = new Random();
            foreach(var block in blocks)
            {
                if(r.Next(0,2) == 0)
                {
                    block.SetConfirmed();
                    block.Highlight();
                }
                else
                {
                    block.SetWrong();
                }
                
            }
        }
    }
}
