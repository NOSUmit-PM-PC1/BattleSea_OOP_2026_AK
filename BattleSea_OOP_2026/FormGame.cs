using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BattleSea_OOP_2026
{
    public partial class FormGame : Form
    {
        static Random rnd = new Random();
        List<Image> images = new List<Image>()
        { 
            Properties.Resources._0,
            Properties.Resources._1,
            Properties.Resources._2,
            Properties.Resources._3,
            Properties.Resources._4
        };


        Field myField, enemyField;

        public FormGame()
        {
            InitializeComponent();
            CreateField(dataGridViewMyField);
            CreateField(dataGridViewEnemyField);
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            myField = new Field();
            enemyField = new Field();
            ShowField(myField, dataGridViewMyField);
            ShowEnemyField(enemyField, dataGridViewEnemyField);
            labelAllShips.Text = myField.CountAllShips().ToString();
            labelAllWounded.Text = myField.CountAllWounded().ToString();
            labelAllDead.Text = myField.CountAllDead().ToString();
        }
        void CreateField(DataGridView gridView)
        {
            gridView.RowCount = Field.SIZE;
            gridView.ColumnCount = Field.SIZE;
            gridView.Size = new Size(470, 450);


            for (int i = 0; i < Field.SIZE; i++)
            {
                gridView.Rows[i].Height = 40;
                gridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                gridView.Columns[i].Width = 40;
                gridView.Columns[i].HeaderCell.Value = ((char)('А' + i)).ToString();
            }
        }
        void ShowField(Field f, DataGridView gridView)
        {
            for (int i = 0; i < Field.SIZE; i++)
                for (int j = 0; j < Field.SIZE; j++)
                {
                    gridView.Rows[i].Cells[j].Value = images[f.Matrix(i, j)];
                }
        }
        void ShowEnemyField(Field f, DataGridView gridView)
        {
            for (int i = 0; i < Field.SIZE; i++)
                for (int j = 0; j < Field.SIZE; j++)
                {
                    int temp = f.Matrix(i, j);
                    if ((ShipStatus)temp == ShipStatus.Normal + 1)
                        temp = 0;

                    gridView.Rows[i].Cells[j].Value = images[temp];
                }
        }

        private void dataGridViewEnemyField_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // наш выстрел
            enemyField.Fire(e.RowIndex, e.ColumnIndex);
          

            // выстрел противника
            myField.Fire(rnd.Next(1, Field.SIZE), rnd.Next(1, Field.SIZE));

            ShowField(myField, dataGridViewMyField);
            ShowEnemyField(enemyField, dataGridViewEnemyField);
            labelEnemyAllShips.Text = enemyField.CountAllShips().ToString();
            labelEnemyAllWounded.Text = enemyField.CountAllWounded().ToString();
            labelEnemyAllDead.Text = enemyField.CountAllDead().ToString();
            labelAllShips.Text = myField.CountAllShips().ToString();
            labelAllWounded.Text = myField.CountAllWounded().ToString();
            labelAllDead.Text = myField.CountAllDead().ToString();

        }

    }
}
