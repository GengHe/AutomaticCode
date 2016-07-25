using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomaticCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path3 = System.AppDomain.CurrentDomain.BaseDirectory + @"FileModel\";
            DataModel data = new DataModel();
            data.DataName = textBox1.Text;
            data.ClassName = textBox2.Text;
            data.IniDataBase();
            
            String pageContent = data.TransformText();
            System.IO.File.WriteAllText(path3 + data.ClassName + ".cs", pageContent);
            MessageBox.Show("OK");
            
        }
    }
}
