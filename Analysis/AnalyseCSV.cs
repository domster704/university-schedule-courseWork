using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PECD.Analysis
{
    public class AnalyseCSV
    {
        public List<ClassroomData> classrooms;
        public AnalyseCSV(string path)
        {
            classrooms = new List<ClassroomData>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                bool isFirstElem = true;
                while ((line = sr.ReadLine()) != null)
                {
                    if (isFirstElem)
                    {
                        isFirstElem = false;
                        continue;
                    }
                    classrooms.Add(new ClassroomData(line));
                }
            }
        }

        public static string PrepareArgument()
        {
            string path = "qEGzExmxoFESU.csv";
            File.WriteAllText(path, "", Encoding.Unicode);
            return path;
        }

        public AnalyseCSV() : this(PrepareArgument()) { }
        

        public void AddNewClassroom(string data)
        {
            string s = "-;-;-;-;-;-;-";
            classrooms.Add(new ClassroomData(String.Format("{0},{1}:{1}:{1}:{1}:{1}:{1}", data, s)));
        }

        private string[] columnNames = new string[]
        {
            "Аудитория",
            "Пара",
            "ПН",
            "ВТ",
            "СР",
            "ЧТ",
            "ПТ",
            "СБ",
        };
        public DataTable toDataTable()
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < columnNames.Length; i++)
                dt.Columns.Add(columnNames[i]);

            int currentRow = 0;
            for (int i = 0; i < classrooms.Count; i++)
            {
                List<string[]> data = classrooms[i].timetable;
                for (int j = 0; j < data.Count; j++)
                {
                    dt.Rows.Add(dt.NewRow());
                    for (int k = 0; k < classrooms[i].timetable[j].Length; k++)
                    {
                        dt.Rows[currentRow][k] = data[j][k];
                    }
                    currentRow++;
                }
            }
            return dt;
        }

        public void ExportToCSV(string path) 
        {
            File.WriteAllText(path, "Аудитория,Этаж,Корпус,Количество,Проектор,Розетки,Расписание\n", Encoding.Unicode);
            foreach (ClassroomData i in classrooms)
            {
                File.AppendAllText(path, i.ToString() + "\n", Encoding.Unicode);
            }
        }
    }
}
