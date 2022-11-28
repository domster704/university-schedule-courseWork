using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using PECD.Analysis;

namespace PECD
{
    public partial class Form1 : Form
    {
        public static AnalyseCSV analyseCSV;
        public Form1()
        {
            InitializeComponent();
            analyseCSV = new AnalyseCSV();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex != 0)
                return;
            DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];

            string message = analyseCSV.classrooms[e.RowIndex / analyseCSV.classrooms[0].lessonsCount].GetData();
            MessageBox.Show(message, "Характеристики", MessageBoxButtons.OK);            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.csv";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String path = dialog.FileName;
                analyseCSV = new AnalyseCSV(path);
                dataGridView1.DataSource = analyseCSV.toDataTable();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new();
            form.ShowDialog();
            dataGridView1.DataSource = analyseCSV.toDataTable();
        }

        int GetNumberOfActionWithCell(int column, int row)
        {
            if (row == 0)
                return 0;
            if (row == dataGridView1.RowCount - 1)
                return 1;

            DataGridViewCell cell1 = dataGridView1[column, row];
            DataGridViewCell cell2 = dataGridView1[column, row - 1];
            DataGridViewCell cell3 = dataGridView1[column, row + 1];

            if (cell1.Value == null || cell2.Value == null || cell3 == null)
                return 0;

            if (cell3.Value.ToString() != "" && cell1.Value.ToString() == "")
                return 1;        

            return -1;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex != 0)
                return;

            if (e.RowIndex != -1 && e.ColumnIndex != -1 && e.ColumnIndex < 2)
            {
                DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];
                cell.ReadOnly = true;
            }

            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            int res = GetNumberOfActionWithCell(e.ColumnIndex, e.RowIndex);
            if (res == 0)
            {
                e.AdvancedBorderStyle.Top = dataGridView1.AdvancedCellBorderStyle.Top;
            }
            else if (res == 1)
            {
                e.AdvancedBorderStyle.Bottom = dataGridView1.AdvancedCellBorderStyle.Bottom;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new();
            dialog.Filter = "Text files | *.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                analyseCSV.ExportToCSV(dialog.FileName);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];
            string value = cell.Value.ToString() == null || cell.Value.ToString() == "" ? "-" : cell.Value.ToString();
            cell.Value = value;
            int rowInClass = e.RowIndex / analyseCSV.classrooms[0].lessonsCount;
            int row = e.RowIndex % analyseCSV.classrooms[0].lessonsCount;
            int column = e.ColumnIndex - 2;
            analyseCSV.classrooms[rowInClass].originViewOfTimetable[column][row] = value;
        }
    }
}