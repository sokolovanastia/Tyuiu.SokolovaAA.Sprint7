using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.V1.Lib;
using System.IO;
namespace Project.V1
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            openFileDialogFile_SAA.Filter = "Значения, разделенные запятыми(*.csv)|*.csv|Все файлы(*.*)|*.*";
        }

        DataService ds = new DataService();
       static string path;
       static string[,] array;
       static int rows;
        static int columns;

        private void buttonAbout_SAA_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }
        public static string[,] GetMatrix(string filePath)
        {
            string fileData = File.ReadAllText(filePath, Encoding.GetEncoding(1251));

            fileData = fileData.Replace('\n', '\r');
            string[] lines = fileData.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            rows = lines.Length;
            columns = lines[0].Split(';').Length;

            string[,] arrayValues = new string[rows, columns];

            for (int r = 0; r < rows; r++)
            {
                string[] line_r = lines[r].Split(';');
                for (int c = 0; c < columns; c++)
                {
                    arrayValues[r, c] = Convert.ToString(line_r[c]);
                }
            }
            return arrayValues;
        }

        private void buttonOpenFile_SAA_Click(object sender, EventArgs e)
        {
            try
            {
                FormMain formmain = new FormMain();
                openFileDialogFile_SAA.ShowDialog();
                path = openFileDialogFile_SAA.FileName;
                array = GetMatrix(path);
                rows = array.GetUpperBound(0) + 1;
                dataGridViewFile_SAA.RowCount = rows;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        dataGridViewFile_SAA.Rows[i].Cells[j].Value = array[i, j];
                    }
                }
                buttonAdd_SAA.Enabled = true;
                buttonDelete_SAA.Enabled = true;
                buttonSortNum_SAA.Enabled = true;
                buttonSortClose_SAA.Enabled = true;
                buttonSortOpen_SAA.Enabled = true;
                buttonSortRate_SAA.Enabled = true;
                buttonSortTime_SAA.Enabled = true;
                buttonSortName_SAA.Enabled = true;

                //Stats
                double[] rates = new double[rows];
                int[] timeOpen = new int[rows];
                int[] timeClose = new int[rows];
                int[] timeDuration = new int[rows];
                string str;
                for (int i = 0; i < rows; i++)
                {
                    rates[i] = Convert.ToDouble(dataGridViewFile_SAA.Rows[i].Cells[5].Value);
                    str = Convert.ToString(dataGridViewFile_SAA.Rows[i].Cells[2].Value);
                    timeOpen[i] = Convert.ToInt32(str.Split(':')[0]) * 60 + Convert.ToInt32(str.Split(':')[1]);
                    str = Convert.ToString(dataGridViewFile_SAA.Rows[i].Cells[3].Value);
                    timeClose[i] = Convert.ToInt32(str.Split(':')[0]) * 60 + Convert.ToInt32(str.Split(':')[1]);
                    timeDuration[i] = timeClose[i] - timeOpen[i];
                }
                textBoxStatCnt_SAA.Text = Convert.ToString(rows);
                textBoxStatMaxRate_SAA.Text = Convert.ToString(rates.Max());
                textBoxStatMinRate_SAA.Text = Convert.ToString(rates.Min());
                textBoxStatAverageRate_SAA.Text = Convert.ToString(Math.Round(rates.Sum() / rows, 3));
                textBoxStatTimeOpen_SAA.Text = Convert.ToString(timeOpen.Min() / 60) + ":" + (timeOpen.Min() % 60).ToString("00");
                textBoxStatTimeClose_SAA.Text = Convert.ToString(timeClose.Max() / 60) + ":" + (timeClose.Max() % 60).ToString("00");
                textBoxStatTimeDuration_SAA.Text = Convert.ToString(timeDuration.Max() / 60) + ":" + (timeDuration.Max() % 60).ToString("00");

            }
            catch
            {
                MessageBox.Show("Выбран неверный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            dataGridViewFile_SAA.RowCount = 10;
            dataGridViewFile_SAA.ColumnCount = 6;
            dataGridViewFile_SAA.Columns[0].Width = 20;
            dataGridViewFile_SAA.Columns[1].Width = 320;
            dataGridViewFile_SAA.Columns[2].Width = 110;
            dataGridViewFile_SAA.Columns[3].Width = 110;
            dataGridViewFile_SAA.Columns[4].Width = 120;
            dataGridViewFile_SAA.Columns[5].Width = 100;
        }

        private void buttonAdd_SAA_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewFile_SAA.RowCount += 1;
                string[] a = {dataGridViewFile_SAA.Rows[rows-1].Cells[0].Value.ToString(),
                              dataGridViewFile_SAA.Rows[rows-1].Cells[1].Value.ToString(),
                              dataGridViewFile_SAA.Rows[rows-1].Cells[2].Value.ToString(),
                              dataGridViewFile_SAA.Rows[rows-1].Cells[3].Value.ToString(),
                              dataGridViewFile_SAA.Rows[rows-1].Cells[4].Value.ToString(),
                              dataGridViewFile_SAA.Rows[rows-1].Cells[5].Value.ToString() };
                dataGridViewFile_SAA.Rows[rows].Cells[0].Value = rows + 1;
                dataGridViewFile_SAA.Rows[rows].Cells[1].Value = textBoxAddName_SAA.Text;
                dataGridViewFile_SAA.Rows[rows].Cells[2].Value = textBoxAddOpen_SAA.Text;
                dataGridViewFile_SAA.Rows[rows].Cells[3].Value = textBoxAddClose_SAA.Text;
                dataGridViewFile_SAA.Rows[rows].Cells[4].Value = textBoxAddPhone_SAA.Text;
                dataGridViewFile_SAA.Rows[rows].Cells[5].Value = textBoxAddRate_SAA.Text;
                for (int i = 0; i < 6; i++)
                {
                    dataGridViewFile_SAA.Rows[rows - 1].Cells[i].Value = a[i];
                }
                rows++;

            }
            catch
            {
                MessageBox.Show("Введены неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_SAA_Click(object sender, EventArgs e)
        {
            try
            {
                int num = Convert.ToInt32(textBoxDeleteNum_SAA);

            }
            catch
            {

            }
        }

        private void buttonSortNum_SAA_Click(object sender, EventArgs e)
        {
            string[,] temp_array = new string[rows, 6];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    temp_array[i, j] = dataGridViewFile_SAA.Rows[i].Cells[j].Value.ToString();
                }
            }
            string[,] new_array = ds.SortNum(temp_array);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    dataGridViewFile_SAA.Rows[i].Cells[j].Value = new_array[i, j];
                }
            }
        }

        private void buttonSortRate_SAA_Click(object sender, EventArgs e)
        {
            string[,] temp_array = new string[rows, 6];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    temp_array[i, j] = dataGridViewFile_SAA.Rows[i].Cells[j].Value.ToString(); 
                }
            }
            string[,] new_array = ds.SortRate(ds.SortNum(temp_array));
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    dataGridViewFile_SAA.Rows[i].Cells[j].Value = new_array[i, j];
                }
            }
        }

        private void buttonSortTime_SAA_Click(object sender, EventArgs e)
        {
            string[,] temp_array = new string[rows, 6];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    temp_array[i, j] = dataGridViewFile_SAA.Rows[i].Cells[j].Value.ToString();
                }
            }
            string[,] new_array = ds.SortTimeDuration(ds.SortNum(temp_array));
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    dataGridViewFile_SAA.Rows[i].Cells[j].Value = new_array[i, j];
                }
            }
        }

        private void buttonSortOpen_SAA_Click(object sender, EventArgs e)
        {
            string[,] temp_array = new string[rows, 6];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    temp_array[i, j] = dataGridViewFile_SAA.Rows[i].Cells[j].Value.ToString();
                }
            }
            string[,] new_array = ds.SortTimeOpen(ds.SortNum(temp_array));
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    dataGridViewFile_SAA.Rows[i].Cells[j].Value = new_array[i, j];
                }
            }
        }

        private void buttonSortClose_SAA_Click(object sender, EventArgs e)
        {
            string[,] temp_array = new string[rows, 6];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    temp_array[i, j] = dataGridViewFile_SAA.Rows[i].Cells[j].Value.ToString();
                }
            }
            string[,] new_array = ds.SortTimeClose(ds.SortNum(temp_array));
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    dataGridViewFile_SAA.Rows[i].Cells[j].Value = new_array[i, j];
                }
            }
        }

        private void buttonAboutProgram_SAA_Click(object sender, EventArgs e)
        {
            FormUsers formAboutProgram = new FormUsers();
            formAboutProgram.ShowDialog();
        }
    }
}
