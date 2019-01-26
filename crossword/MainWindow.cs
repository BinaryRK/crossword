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
        Crossword activeCrossword = new Crossword();

        private int blockSizePx = 21;

        Word selectedWord;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            activeCrossword.GenerateNewCrossword(GameDifficulty.Easy);
            RemakeTable();
            RemakeWords();
        }

        public void RemakeWords()
        {
            ListBox text;
            Word[] words = activeCrossword.GetWords();
            int i = 0;
            for (i = 0; i < words.Length; ++i)
            {


                if (words[i].GetDirection() == Direction.Horizontal)
                {
                    text = new ListBox();
                    text.Text = words[i].GetDescription();
                    listBoxhorizontal.Items.Add(words[i]);
                    listBoxhorizontal.Size = new Size(listBoxhorizontal.Width, listBoxhorizontal.Items.Count * 20 + 30);
                }
                else
                {
                    text = new ListBox();
                    text.Text = words[i].GetDescription();
                    listBoxvertical.Items.Add(words[i]);
                }


            }
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
            activeCrossword = new Crossword();
            activeCrossword.GenerateNewCrossword(GameDifficulty.Easy);
            //RemakeTable();
        }

        private void listBoxhorizontal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedWord != null)
            {
                selectedWord.DeSelect();
            }
            Word high = listBoxhorizontal.SelectedItem as Word;
            selectedWord = high;
            high.Select();
        }

        private void listBoxvertical_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (selectedWord != null)
            {
                selectedWord.DeSelect();
            }
            Word high = listBoxvertical.SelectedItem as Word;
            selectedWord = high;
            high.Select();
            
        }
    }
}
