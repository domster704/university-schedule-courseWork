using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PECD
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void createNewClass_Click(object sender, EventArgs e)
        {
            if (!isAllFieldFilled())
            {
                return;
            }
            string res = "";
            res = res + classET.Text + ",";
            res = res + floorET.Text + ",";
            res = res + corpusET.Text + ",";
            res = res + capacityET.Text + ",";
            res = res + projectorET.Text + ",";
            res = res + socketET.Text;

            Form1.analyseCSV.AddNewClassroom(res);
            this.Close();
        }

        private bool isAllFieldFilled()
        {
            if (classET.Text == "" || floorET.Text == "" || corpusET.Text == "" || capacityET.Text == "" || projectorET.Text == "" || socketET.Text == "")
            {
                return false;
            } 

            return true;
        }
    }
}
