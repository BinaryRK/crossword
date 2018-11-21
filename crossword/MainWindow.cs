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

        private int blockSizePx = 44;

        public MainWindow()
        {
            InitializeComponent();
            activeCrossword = new Crossword();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            RemakeTable();
        }

        public void RemakeTable()
        {
            Block [][] blockStates = activeCrossword.GetBlockStates();

            int rowcount = blockStates.Length;
            int columncount = blockStates[0].Length;

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
                    UI_TablePanel.Controls.Add(blockStates[row][col].GenerateControl(), col, row);
                }
            }
        }

        // TODO: Use Block Class here
        // Uses array indexes (1st row is row=0)
        private void SetTableElement(int block, int row, int column)
        {
            //UI_TablePanel.Controls.Remove(UI_TablePanel.GetControlFromPosition(column+1, row+1));

        }

        
    }
}
