using System.Data;
using System.Windows.Forms;
using PECD.Analysis;

namespace PECD
{
    public partial class Form1 : Form
    {
        private AnalyseCSV analyseCSV;
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex != 0)
                return;
            DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];

            string message = analyseCSV.classrooms[e.RowIndex / analyseCSV.classrooms[0].lessonsCount].ToString();
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
    }
}