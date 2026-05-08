using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSea_OOP_2026
{
    public partial class FormGame : Form
    {
        List<Image> images = new List<Image>()
        { 
            Properties.Resources._0,
            Properties.Resources._1,
            Properties.Resources._2,
            Properties.Resources._3,
            Properties.Resources._4
        };


        Field myField;

        public FormGame()
        {
            InitializeComponent();
            CreateField();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            myField = new Field();
            ShowField(myField);
            labelAllShips.Text = myField.CountAllShips().ToString();
            labelAllWounded.Text = myField.CountAllWounded().ToString();
            labelAllDead.Text = myField.CountAllDead().ToString();
        }
        void CreateField()
        {
            dataGridViewMyField.RowCount = Field.SIZE;
            dataGridViewMyField.ColumnCount = Field.SIZE;
            dataGridViewMyField.Size = new Size(470, 450);


            for (int i = 0; i < Field.SIZE; i++)
            {
                dataGridViewMyField.Rows[i].Height = 40;
                dataGridViewMyField.Rows[i].HeaderCell.Value = (i + 1).ToString();
                dataGridViewMyField.Columns[i].Width = 40;
                dataGridViewMyField.Columns[i].HeaderCell.Value = ((char)('А' + i)).ToString();
            }
        }
        void ShowField(Field f)
        {
            for (int i = 0; i < Field.SIZE; i++)
                for (int j = 0; j < Field.SIZE; j++)
                {
                    dataGridViewMyField.Rows[i].Cells[j].Value = images[f.Matrix(i, j)];
                }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridViewMyField_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            myField.Fire(e.RowIndex, e.ColumnIndex);
            ShowField(myField);
            labelAllShips.Text = myField.CountAllShips().ToString();
            labelAllWounded.Text = myField.CountAllWounded().ToString();
            labelAllDead.Text = myField.CountAllDead().ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
