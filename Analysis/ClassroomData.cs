using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PECD.Analysis
{
    public class ClassroomData
    {
        public string         name;
        public int            floor;
        public string         corpus;
        public int            capacity;
        public bool           hasSockets;
        public bool           hasProjector;
        public List<string[]> timetable;
        public List<string[]> originViewOfTimetable;

        public int lessonsCount;
        public ClassroomData(string str)
        {
            timetable = new List<string[]>();
            originViewOfTimetable = new List<string[]>();
            string[] data = str.Split(",");

            name = data[0];
            Int32.TryParse(data[1], out floor);
            corpus = data[2];
            Int32.TryParse(data[3], out capacity);
            hasProjector = data[4] == "+" ? true : false;
            hasSockets = data[5] == "+" ? true : false;

            string[] schedule = data[6].Split(":");
            lessonsCount = schedule[0].Split(";").Length;
            for (int i = 0; i < schedule[0].Split(";").Length; i++)
            {
                string[] row = new string[8];
                row[0] = i == 0 ? name : "";
                row[1] = (i + 1).ToString();
                for (int j = 0; j < schedule.Length; j++)
                {
                    string value = schedule[j].Split(";")[i];
                    row[j + 2] = value;
                }
                timetable.Add(row);
            }

            for (int i = 0; i < schedule.Length; i++)
            {
                originViewOfTimetable.Add(schedule[i].Split(";"));
            }            
        }

        public string GetData()
        {
            return String.Format("Аудитория: {0}\nЭтаж: {1}\nКорпус: {2}\nВместимость: {3}\nРозетки: {4}\nПроектор: {5}", name, floor, corpus, capacity, hasSockets ? "есть" : "нет", hasProjector ? "есть" : "нет");
        }

        public override string ToString()
        {
            string res = String.Format("{0},{1},{2},{3},{4},{5},", name, floor, corpus, capacity, hasSockets, hasProjector);
            for (int i = 0; i < originViewOfTimetable.Count; i++)
            {
                for (int j = 0; j < originViewOfTimetable[i].Length; j++)
                {
                    res += originViewOfTimetable[i][j];
                    if (j != originViewOfTimetable[i].Length - 1)
                    {
                        res += ";";
                    }
                }
                if (i != originViewOfTimetable.Count - 1)
                {
                    res += ":";
                }
            }
            return res;
        }
    }
}
